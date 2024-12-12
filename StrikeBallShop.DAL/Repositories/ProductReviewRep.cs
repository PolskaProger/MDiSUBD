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
    public class ProductReviewRepository
    {
        private DBConnect database;

        public ProductReviewRepository(DBConnect database)
        {
            this.database = database;
        }

        public ProductReview GetProductReviewById(int id)
        {
            string query = "SELECT * FROM \"ProductReview\" WHERE \"Id\" = @id";
            DataTable dataTable = database.ExecuteQuery(query);
            ProductReview productReview = new ProductReview();
            foreach (DataRow row in dataTable.Rows)
            {
                productReview.Id = Convert.ToInt32(row["Id"]);
                productReview.UserId = Convert.ToInt32(row["UserId"]);
                productReview.ProductId = Convert.ToInt32(row["ProductId"]);
                productReview.Description = row["Description"].ToString();
                productReview.DateOfReview = Convert.ToDateTime(row["DateOfReview"]);
            }
            return productReview;
        }

        public void CreateProductReview(ProductReview productReview)
        {
            string query = "INSERT INTO \"ProductReview\" (\"UserId\", \"ProductId\", \"Description\", \"DateOfReview\") VALUES (@userId, @productId, @description, @dateOfReview)";
            database.ExecuteNonQuery(query);
        }

        public void UpdateProductReview(ProductReview productReview)
        {
            string query = "UPDATE \"ProductReview\" SET \"UserId\" = @userId, \"ProductId\" = @productId, \"Description\" = @description, \"DateOfReview\" = @dateOfReview WHERE \"Id\" = @id";
            database.ExecuteNonQuery(query);
        }

        public void DeleteProductReview(int id)
        {
            string query = "DELETE FROM \"ProductReview\" WHERE \"Id\" = @id";
            database.ExecuteNonQuery(query);
        }
    }
}
