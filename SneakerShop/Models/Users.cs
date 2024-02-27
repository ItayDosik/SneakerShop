using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SneakerShop.Models
{
    public class Users: IdentityUser
    {
        [Key]
        public string? UID { get; set; }
        public string? Name { get; set; }

        //using the UserName property from the IdentityUser class
        //public string? UserName { get; set; }
        public string? Phone { get; set; }

        //using the Email property from the IdentityUser class
        //public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? CartID { get; set; }
    }
}
