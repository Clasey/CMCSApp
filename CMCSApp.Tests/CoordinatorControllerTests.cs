using Xunit;
using CMCSApp.Controllers;
using CMCSApp.Data;
using CMCSApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CMCSApp.Tests
{
    public class CoordinatorControllerTests
    {
        [Fact]
        public void Review_ShouldReturnAViewResult_WithListOfClaims()
        {
            // Arrange
            var repo = new InMemoryRepository();
            repo.AddClaim(new Claim { Id = 1, Month = "October", HoursWorked = 5, HourlyRate = 100 });
            var controller = new CoordinatorController(repo);

            // Act
            var result = controller.Review() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Claim>>(result.Model);
        }
    }
}
