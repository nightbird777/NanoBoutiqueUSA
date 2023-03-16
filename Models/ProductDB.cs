using Dapper;
using System.Data.SqlClient;

namespace NanoBoutiqueUSA.Models
{
    public class ProductDB
    {
        //private string connectionString = ("server=KHALIFABUILD202; database=NanoBoutique; user id=raju; password=raju123");
        private string connectionString = ("server=nyctotampa; database=NanoBoutique; user id=raju; password=raju123");
        public List<Product> getAllProducts()
        {
            List<Product> products = new List<Product>();
            string sql = "select * from [Products]";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                products = conn.Query<Product>(sql).ToList();
            }
            return products;
        }


        public Product getProductById(int id)
        {
            Product product = new Product();
            string sql = "select * from [Products] where Id = " + id;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                product = conn.QueryFirst<Product>(sql, new { id });
            }
            return product;
        }

        public void saveEditProduct(Product product)
        {
            string sql = "update [Products] set Name = '" + product.Name + "', Description = '" + product.Description + "', Price = '" + product.Price + "', " +
                "Image = '" + product.Image + "', UpdatedBy = '" + product.UpdatedBy + "', DateLastUpdated = GETDATE(), Active = '" + product.Active + "' where Id = " + product.Id;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(sql, product);
            }
        }

        public void removeProduct(int id)
        {
            Product product = new Product();
            string sql = "delete from [Products] where Id = " + id;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(sql, new { id });
            }
        }

        public void CreateNewProduct(Product product)
        {
            string sql = "insert into [Products] (Name, Description, Price, Image, CreatedBy, DateCreated, Active) " +
                "values ('" + product.Name + "', '" + product.Description + "', '" + product.Price + "', '" + product.Image + "', " +
                "'" + product.CreatedBy + "', GETDATE(), '" + product.Active + "')";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(sql, product);
            }
        }


        public List<Product> searchProduct(string search)
        {            
            string sql = "select * from [Products] where (Name like '%" + search + "%')";
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                products = conn.Query<Product>(sql).ToList();
            }
            return products;
        }
    }
}
