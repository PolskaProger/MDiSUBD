using StrikeBallShop.DAL.Database;
using StrikeBallShop.DML.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace StrikeBallShop.DAL.Repositories
{
    public class CategoryRepository
    {
        private DBConnect database;

        public CategoryRepository(DBConnect database)
        {
            this.database = database;
        }

        public Category GetCategoryById(int id)
        {
            string query = "SELECT * FROM \"Category\" WHERE \"Id\" = @id";
            DataTable dataTable = database.ExecuteQuery(query);
            Category category = new Category();
            foreach (DataRow row in dataTable.Rows)
            {
                category.Id = Convert.ToInt32(row["Id"]);
                category.NameOfCategory = row["NameOfCategory"].ToString();
            }
            return category;
        }

        public void CreateCategory(Category category)
        {
            string query = "INSERT INTO \"Category\" (\"NameOfCategory\") VALUES (@nameOfCategory)";
            database.ExecuteNonQuery(query);
        }

        public void UpdateCategory(Category category)
        {
            string query = "UPDATE \"Category\" SET \"NameOfCategory\" = @nameOfCategory WHERE \"Id\" = @id";
            database.ExecuteNonQuery(query);
        }

        public void DeleteCategory(int id)
        {
            string query = "DELETE FROM \"Category\" WHERE \"Id\" = @id";
            database.ExecuteNonQuery(query);
        }

        public List<Category> GetAllCategories()
        {
            string query = "SELECT * FROM \"Category\"";
            DataTable dataTable = database.ExecuteQuery(query);
            List<Category> categories = new List<Category>();
            foreach (DataRow row in dataTable.Rows)
            {
                Category category = new Category
                {
                    Id = Convert.ToInt32(row["Id"]),
                    NameOfCategory = row["NameOfCategory"].ToString()
                };
                categories.Add(category);
            }
            return categories;
        }
    }
}