using StrikeBallShop.DAL.Repositories;
using StrikeBallShop.DML.Models;
using System.Collections.Generic;

namespace StrikeBallShop.BLL.Services
{
    public class CartService
    {
        private CartRepository cartRepository;

        public CartService(CartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public Cart GetCartById(int id)
        {
            return cartRepository.GetCartById(id);
        }

        public void CreateCart(Cart cart)
        {
            cartRepository.CreateCart(cart);
        }

        public void UpdateCart(Cart cart)
        {
            cartRepository.UpdateCart(cart);
        }

        public void DeleteCart(int id)
        {
            cartRepository.DeleteCart(id);
        }

        public List<CartItem> GetCartItems(int cartId)
        {
            return cartRepository.GetCartItems(cartId);
        }
        public Cart GetCartByUserId(int userId)
        {
            return cartRepository.GetCartByUserId(userId);
        }

        public void AddProductToCart(CartItem cartItem)
        {
            cartRepository.AddProductToCart(cartItem);
        }
        public void RemoveProductFromCart(int cartId, int productId)
        {
            cartRepository.RemoveProductFromCart(cartId, productId);
        }

        public void ClearCart(int cartId)
        {
            cartRepository.ClearCart(cartId);
        }
    }
}