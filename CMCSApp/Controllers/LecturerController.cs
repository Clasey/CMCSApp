using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CMCSApp.Data;
using CMCSApp.Models;
using CMCSApp.ViewModels;

namespace CMCSApp.Controllers
{
    [Authorize(Roles = "Lecturer")]
    public class LecturerController : Controller
    {
        private readonly InMemoryRepository _repo;
        public LecturerController(InMemoryRepository repo) => _repo = repo;

        [HttpGet]
        public IActionResult Fill()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Fill(ClaimViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            // tie the claim to the logged-in username
            var lecturerUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                               ?? User.Identity?.Name
                               ?? "UnknownLecturer";

            var claim = new Claim
            {
                LecturerId = lecturerUser,   // ✅ this is key
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
            // In this template, we just mark upload completed.
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

            // defensive: make sure we have something
            if (string.IsNullOrWhiteSpace(lecturerUser))
                return View(Enumerable.Empty<Claim>());

            var claims = _repo.GetClaimsByLecturer(lecturerUser).ToList();

            // optional debug output
            System.Diagnostics.Debug.WriteLine($"LecturerUser = {lecturerUser}, found {claims.Count} claims.");

            return View(claims);
        }



    }
}
