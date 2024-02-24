using System;

namespace SneakerShop.Models
{
    public class Products
    {
        public Products() 
        { 
            allProducts = new List<Product>(); 
        }
        public List<Product> allProducts { get; set; }
    }
}
