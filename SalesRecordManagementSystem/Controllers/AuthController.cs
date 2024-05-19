using AimaanRegistrationSystem.ActionFilter;
using AimaanRegistrationSystem.Utility;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesRecordManagementSystem.Context;
using SalesRecordManagementSystem.Models;
using SalesRecordManagementSystem.Models.Auth;

namespace SalesRecordManagementSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly INotyfService _notyfService;
        private readonly SRSContext _srsContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, INotyfService notyfService, SRSContext srsContext, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _notyfService = notyfService;
            _srsContext = srsContext;
            _httpContextAccessor = httpContextAccessor;
        }

        [RedirectAuthenticatedUsers]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username) ?? await _userManager.FindByEmailAsync(model.Username);

                var result = await _signInManager.PasswordSignInAsync(user!.UserName!, model.Password, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var userDetails = await Helper.GetCurrentUserIdAsync(_httpContextAccessor, _userManager);
                    var businesses = await _srsContext.Businesses.AnyAsync(x => x.Id == userDetails.userId);

                    var redirectResult = businesses ? RedirectToAction("Index", "Business") : RedirectToAction("CreateBusiness", "Business");

                    _notyfService.Success("Login succesful");
                    return redirectResult;
                }

                ModelState.AddModelError("", "Invalid login attempt");
                _notyfService.Error("Invalid login attempt");
                return View(model);
            }

            return View(model);
        }

        [RedirectAuthenticatedUsers]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == model.Email || u.UserName == model.Username);

                if (existingUser != null)
                {
                    _notyfService.Warning("User already exist!");
                    return View();
                }

                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    _notyfService.Error("An error occured while registering user!");
                    return View();
                }

                _notyfService.Success("Registration was successful");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("CreateBusiness", "Business");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Auth");
        }
    }
}
