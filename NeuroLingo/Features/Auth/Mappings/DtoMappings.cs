using NeuroLingo.Features.Auth.Dtos;
using NeuroLingo.Features.Auth.Models;

namespace NeuroLingo.Features.Auth.Mappings
{
    public static class DtoMappings
    {
        public static RegisterUserDto FromUserToRegister(User user)
        {
            return new RegisterUserDto
            {
                Email = user.Email!,
                ConfirmPassword = user.Password,
                Password = user.Password!,
                UserName = user.UserName!
            };
        }

        public static LoginUserDto FromUserToLogin(User user)
        {
            return new LoginUserDto
            {
                Email = user.Email,
                Password = user.Password
            };
        }

        public static User ToUserFromLogin(LoginUserDto user)
        {
            return new User
            {
                Email = user.Email,
                Password = user.Password
            };
        }
        public static User ToUserFromRegister(RegisterUserDto user)
        {
            return new User
            {
                Password = user.Password,
                Email = user.Email,
                UserName= user.UserName
            };
        }
    }
}
