using Microsoft.AspNetCore.Mvc;
using SneakerShop.Models;
using System.Data;
using System.Data.SqlClient;


namespace SneakerShop.Controllers
{
    public class ProductController : Controller
    {
        private IConfiguration _configuration;
       public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActionResult Enter()
        {
            return View();
        }

        public ActionResult AddProduct(Product product)
        {
            string connectionString = _configuration.GetConnectionString("myConnect");
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "INSERT INTO PRODUCT VALUES(@ProductID,@ProductName,@ProductDescription,@Price,@ProductPictureURL1,@ProductPictureURL2,@ProductPictureURL3,@Qnt,@Category,@Size)";
                using (SqlCommand command=new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", product.ProductID);
                    command.Parameters.AddWithValue("@ProductName", product.ProductName);
                    command.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@ProductPictureURL1", product.ProductPictureURL1);
                    command.Parameters.AddWithValue("@ProductPictureURL2", product.ProductPictureURL2);
                    command.Parameters.AddWithValue("@ProductPictureURL3", product.ProductPictureURL3);
                    command.Parameters.AddWithValue("@Qnt", product.Qnt);
                    command.Parameters.AddWithValue("@Category", product.Category);
                    command.Parameters.AddWithValue("@Size", product.Size);


                    int ra=command.ExecuteNonQuery();
                    if (ra > 0)
                    {
                        return ViewProduct();
                    }
                    else
                    {
                        return View("Enter");
                    }
                }
                connection.Close();
            }
           

                //return View("Product", product);
        }

        public ActionResult ViewProduct()
        {
            Product product=new Product();
            string connectionString = _configuration.GetConnectionString("myConnect");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM product WHERE ProductID = '1'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        product.ProductID=reader.GetString(0);
                        product.ProductName=reader.GetString(1);
                        product.ProductDescription=reader.GetString(2);
                        product.Price=reader.GetString(3);
                        product.ProductPictureURL1=reader.GetString(4);
                        product.ProductPictureURL2=reader.GetString(5);
                        product.ProductPictureURL3=reader.GetString(6);
                        product.Qnt=reader.GetInt32(7);
                        product.Category=reader.GetString(8);
                        product.Size=reader.GetString(9);
                    }
                    
                    reader.Close();
                }
                connection.Close();
            }

            return View("Product",product);
        }

    }


}
