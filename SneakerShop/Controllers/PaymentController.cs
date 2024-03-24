using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SneakerShop.Models;
using SneakerShop.ViewModels;
using SneakerShop.Models.Data;
using System.ComponentModel.DataAnnotations.Schema;
using SneakerShop.Migrations;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using SneakerShop.Clients;
using Newtonsoft.Json;

namespace SneakerShop.Controllers
{
    public class PaymentController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<Users> _userManager;
        private readonly PaypalClient _paypalClient;
        public PaymentController(AppDbContext db, UserManager<Users> userManager, PaypalClient paypalClient)
        {
            _db = db;
            _userManager = userManager;
            _paypalClient = paypalClient;
        }


        public IActionResult Index(string userID)
        {
            PaymentVM payment = new PaymentVM();
            
            if(userID != null)
            {
                payment.cart = _db.Carts.Include(u => u.user).Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.UserId == userID);
            }
            else
            {
                var cartSession = HttpContext.Session.GetString("cart");
                if (cartSession != null)
                {
                    int cartSessionID = int.Parse(cartSession);
                    payment.cart = _db.Carts.Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.CartId == cartSessionID);
                }  
            }

            foreach (var item in payment.cart.cartItems)
            {
                if (item.quantity > _db.Products.Find(item.ProductId).Qnt)
                {
                    TempData["ErrorMessage"] = item.product.ProductName + " is not available in selected quantity.";
                    return RedirectToAction("ViewAllProducts", "Product");
                }
            }

            if (_db.Payments.FirstOrDefault(c => c.UserId == userID) != null)
            {
                Payment userPayment = _db.Payments.FirstOrDefault(c => c.UserId == userID);
                byte[] cvv = Convert.FromBase64String(userPayment.creditCVV);
                byte[] num = Convert.FromBase64String(userPayment.creditNum);

                payment.creditCVV = DecryptStringFromBytes_Aes(cvv);
                payment.creditNum = DecryptStringFromBytes_Aes(num);
                payment.creditExp = (userPayment.creditDate.Month).ToString() + '/' + (userPayment.creditDate.Year).ToString();

            }
           
            return View("checkout", payment);
        }


        public async Task<IActionResult> buyNowPayment(string userID, int productID)
        {
            Cart cart = new Cart()
            {
                UserId = userID,
                user = await _userManager.GetUserAsync(User),
                cartItems = new List<CartItem>(),  
            };
            var prod = _db.Products.Find(productID);
            cart.cartItems.Add(new CartItem()
            {
                quantity = 1,
                product = prod
            });

            _db.Carts.Add(cart);
            _db.SaveChanges();
            PaymentVM payment = new PaymentVM();
            payment.cart = cart;
            if (_db.Payments.FirstOrDefault(c => c.UserId == userID) != null)
            {          
                Payment userPayment = _db.Payments.FirstOrDefault(c => c.UserId == userID);
                byte[] cvv = Convert.FromBase64String(userPayment.creditCVV);
                byte[] num = Convert.FromBase64String(userPayment.creditNum);

                payment.creditCVV = DecryptStringFromBytes_Aes(cvv);
                payment.creditNum = DecryptStringFromBytes_Aes(num);
                payment.creditExp = (userPayment.creditDate.Month).ToString() + '/' + (userPayment.creditDate.Year).ToString();

            }

            return View("checkout", payment);
        }

        public IActionResult checkoutPur(PaymentVM pay)
        {
            pay.cart = _db.Carts.Include(u => u.user).Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.CartId == pay.cartID);
            if(pay.cart == null)
            {
                TempData["ErrorMessage"] = "Something went wrong, try again later. 🤒";
                return RedirectToAction("ViewAllProducts", "Product");
            }
            var cartItems = pay.cart.cartItems;
            ModelState.Remove("cart");
            ModelState.Remove("paymentId");
            if (ModelState.IsValid)
            {

                foreach (var item in pay.cart.cartItems)
                {
                    _db.Products.Find(item.ProductId).Qnt -= item.quantity;
                    _db.SaveChanges();
                }
                    if(pay.cart.UserId != null)
                    {
                        if (_db.Payments.FirstOrDefault(c => c.UserId == pay.cart.UserId) == null)
                        {
                            Payment p = new Payment()
                            {
                                UserId = pay.cart.UserId,
                                creditNum = Convert.ToBase64String(EncryptStringToBytes_Aes(pay.creditNum)),
                                creditCVV = Convert.ToBase64String(EncryptStringToBytes_Aes(pay.creditCVV)),
                                creditDate = DateTime.Parse(pay.creditExp)
                            };
                            _db.Payments.Add(p);
                            _db.SaveChanges();
                        }
                    }
             
                _db.Carts.Remove(pay.cart);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Thank You! Enjoy 🤑" ;
                return RedirectToAction("ViewAllProducts","Product");
            }

            return View("checkout", pay);
        }

        [HttpGet]
        public IActionResult DiscountCoupon(string promoCode)
        {

            Dictionary<string, double> codes = new Dictionary<string, double>();
            codes.Add("CODE1", 0.1); // 10% discount
            codes.Add("CODE2", 0.15); // 15% discount
            codes.Add("CODE3", 0.2); // 20% discount

            double discount = 0; // Default discount
            if(promoCode != null)
            {
                if (codes.ContainsKey(promoCode))
                {
                    discount = codes[promoCode];
                }
            }

            return Json(new { discount = discount });
        } 

        public IActionResult promoCodeParty()
        {
            return View("promoCodeParty");
        }


        //PayPal

        [HttpPost]
        public async Task<IActionResult> Order(CancellationToken cancellationToken)
        {
            try
            {
                // set the transaction price and currency
                string requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                decimal totalPrice = data.totalPrice;
                var currency = "USD";

                // "reference" is the transaction key
                var reference = GetRandomInvoiceNumber();


                var response = await _paypalClient.CreateOrder(totalPrice.ToString(), currency, reference);


                return Ok(response);
            }
            catch (Exception e)
            {
                var error = new
                {
                    e.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }
        public async Task<IActionResult> Capture(string orderId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _paypalClient.CaptureOrder(orderId);

                var reference = response.purchase_units[0].reference_id;

                return Ok(response);
            }
            catch (Exception e)
            {
                var error = new
                {
                    e.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }
        public static string GetRandomInvoiceNumber()
        {
            return new Random().Next(999999).ToString();
        }
        public IActionResult Success()
        {
            return View();
        }

        public IActionResult PaymentSuccess(int cartId)
        {
            Cart cart = _db.Carts.Include(u => u.user).Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.CartId == cartId);
            if (cart != null)
            {
                foreach (var item in cart.cartItems)
                {
                    _db.Products.Find(item.ProductId).Qnt -= item.quantity;
                    _db.SaveChanges();
                }
                _db.Carts.Remove(cart);
                _db.SaveChanges();
            }

            TempData["SuccessMessage"] = "Thank You! Enjoy 🤑";
            return RedirectToAction("ViewAllProducts", "Product");
        }

        //EOF PayPal



        static byte[] EncryptStringToBytes_Aes(string plainText)
        {
            byte[] Key = Encoding.UTF8.GetBytes("adivtomeritay123"); 
            byte[] IV = Encoding.UTF8.GetBytes("itaytomeradiv321"); 
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText)
        {

            string plaintext = null;


            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes("adivtomeritay123");
                aesAlg.IV = Encoding.UTF8.GetBytes("itaytomeradiv321");

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }






    }
}
