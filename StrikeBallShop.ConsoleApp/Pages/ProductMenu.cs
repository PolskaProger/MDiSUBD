using StrikeBallShop.BLL.Services;
using StrikeBallShop.DML.Models;
using System;
using System.Collections.Generic;

namespace StrikeBallShop.ConsoleApp.Pages
{
    public class ProductMenu
    {
        private ProductService productService;
        private CartService cartService;

        public ProductMenu(ProductService productService, CartService cartService)
        {
            this.productService = productService;
            this.cartService = cartService;
        }

        public void ShowProductCatalog(User currentUser)
        {
            Console.WriteLine("Каталог товаров:");
            List<Product> products = productService.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Название: {product.NameOfProduct}, Цена: {product.Price}, Рейтинг: {product.Rating}");
            }

            Console.WriteLine("1. Добавить товар в корзину");
            Console.WriteLine("0. Назад");

            int choice = ConsoleHelper.GetIntInput("Выберите пункт меню: ");
            switch (choice)
            {
                case 1:
                    AddProductToCart(currentUser);
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    ShowProductCatalog(currentUser);
                    break;
            }
        }

        private void AddProductToCart(User currentUser)
        {
            int productId = ConsoleHelper.GetIntInput("Введите ID товара для добавления в корзину: ");
            int quantity = ConsoleHelper.GetIntInput("Введите количество: ");

            // Проверяем, существует ли товар с указанным ID
            Product product = productService.GetProductById(productId);
            if (product == null)
            {
                Console.WriteLine("Товар с указанным ID не найден.");
                ShowProductCatalog(currentUser);
                return;
            }

            // Проверяем, есть ли у пользователя корзина
            Cart cart = cartService.GetCartByUserId(currentUser.id);
            if (cart == null)
            {
                // Если корзины нет, создаем новую корзину
                cart = new Cart
                {
                    UserId = currentUser.id,
                    TotalPrice = 0
                };
                cartService.CreateCart(cart);
            }

            // Добавляем товар в корзину
            CartItem cartItem = new CartItem
            {
                ProductId = productId,
                CartId = cart.Id,
                Count = quantity
            };
            cartService.AddProductToCart(cartItem);

            // Обновляем общую стоимость корзины
            cart.TotalPrice += product.Price * quantity;
            cartService.UpdateCart(cart);

            Console.WriteLine($"Товар с ID {productId} добавлен в корзину в количестве {quantity}.");
            ShowProductCatalog(currentUser);
        }
    }
}