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
                            Qnt = 5,
                            Category = "Jordan",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 1"
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
                            locationInStore = "Aisle 2"
                        },
                          new SneakerShop.Models.Product()
                        {
                            ProductName = "Yeezy Slide - Salt",
                            ProductDescription = "Enter the realm of sleek minimalism with the adidas Yeezy Slide Salt, a silhouette that redefines casual footwear. Embracing a monochromatic salt hue, this slide is a testament to the idea that true sophistication lies in simplicity. Designed with a smooth, seamless construction, the Yeezy Slide Salt offers an uncluttered, organic aesthetic that's as tranquil as its namesake.\n",
                            Price = 120.00M,
                            ProductPictureURL1 = "https://image.goat.com/transform/v1/attachments/product_template_additional_pictures/images/093/048/547/original/1267305_01.jpg.jpeg?action=crop&width=360",
                            ProductPictureURL2 = "https://i.ibb.co/qrdPVTQ/yeezygrey.png",
                            ProductPictureURL3 = "https://i.ibb.co/qrdPVTQ/yeezygrey.png",
                            Qnt = 5,
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
                            Qnt = 5,
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
                            ProductPictureURL1 = "https://alphet.co.il/cdn/shop/products/export-lobster.jpg?v=1671136120&width=640",
                            ProductPictureURL2 = "https://i.ibb.co/192zM7V/SbApril.png",
                            ProductPictureURL3 = "https://i.ibb.co/192zM7V/SbApril.png",
                            Qnt = 5,
                            Category = "NikeSb",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 3"
                        },
                        new SneakerShop.Models.Product()
                        {
                            ProductName = "Vans Old Skool",
                            ProductDescription = "If you are looking for a staple pair of black and white Vans for your rotation, the Vans Old Skool Black White is a popular choice. It arrives with a low-top black canvas upper with white leather Vans stripe detailing on lateral and medial sides. At the base, a white and gum waffle cup sole with a red, branded heel patch adds the finishing touch.",
                            Price = 70.00M,
                            ProductPictureURL1 = "https://www.vans.co.il/media/catalog/product/cache/c561981a6a0fbcb7af3b8463720b42ef/v/n/vn0a5fcby28-hero.jpg",
                            ProductPictureURL2 = "https://i.ibb.co/192zM7V/SbApril.png",
                            ProductPictureURL3 = "https://i.ibb.co/192zM7V/SbApril.png",
                            Qnt = 5,
                            Category = "Vans",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 4"
                        },
                        new SneakerShop.Models.Product()
                        {
                            ProductName = "Vans Sk8-Hi",
                            ProductDescription = "The Vans Sk8-Hi Black White is an iconic sneaker from the popular skateboarding brand Vans, featuring its classic black and white colorway, which remains timeless close to fifty years since it was first launched.",
                            Price = 75.00M,
                            ProductPictureURL1 = "https://www.vans.co.il/media/catalog/product/cache/c561981a6a0fbcb7af3b8463720b42ef/v/n/vn000d5ib8c-hero_2.jpg",
                            ProductPictureURL2 = "https://i.ibb.co/192zM7V/SbApril.png",
                            ProductPictureURL3 = "https://i.ibb.co/192zM7V/SbApril.png",
                            Qnt = 5,
                            Category = "Vans",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 4"
                        },
                         new SneakerShop.Models.Product()
                        {
                            ProductName = "Crocs Classic Clog - Lightning McQueen",
                            ProductDescription = "The Crocs Classic Clog Lightning McQueen is an official collaboration with Disney Pixar's Cars franchise. These Lightning McQueen Crocs pay homage to the movie's lead character, who is voiced by Owen Wilson. The Crocs look is loosely based on the Chevrolet Corvette C1, a racing car produced between 1953 and 1962.",
                            Price = 120.00M,
                            ProductPictureURL1 = "https://cdn.eql.media/prismic/c71d7923-6c99-442d-a4b2-997d0494db3c_15.png?auto=format&ar=1%3A0.6666666666666666&fit=crop&ixlib=react-9.7.0&w=1024&dpr=1&q=75",
                            ProductPictureURL2 = "https://i.ibb.co/192zM7V/SbApril.png",
                            ProductPictureURL3 = "https://i.ibb.co/192zM7V/SbApril.png",
                            Qnt = 5,
                            Category = "Crocs",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 5"
                        },
                          new SneakerShop.Models.Product()
                        {
                            ProductName = "Crocs Classic Clog - Toy Story Buzz Lightyear",
                            ProductDescription = "Attention Star Command! The exclusive Buzz Classic Clog has crash landed on a strange planet. This limited-edition style features a fully wrapped print design inspired by the iconic Space Ranger costume, including four exclusive Jibbitz™ charms. These clogs also feature the beloved “Andy” signature on the inside sole.",
                            Price = 100.00M,
                            ProductPictureURL1 = "https://media.crocs.com/images/t_pdphero/f_auto%2Cq_auto/products/209545_0ID_ALT120/crocs",
                            ProductPictureURL2 = "https://i.ibb.co/192zM7V/SbApril.png",
                            ProductPictureURL3 = "https://i.ibb.co/192zM7V/SbApril.png",
                            Qnt = 5,
                            Category = "Crocs",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 5"
                        },
                              new SneakerShop.Models.Product()
                        {
                            ProductName = "Adidas Campus Light - Bad Bunny Chalky Brown",
                            ProductDescription = "The adidas Campus Light Bad Bunny Chalky Brown is a notable collaboration between adidas and the Latin music superstar – Bad Bunny. The sneaker adopts a classic low-top silhouette with clean lines in a blend of and beige, brown, and Cream White hues.",
                            Price = 190.00M,
                            ProductPictureURL1 = "https://kingshoes.co.il/wp-content/uploads/2023/10/Product-Image-2-12-655x655.png",
                            ProductPictureURL2 = "https://i.ibb.co/192zM7V/SbApril.png",
                            ProductPictureURL3 = "https://i.ibb.co/192zM7V/SbApril.png",
                            Qnt = 5,
                            Category = "Adidas",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 6"
                        },
                                new SneakerShop.Models.Product()
                        {
                            ProductName = "Adidas Campus 00s - Grey White",
                            ProductDescription = "The adidas Campus 00s Grey White comes in a grey three, footwear white, and off-white color scheme.",
                            Price = 120.00M,
                            ProductPictureURL1 = "https://alphet.co.il/cdn/shop/files/adidas-ampus-00s-grey-white.jpg?v=1683379114&width=640",
                            ProductPictureURL2 = "https://i.ibb.co/192zM7V/SbApril.png",
                            ProductPictureURL3 = "https://i.ibb.co/192zM7V/SbApril.png",
                            Qnt = 5,
                            Category = "Adidas",
                            Size = "12",
                            IsOnSale = false,
                            SalePercentage = 0,
                            locationInStore = "Aisle 6"
                        }

                    });
                    context.SaveChanges();
                }


            }
        }
    }
}
