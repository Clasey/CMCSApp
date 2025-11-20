using System.Collections.Generic;
using System.Linq;
using CMCSApp.Models;

namespace CMCSApp.Data
{
    public class InMemoryRepository
    {
        private readonly List<User> _users = new();
        private readonly List<Claim> _claims = new();

        public InMemoryRepository()
        {
            // demo users
            _users.Add(new User { Email = "lect1", PasswordHash = "12345", Role = "Lecturer", FullName = "Lecturer One" });
            _users.Add(new User { Email = "coord1", PasswordHash = "12345", Role = "Coordinator", FullName = "Coordinator One" });
            _users.Add(new User { Email = "mgr1", PasswordHash = "12345", Role = "Manager", FullName = "Manager One" });

        }

        // Users
        public User? ValidateUser(string email, string passwordHash)
        {
            return _users.FirstOrDefault(u => u.Email == email && u.PasswordHash == passwordHash);
        }


        public User? GetUserByEmail(string Email)
        {
            return _users.FirstOrDefault(u => u.Email == Email);
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        // Claims
        public void AddClaim(Claim claim)
        {
            _claims.Add(claim);
        }

        public Claim? GetById(string id)
        {
            return _claims.FirstOrDefault(c => c.ClaimId == id);
        }

        public IEnumerable<Claim> GetAllClaims()
        {
            return _claims;
        }

        public IEnumerable<Claim> GetClaimsByLecturer(string lecturerId)
        {
            return _claims.Where(c => c.LecturerId == lecturerId);
        }

        public void UpdateClaim(Claim claim)
        {
            var existing = _claims.FirstOrDefault(c => c.ClaimId == claim.ClaimId);
            if (existing != null)
            {
                existing.HoursWorked = claim.HoursWorked;
                existing.HourlyRate = claim.HourlyRate;
                existing.Month = claim.Month;
                existing.Status = claim.Status;
                existing.Notes = claim.Notes;
                existing.RejectionReason = claim.RejectionReason;
                existing.DocumentPath = claim.DocumentPath;
            }
        }
    }
}
