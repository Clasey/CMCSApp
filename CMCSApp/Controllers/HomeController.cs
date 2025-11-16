using Microsoft.AspNetCore.Mvc;
using CMCSApp.Models;

namespace CMCSApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Error() => View();
    }
}
