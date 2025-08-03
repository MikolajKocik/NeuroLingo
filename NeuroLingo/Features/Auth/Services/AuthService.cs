using Microsoft.AspNetCore.Identity;
using NeuroLingo.Features.Auth.Models;
using System.ComponentModel.DataAnnotations;

namespace NeuroLingo.Features.Auth.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AuthService(
            UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher
            )
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> RegisterUserAsync(string email, string password, string username)
        {
            User? emailExists = await _userManager.FindByEmailAsync(email);
            if (emailExists is not null) 
            {
                throw new ArgumentException("Email already exists", nameof(email));
            }

            var user = new User(password, email, username);
            password = _passwordHasher.HashPassword(user, password);

            // TODO do kontrolera
            var results = new List<ValidationResult>();
            var context = new ValidationContext(user, null, null);

            if (!Validator.TryValidateObject(user, context, results, true))
            {
                throw new ValidationException(
                    "User validation failed: " + string.Join(", ", results.Select(r => r.ErrorMessage)));
            }

            var newUser = await _userManager.CreateAsync(user, password);

            return newUser.Succeeded
                ? user 
                : throw new InvalidOperationException("User creation failed");
        }

        public async Task<User> LoginUserAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
