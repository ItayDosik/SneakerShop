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
                            Qnt = 10,
                            Category = "Jordan",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 1"

                        },
                        new SneakerShop.Models.Product()
                        {
                            ProductName = "Jordan 1 Retro High OG SP",
                            ProductDescription = "The Air Jordan 1 High OG SP Fragment Design x Travis Scott fragment draws inspiration from a Jordan 1 Royal press sample from 1985 with its white and blue tumbled leather upper.",
                            Price = 2200.00M,
                            ProductPictureURL1 = "https://i.ibb.co/p1kQM5H/travis.png",
                            ProductPictureURL2 = "https://i.ibb.co/p1kQM5H/travis.png",
                            ProductPictureURL3 = "https://i.ibb.co/p1kQM5H/travis.png",
                            Qnt = 2,
                            Category = "Jordan",
                            Size = "12",
                            IsOnSale = true,
                            SalePercentage = 15,
                            locationInStore = "Aisle 1"
                        },
                        new SneakerShop.Models.Product()
                        {
                            ProductName = "Yeezy Boost 350 V2 - Steel Grey",
                            ProductDescription = "After adidas announced the official release information regarding upcoming Yeezy product, Kanye West himself took to Instagram to speak out against the shoes that he once designed in collaboration with the brand.",
                            Price = 220.00M,
                            ProductPictureURL1 = "https://i.ibb.co/qrdPVTQ/yeezygrey.png",
                            ProductPictureURL2 = "https://i.ibb.co/qrdPVTQ/yeezygrey.png",
                            ProductPictureURL3 = "https://i.ibb.co/qrdPVTQ/yeezygrey.png",
                            Qnt = 20,
                            Category = "Yeezy",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 2"
                        },
                          new SneakerShop.Models.Product()
                        {
                            ProductName = "Yeezy Slide - Salt",
                            ProductDescription = "Enter the realm of sleek minimalism with the adidas Yeezy Slide Salt, a silhouette that redefines casual footwear. Embracing a monochromatic salt hue, this slide is a testament to the idea that true sophistication lies in simplicity. Designed with a smooth, seamless construction, the Yeezy Slide Salt offers an uncluttered, organic aesthetic that's as tranquil as its namesake.\n",
                            Price = 120.00M,
                            ProductPictureURL1 = "https://i.ibb.co/vXXdd66/Yeezy-Slide.png",
                            ProductPictureURL2 = "https://i.ibb.co/vXXdd66/Yeezy-Slide.png",
                            ProductPictureURL3 = "https://i.ibb.co/vXXdd66/Yeezy-Slide.png",
                            Qnt = 0,
                            Category = "Yeezy",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 2"
                        },
                        new SneakerShop.Models.Product()
                        {
                            ProductName = "Nike SB Dunk Low - April Skateboards",
                            ProductDescription = "Strap in for a ride with the Nike SB Dunk Low April Skateboards, a sneaker that’s as much about performance as it is about making a statement. The Turbo Green/Metallic Silver/Turbo Green colorway is a nod to the fearless energy of the skate parks.",
                            Price = 285.00M,
                            ProductPictureURL1 = "https://i.ibb.co/192zM7V/SbApril.png",
                            ProductPictureURL2 = "https://i.ibb.co/192zM7V/SbApril.png",
                            ProductPictureURL3 = "https://i.ibb.co/192zM7V/SbApril.png",
                            Qnt = 15,
                            Category = "NikeSb",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 3"
                        },
                          new SneakerShop.Models.Product()
                        {
                            ProductName = "Nike SB Dunk Low - Concepts Orange Lobster",
                            ProductDescription = "The Nike SB Dunk Low Concepts Orange Lobster comes toned in a mixture of three main colors: electro orange, orange frost, and white.",
                            Price = 315.00M,
                            ProductPictureURL1 = "https://i.ibb.co/jJ85BY0/orange.png",
                            ProductPictureURL2 = "https://i.ibb.co/jJ85BY0/orange.png",
                            ProductPictureURL3 = "https://i.ibb.co/jJ85BY0/orange.png",
                            Qnt = 10,
                            Category = "NikeSb",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 3"
                        },
                         new SneakerShop.Models.Product()
                        {
                            ProductName = "Crocs Classic Clog - Lightning McQueen",
                            ProductDescription = "The Crocs Classic Clog Lightning McQueen is an official collaboration with Disney Pixar's Cars franchise. These Lightning McQueen Crocs pay homage to the movie's lead character, who is voiced by Owen Wilson. The Crocs look is loosely based on the Chevrolet Corvette C1, a racing car produced between 1953 and 1962.",
                            Price = 120.00M,
                            ProductPictureURL1 = "https://i.ibb.co/48WSDvP/speedy.png",
                            ProductPictureURL2 = "https://i.ibb.co/48WSDvP/speedy.png",
                            ProductPictureURL3 = "https://i.ibb.co/48WSDvP/speedy.png",
                            Qnt = 25,
                            Category = "Others",
                            Size = "12",
                            IsOnSale = true,
                            SalePercentage = 25,
                            locationInStore = "Aisle 4"
                        },
                          new SneakerShop.Models.Product()
                        {
                            ProductName = "Crocs Classic Clog - Toy Story Buzz Lightyear",
                            ProductDescription = "Attention Star Command! The exclusive Buzz Classic Clog has crash landed on a strange planet. This limited-edition style features a fully wrapped print design inspired by the iconic Space Ranger costume, including four exclusive Jibbitz™ charms. These clogs also feature the beloved “Andy” signature on the inside sole.",
                            Price = 100.00M,
                            ProductPictureURL1 = "https://i.ibb.co/594SstT/Toy-Story-Buzz.png",
                            ProductPictureURL2 = "https://i.ibb.co/594SstT/Toy-Story-Buzz.png",
                            ProductPictureURL3 = "https://i.ibb.co/594SstT/Toy-Story-Buzz.png",
                            Qnt = 20,
                            Category = "Others",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 4"
                        },
                              new SneakerShop.Models.Product()
                        {
                            ProductName = "Adidas Campus Light - Bad Bunny Chalky Brown",
                            ProductDescription = "The adidas Campus Light Bad Bunny Chalky Brown is a notable collaboration between adidas and the Latin music superstar – Bad Bunny. The sneaker adopts a classic low-top silhouette with clean lines in a blend of and beige, brown, and Cream White hues.",
                            Price = 190.00M,
                            ProductPictureURL1 = "https://i.ibb.co/NyGYsMN/Adidas-Campus-Light.png",
                            ProductPictureURL2 = "https://i.ibb.co/NyGYsMN/Adidas-Campus-Light.png",
                            ProductPictureURL3 = "https://i.ibb.co/NyGYsMN/Adidas-Campus-Light.png",
                            Qnt = 30,
                            Category = "Adidas",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 2"
                        },
                                new SneakerShop.Models.Product()
                        {
                            ProductName = "Adidas Campus 00s - Grey White",
                            ProductDescription = "The adidas Campus 00s Grey White comes in a grey three, footwear white, and off-white color scheme.",
                            Price = 120.00M,
                            ProductPictureURL1 = "https://i.ibb.co/Lg6pHfg/Adidas-Campus-00.png",
                            ProductPictureURL2 = "https://i.ibb.co/Lg6pHfg/Adidas-Campus-00.png",
                            ProductPictureURL3 = "https://i.ibb.co/Lg6pHfg/Adidas-Campus-00.png",
                            Qnt = 30,
                            Category = "Adidas",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 2"
                        }

                    });
                    context.SaveChanges();
                }


            }
        }
    }
}
