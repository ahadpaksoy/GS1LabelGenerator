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
            // ARRANGE
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

        [Test]

        public void Submit_WithNullInputs_ReturnJsonError()
        {
            // ACT
            var result = _controller.Submit(null) as JsonResult;

            // ASSERT
            var jsonData = result.Data;
            var successProperty = jsonData.GetType().GetProperty("success");
            var messageProperty = jsonData.GetType().GetProperty("message");

            Assert.IsNotNull(successProperty, "The JSON data should have a 'success' property");
            Assert.IsNotNull(messageProperty, "The JSON data should have a 'message' property");

            var successValue = (bool)successProperty.GetValue(jsonData, null);
            var messageValue = messageProperty.GetValue(jsonData, null) as string;

            Assert.IsFalse(successValue, "The 'success' property should be false or null input");
            Assert.IsNotEmpty(messageValue, "An error message should be returned for null property");
        }
    }
}