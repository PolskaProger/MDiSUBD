using StrikeBallShop.DAL.Repositories;
using StrikeBallShop.DML.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrikeBallShop.BLL.Services
{
    public class ProductReviewService
    {
        private ProductReviewRepository productReviewRepository;

        public ProductReviewService(ProductReviewRepository productReviewRepository)
        {
            this.productReviewRepository = productReviewRepository;
        }

        public ProductReview GetProductReviewById(int id)
        {
            return productReviewRepository.GetProductReviewById(id);
        }

        public void CreateProductReview(ProductReview productReview)
        {
            productReviewRepository.CreateProductReview(productReview);
        }

        public void UpdateProductReview(ProductReview productReview)
        {
            productReviewRepository.UpdateProductReview(productReview);
        }

        public void DeleteProductReview(int id)
        {
            productReviewRepository.DeleteProductReview(id);
        }
    }
}
