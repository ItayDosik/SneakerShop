using System.ComponentModel.DataAnnotations;

namespace SneakerShop.ViewModels
{
    public class EditUserVM
    {
        
        [Required]
        [Display(Name = "User Name")]
        public string? editUserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? editEmail { get; set; }
        [Display(Name = "Name")]
        public string? editName { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string? editRole { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string? editAddress {  get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression("^0(5[0-9])\\d{7}$", ErrorMessage = "Invalid Phone number.")]
        public string? editPhoneNumber { get; set; }

        public string? id {  get; set; } 

        public string? Role { get; set; }

        

    }
}
