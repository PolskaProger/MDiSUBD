using StrikeBallShop.BLL.Services;
using StrikeBallShop.DML.Models;
using System;
using System.Collections.Generic;

namespace StrikeBallShop.ConsoleApp
{
    public class OrderMenu
    {
        private OrderService orderService;

        public OrderMenu(OrderService orderService)
        {
            this.orderService = orderService;
        }

        public void ShowOrders(User currentUser)
        {
            Console.WriteLine("История заказов:");
            List<Order> orders = orderService.GetAllOrdersByUserId(currentUser.id);

            if (orders.Count == 0)
            {
                Console.WriteLine("У вас пока нет заказов.");
                return;
            }

            foreach (var order in orders)
            {
                Console.WriteLine($"ID заказа: {order.Id}, Статус: {order.Status}, Дата заказа: {order.DateOfOrder}");
            }

            Console.WriteLine("1. Просмотреть детали заказа");
            Console.WriteLine("0. Назад");

            int choice = ConsoleHelper.GetIntInput("Выберите пункт меню: ");
            switch (choice)
            {
                case 1:
                    ViewOrderDetails(currentUser);
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    ShowOrders(currentUser);
                    break;
            }
        }

        private void ViewOrderDetails(User currentUser)
        {
            int orderId = ConsoleHelper.GetIntInput("Введите ID заказа для просмотра деталей: ");

            Order order = orderService.GetOrderById(orderId);
            if (order == null)
            {
                Console.WriteLine("Заказ с указанным ID не найден.");
                ShowOrders(currentUser);
                return;
            }

            if (order.UserId != currentUser.id)
            {
                Console.WriteLine("У вас нет доступа к этому заказу.");
                ShowOrders(currentUser);
                return;
            }

            Console.WriteLine($"Детали заказа ID: {order.Id}");
            Console.WriteLine($"Статус: {order.Status}");
            Console.WriteLine($"Дата заказа: {order.DateOfOrder}");

            List<CartItem> orderItems = orderService.GetOrderItems(order.CartId);
            if (orderItems.Count == 0)
            {
                Console.WriteLine("В заказе нет товаров.");
            }
            else
            {
                Console.WriteLine("Товары в заказе:");
                foreach (var item in orderItems)
                {
                    Product product = orderService.GetProductById(item.ProductId);
                    Console.WriteLine($"ID товара: {item.ProductId}, Название: {product.NameOfProduct}, Количество: {item.Count}, Цена: {product.Price}");
                }
            }

            Console.WriteLine("0. Назад");
            int choice = ConsoleHelper.GetIntInput("Выберите пункт меню: ");
            if (choice == 0)
            {
                ShowOrders(currentUser);
            }
        }
    }
}