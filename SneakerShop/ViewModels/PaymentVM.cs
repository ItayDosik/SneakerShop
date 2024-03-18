namespace SneakerShop.ViewModels;
using SneakerShop.Models;
using System.ComponentModel.DataAnnotations;

public class PaymentVM
{
    public class MyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string dateString = value.ToString();
                if (DateTime.TryParseExact(dateString, "MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime expDate))
                {
                    
                    return expDate >= DateTime.Now.Date;
                }
                else
                {     
                    return false;
                }
            }  
            return false;
        }
    }

    public Cart cart { get; set; } = null;


    [Required(ErrorMessage = "Credit Number is required")]
    [RegularExpression(@"^\d{16}$", ErrorMessage = "Invalid credit number.")]
    public string creditNum { get; set; }

    [Required(ErrorMessage = "CVV is reqiured.")]
    [RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVV must contain a 3 or 4-digit number.")]
    public string creditCVV { get; set; }

    [Required(ErrorMessage="Expiration Date is required")]
    [MyDate(ErrorMessage = "Invalid or Expiration Date passed")]
    public string creditExp { get; set; }

    [Required(ErrorMessage = "Credit name is required.")]
    [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)?$", ErrorMessage = "Invalid input for Credit Name.")]
    public string creditName { get; set; } 

    public int cartID { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "First name can only contain letters and numbers.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
    public string firstName {get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Last name can only contain letters and numbers.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Address can only contain letters and numbers.")]
    public string address { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string email { get; set; }

    [Required(ErrorMessage = "Zip code is Required.")]
    [RegularExpression(@"^\d{7}$", ErrorMessage = "Invalid zip code (7 digits nubmer).")]
     public string zip { get; set; } 


}
