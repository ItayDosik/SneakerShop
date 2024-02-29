using SneakerShop.Models;
using System.ComponentModel.DataAnnotations;


namespace SneakerShop.ViewModels
{
    public class RemoveUserVM
    {
        [Required]
        public string? identifier { get; set; }
        public List<Users> users { get; set; }
        public string? method { get; set; }
    }
}
