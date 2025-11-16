using CMCSApp.Data;
using CMCSApp.Models;
using CMCSApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SecClaim = System.Security.Claims.Claim;

namespace CMCSApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly InMemoryRepository? _repo;

        public AccountController(InMemoryRepository repo)
        {
            _repo = repo;
        }

        // ---------------- LOGIN ----------------
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            var vm = new LoginViewModel { ReturnUrl = returnUrl };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = _repo?.ValidateUser(vm.Username, vm.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View(vm);
            }

            var claims = new List<SecClaim>
            {
                new SecClaim(System.Security.Claims.ClaimTypes.Name, user.DisplayName),
                new SecClaim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Username),
                new SecClaim(System.Security.Claims.ClaimTypes.Role, user.Role)
            };

            var identity = new System.Security.Claims.ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new System.Security.Claims.ClaimsPrincipal(identity));

            if (!string.IsNullOrEmpty(vm.ReturnUrl) && Url.IsLocalUrl(vm.ReturnUrl))
                return Redirect(vm.ReturnUrl);

            return RedirectToAction("Index", "Home");
        }

        // ---------------- LOGOUT ----------------
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // ---------------- PROFILE ----------------
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        // ---------------- REGISTER ----------------
        [HttpGet]
        public IActionResult Register()
        {
            var vm = new RegisterViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            // Check if username exists
            var existingUser = _repo?.GetUserByUsername(vm.Username);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Username already exists.");
                return View(vm);
            }

            // Create new user
            var newUser = new User
            {
                Username = vm.Username,
                DisplayName = vm.DisplayName,
                Password = vm.Password, // For demo purposes; ideally hash this!
                Role = vm.SelectedRole
            };

            _repo?.AddUser(newUser);
            TempData["Message"] = $"User {vm.Username} registered successfully. You can now log in.";

            return RedirectToAction("Login");
        }
    }
}
