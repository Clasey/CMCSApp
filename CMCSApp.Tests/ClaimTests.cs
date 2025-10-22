using Xunit;
using CMCSApp.Models;
using System;

namespace CMCSApp.Tests
{
    public class ClaimTests
    {
        [Fact]
        public void Claim_DefaultStatus_ShouldBePending()
        {
            // Arrange
            var claim = new Claim();

            // Assert
            Assert.Equal(ClaimStatus.Pending, claim.Status);
        }

        [Fact]
        public void Claim_ShouldStoreCorrectData()
        {
            // Arrange
            var claim = new Claim
            {
                Month = "October",
                HoursWorked = 10,
                HourlyRate = 100,
                Notes = "Guest lecture",
                UserId = 1
            };

            // Assert
            Assert.Equal("October", claim.Month);
            Assert.Equal(10, claim.HoursWorked);
            Assert.Equal(100, claim.HourlyRate);
        }
    }
}
