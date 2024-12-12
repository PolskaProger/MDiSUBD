using StrikeBallShop.DAL.Repositories;
using StrikeBallShop.DML.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrikeBallShop.BLL.Services
{
    public class CartItemService
    {
        private CartItemRepository cartItemRepository;

        public CartItemService(CartItemRepository cartItemRepository)
        {
            this.cartItemRepository = cartItemRepository;
        }

        public CartItem GetCartItemById(int id)
        {
            return cartItemRepository.GetCartItemById(id);
        }

        public void CreateCartItem(CartItem cartItem)
        {
            cartItemRepository.CreateCartItem(cartItem);
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            cartItemRepository.UpdateCartItem(cartItem);
        }

        public void DeleteCartItem(int id)
        {
            cartItemRepository.DeleteCartItem(id);
        }
    }
}

