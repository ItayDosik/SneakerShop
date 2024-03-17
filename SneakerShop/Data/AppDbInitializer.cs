using SneakerShop.Models.Data;


namespace SneakerShop.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();


                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<SneakerShop.Models.Product>()
                    {
                        
                        new SneakerShop.Models.Product()
                        {
                            ProductName = "Jordan 1 Retro High Bred Toe",
                            ProductDescription = "Combining elements of three certified classic AJ1’s, the 2018 Jordan 1 Retro High Bred Toe is like the Coachella lineup of OG 1s.",
                            Price = 390.00M,
                            ProductPictureURL1 = "https://i.ibb.co/tQ7wN86/bredtoe.png",
                            ProductPictureURL2 = "https://i.ibb.co/tQ7wN86/bredtoe.png",
                            ProductPictureURL3 = "https://i.ibb.co/tQ7wN86/bredtoe.png",
                            Qnt = 5,
                            Category = "Jordan",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 4"

                        },
                        new SneakerShop.Models.Product()
                        {
                            ProductName = "Jordan 1 Retro High OG SP",
                            ProductDescription = "The Air Jordan 1 High OG SP Fragment Design x Travis Scott fragment draws inspiration from a Jordan 1 Royal press sample from 1985 with its white and blue tumbled leather upper.",
                            Price = 2200.00M,
                            ProductPictureURL1 = "https://i.ibb.co/p1kQM5H/travis.png",
                            ProductPictureURL2 = "https://i.ibb.co/p1kQM5H/travis.png",
                            ProductPictureURL3 = "https://i.ibb.co/p1kQM5H/travis.png",
                            Qnt = 5,
                            Category = "Jordan",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 2"
                        },
                        new SneakerShop.Models.Product()
                        {
                            ProductName = "Yeezy Boost 350 V2 - Steel Grey",
                            ProductDescription = "After adidas announced the official release information regarding upcoming Yeezy product, Kanye West himself took to Instagram to speak out against the shoes that he once designed in collaboration with the brand.",
                            Price = 2200.00M,
                            ProductPictureURL1 = "https://i.ibb.co/qrdPVTQ/yeezygrey.png",
                            ProductPictureURL2 = "https://i.ibb.co/qrdPVTQ/yeezygrey.png",
                            ProductPictureURL3 = "https://i.ibb.co/qrdPVTQ/yeezygrey.png",
                            Qnt = 5,
                            Category = "Yeezy",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 1"
                        },
                        new SneakerShop.Models.Product()
                        {
                            ProductName = "Nike SB Dunk Low",
                            ProductDescription = "Strap in for a ride with the Nike SB Dunk Low April Skateboards, a sneaker that’s as much about performance as it is about making a statement. The Turbo Green/Metallic Silver/Turbo Green colorway is a nod to the fearless energy of the skate parks.",
                            Price = 285.00M,
                            ProductPictureURL1 = "https://i.ibb.co/192zM7V/SbApril.png",
                            ProductPictureURL2 = "https://i.ibb.co/192zM7V/SbApril.png",
                            ProductPictureURL3 = "https://i.ibb.co/192zM7V/SbApril.png",
                            Qnt = 5,
                            Category = "NikeSb",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 4"
                        }

                    });
                    context.SaveChanges();
                }


            }
        }
    }
}
