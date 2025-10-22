using System.Collections.Generic;
using System.Linq;
using CMCSApp.Models;
using System;

namespace CMCSApp.Data
{
    // Very small in-memory repository for demo / template
    public class InMemoryRepository
    {
        private readonly List<Claim> _claims = new();
        private readonly List<User> _users = new();

        public InMemoryRepository()
        {
            // Seed some users
            _users.Add(new User { Username = "lect1", Password = "password", Role = "Lecturer", DisplayName = "Lecturer One" });
            _users.Add(new User { Username = "coord1", Password = "password", Role = "Coordinator", DisplayName = "Programme Coordinator" });
            _users.Add(new User { Username = "mgr1", Password = "password", Role = "Manager", DisplayName = "Academic Manager" });

            // Seed some claims
            _claims.Add(new Claim { ClaimId = "112233", LecturerId = "ST10274003", Month = "August", HoursWorked = 15, HourlyRate = 15, Notes = "Tutoring", Status = ClaimStatus.Pending, SubmittedAt = DateTime.UtcNow.AddDays(-10) });
            _claims.Add(new Claim { ClaimId = "667744", LecturerId = "ST10274003", Month = "August", HoursWorked = 5, HourlyRate = 15, Notes = "Exam marking", Status = ClaimStatus.Approved, SubmittedAt = DateTime.UtcNow.AddDays(-12) });
        }

        public IEnumerable<Claim> GetAllClaims() => _claims.OrderByDescending(c => c.SubmittedAt);
        public IEnumerable<Claim> GetClaimsByLecturer(string lecturerId) => _claims.Where(c => c.LecturerId == lecturerId);
        public Claim? GetById(string claimId) => _claims.FirstOrDefault(c => c.ClaimId == claimId);
        public void AddClaim(Claim claim)
        {
            claim.ClaimId = claim.ClaimId ?? Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            claim.SubmittedAt = DateTime.UtcNow;
            _claims.Add(claim);
        }
        public void UpdateClaim(Claim claim)
        {
            var existing = GetById(claim.ClaimId);
            if (existing == null) return;
            existing.LecturerId = claim.LecturerId;
            existing.Month = claim.Month;
            existing.HoursWorked = claim.HoursWorked;
            existing.HourlyRate = claim.HourlyRate;
            existing.Notes = claim.Notes;
            existing.Status = claim.Status;
        }

        public User? ValidateUser(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public IEnumerable<User> GetUsers() => _users;
    }
}
