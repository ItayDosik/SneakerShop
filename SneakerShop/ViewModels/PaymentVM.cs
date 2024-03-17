namespace SneakerShop.ViewModels;
using SneakerShop.Models;
using System.ComponentModel.DataAnnotations;

public class PaymentVM
{
    
    public Cart cart { get; set; }
    [Required(ErrorMessage = "Invali")]
    public string creditNum { get; set; }
    [Required(ErrorMessage = "Invali")]
    public string creditCVV { get; set; }
    [Required(ErrorMessage = "Invali")]
    public DateTime creditExp { get; set; }
    [Required(ErrorMessage = "Invali")]
    public string creditName { get; set; } 

    public int cartID { get; set; }
}
