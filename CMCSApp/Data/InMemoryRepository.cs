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
            _users.Add(new User { Username = "lect1", Password = "password", Role = "Lecturer", DisplayName = "Lecturer One" });
            _users.Add(new User { Username = "coord1", Password = "password", Role = "Coordinator", DisplayName = "Coordinator One" });
            _users.Add(new User { Username = "mgr1", Password = "password", Role = "Manager", DisplayName = "Manager One" });
        }

        // Users
        public User? ValidateUser(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public User? GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
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
