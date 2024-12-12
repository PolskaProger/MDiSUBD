using StrikeBallShop.DAL.Database;
using StrikeBallShop.DML.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace StrikeBallShop.DAL.Repositories
{
    public class ProductRepository
    {
        private DBConnect database;

        public ProductRepository(DBConnect database)
        {
            this.database = database;
        }

        public Product GetProductById(int id)
        {
            string query = "SELECT * FROM \"Product\" WHERE \"Id\" = @id";
            DataTable dataTable = database.ExecuteQuery(query);
            Product product = null;
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                product = new Product
                {
                    Id = Convert.ToInt32(row["Id"]),
                    NameOfProduct = row["NameOfProduct"].ToString(),
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    Description = row["Description"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    Rating = Convert.ToDecimal(row["Rating"]),
                    InStorage = Convert.ToBoolean(row["InStorage"])
                };
            }
            return product;
        }

        public void CreateProduct(Product product)
        {
            string query = "INSERT INTO \"Product\" (\"NameOfProduct\", \"CategoryId\", \"Description\", \"Price\", \"Rating\", \"InStorage\") VALUES (@nameOfProduct, @categoryId, @description, @price, @rating, @inStorage)";
            database.ExecuteNonQuery(query);
        }

        public void UpdateProduct(Product product)
        {
            string query = "UPDATE \"Product\" SET \"NameOfProduct\" = @nameOfProduct, \"CategoryId\" = @categoryId, \"Description\" = @description, \"Price\" = @price, \"Rating\" = @rating, \"InStorage\" = @inStorage WHERE \"Id\" = @id";
            database.ExecuteNonQuery(query);
        }

        public void DeleteProduct(int id)
        {
            string query = "DELETE FROM \"Product\" WHERE \"Id\" = @id";
            database.ExecuteNonQuery(query);
        }

        public List<Product> GetAllProducts()
        {
            string query = "SELECT * FROM \"Product\"";
            DataTable dataTable = database.ExecuteQuery(query);
            List<Product> products = new List<Product>();
            foreach (DataRow row in dataTable.Rows)
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(row["Id"]),
                    NameOfProduct = row["NameOfProduct"].ToString(),
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    Description = row["Description"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    Rating = Convert.ToDecimal(row["Rating"]),
                    InStorage = Convert.ToBoolean(row["InStorage"])
                };
                products.Add(product);
            }
            return products;
        }

        public List<Product> GetFilteredAndSortedProducts(string filter, string sort)
        {
            string query = "SELECT * FROM \"Product\"";

            if (!string.IsNullOrEmpty(filter))
            {
                query += $" WHERE {filter}";
            }

            if (!string.IsNullOrEmpty(sort))
            {
                query += $" ORDER BY {sort}";
            }

            DataTable dataTable = database.ExecuteQuery(query);
            List<Product> products = new List<Product>();
            foreach (DataRow row in dataTable.Rows)
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(row["Id"]),
                    NameOfProduct = row["NameOfProduct"].ToString(),
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    Description = row["Description"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    Rating = Convert.ToDecimal(row["Rating"]),
                    InStorage = Convert.ToBoolean(row["InStorage"])
                };
                products.Add(product);
            }
            return products;
        }
    }
}