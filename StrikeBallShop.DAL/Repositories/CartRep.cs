using StrikeBallShop.DAL.Database;
using StrikeBallShop.DML.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace StrikeBallShop.DAL.Repositories
{
    public class CartRepository
    {
        private DBConnect database;

        public CartRepository(DBConnect database)
        {
            this.database = database;
        }

        public Cart GetCartById(int id)
        {
            string query = "SELECT * FROM \"Cart\" WHERE \"Id\" = @id";
            DataTable dataTable = database.ExecuteQuery(query);
            Cart cart = new Cart();
            foreach (DataRow row in dataTable.Rows)
            {
                cart.Id = Convert.ToInt32(row["Id"]);
                cart.UserId = Convert.ToInt32(row["UserId"]);
                cart.TotalPrice = Convert.ToDecimal(row["TotalPrice"]);
            }
            return cart;
        }

        public void CreateCart(Cart cart)
        {
            string query = "INSERT INTO \"Cart\" (\"UserId\", \"TotalPrice\") VALUES (@userId, @totalPrice)";
            database.ExecuteNonQuery(query);
        }

        public void DeleteCart(int id)
        {
            string query = "DELETE FROM \"Cart\" WHERE \"Id\" = @id";
            database.ExecuteNonQuery(query);
        }

        public List<CartItem> GetCartItems(int cartId)
        {
            string query = "SELECT * FROM \"CartItem\" WHERE \"CartId\" = @cartId";
            DataTable dataTable = database.ExecuteQuery(query);
            List<CartItem> cartItems = new List<CartItem>();
            foreach (DataRow row in dataTable.Rows)
            {
                CartItem cartItem = new CartItem
                {
                    Id = Convert.ToInt32(row["Id"]),
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    CartId = Convert.ToInt32(row["CartId"]),
                    Count = Convert.ToInt32(row["Count"])
                };
                cartItems.Add(cartItem);
            }
            return cartItems;
        }
        public Cart GetCartByUserId(int userId)
        {
            string query = "SELECT * FROM \"Cart\" WHERE \"UserId\" = @userId";
            DataTable dataTable = database.ExecuteQuery(query);
            Cart cart = null;
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                cart = new Cart
                {
                    Id = Convert.ToInt32(row["Id"]),
                    UserId = Convert.ToInt32(row["UserId"]),
                    TotalPrice = Convert.ToDecimal(row["TotalPrice"])
                };
            }
            return cart;
        }

        public void AddProductToCart(CartItem cartItem)
        {
            string query = "INSERT INTO \"CartItem\" (\"ProductId\", \"CartId\", \"Count\") VALUES (@productId, @cartId, @count)";
            database.ExecuteNonQuery(query);
        }

        public void UpdateCart(Cart cart)
        {
            string query = "UPDATE \"Cart\" SET \"TotalPrice\" = @totalPrice WHERE \"Id\" = @id";
            database.ExecuteNonQuery(query);
        }
        public void RemoveProductFromCart(int cartId, int productId)
        {
            string query = "DELETE FROM \"CartItem\" WHERE \"CartId\" = @cartId AND \"ProductId\" = @productId";
            database.ExecuteNonQuery(query);
        }

        public void ClearCart(int cartId)
        {
            string query = "DELETE FROM \"CartItem\" WHERE \"CartId\" = @cartId";
            database.ExecuteNonQuery(query);
        }
    }
}