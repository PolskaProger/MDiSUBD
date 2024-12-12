using StrikeBallShop.DAL.Repositories;
using StrikeBallShop.DML.Models;
using System.Collections.Generic;

namespace StrikeBallShop.BLL.Services
{
    public class ProductService
    {
        private ProductRepository productRepository;

        public ProductService(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public Product GetProductById(int id)
        {
            return productRepository.GetProductById(id);
        }

        public void CreateProduct(Product product)
        {
            productRepository.CreateProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            productRepository.UpdateProduct(product);
        }

        public void DeleteProduct(int id)
        {
            productRepository.DeleteProduct(id);
        }

        public List<Product> GetAllProducts()
        {
            return productRepository.GetAllProducts();
        }

        public List<Product> GetFilteredAndSortedProducts(string filter, string sort)
        {
            return productRepository.GetFilteredAndSortedProducts(filter, sort);
        }
    }
}