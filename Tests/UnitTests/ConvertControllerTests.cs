using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NumberToWordsApp.Controllers;
using NumberToWordsApp.Models;

namespace NumberToWordsApp.Tests.UnitTests
{
    public class ConvertControllerTests
    {
        private readonly ConvertController _controller;

        public ConvertControllerTests()
        {
            _controller = new ConvertController();
        }

        [Fact]
        public void Index_Get_ShouldReturnViewWithEmptyModel()
        {
            // Act
            var result = _controller.Index();

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;
            viewResult!.Model.Should().BeOfType<ConvertViewModel>();
            
            var model = viewResult.Model as ConvertViewModel;
            model!.NumberInput.Should().BeNull();
            model.Result.Should().BeNull();
            model.HasError.Should().BeFalse();
            model.ErrorMessage.Should().BeNull();
        }

        [Theory]
        [InlineData("123", "ONE HUNDRED AND TWENTY-THREE DOLLARS")]
        [InlineData("123.45", "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS")]
        [InlineData("0", "ZERO DOLLARS")]
        [InlineData("0.01", "ZERO DOLLARS AND ONE CENT")]
        [InlineData("-100", "MINUS ONE HUNDRED DOLLARS")]
        public void Index_Post_WithValidNumber_ShouldReturnCorrectResult(string input, string expectedResult)
        {
            // Arrange
            var model = new ConvertViewModel { NumberInput = input };

            // Act
            var result = _controller.Index(model);

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;
            viewResult!.Model.Should().BeOfType<ConvertViewModel>();
            
            var resultModel = viewResult.Model as ConvertViewModel;
            resultModel!.HasError.Should().BeFalse();
            resultModel.ErrorMessage.Should().BeNull();
            resultModel.Result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Index_Post_WithEmptyInput_ShouldReturnError(string? input)
        {
            // Arrange
            var model = new ConvertViewModel { NumberInput = input };

            // Act
            var result = _controller.Index(model);

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;
            viewResult!.Model.Should().BeOfType<ConvertViewModel>();
            
            var resultModel = viewResult.Model as ConvertViewModel;
            resultModel!.HasError.Should().BeTrue();
            resultModel.ErrorMessage.Should().Be("please enter a valid number");
            resultModel.Result.Should().Be("");
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("12.34.56")]
        [InlineData("not a number")]
        [InlineData("123abc")]
        public void Index_Post_WithInvalidNumber_ShouldReturnError(string input)
        {
            // Arrange
            var model = new ConvertViewModel { NumberInput = input };

            // Act
            var result = _controller.Index(model);

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;
            viewResult!.Model.Should().BeOfType<ConvertViewModel>();
            
            var resultModel = viewResult.Model as ConvertViewModel;
            resultModel!.HasError.Should().BeTrue();
            resultModel.ErrorMessage.Should().Be("please enter a valid number");
            resultModel.Result.Should().Be("");
        }

        [Fact]
        public void Index_Post_WithValidDecimal_ShouldHandleDecimalCorrectly()
        {
            // Arrange
            var model = new ConvertViewModel { NumberInput = "123.456" };

            // Act
            var result = _controller.Index(model);

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;
            viewResult!.Model.Should().BeOfType<ConvertViewModel>();
            
            var resultModel = viewResult.Model as ConvertViewModel;
            resultModel!.HasError.Should().BeFalse();
            resultModel.Result.Should().Be("ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS");
        }

        [Fact]
        public void Index_Post_WithLargeNumber_ShouldHandleLargeNumbers()
        {
            // Arrange
            var model = new ConvertViewModel { NumberInput = "1000000" };

            // Act
            var result = _controller.Index(model);

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;
            viewResult!.Model.Should().BeOfType<ConvertViewModel>();
            
            var resultModel = viewResult.Model as ConvertViewModel;
            resultModel!.HasError.Should().BeFalse();
            resultModel.Result.Should().Be("ONE MILLION DOLLARS");
        }
    }
}
