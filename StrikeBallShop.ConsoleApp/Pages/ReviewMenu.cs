using StrikeBallShop.BLL.Services;
using StrikeBallShop.DML.Models;
using System;

namespace StrikeBallShop.ConsoleApp
{
    public class ReviewMenu
    {
        private ProductService productService;
        private ProductReviewService productReviewService;
        private ProductRatingService productRatingService;

        public ReviewMenu(ProductService productService, ProductReviewService productReviewService, ProductRatingService productRatingService)
        {
            this.productService = productService;
            this.productReviewService = productReviewService;
            this.productRatingService = productRatingService;
        }

        public void ShowReviewsAndRatings(User currentUser)
        {
            Console.WriteLine("Отзывы и рейтинги:");
            // Логика отображения отзывов и рейтингов
            Console.WriteLine("1. Добавить отзыв");
            Console.WriteLine("0. Назад");

            int choice = ConsoleHelper.GetIntInput("Выберите пункт меню: ");
            switch (choice)
            {
                case 1:
                    AddReview(currentUser);
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    ShowReviewsAndRatings(currentUser);
                    break;
            }
        }

        private void AddReview(User currentUser)
        {
            int productId = ConsoleHelper.GetIntInput("Введите ID товара для добавления отзыва: ");

            // Проверяем, существует ли товар с указанным ID
            Product product = productService.GetProductById(productId);
            if (product == null)
            {
                Console.WriteLine("Товар с указанным ID не найден.");
                ShowReviewsAndRatings(currentUser);
                return;
            }

            string reviewText = ConsoleHelper.GetStringInput("Введите текст отзыва: ");
            int rating = ConsoleHelper.GetIntInput("Введите рейтинг (1-5): ");

            // Проверяем, что рейтинг находится в диапазоне от 1 до 5
            if (rating < 1 || rating > 5)
            {
                Console.WriteLine("Рейтинг должен быть в диапазоне от 1 до 5.");
                ShowReviewsAndRatings(currentUser);
                return;
            }

            // Создаем объект отзыва
            ProductReview productReview = new ProductReview
            {
                UserId = currentUser.id,
                ProductId = productId,
                Description = reviewText,
                DateOfReview = DateTime.Now
            };

            // Создаем объект рейтинга
            ProductRating productRating = new ProductRating
            {
                UserId = currentUser.id,
                ProductId = productId,
                Mark = rating
            };

            // Сохраняем отзыв
            productReviewService.CreateProductReview(productReview);

            // Сохраняем рейтинг
            productRatingService.CreateProductRating(productRating);

            Console.WriteLine("Отзыв и рейтинг добавлены.");
            ShowReviewsAndRatings(currentUser);
        }
    }
}