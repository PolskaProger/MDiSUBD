using StrikeBallShop.BLL.Services;
using StrikeBallShop.DML.Models;
using System;
using System.Collections.Generic;

namespace StrikeBallShop.ConsoleApp
{
    public class AdminMenu
    {
        private UserService userService;
        private ProductService productService;
        private CategoryService categoryService;
        private OrderService orderService;
        private StorageService storageService;

        public AdminMenu(UserService userService, ProductService productService, CategoryService categoryService, OrderService orderService, StorageService storageService)
        {
            this.userService = userService;
            this.productService = productService;
            this.categoryService = categoryService;
            this.orderService = orderService;
            this.storageService = storageService;
        }

        public void ShowAdminPanel(User currentUser)
        {
            Console.WriteLine("Административная панель:");
            Console.WriteLine("1. Управление пользователями");
            Console.WriteLine("2. Управление товарами");
            Console.WriteLine("3. Управление категориями");
            Console.WriteLine("4. Управление заказами");
            Console.WriteLine("5. Управление складом");
            Console.WriteLine("0. Назад");

            int choice = ConsoleHelper.GetIntInput("Выберите пункт меню: ");
            switch (choice)
            {
                case 1:
                    ManageUsers(currentUser);
                    break;
                case 2:
                    ManageProducts(currentUser);
                    break;
                case 3:
                    ManageCategories(currentUser);
                    break;
                case 4:
                    ManageOrders(currentUser);
                    break;
                case 5:
                    ManageStorage(currentUser);
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    ShowAdminPanel(currentUser);
                    break;
            }
        }

        private void ManageUsers(User currentUser)
        {
            Console.WriteLine("Управление пользователями:");
            List<User> users = userService.GetAllUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.id}, Логин: {user.login}, Email: {user.email}, Роль: {user.roleid}, Дата регистрации: {user.regdate}");
            }

            Console.WriteLine("1. Добавить пользователя");
            Console.WriteLine("2. Редактировать пользователя");
            Console.WriteLine("3. Удалить пользователя");
            Console.WriteLine("0. Назад");

            int choice = ConsoleHelper.GetIntInput("Выберите пункт меню: ");
            switch (choice)
            {
                case 1:
                    AddUser(currentUser);
                    break;
                case 2:
                    EditUser(currentUser);
                    break;
                case 3:
                    DeleteUser(currentUser);
                    break;
                case 0:
                    ShowAdminPanel(currentUser);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    ManageUsers(currentUser);
                    break;
            }
        }

        private void AddUser(User currentUser)
        {
            string login = ConsoleHelper.GetStringInput("Введите логин: ");
            string email = ConsoleHelper.GetStringInput("Введите email: ");
            string password = ConsoleHelper.GetStringInput("Введите пароль: ");
            int roleId = ConsoleHelper.GetIntInput("Введите ID роли (1 - Admin, 2 - User): ");

            User user = new User
            {
                login = login,
                email = email,
                passwordhash = password,
                roleid = roleId,
                regdate = DateTime.Now
            };

            userService.CreateUser(user);
            Console.WriteLine("Пользователь добавлен.");
            ManageUsers(currentUser);
        }

        private void EditUser(User currentUser)
        {
            int userId = ConsoleHelper.GetIntInput("Введите ID пользователя для редактирования: ");
            User user = userService.GetUserById(userId);
            if (user == null)
            {
                Console.WriteLine("Пользователь не найден.");
                ManageUsers(currentUser);
                return;
            }

            string newLogin = ConsoleHelper.GetStringInput($"Текущий логин: {user.login}. Введите новый логин: ");
            string newEmail = ConsoleHelper.GetStringInput($"Текущий email: {user.email}. Введите новый email: ");
            string newPassword = ConsoleHelper.GetStringInput("Введите новый пароль: ");
            int newRoleId = ConsoleHelper.GetIntInput($"Текущая роль: {user.roleid}. Введите новую роль (1 - Admin, 2 - User): ");

            user.login = newLogin;
            user.email = newEmail;
            user.passwordhash = newPassword;
            user.roleid = newRoleId;

            userService.UpdateUser(user);
            Console.WriteLine("Пользователь обновлен.");
            ManageUsers(currentUser);
        }

        private void DeleteUser(User currentUser)
        {
            int userId = ConsoleHelper.GetIntInput("Введите ID пользователя для удаления: ");
            userService.DeleteUser(userId);
            Console.WriteLine("Пользователь удален.");
            ManageUsers(currentUser);
        }

        private void ManageProducts(User currentUser)
        {
            Console.WriteLine("Управление товарами:");
            List<Product> products = productService.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Название: {product.NameOfProduct}, Категория: {product.CategoryId}, Цена: {product.Price}, Рейтинг: {product.Rating}, Наличие: {product.InStorage}");
            }

            Console.WriteLine("1. Добавить товар");
            Console.WriteLine("2. Редактировать товар");
            Console.WriteLine("3. Удалить товар");
            Console.WriteLine("0. Назад");

            int choice = ConsoleHelper.GetIntInput("Выберите пункт меню: ");
            switch (choice)
            {
                case 1:
                    AddProduct(currentUser);
                    break;
                case 2:
                    EditProduct(currentUser);
                    break;
                case 3:
                    DeleteProduct(currentUser);
                    break;
                case 0:
                    ShowAdminPanel(currentUser);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    ManageProducts(currentUser);
                    break;
            }
        }

        private void AddProduct(User currentUser)
        {
            string name = ConsoleHelper.GetStringInput("Введите название товара: ");
            int categoryId = ConsoleHelper.GetIntInput("Введите ID категории: ");
            string description = ConsoleHelper.GetStringInput("Введите описание товара: ");
            decimal price = ConsoleHelper.GetDecimalInput("Введите цену товара: ");
            bool inStorage = ConsoleHelper.GetBoolInput("Товар в наличии? (Да/Нет): ");

            Product product = new Product
            {
                NameOfProduct = name,
                CategoryId = categoryId,
                Description = description,
                Price = price,
                InStorage = inStorage
            };

            productService.CreateProduct(product);
            Console.WriteLine("Товар добавлен.");
            ManageProducts(currentUser);
        }

        private void EditProduct(User currentUser)
        {
            int productId = ConsoleHelper.GetIntInput("Введите ID товара для редактирования: ");
            Product product = productService.GetProductById(productId);
            if (product == null)
            {
                Console.WriteLine("Товар не найден.");
                ManageProducts(currentUser);
                return;
            }

            string newName = ConsoleHelper.GetStringInput($"Текущее название: {product.NameOfProduct}. Введите новое название: ");
            int newCategoryId = ConsoleHelper.GetIntInput($"Текущая категория: {product.CategoryId}. Введите новую категорию: ");
            string newDescription = ConsoleHelper.GetStringInput($"Текущее описание: {product.Description}. Введите новое описание: ");
            decimal newPrice = ConsoleHelper.GetDecimalInput($"Текущая цена: {product.Price}. Введите новую цену: ");
            bool newInStorage = ConsoleHelper.GetBoolInput($"Текущее наличие: {product.InStorage}. Введите новое наличие (Да/Нет): ");

            product.NameOfProduct = newName;
            product.CategoryId = newCategoryId;
            product.Description = newDescription;
            product.Price = newPrice;
            product.InStorage = newInStorage;

            productService.UpdateProduct(product);
            Console.WriteLine("Товар обновлен.");
            ManageProducts(currentUser);
        }

        private void DeleteProduct(User currentUser)
        {
            int productId = ConsoleHelper.GetIntInput("Введите ID товара для удаления: ");
            productService.DeleteProduct(productId);
            Console.WriteLine("Товар удален.");
            ManageProducts(currentUser);
        }

        private void ManageCategories(User currentUser)
        {
            Console.WriteLine("Управление категориями:");
            List<Category> categories = categoryService.GetAllCategories();
            foreach (var category in categories)
            {
                Console.WriteLine($"ID: {category.Id}, Название: {category.NameOfCategory}");
            }

            Console.WriteLine("1. Добавить категорию");
            Console.WriteLine("2. Редактировать категорию");
            Console.WriteLine("3. Удалить категорию");
            Console.WriteLine("0. Назад");

            int choice = ConsoleHelper.GetIntInput("Выберите пункт меню: ");
            switch (choice)
            {
                case 1:
                    AddCategory(currentUser);
                    break;
                case 2:
                    EditCategory(currentUser);
                    break;
                case 3:
                    DeleteCategory(currentUser);
                    break;
                case 0:
                    ShowAdminPanel(currentUser);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    ManageCategories(currentUser);
                    break;
            }
        }

        private void AddCategory(User currentUser)
        {
            string name = ConsoleHelper.GetStringInput("Введите название категории: ");

            Category category = new Category
            {
                NameOfCategory = name
            };

            categoryService.CreateCategory(category);
            Console.WriteLine("Категория добавлена.");
            ManageCategories(currentUser);
        }

        private void EditCategory(User currentUser)
        {
            int categoryId = ConsoleHelper.GetIntInput("Введите ID категории для редактирования: ");
            Category category = categoryService.GetCategoryById(categoryId);
            if (category == null)
            {
                Console.WriteLine("Категория не найдена.");
                ManageCategories(currentUser);
                return;
            }

            string newName = ConsoleHelper.GetStringInput($"Текущее название: {category.NameOfCategory}. Введите новое название: ");

            category.NameOfCategory = newName;

            categoryService.UpdateCategory(category);
            Console.WriteLine("Категория обновлена.");
            ManageCategories(currentUser);
        }

        private void DeleteCategory(User currentUser)
        {
            int categoryId = ConsoleHelper.GetIntInput("Введите ID категории для удаления: ");
            categoryService.DeleteCategory(categoryId);
            Console.WriteLine("Категория удалена.");
            ManageCategories(currentUser);
        }

        private void ManageOrders(User currentUser)
        {
            Console.WriteLine("Управление заказами:");
            List<Order> orders = orderService.GetAllOrdersByUserId(currentUser.id);
            foreach (var order in orders)
            {
                Console.WriteLine($"ID: {order.Id}, Статус: {order.Status}, Дата заказа: {order.DateOfOrder}");
            }

            Console.WriteLine("1. Изменить статус заказа");
            Console.WriteLine("2. Удалить заказ");
            Console.WriteLine("0. Назад");

            int choice = ConsoleHelper.GetIntInput("Выберите пункт меню: ");
            switch (choice)
            {
                case 1:
                    ChangeOrderStatus(currentUser);
                    break;
                case 2:
                    DeleteOrder(currentUser);
                    break;
                case 0:
                    ShowAdminPanel(currentUser);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    ManageOrders(currentUser);
                    break;
            }
        }

        private void ChangeOrderStatus(User currentUser)
        {
            int orderId = ConsoleHelper.GetIntInput("Введите ID заказа для изменения статуса: ");
            Order order = orderService.GetOrderById(orderId);
            if (order == null)
            {
                Console.WriteLine("Заказ не найден.");
                ManageOrders(currentUser);
                return;
            }

            string newStatus = ConsoleHelper.GetStringInput($"Текущий статус: {order.Status}. Введите новый статус: ");

            order.Status = newStatus;

            orderService.UpdateOrder(order);
            Console.WriteLine("Статус заказа изменен.");
            ManageOrders(currentUser);
        }

        private void DeleteOrder(User currentUser)
        {
            int orderId = ConsoleHelper.GetIntInput("Введите ID заказа для удаления: ");
            orderService.DeleteOrder(orderId);
            Console.WriteLine("Заказ удален.");
            ManageOrders(currentUser);
        }

        private void ManageStorage(User currentUser)
        {
            Console.WriteLine("Управление складом:");
            List<Storage> storages = storageService.GetAllStorages();
            foreach (var storage in storages)
            {
                Console.WriteLine($"ID товара: {storage.ProductId}, Количество: {storage.Count}");
            }

            Console.WriteLine("1. Добавить товар на склад");
            Console.WriteLine("2. Редактировать количество товара на складе");
            Console.WriteLine("3. Удалить товар со склада");
            Console.WriteLine("0. Назад");

            int choice = ConsoleHelper.GetIntInput("Выберите пункт меню: ");
            switch (choice)
            {
                case 1:
                    AddProductToStorage(currentUser);
                    break;
                case 2:
                    EditProductInStorage(currentUser);
                    break;
                case 3:
                    DeleteProductFromStorage(currentUser);
                    break;
                case 0:
                    ShowAdminPanel(currentUser);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    ManageStorage(currentUser);
                    break;
            }
        }

        private void AddProductToStorage(User currentUser)
        {
            int productId = ConsoleHelper.GetIntInput("Введите ID товара для добавления на склад: ");
            int count = ConsoleHelper.GetIntInput("Введите количество: ");

            Storage storage = new Storage
            {
                ProductId = productId,
                Count = count
            };

            storageService.CreateStorage(storage);
            Console.WriteLine("Товар добавлен на склад.");
            ManageStorage(currentUser);
        }

        private void EditProductInStorage(User currentUser)
        {
            int productId = ConsoleHelper.GetIntInput("Введите ID товара для редактирования количества на складе: ");
            Storage storage = storageService.GetStorageByProductId(productId);
            if (storage == null)
            {
                Console.WriteLine("Товар не найден на складе.");
                ManageStorage(currentUser);
                return;
            }

            int newCount = ConsoleHelper.GetIntInput($"Текущее количество: {storage.Count}. Введите новое количество: ");

            storage.Count = newCount;

            storageService.UpdateStorage(storage);
            Console.WriteLine("Количество товара на складе обновлено.");
            ManageStorage(currentUser);
        }

        private void DeleteProductFromStorage(User currentUser)
        {
            int productId = ConsoleHelper.GetIntInput("Введите ID товара для удаления со склада: ");
            storageService.DeleteStorage(productId);
            Console.WriteLine("Товар удален со склада.");
            ManageStorage(currentUser);
        }
    }
}