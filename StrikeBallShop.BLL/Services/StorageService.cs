using StrikeBallShop.DAL.Repositories;
using StrikeBallShop.DML.Models;
using System.Collections.Generic;

namespace StrikeBallShop.BLL.Services
{
    public class StorageService
    {
        private StorageRepository storageRepository;

        public StorageService(StorageRepository storageRepository)
        {
            this.storageRepository = storageRepository;
        }

        public Storage GetStorageByProductId(int productId)
        {
            return storageRepository.GetStorageByProductId(productId);
        }

        public void CreateStorage(Storage storage)
        {
            storageRepository.CreateStorage(storage);
        }

        public void UpdateStorage(Storage storage)
        {
            storageRepository.UpdateStorage(storage);
        }

        public void DeleteStorage(int productId)
        {
            storageRepository.DeleteStorage(productId);
        }

        public List<Storage> GetAllStorages()
        {
            return storageRepository.GetAllStorages();
        }
    }
}