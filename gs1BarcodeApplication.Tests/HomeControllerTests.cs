using System.Collections.Generic;
using System.Reflection;
using gs1BarcodeApplication.Controllers;
using gs1BarcodeApplication.Models;
using Xunit;
using System.Web.Mvc;

namespace gs1BarcodeApplication.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Submit_ValidInputs_ShouldMapToProductLabelModel()
        {
            // Arrange
            var controller = new HomeController();
            var inputs = new List<Gs1FieldInput>
            {
                new Gs1FieldInput { Field = "GTIN", Value = "01234567890128" },
                new Gs1FieldInput { Field = "batch_lotNumber", Value = "BATCH123" },
                new Gs1FieldInput { Field = "bestBeforeDate", Value = "250901" }
            };

            // Act
            var result = controller.Submit(inputs) as ViewResult;
            var model = result.Model as ProductLabelModel;

            // Assert
            Assert.NotNull(model);
            Assert.Equal("01234567890128", model.GTIN);
            Assert.Equal("BATCH123", model.batch_lotNumber);
            Assert.Equal("250901", model.bestBeforeDate);
            Assert.Equal("(01)01234567890128 (10)BATCH123 (15)250901", result.ViewBag.Gs1String);
        }

        [Fact]
        public void Submit_InvalidField_ShouldIgnoreUnmatchedField()
        {
            // Arrange
            var controller = new HomeController();
            var inputs = new List<Gs1FieldInput>
            {
                new Gs1FieldInput { Field = "InvalidField", Value = "Something" },
                new Gs1FieldInput { Field = "GTIN", Value = "99999999999999" }
            };

            // Act
            var result = controller.Submit(inputs) as ViewResult;
            var model = result.Model as ProductLabelModel;

            // Assert
            Assert.NotNull(model);
            Assert.Equal("99999999999999", model.GTIN);
            Assert.Null(model.batch_lotNumber);
            Assert.Contains("(01)99999999999999", result.ViewBag.Gs1String);
            Assert.DoesNotContain("InvalidField", result.ViewBag.Gs1String);
        }

        [Fact]
        public void Submit_EmptyInputs_ShouldReturnEmptyGs1String()
        {
            // Arrange
            var controller = new HomeController();
            var inputs = new List<Gs1FieldInput>();

            // Act
            var result = controller.Submit(inputs) as ViewResult;

            // Assert
            Assert.Equal("", result.ViewBag.Gs1String);
        }
    }
}
