using StrikeBallShop.DAL.Database;
using StrikeBallShop.DML.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrikeBallShop.DAL.Repositories
{
    public class ProductRatingRepository
    {
        private DBConnect database;

        public ProductRatingRepository(DBConnect database)
        {
            this.database = database;
        }

        public ProductRating GetProductRatingById(int id)
        {
            string query = "SELECT * FROM \"ProductRating\" WHERE \"Id\" = @id";
            DataTable dataTable = database.ExecuteQuery(query);
            ProductRating productRating = new ProductRating();
            foreach (DataRow row in dataTable.Rows)
            {
                productRating.Id = Convert.ToInt32(row["Id"]);
                productRating.UserId = Convert.ToInt32(row["UserId"]);
                productRating.ProductId = Convert.ToInt32(row["ProductId"]);
                productRating.Mark = Convert.ToInt32(row["Mark"]);
            }
            return productRating;
        }

        public void CreateProductRating(ProductRating productRating)
        {
            string query = "INSERT INTO \"ProductRating\" (\"UserId\", \"ProductId\", \"Mark\") VALUES (@userId, @productId, @mark)";
            database.ExecuteNonQuery(query);
        }

        public void UpdateProductRating(ProductRating productRating)
        {
            string query = "UPDATE \"ProductRating\" SET \"UserId\" = @userId, \"ProductId\" = @productId, \"Mark\" = @mark WHERE \"Id\" = @id";
            database.ExecuteNonQuery(query);
        }

        public void DeleteProductRating(int id)
        {
            string query = "DELETE FROM \"ProductRating\" WHERE \"Id\" = @id";
            database.ExecuteNonQuery(query);
        }
    }
}
