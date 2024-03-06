using System.ComponentModel.DataAnnotations;

namespace SneakerShop.ViewModels
{
    public class RegisterVM
    {
        
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [RegularExpression("^0(5[0-9])\\d{7}$", ErrorMessage = "Invalid Phone number.")]
        public string? PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        
        public string? Address { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords are not the same.")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        public string? Role {  get; set; } 
    }
}
