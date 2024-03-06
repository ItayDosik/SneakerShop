using System.ComponentModel.DataAnnotations;
using SneakerShop.Models;

namespace SneakerShop.ViewModels
{
    public class productManagement
    {
        [Required]
        public string? keyWord {  get; set; }
        public List<Product> products { get; set; }
    }
}
