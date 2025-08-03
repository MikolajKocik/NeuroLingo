using Microsoft.AspNetCore.Mvc;
using NeuroLingo.Exceptions;
using NeuroLingo.Features.Auth.Dtos;
using NeuroLingo.Features.Auth.Services;
using NeuroLingo.Features.Auth.ViewModels;
using NeuroLingo.Utils.ValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace NeuroLingo.Features.Auth.Controllers
{
    /// <summary>
    /// Provides authentication-related actions, including user registration, login, and logout.
    /// </summary>
    /// <remarks>This controller handles user authentication workflows such as registering new users, logging
    /// in, and logging out.  It interacts with the <see cref="IAuthService"/> to perform the necessary operations and
    /// ensures proper validation  of user input through attributes like <see cref="ValidateDto"/> and <see
    /// cref="ValidateAntiForgeryTokenAttribute"/>.</remarks>
    [ValidateDto]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel vm)
        {
            try
            {
                var dto = new RegisterUserDto
                {
                    Email = vm.Email,
                    Password = vm.Password,
                };

                await _authService.RegisterUserAsync(dto);

                TempData["Success"] = "Registered successfull";
                return RedirectToAction("Index", "Home");
            }
            catch (ConflictException)
            {
                ModelState.AddModelError(nameof(vm.Email), "This email is already in used");
                return View(vm);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel vm)
        {
            try
            {
                var dto = new LoginUserDto
                {
                    Email = vm.Email,
                    Password = vm.Password
                };

                await _authService.LoginUserAsync(dto);

                TempData["Success"] = "Logged successfull";
                return RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedAccessException)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password");
                return View(vm);
            }
            catch (NotFoundException)
            {
                ModelState.AddModelError(nameof(vm.Email), $"User with: {vm.Email} not found");
                return View(vm);
            }
        }
    }
}
