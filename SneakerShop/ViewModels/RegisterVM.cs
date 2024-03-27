using System.ComponentModel.DataAnnotations;

namespace SneakerShop.ViewModels
{
    public class RegisterVM
    {
        
        [Required(ErrorMessage = "Full name required.")]
        [RegularExpression("^[A-Za-z][a-z]+\\s[A-Za-z][a-z]+$", ErrorMessage = "Invalid full name.")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "User Name is required")]
        public string? UserName { get; set; }


        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression("^0(5[0-9])\\d{7}$", ErrorMessage = "Invalid Phone number.")]
        public string? PhoneNumber { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Address is required.")]
        
        public string? Address { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "The field must be at least 6 characters long.")]
        public string? Password { get; set; }



        [Compare("Password", ErrorMessage = "Passwords are not the same.")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }


        public string? Role {  get; set; } 
    }
}
