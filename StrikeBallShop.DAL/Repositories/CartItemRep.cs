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
    public class CartItemRepository
    {
        private DBConnect database;

        public CartItemRepository(DBConnect database)
        {
            this.database = database;
        }

        public CartItem GetCartItemById(int id)
        {
            string query = "SELECT * FROM \"CartItem\" WHERE \"Id\" = @id";
            DataTable dataTable = database.ExecuteQuery(query);
            CartItem cartItem = new CartItem();
            foreach (DataRow row in dataTable.Rows)
            {
                cartItem.Id = Convert.ToInt32(row["Id"]);
                cartItem.ProductId = Convert.ToInt32(row["ProductId"]);
                cartItem.CartId = Convert.ToInt32(row["CartId"]);
                cartItem.Count = Convert.ToInt32(row["Count"]);
            }
            return cartItem;
        }

        public void CreateCartItem(CartItem cartItem)
        {
            string query = "INSERT INTO \"CartItem\" (\"ProductId\", \"CartId\", \"Count\") VALUES (@productId, @cartId, @count)";
            database.ExecuteNonQuery(query);
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            string query = "UPDATE \"CartItem\" SET \"ProductId\" = @productId, \"CartId\" = @cartId, \"Count\" = @count WHERE \"Id\" = @id";
            database.ExecuteNonQuery(query);
        }

        public void DeleteCartItem(int id)
        {
            string query = "DELETE FROM \"CartItem\" WHERE \"Id\" = @id";
            database.ExecuteNonQuery(query);
        }
    }
}
