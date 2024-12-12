using StrikeBallShop.DAL.Repositories;
using StrikeBallShop.DML.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrikeBallShop.BLL.Services
{
    public class OrderService
    {
        private OrderRepository orderRepository;
        private ProductRepository productRepository;

        public OrderService(OrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public Order GetOrderById(int id)
        {
            return orderRepository.GetOrderById(id);
        }

        public void CreateOrder(Order order)
        {
            orderRepository.CreateOrder(order);
        }

        public void UpdateOrder(Order order)
        {
            orderRepository.UpdateOrder(order);
        }

        public void DeleteOrder(int id)
        {
            orderRepository.DeleteOrder(id);
        }
        public List<Order> GetAllOrdersByUserId(int userId)
        {
            return orderRepository.GetAllOrdersByUserId(userId);
        }

        public List<CartItem> GetOrderItems(int cartId)
        {
            return orderRepository.GetOrderItems(cartId);
        }

        public Product GetProductById(int productId)
        {
            return productRepository.GetProductById(productId);
        }
    }
}
