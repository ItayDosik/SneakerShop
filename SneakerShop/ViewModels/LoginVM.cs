using System.ComponentModel.DataAnnotations;

namespace SneakerShop.ViewModels
{
    public class LoginVM
    {

        [Required(ErrorMessage = "UserName is Required.")]
        public string? UserName { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required.")]
        public string? Password { get; set; }


        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}
