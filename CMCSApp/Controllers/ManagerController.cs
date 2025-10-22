using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CMCSApp.Data;
using CMCSApp.Models;

namespace CMCSApp.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly InMemoryRepository _repo;
        public ManagerController(InMemoryRepository repo) => _repo = repo;

        [HttpGet]
        public IActionResult ApproveReject()
        {
            var claims = _repo.GetAllClaims().Where(c => c.Status == ClaimStatus.Verified || c.Status == ClaimStatus.Pending);
            return View(claims);
        }

        [HttpPost]
        public IActionResult Approve(string id)
        {
            var claim = _repo.GetById(id);
            if (claim == null) return NotFound();
            claim.Status = ClaimStatus.Approved;
            _repo.UpdateClaim(claim);
            TempData["Message"] = $"Claim {id} approved.";
            return RedirectToAction("ApproveReject");
        }

        [HttpGet]
        public IActionResult Reject(string id)
        {
            var claim = _repo.GetById(id);
            if (claim == null) return NotFound();
            return View(claim);
        }

        [HttpPost]
        public IActionResult Reject(string id, string reason)
        {
            var claim = _repo.GetById(id);
            if (claim == null) return NotFound();
            claim.Status = ClaimStatus.Rejected;
            claim.RejectionReason = reason;
            _repo.UpdateClaim(claim);
            TempData["Message"] = $"Claim {id} rejected.";
            return RedirectToAction("ApproveReject");
        }

    }
}
