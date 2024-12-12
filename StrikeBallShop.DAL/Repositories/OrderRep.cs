using StrikeBallShop.DAL.Database;
using StrikeBallShop.DML.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace StrikeBallShop.DAL.Repositories
{
    public class OrderRepository
    {
        private DBConnect database;

        public OrderRepository(DBConnect database)
        {
            this.database = database;
        }

        public Order GetOrderById(int id)
        {
            string query = "SELECT * FROM \"Order\" WHERE \"Id\" = @id";
            DataTable dataTable = database.ExecuteQuery(query);
            Order order = new Order();
            foreach (DataRow row in dataTable.Rows)
            {
                order.Id = Convert.ToInt32(row["Id"]);
                order.UserId = Convert.ToInt32(row["UserId"]);
                order.CartId = Convert.ToInt32(row["CartId"]);
                order.Status = row["Status"].ToString();
                order.DateOfOrder = Convert.ToDateTime(row["DateOfOrder"]);
            }
            return order;
        }

        public void CreateOrder(Order order)
        {
            string query = "INSERT INTO \"Order\" (\"UserId\", \"CartId\", \"Status\", \"DateOfOrder\") VALUES (@userId, @cartId, @status, @dateOfOrder)";
            database.ExecuteNonQuery(query);
        }

        public void UpdateOrder(Order order)
        {
            string query = "UPDATE \"Order\" SET \"UserId\" = @userId, \"CartId\" = @cartId, \"Status\" = @status, \"DateOfOrder\" = @dateOfOrder WHERE \"Id\" = @id";
            database.ExecuteNonQuery(query);
        }

        public void DeleteOrder(int id)
        {
            string query = "DELETE FROM \"Order\" WHERE \"Id\" = @id";
            database.ExecuteNonQuery(query);
        }

        public List<Order> GetAllOrdersByUserId(int userId)
        {
            string query = "SELECT * FROM \"Order\" WHERE \"UserId\" = @userId";
            DataTable dataTable = database.ExecuteQuery(query);
            List<Order> orders = new List<Order>();
            foreach (DataRow row in dataTable.Rows)
            {
                Order order = new Order
                {
                    Id = Convert.ToInt32(row["Id"]),
                    UserId = Convert.ToInt32(row["UserId"]),
                    CartId = Convert.ToInt32(row["CartId"]),
                    Status = row["Status"].ToString(),
                    DateOfOrder = Convert.ToDateTime(row["DateOfOrder"])
                };
                orders.Add(order);
            }
            return orders;
        }

        public List<CartItem> GetOrderItems(int cartId)
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
    }
}