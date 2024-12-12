using StrikeBallShop.BLL.Services;
using StrikeBallShop.DML.Models;
using System;

namespace StrikeBallShop.ConsoleApp.Pages
{
    public class ConsoleMenu
    {
        private UserService userService;
        private ProductService productService;
        private CartService cartService;
        private OrderService orderService;
        private CategoryService categoryService;
        private StorageService storageService;
        private PasswordService passwordService;
        private ProductRatingService productRatingService;
        private ProductReviewService productReviewService;

        private User currentUser; // Текущий пользователь

        public ConsoleMenu(UserService userService, ProductService productService, CartService cartService, OrderService orderService, CategoryService categoryService, StorageService storageService, PasswordService passwordService, ProductRatingService productRatingService, ProductReviewService productReviewService)
        {
            this.userService = userService;
            this.productService = productService;
            this.cartService = cartService;
            this.orderService = orderService;
            this.categoryService = categoryService;
            this.storageService = storageService;
            this.passwordService = passwordService;
            this.productRatingService = productRatingService;
            this.productReviewService = productReviewService;
        }

        public void Start()
        {
            while (true)
            {
                if (currentUser == null)
                {
                    ShowLoginRegistrationMenu();
                }
                else
                {
                    ShowMainMenu();
                }
            }
        }

        private void ShowLoginRegistrationMenu()
        {
            Console.WriteLine("1. Вход");
            Console.WriteLine("2. Регистрация");
            Console.WriteLine("0. Выход");

            int choice = ConsoleHelper.GetIntInput("Выберите пункт меню: ");
            switch (choice)
            {
                case 1:
                    Login();
                    break;
                case 2:
                    Register();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    ShowLoginRegistrationMenu();
                    break;
            }
        }

        private void Login()
        {
            string login = ConsoleHelper.GetStringInput("Введите логин: ");
            string password = ConsoleHelper.GetStringInput("Введите пароль: ");

            User user = userService.AuthenticateUser(login, password);
            if (user != null)
            {
                currentUser = user; // Сохраняем текущего пользователя
                Console.WriteLine("Успешный вход!");
            }
            else
            {
                Console.WriteLine("Неверный логин или пароль.");
            }
        }

        private void Register()
        {
            string login = ConsoleHelper.GetStringInput("Введите логин: ");
            string email = ConsoleHelper.GetStringInput("Введите email: ");
            string password = ConsoleHelper.GetStringInput("Введите пароль: ");
            string confirmPassword = ConsoleHelper.GetStringInput("Подтвердите пароль: ");

            if (password != confirmPassword)
            {
                Console.WriteLine("Пароли не совпадают.");
                return;
            }

            User user = new User
            {
                login = login,
                email = email,
                passwordhash = password,
                roleid = 2, // Обычный пользователь
                regdate = DateTime.Now
            };

            userService.CreateUser(user);
            Console.WriteLine("Регистрация успешна!");
        }

        private void ShowMainMenu()
        {
            Console.WriteLine("1. Каталог товаров");
            Console.WriteLine("2. Корзина");
            Console.WriteLine("3. Отзывы и рейтинги");
            Console.WriteLine("4. Заказы");
            Console.WriteLine("5. Административная панель");
            Console.WriteLine("0. Выход");

            int choice = ConsoleHelper.GetIntInput("Выберите пункт меню: ");
            switch (choice)
            {
                case 1:
                    ShowProductCatalog();
                    break;
                case 2:
                    ShowCart();
                    break;
                case 3:
                    ShowReviewsAndRatings();
                    break;
                case 4:
                    ShowOrders();
                    break;
                case 5:
                    ShowAdminPanel();
                    break;
                case 0:
                    Logout();
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    ShowMainMenu();
                    break;
            }
        }

        private void Logout()
        {
            currentUser = null;
            Console.WriteLine("Вы вышли из системы.");
        }

        private void ShowProductCatalog()
        {
            ProductMenu productMenu = new ProductMenu(productService, cartService);
            productMenu.ShowProductCatalog(currentUser);
        }

        private void ShowCart()
        {
            CartMenu cartMenu = new CartMenu(cartService, orderService, productService);
            cartMenu.ShowCart(currentUser);
        }

        private void ShowReviewsAndRatings()
        {
            ReviewMenu reviewMenu = new ReviewMenu(productService, productReviewService, productRatingService);
            reviewMenu.ShowReviewsAndRatings(currentUser);
        }

        private void ShowOrders()
        {
            OrderMenu orderMenu = new OrderMenu(orderService);
            orderMenu.ShowOrders(currentUser);
        }

        private void ShowAdminPanel()
        {
            if (currentUser == null || currentUser.roleid != 1) // Проверка на роль администратора
            {
                Console.WriteLine("Доступ запрещен. Только администратор может получить доступ к этому разделу.");
                ShowMainMenu();
                return;
            }

            AdminMenu adminMenu = new AdminMenu(userService, productService, categoryService, orderService, storageService);
            adminMenu.ShowAdminPanel(currentUser);
        }
    }
}