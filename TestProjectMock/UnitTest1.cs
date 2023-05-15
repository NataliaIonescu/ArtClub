using ArtClub.Controllers;
using ArtClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace TestProjectMock
{
    [TestClass]
    public class ResourcesControllerTests
    {
        [TestMethod]
        public void Index_ReturnsViewResult_WithResourceList()
        {
            // Arrange
            var resources = new List<Resource>
            {
                new Resource { Id = 1, Name = "Resource 1" },
                new Resource { Id = 2, Name = "Resource 2" }
            }.AsQueryable();

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Resources).Returns(MockDbSet(resources));

            var controller = new ResourcesController(mockContext.Object);

            // Act
            var result = controller.Index().Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Resource>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [TestMethod]
        public void Details_WithValidId_ReturnsViewResult_WithResource()
        {
            // Arrange
            var resource = new Resource { Id = 1, Name = "Resource 1" };

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Resources.FindAsync(It.IsAny<int>())).ReturnsAsync(resource);

            var controller = new ResourcesController(mockContext.Object);

            // Act
            var result = controller.Details(1).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Resource>(viewResult.ViewData.Model);
            Assert.Equal(resource, model);
        }

        [TestMethod]
        public void Create_WithValidModel_ReturnsRedirectToActionResult()
        {
            // Arrange
            var resource = new Resource { Name = "New Resource" };

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Add(It.IsAny<Resource>())).Verifiable();
          //  var unused = mockContext.Setup(c => c.SaveChangesAsync()).Returns(Task.FromResult(0));

            var controller = new ResourcesController(mockContext.Object);
            controller.ModelState.AddModelError("", "error"); // Simulate ModelState error

            // Act
            var result = controller.Create(resource).Result;

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        // Helper method to create a mocked DbSet
        private static DbSet<T> MockDbSet<T>(IEnumerable<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());
            return mockSet.Object;
        }
    }
}
