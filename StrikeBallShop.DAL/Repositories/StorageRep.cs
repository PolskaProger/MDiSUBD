using StrikeBallShop.DAL.Database;
using StrikeBallShop.DML.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace StrikeBallShop.DAL.Repositories
{
    public class StorageRepository
    {
        private DBConnect database;

        public StorageRepository(DBConnect database)
        {
            this.database = database;
        }

        public Storage GetStorageByProductId(int productId)
        {
            string query = "SELECT * FROM \"Storage\" WHERE \"ProductId\" = @productId";
            DataTable dataTable = database.ExecuteQuery(query);
            Storage storage = new Storage();
            foreach (DataRow row in dataTable.Rows)
            {
                storage.ProductId = Convert.ToInt32(row["ProductId"]);
                storage.Count = Convert.ToInt32(row["Count"]);
            }
            return storage;
        }

        public void CreateStorage(Storage storage)
        {
            string query = "INSERT INTO \"Storage\" (\"ProductId\", \"Count\") VALUES (@productId, @count)";
            database.ExecuteNonQuery(query);
        }

        public void UpdateStorage(Storage storage)
        {
            string query = "UPDATE \"Storage\" SET \"Count\" = @count WHERE \"ProductId\" = @productId";
            database.ExecuteNonQuery(query);
        }

        public void DeleteStorage(int productId)
        {
            string query = "DELETE FROM \"Storage\" WHERE \"ProductId\" = @productId";
            database.ExecuteNonQuery(query);
        }

        public List<Storage> GetAllStorages()
        {
            string query = "SELECT * FROM \"Storage\"";
            DataTable dataTable = database.ExecuteQuery(query);
            List<Storage> storages = new List<Storage>();
            foreach (DataRow row in dataTable.Rows)
            {
                Storage storage = new Storage
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    Count = Convert.ToInt32(row["Count"])
                };
                storages.Add(storage);
            }
            return storages;
        }
    }
}