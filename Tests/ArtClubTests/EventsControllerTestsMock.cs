using ArtClub.Controllers;
using ArtClub.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Language.Flow;
using Microsoft.EntityFrameworkCore;
namespace ArtClubTests
{
    [TestClass]
    public class EventsControllerTestsMock
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly EventsController _controller;

        public EventsControllerTestsMock()
            : this(new AppDbContext())
        {
        }

        public EventsControllerTestsMock(AppDbContext dbContext)
        {
            _mockContext = new Mock<AppDbContext>(dbContext);
            _controller = new EventsController(_mockContext.Object);
        }


        [TestMethod]
        public async Task Create_ValidEvent_ReturnsRedirectToIndex()
        {
            // Arrange
            var validEvent = new Event
            {
                StartDate = new DateTime(2023, 6, 1),
                EndDate = new DateTime(2023, 6, 3),
                Description = "Test event",
                ResourceId = 1
            };
            _mockContext.Setup(c => c.Events.AnyAsync(e =>
                e.StartDate == validEvent.StartDate &&
                e.EndDate == validEvent.EndDate &&
                e.ResourceId == validEvent.ResourceId,
                CancellationToken.None)).ReturnsAsync(false);

            // Act
            var result = await _controller.Create(validEvent) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public async Task Create_InvalidEvent_ReturnsViewWithModelError()
        {
            // Arrange
            var invalidEvent = new Event
            {
                StartDate = new DateTime(2023, 6, 1),
                EndDate = new DateTime(2023, 6, 3),
                Description = "Test event",
                ResourceId = 1
            };
            _mockContext.Setup(c => c.Events.AnyAsync(e =>
                e.StartDate == invalidEvent.StartDate &&
                e.EndDate == invalidEvent.EndDate &&
                e.ResourceId == invalidEvent.ResourceId,
                It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Create(invalidEvent) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.ViewData.ModelState.IsValid);
            Assert.IsTrue(result.ViewData.ModelState.ContainsKey(""));
            Assert.AreEqual("An event with the same StartDate, EndDate and ResourceId already exists.", result.ViewData.ModelState[""].Errors[0].ErrorMessage);
        }
  
        [TestMethod]
        public async Task Edit_ValidEvent_ReturnsRedirectToIndex()
        {
            // Arrange
            var existingEvent = new Event
            {
                Id = 1,
                StartDate = new DateTime(2023, 6, 1),
                EndDate = new DateTime(2023, 6, 3),
                Description = "Test event",
                ResourceId = 1
            };
            _mockContext.Setup(c => c.Events.FindAsync(existingEvent.Id))
                .ReturnsAsync(existingEvent);

            var updatedEvent = new Event
            {
                Id = 1,
                StartDate = new DateTime(2023, 6, 4),
                EndDate = new DateTime(2023, 6, 6),
                Description = "Updated event",
                ResourceId = 1
            };

            _mockContext.Setup(c => c.Events.AnyAsync(e =>
                e.StartDate == updatedEvent.StartDate &&
                e.EndDate == updatedEvent.EndDate &&
                e.ResourceId == updatedEvent.ResourceId &&
                e.Id != updatedEvent.Id,
                It.IsAny<CancellationToken>())).ReturnsAsync(false);

            // Act
            var result = await _controller.Edit(updatedEvent.Id, updatedEvent) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }


        [TestMethod]
        public async Task Delete_ValidEvent_ReturnsRedirectToIndex()
        {
            // Arrange
            var existingEvent = new Event
            {
                Id = 1,
                StartDate = new DateTime(2023, 6, 1),
                EndDate = new DateTime(2023, 6, 3),
                Description = "Test event",
                ResourceId = 1
            };
            _mockContext.Setup(c => c.Events.FindAsync(existingEvent.Id))
                .ReturnsAsync(existingEvent);

            // Act
            var result = await _controller.DeleteConfirmed(existingEvent.Id) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public async Task Delete_InvalidEvent_ReturnsNotFound()
        {
            // Arrange
            int nonExistingEventId = 99;
            _mockContext.Setup(c => c.Events.FindAsync(nonExistingEventId))
                .ReturnsAsync((Event)null);

            // Act
            var result = await _controller.DeleteConfirmed(nonExistingEventId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }




    }
}


