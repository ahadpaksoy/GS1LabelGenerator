using NUnit.Framework;
using gs1BarcodeApplication.Helpers;
using gs1BarcodeApplication.Models;

namespace gs1BarcodeApplication.Tests.Helpers
{
    [TestFixture] 
    public class Gs1BuilderTests
    {
        [Test] 
        public void BuildGs1Data_WithGtinAndBatch_ReturnsCorrectlyFormattedString()
        {
            // ARRANGE
            var model = new ProductLabelModel
            {
                GTIN = "01234567890123",
                batch_lotNumber = "LOT123"
            };

            // ACT
            var result = Gs1Builder.BuildGs1Data(model);

            // ASSERT
            var expectedString = "(01)01234567890123(10)LOT123";
            Assert.AreEqual(expectedString, result);
        }

        [Test]
        public void BuildGs1Data_WithOnlySerialNumber_ReturnsOnlySerialNumberPart()
        {
            // ARRANGE
            var model = new ProductLabelModel
            {
                serialNumber = "SN98765"
            };

            // ACT
            var result = Gs1Builder.BuildGs1Data(model);

            // ASSERT
            Assert.AreEqual("(21)SN98765", result);
        }

        [Test]
        public void BuildGs1Data_WithNullAndEmptyProperties_IgnoresThem()
        {
            // ARRANGE
            var model = new ProductLabelModel
            {
                GTIN = "01234567890123",
                batch_lotNumber = "",
                serialNumber = null,
                expirationDate = "251231"
            };

            // ACT
            var result = Gs1Builder.BuildGs1Data(model);

            // ASSERT
            var expectedString = "(01)01234567890123(17)251231";
            Assert.AreEqual(expectedString, result);
        }

        [Test]
        public void BuildGs1Data_WithNoPropertiesSet_ReturnsEmptyString()
        {
            // ARRANGE
            var model = new ProductLabelModel();

            // ACT
            var result = Gs1Builder.BuildGs1Data(model);

            // ASSERT
            Assert.AreEqual(string.Empty, result);
        }
    }
}