using StrikeBallShop.BLL.Services;
using StrikeBallShop.DML.Models;
using System;
using System.Collections.Generic;

namespace StrikeBallShop.ConsoleApp
{
    public class CartMenu
    {
        private CartService cartService;
        private OrderService orderService;
        private ProductService productService;

        public CartMenu(CartService cartService, OrderService orderService, ProductService productService)
        {
            this.cartService = cartService;
            this.orderService = orderService;
            this.productService = productService;
        }

        public void ShowCart(User currentUser)
        {
            Console.WriteLine("Корзина:");

            // Получаем корзину текущего пользователя
            Cart cart = cartService.GetCartByUserId(currentUser.id);
            if (cart == null || cart.TotalPrice == 0)
            {
                Console.WriteLine("Ваша корзина пуста.");
                return;
            }

            // Получаем список товаров в корзине
            List<CartItem> cartItems = cartService.GetCartItems(cart.Id);
            if (cartItems.Count == 0)
            {
                Console.WriteLine("Ваша корзина пуста.");
                return;
            }

            // Отображаем товары в корзине
            Console.WriteLine("Товары в корзине:");
            foreach (var item in cartItems)
            {
                Product product = productService.GetProductById(item.ProductId);
                Console.WriteLine($"ID: {item.ProductId}, Название: {product.NameOfProduct}, Количество: {item.Count}, Цена: {product.Price}");
            }

            Console.WriteLine($"Общая стоимость: {cart.TotalPrice}");

            Console.WriteLine("1. Удалить товар из корзины");
            Console.WriteLine("2. Оформить заказ");
            Console.WriteLine("0. Назад");

            int choice = ConsoleHelper.GetIntInput("Выберите пункт меню: ");
            switch (choice)
            {
                case 1:
                    RemoveProductFromCart(currentUser);
                    break;
                case 2:
                    PlaceOrder(currentUser);
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    ShowCart(currentUser);
                    break;
            }
        }

        private void RemoveProductFromCart(User currentUser)
        {
            int productId = ConsoleHelper.GetIntInput("Введите ID товара для удаления из корзины: ");

            // Получаем корзину текущего пользователя
            Cart cart = cartService.GetCartByUserId(currentUser.id);
            if (cart == null)
            {
                Console.WriteLine("Ваша корзина пуста.");
                ShowCart(currentUser);
                return;
            }

            // Удаляем товар из корзины
            cartService.RemoveProductFromCart(cart.Id, productId);

            Console.WriteLine($"Товар с ID {productId} удален из корзины.");
            ShowCart(currentUser);
        }

        private void PlaceOrder(User currentUser)
        {
            // Получаем корзину текущего пользователя
            Cart cart = cartService.GetCartByUserId(currentUser.id);
            if (cart == null || cart.TotalPrice == 0)
            {
                Console.WriteLine("Ваша корзина пуста. Невозможно оформить заказ.");
                ShowCart(currentUser);
                return;
            }

            // Создаем новый заказ
            Order order = new Order
            {
                UserId = currentUser.id,
                CartId = cart.Id,
                Status = "Ожидает оплаты",
                DateOfOrder = DateTime.Now
            };

            // Сохраняем заказ в базе данных
            orderService.CreateOrder(order);

            // Очищаем корзину
            cartService.ClearCart(cart.Id);

            Console.WriteLine("Заказ оформлен.");
            ShowCart(currentUser);
        }
    }
}