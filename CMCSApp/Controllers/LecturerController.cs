using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CMCSApp.Data;
using CMCSClaim = CMCSApp.Models.Claim;
using CMCSApp.Models;
using CMCSApp.ViewModels;
using System.Linq;

namespace CMCSApp.Controllers
{
    [Authorize(Roles = "Lecturer")]
    public class LecturerController : Controller
    {
        private readonly InMemoryRepository _repo;

        public LecturerController(InMemoryRepository repo)
        {
            _repo = repo ?? throw new System.ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        public IActionResult Fill()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Fill(ClaimViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var lecturerUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                               ?? User.Identity?.Name
                               ?? "UnknownLecturer";

            var claim = new CMCSClaim
            {
                LecturerId = lecturerUser,
                Month = vm.Month,
                HoursWorked = vm.HoursWorked,
                HourlyRate = vm.HourlyRate,
                Notes = vm.Notes,
                Status = ClaimStatus.Pending
            };

            _repo.AddClaim(claim);
            TempData["ClaimId"] = claim.ClaimId;
            return RedirectToAction("Confirmation", new { id = claim.ClaimId });
        }

        [HttpGet]
        public IActionResult Confirmation(string id)
        {
            var claim = _repo.GetById(id);
            if (claim == null) return NotFound();
            return View(claim);
        }

        [HttpGet]
        public IActionResult Upload(string id)
        {
            var claim = _repo.GetById(id);
            if (claim == null) return NotFound();
            return View(claim);
        }

        [HttpPost]
        public IActionResult Complete(string id)
        {
            var claim = _repo.GetById(id);
            if (claim == null) return NotFound();

            TempData["Message"] = "Claim submitted and files uploaded (template behavior).";
            return RedirectToAction("Fill");
        }

        [HttpGet]
        public IActionResult MyClaims()
        {
            var lecturerUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                               ?? User.Identity?.Name
                               ?? "";

            if (string.IsNullOrWhiteSpace(lecturerUser))
                return View(Enumerable.Empty<CMCSClaim>());

            var claims = _repo.GetClaimsByLecturer(lecturerUser).ToList();
            return View(claims);
        }
    }
}
