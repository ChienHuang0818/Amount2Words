using Xunit;
using FluentAssertions;
using NumberToWordsApp.Models;

namespace NumberToWordsApp.Tests.UnitTests
{
    public class NumberToWordsConverterTests
    {
        [Theory]
        [InlineData(0, "ZERO DOLLARS")]
        [InlineData(1, "ONE DOLLAR")]
        [InlineData(2, "TWO DOLLARS")]
        [InlineData(10, "TEN DOLLARS")]
        [InlineData(15, "FIFTEEN DOLLARS")]
        [InlineData(20, "TWENTY DOLLARS")]
        [InlineData(25, "TWENTY-FIVE DOLLARS")]
        [InlineData(100, "ONE HUNDRED DOLLARS")]
        [InlineData(101, "ONE HUNDRED AND ONE DOLLARS")]
        [InlineData(1000, "ONE THOUSAND DOLLARS")]
        [InlineData(1001, "ONE THOUSAND AND ONE DOLLARS")]
        [InlineData(1000000, "ONE MILLION DOLLARS")]
        [InlineData(1234567, "ONE MILLION TWO HUNDRED AND THIRTY-FOUR THOUSAND FIVE HUNDRED AND SIXTY-SEVEN DOLLARS")]
        public void Convert_WholeNumbers_ShouldReturnCorrectWords(decimal input, string expected)
        {
            // Act
            var result = NumberToWordsConverter.Convert(input);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(0.01, "ZERO DOLLARS AND ONE CENT")]
        [InlineData(0.99, "ZERO DOLLARS AND NINETY-NINE CENTS")]
        [InlineData(1.50, "ONE DOLLAR AND FIFTY CENTS")]
        [InlineData(10.25, "TEN DOLLARS AND TWENTY-FIVE CENTS")]
        [InlineData(100.01, "ONE HUNDRED DOLLARS AND ONE CENT")]
        [InlineData(1234.56, "ONE THOUSAND TWO HUNDRED AND THIRTY-FOUR DOLLARS AND FIFTY-SIX CENTS")]
        public void Convert_DecimalNumbers_ShouldReturnCorrectWords(decimal input, string expected)
        {
            // Act
            var result = NumberToWordsConverter.Convert(input);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-1, "MINUS ONE DOLLAR")]
        [InlineData(-10.50, "MINUS TEN DOLLARS AND FIFTY CENTS")]
        [InlineData(-100.25, "MINUS ONE HUNDRED DOLLARS AND TWENTY-FIVE CENTS")]
        public void Convert_NegativeNumbers_ShouldReturnCorrectWords(decimal input, string expected)
        {
            // Act
            var result = NumberToWordsConverter.Convert(input);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(0.00, "ZERO DOLLARS")]
        [InlineData(1.00, "ONE DOLLAR")]
        [InlineData(100.00, "ONE HUNDRED DOLLARS")]
        public void Convert_WholeDollars_ShouldNotIncludeCents(decimal input, string expected)
        {
            // Act
            var result = NumberToWordsConverter.Convert(input);

            // Assert
            result.Should().Be(expected);
            result.Should().NotContain("CENT");
        }

        [Theory]
        [InlineData(0.01, "ZERO DOLLARS AND ONE CENT")]
        [InlineData(0.02, "ZERO DOLLARS AND TWO CENTS")]
        [InlineData(1.01, "ONE DOLLAR AND ONE CENT")]
        [InlineData(1.02, "ONE DOLLAR AND TWO CENTS")]
        public void Convert_SingleCent_ShouldUseSingularForm(decimal input, string expected)
        {
            // Act
            var result = NumberToWordsConverter.Convert(input);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(0.99, "ZERO DOLLARS AND NINETY-NINE CENTS")]
        [InlineData(1.50, "ONE DOLLAR AND FIFTY CENTS")]
        [InlineData(10.25, "TEN DOLLARS AND TWENTY-FIVE CENTS")]
        public void Convert_MultipleCents_ShouldUsePluralForm(decimal input, string expected)
        {
            // Act
            var result = NumberToWordsConverter.Convert(input);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(1, "ONE DOLLAR")]
        [InlineData(1.00, "ONE DOLLAR")]
        [InlineData(1.01, "ONE DOLLAR AND ONE CENT")]
        public void Convert_SingleDollar_ShouldUseSingularForm(decimal input, string expected)
        {
            // Act
            var result = NumberToWordsConverter.Convert(input);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(2, "TWO DOLLARS")]
        [InlineData(2.00, "TWO DOLLARS")]
        [InlineData(2.01, "TWO DOLLARS AND ONE CENT")]
        public void Convert_MultipleDollars_ShouldUsePluralForm(decimal input, string expected)
        {
            // Act
            var result = NumberToWordsConverter.Convert(input);

            // Assert
            result.Should().Be(expected);
        }
    }
}
