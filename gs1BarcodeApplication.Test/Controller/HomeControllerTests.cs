using NUnit.Framework;
using Moq; 
using gs1BarcodeApplication.Services;
using gs1BarcodeApplication.Controllers; 
using System.Collections.Generic;
using System.Web.Mvc;

namespace gs1BarcodeApplication.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
      
        private Mock<IPresetService> _mockPresetService;
        private HomeController _controller;

        [SetUp]
        public void Setup()
        {
            
            _mockPresetService = new Mock<IPresetService>();

            
            _controller = new HomeController(_mockPresetService.Object);
        }

        [Test]
        public void Index_WhenCalled_CallsGetDefaultPresetsOnce()
        {
            // ACT
            _controller.Index();

            // ASSERT
            _mockPresetService.Verify(s => s.GetDefaultPresets(), Times.Once);
        }

        [Test]
        public void Index_WhenCalled_ReturnsViewResultWithCorrectPresetsInViewBag()
        {
            // ARRANGE: 
            var expectedPresets = new Dictionary<string, List<string>>
            {
                { "Test Preset", new List<string> { "GTIN", "serialNumber" } }
            };
            _mockPresetService.Setup(s => s.GetDefaultPresets()).Returns(expectedPresets);

            // ACT
            var result = _controller.Index() as ViewResult;

            // ASSERT
            Assert.IsNotNull(result);

            var actualPresets = result.ViewBag.Presets as Dictionary<string, List<string>>;

            Assert.IsNotNull(actualPresets);
            Assert.AreEqual(expectedPresets, actualPresets);
        }
    }
}