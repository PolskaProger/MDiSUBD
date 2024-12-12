using StrikeBallShop.BLL.Services;
using StrikeBallShop.ConsoleApp.Pages;
using StrikeBallShop.DAL.Database;
using StrikeBallShop.DAL.Repositories;
using System;

namespace StrikeBallShop.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Строка подключения к базе данных
            string connectionString = "Host=localhost;Database=AirsoftStoreDB;Username=postgres;Password=011020041;Include Error Detail=true";

            // Создаем экземпляр DBConnect
            DBConnect dbConnect = new DBConnect(connectionString);

            // Создаем репозитории
            UserRepository userRepository = new UserRepository(dbConnect);
            ProductRepository productRepository = new ProductRepository(dbConnect);
            CartRepository cartRepository = new CartRepository(dbConnect);
            OrderRepository orderRepository = new OrderRepository(dbConnect);
            CategoryRepository categoryRepository = new CategoryRepository(dbConnect);
            StorageRepository storageRepository = new StorageRepository(dbConnect);
            ProductReviewRepository productReviewRepository = new ProductReviewRepository(dbConnect);
            ProductRatingRepository productRatingRepository = new ProductRatingRepository(dbConnect);

            // Создаем дополнительные сервисы
            PasswordService passwordService = new PasswordService(); // Убедитесь, что этот сервис существует

            // Создаем сервисы
            UserService userService = new UserService(userRepository, passwordService); // Передаем оба параметра
            ProductService productService = new ProductService(productRepository);
            CartService cartService = new CartService(cartRepository);
            OrderService orderService = new OrderService(orderRepository);
            CategoryService categoryService = new CategoryService(categoryRepository);
            StorageService storageService = new StorageService(storageRepository);
            ProductReviewService productReviewService = new ProductReviewService(productReviewRepository);
            ProductRatingService productRatingService = new ProductRatingService(productRatingRepository);

            // Создаем меню
            ConsoleMenu consoleMenu = new ConsoleMenu(
                userService,
                productService,
                cartService,
                orderService,
                categoryService,
                storageService,
                passwordService,
                productRatingService,
                productReviewService
            );

            // Запускаем меню
            consoleMenu.Start();
        }
    }
}