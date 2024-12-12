using StrikeBallShop.DML.Models;

namespace StrikeBallShop.BLL.Services
{
    public class AuthenticationService
    {
        private UserService userService;
        private PasswordService passwordService;

        public AuthenticationService(UserService userService, PasswordService passwordService)
        {
            this.userService = userService;
            this.passwordService = passwordService;
        }

        public User AuthenticateUser(string login, string password)
        {
            User user = userService.AuthenticateUser(login, password);
            if (user != null && passwordService.VerifyPassword(password, user.passwordhash))
            {
                return user;
            }
            return null;
        }

        public void RegisterUser(User user)
        {
            user.passwordhash = passwordService.HashPassword(user.passwordhash);
            userService.CreateUser(user);
        }

        public bool IsAdmin(User user)
        {
            return user.roleid == 1; // Предположим, что роль ADMIN имеет ID 1
        }
    }
}