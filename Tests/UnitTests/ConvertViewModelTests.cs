using Xunit;
using FluentAssertions;
using NumberToWordsApp.Models;

namespace NumberToWordsApp.Tests.UnitTests
{
    public class ConvertViewModelTests
    {
        [Fact]
        public void ConvertViewModel_DefaultConstructor_ShouldInitializeWithDefaultValues()
        {
            // Act
            var model = new ConvertViewModel();

            // Assert
            model.NumberInput.Should().BeNull();
            model.Result.Should().BeNull();
            model.HasError.Should().BeFalse();
            model.ErrorMessage.Should().BeNull();
            model.RequestId.Should().BeNull();
        }

        [Fact]
        public void ConvertViewModel_Properties_ShouldBeSettable()
        {
            // Arrange
            var model = new ConvertViewModel();

            // Act
            model.NumberInput = "123.45";
            model.Result = "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS";
            model.HasError = true;
            model.ErrorMessage = "Test error message";
            model.RequestId = "test-request-id";

            // Assert
            model.NumberInput.Should().Be("123.45");
            model.Result.Should().Be("ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS");
            model.HasError.Should().BeTrue();
            model.ErrorMessage.Should().Be("Test error message");
            model.RequestId.Should().Be("test-request-id");
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void ConvertViewModel_NumberInput_ShouldAcceptNullOrEmptyValues(string input)
        {
            // Arrange
            var model = new ConvertViewModel();

            // Act
            model.NumberInput = input;

            // Assert
            model.NumberInput.Should().Be(input);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("123.45")]
        [InlineData("-123.45")]
        [InlineData("0")]
        [InlineData("0.01")]
        public void ConvertViewModel_NumberInput_ShouldAcceptValidNumberStrings(string input)
        {
            // Arrange
            var model = new ConvertViewModel();

            // Act
            model.NumberInput = input;

            // Assert
            model.NumberInput.Should().Be(input);
        }

        [Fact]
        public void ConvertViewModel_HasError_ShouldToggleCorrectly()
        {
            // Arrange
            var model = new ConvertViewModel();

            // Act & Assert
            model.HasError.Should().BeFalse();

            model.HasError = true;
            model.HasError.Should().BeTrue();

            model.HasError = false;
            model.HasError.Should().BeFalse();
        }

        [Fact]
        public void ConvertViewModel_ErrorMessage_ShouldBeNullable()
        {
            // Arrange
            var model = new ConvertViewModel();

            // Act
            model.ErrorMessage = "Some error";
            model.ErrorMessage.Should().Be("Some error");

            model.ErrorMessage = null;
            model.ErrorMessage.Should().BeNull();
        }

        [Fact]
        public void ConvertViewModel_Result_ShouldBeNullable()
        {
            // Arrange
            var model = new ConvertViewModel();

            // Act
            model.Result = "Some result";
            model.Result.Should().Be("Some result");

            model.Result = null;
            model.Result.Should().BeNull();
        }
    }
}
