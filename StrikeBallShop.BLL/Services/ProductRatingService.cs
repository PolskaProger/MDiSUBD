using StrikeBallShop.DAL.Repositories;
using StrikeBallShop.DML.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrikeBallShop.BLL.Services
{
    public class ProductRatingService
    {
        private ProductRatingRepository productRatingRepository;

        public ProductRatingService(ProductRatingRepository productRatingRepository)
        {
            this.productRatingRepository = productRatingRepository;
        }

        public ProductRating GetProductRatingById(int id)
        {
            return productRatingRepository.GetProductRatingById(id);
        }

        public void CreateProductRating(ProductRating productRating)
        {
            productRatingRepository.CreateProductRating(productRating);
        }

        public void UpdateProductRating(ProductRating productRating)
        {
            productRatingRepository.UpdateProductRating(productRating);
        }

        public void DeleteProductRating(int id)
        {
            productRatingRepository.DeleteProductRating(id);
        }
    }
}
