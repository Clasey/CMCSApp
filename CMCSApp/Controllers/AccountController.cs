using CMCSApp.Data;
using CMCSApp.Models;
using CMCSApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CMCSApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

        // ---------------------------
        // SHOW LOGIN PAGE
        // ---------------------------
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        // ---------------------------
        // HANDLE LOGIN
        // ---------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string hashed = HashPassword(model.Password);

            var user = _db.Users
                .FirstOrDefault(u => u.Email == model.Email && u.PasswordHash == hashed);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login credentials.");
                return View(model);
            }

            // ---------------------------
            // CREATE CLAIMS & COOKIE
            // ---------------------------
            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new System.Security.Claims.Claim(ClaimTypes.Name, user.FullName),
                new(ClaimTypes.Role, user.Role),
                new System.Security.Claims.Claim(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Optional: store user info in session if you need
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", user.FullName);

            // ---------------------------
            // REDIRECT BASED ON ROLE
            // ---------------------------
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            return user.Role switch
            {
                "Lecturer" => RedirectToAction("Fill", "Lecturer"),
                "Coordinator" => RedirectToAction("Review", "Coordinator"),
                "Manager" => RedirectToAction("ApproveReject", "Manager"),
                _ => RedirectToAction("Index", "Home")
            };
        }

        // ---------------------------
        // SHOW REGISTER PAGE
        // ---------------------------
        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel
            {
                Roles = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Lecturer", Text = "Lecturer" },
                    new SelectListItem { Value = "Coordinator", Text = "Programme Coordinator" },
                    new SelectListItem { Value = "Manager", Text = "Academic Manager" }
                }
            };

            return View(model);
        }

        // ---------------------------
        // HANDLE REGISTRATION
        // ---------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_db.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("", "This email is already registered.");
                return View(model);
            }

            var user = new User
            {
                Email = model.Email,
                FullName = model.FullName,
                Role = model.SelectedRole,
                PasswordHash = HashPassword(model.Password)
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            TempData["Success"] = "Registration successful. Please login.";
            return RedirectToAction("Login");
        }

        // ---------------------------
        // LOGOUT
        // ---------------------------
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // ---------------------------
        // PASSWORD HASHING
        // ---------------------------
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
