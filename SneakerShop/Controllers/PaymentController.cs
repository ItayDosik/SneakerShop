﻿using Microsoft.AspNetCore.Identity;
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

namespace SneakerShop.Controllers
{
    public class PaymentController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<Users> _userManager;
        public PaymentController(AppDbContext db, UserManager<Users> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index(string userID)
        {
            


            PaymentVM payment = new PaymentVM();
            
        
            payment.cart = _db.Carts.Include(u => u.user).Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.UserId == userID);
            if(_db.Payments.FirstOrDefault(c => c.UserId == userID) != null)
            {
                Payment userPayment = _db.Payments.FirstOrDefault(c => c.UserId == userID);
                byte[] cvv = Convert.FromBase64String(userPayment.creditCVV);
                byte[] num = Convert.FromBase64String(userPayment.creditNum);

                payment.creditCVV = DecryptStringFromBytes_Aes(cvv);
                payment.creditNum = DecryptStringFromBytes_Aes(num);
                payment.creditExp = (userPayment.creditDate.Month).ToString() + '/' + (userPayment.creditDate.Year).ToString();


            }
            foreach (var item in payment.cart.cartItems)
            {
                if(item.quantity > _db.Products.Find(item.ProductId).Qnt)
                {
                    TempData["ErrorMessage"] = item.product.ProductName + " is not available in selected quantity.";
                    return RedirectToAction("ViewAllProducts","Product");
                }
            }

            //Cart my_cart = _db.Carts.Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.CartId == cartSessionID);
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
                Console.WriteLine(userPayment.creditNum);
                payment.creditCVV = DecryptStringFromBytes_Aes(cvv);
                payment.creditNum = DecryptStringFromBytes_Aes(num);
                payment.creditExp = (userPayment.creditDate.Month).ToString() + '/' + (userPayment.creditDate.Year).ToString();



            }

            return View("checkout", payment);
        }

        public IActionResult checkoutPur(PaymentVM pay)
        {
            pay.cart = _db.Carts.Include(u => u.user).Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.CartId == pay.cartID);
            ModelState.Remove("cart");
            ModelState.Remove("paymentId");
            if (ModelState.IsValid)
            {

            foreach (var item in pay.cart.cartItems)
            {
                _db.Products.Find(item.ProductId).Qnt -= item.quantity;
                _db.SaveChanges();
            }

                if (_db.Payments.FirstOrDefault(c => c.UserId == pay.cart.UserId) == null)
                {
                    Payment p = new Payment() {
                    UserId = pay.cart.UserId,
                    creditNum = Convert.ToBase64String(EncryptStringToBytes_Aes(pay.creditNum)),
                    creditCVV = Convert.ToBase64String(EncryptStringToBytes_Aes(pay.creditCVV)),
                    creditDate = DateTime.Parse(pay.creditExp)
                    };
                    _db.Payments.Add(p);
                    _db.SaveChanges();
                }
             
            _db.Carts.Remove(pay.cart);
            _db.SaveChanges();
            return View("paymentSuccessed");
            }
            return View("checkout", pay);
        }







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
