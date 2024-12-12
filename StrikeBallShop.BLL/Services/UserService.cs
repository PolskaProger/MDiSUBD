using StrikeBallShop.DAL.Repositories;
using StrikeBallShop.DML.Models;
using System.Collections.Generic;

namespace StrikeBallShop.BLL.Services
{
    public class UserService
    {
        private UserRepository userRepository;
        private PasswordService passwordService;

        public UserService(UserRepository userRepository, PasswordService passwordService)
        {
            this.userRepository = userRepository;
            this.passwordService = passwordService;
        }

        public User GetUserById(int id)
        {
            return userRepository.GetUserById(id);
        }

        public void CreateUser(User user)
        {
            user.passwordhash = passwordService.HashPassword(user.passwordhash);
            userRepository.CreateUser(user);
        }

        public void UpdateUser(User user)
        {
            userRepository.UpdateUser(user);
        }

        public void DeleteUser(int id)
        {
            userRepository.DeleteUser(id);
        }

        public List<User> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }

        public User AuthenticateUser(string login, string password)
        {
            User user = userRepository.AuthenticateUser(login);
            if (user != null && passwordService.VerifyPassword(password, user.passwordhash))
            {
                return user;
            }
            return null;
        }
    }
}