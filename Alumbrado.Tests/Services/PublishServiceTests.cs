using Alumbrado.BLL.Abstracts;
using Alumbrado.BLL.Models;
using Alumbrado.BLL.Services;
using FluentAssertions;
using System.Text.Json;

namespace Alumbrado.Tests.Services
{
    public class PublishServiceTests
    {
        private IPublishService _PublishService;

        [OneTimeSetUp]
        public void Setup()
        {
            _PublishService = new PublishService();
        }

        #region LoadReadings

        [Test]
        [TestCase("", "Fuente de datos vacía")]
        [TestCase("[Invalid JSON}", "Error al parsear JSON invalido")]
        [TestCase("{}", "Error al parsear JSON invalido")]
        public void When_LoadReadings_ThrowsExeption(string source, string message)
        {
            // Arrange, Act & Assert
            _PublishService.Invoking(p => p.LoadReadings(source))
                .Should()
                .Throw<Exception>()
                .WithMessage(message);
        }

        [Test]
        public void When_LoadReadings_IsSuccessful()
        {
            // Arrange
            var readings = new Reading[] {
                GetValidReading(),
                GetInvalidReading()
            };
            var source = SerializeReadings(readings);

            // Act
            var result = _PublishService.LoadReadings(source);

            // Assert
            result.Should()
                .NotBeNull()
                .And
                .HaveCount(2);
        }

        #endregion

        #region TBD

        [Test]
        public void When_ValidateReadings_IsInvalid()
        {
            // Arrange
            var readings = new Reading[] {
                GetInvalidReading()
            };

            // Act
            var isValid = _PublishService.ValidateReadings(readings);

            // Assert
            isValid.Should().BeFalse();
        }

        [Test]
        public void When_ValidateReadings_IsValid()
        {
            // Arrange
            var readings = new Reading[] {
                GetValidReading()
            };

            // Act
            var isValid = _PublishService.ValidateReadings(readings);

            // Assert
            isValid.Should().BeTrue();
        }

        #endregion

        #region Private Methods

        private string SerializeReadings(Reading[] readings)
        {            
            return JsonSerializer.Serialize(readings);
        }

        private static Reading GetValidReading()
        {
            return new Reading
            {
                Consumo = 100f
            };
        }

        private static Reading GetInvalidReading()
        {
            return new Reading{
                Consumo = 0f
            };
        }

        #endregion
    }
}