using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SneakerShop.Models
{
    public class Users: IdentityUser
    {
        public string? Name { get; set; }
        public string? Address { get; set; }     
        public string? Role { get; set; }
        public string? CartID { get; set; }
    }
}
