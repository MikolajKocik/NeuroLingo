using Microsoft.AspNetCore.Identity;
using NeuroLingo.Exceptions;
using NeuroLingo.Features.Auth.Dtos;
using NeuroLingo.Features.Auth.Models;
using System.ComponentModel.DataAnnotations;

namespace NeuroLingo.Features.Auth.Services
{
    /// <summary>
    /// Provides authentication-related operations, including user registration and login.
    /// </summary>
    /// <remarks>This service is responsible for managing user authentication tasks, such as creating new user
    /// accounts and verifying user credentials during login. It relies on <see cref="UserManager{TUser}"/> for user
    /// management and <see cref="IPasswordHasher{TUser}"/> for password hashing.</remarks>
    public sealed class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly SignInManager<User> _signInManager;
        public AuthService(
            UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher,
            SignInManager<User> signInManager
            )
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Registers a new user asynchronously.
        /// </summary>
        /// <remarks>This method creates a new user account using the provided registration details.  If
        /// the email address already exists, a <see cref="ConflictException"/> is thrown.  If user creation fails due
        /// to validation errors, a <see cref="ValidationException"/> is thrown with details about the errors.</remarks>
        /// <param name="dto">The data transfer object containing the user's registration details, including username, email, and
        /// password.</param>
        /// <returns>A <see cref="User"/> object representing the newly registered user.</returns>
        /// <exception cref="ConflictException">Thrown if a user with the specified email address already exists.</exception>
        /// <exception cref="ValidationException">Thrown if the user creation process fails due to validation errors.</exception>
        public async Task<User> RegisterUserAsync(RegisterUserDto dto)
        {
            if(await _userManager.FindByEmailAsync(dto.Email) != null)
            {
                throw new ConflictException("Email already exists");
            }

            var user = new User(dto.Email);
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ValidationException("User creation failed" + string.Join("; ", errors));
            }

            return user;
        }
        
        /// <summary>
        /// Authenticates a user based on the provided login credentials.
        /// </summary>
        /// <remarks>This method performs user authentication by verifying the email and password provided
        /// in the  <paramref name="dto"/>. If the credentials are valid, the method returns the authenticated user. 
        /// Otherwise, it throws an exception indicating the failure reason.</remarks>
        /// <param name="dto">An object containing the user's email and password. The email is used to locate the user,  and the password
        /// is used to verify their identity.</param>
        /// <returns>The authenticated <see cref="User"/> object if the login is successful.</returns>
        /// <exception cref="NotFoundException">Thrown if no user is found with the specified email.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown if the provided credentials are invalid.</exception>
        public async Task<User> LoginUserAsync(LoginUserDto dto)
        {
            User user = await _userManager.FindByEmailAsync(dto.Email) 
                ?? throw new NotFoundException("User not found");

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName!,
                dto.Password, 
                isPersistent: true,
                lockoutOnFailure: false
                );

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            return user; 
        }

        /// <summary>
        /// Signs the current user out of the application.
        /// </summary>
        /// <remarks>This method asynchronously signs out the currently authenticated user by invalidating
        /// their session. After calling this method, the user will no longer be authenticated and will need to log in
        /// again to access protected resources.</remarks>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
