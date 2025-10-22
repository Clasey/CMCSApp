using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CMCSApp.Data;
using CMCSApp.Models;

namespace CMCSApp.Controllers
{
    [Authorize(Roles = "Coordinator")]
    public class CoordinatorController : Controller
    {
        private readonly InMemoryRepository _repo;
        public CoordinatorController(InMemoryRepository repo) => _repo = repo;

        [HttpGet]
        public IActionResult Review()
        {
            var claims = _repo.GetAllClaims();
            return View(claims);
        }

        [HttpPost]
        public IActionResult Verify(string id)
        {
            var claim = _repo.GetById(id);
            if (claim == null) return NotFound();
            claim.Status = ClaimStatus.Verified;
            _repo.UpdateClaim(claim);
            TempData["Message"] = $"Claim {id} verified.";
            return RedirectToAction("Review");
        }

        [HttpPost]
        public IActionResult SendBack(string id)
        {
            var claim = _repo.GetById(id);
            if (claim == null) return NotFound();
            claim.Status = ClaimStatus.SentBack;
            _repo.UpdateClaim(claim);
            TempData["Message"] = $"Claim {id} sent back for changes.";
            return RedirectToAction("Review");
        }

        [HttpGet]
        public IActionResult Verified()
        {
            var claims = _repo.GetAllClaims().Where(c => c.Status == ClaimStatus.Verified);
            return View(claims);
        }
    }
}
