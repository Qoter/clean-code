using System;
using FluentAssertions;
using Markdown.Infrastructure;
using NUnit.Framework;

namespace Markdown.Tests
{
    public class StringExtensionsTests
    {
        [TestCase("", "")]
        [TestCase("", "1")]
        [TestCase("1", "")]
        [TestCase("12", "123")]
        [TestCase("123", "12")]
        [TestCase("123", "123")]
        public void StartsWith_ZeroStartIndex_ReturnSameAsStartsWithResult(string str, string substring)
        {
            StringExtensions.StartsWith(str, substring, 0).Should().Be(str.StartsWith(substring));
        }

        [TestCase("01234", "23", 2)]
        [TestCase("01234", "4", 4)]
        [TestCase("01234", "", 3)]
        public void StartsWith_RealyStart_ReturnTrue(string str, string substrin, int startIndex)
        {
            str.StartsWith(substrin, startIndex).Should().BeTrue();
        }

        [TestCase("01234", "123", 2)]
        [TestCase("01234", "123", 4)]
        [TestCase("01234", "123", 5)]
        public void StartsWith_NotStart_ReturnFalse(string str, string substrin, int startIndex)
        {
            str.StartsWith(substrin, startIndex).Should().BeFalse();
        }

        [Test]
        public void StartsWith_IndexGreaterThenLengthAndEmptyString_ReturnTrue()
        {
            var searchResult = "123".StartsWith(string.Empty, 4);

            searchResult.Should().BeTrue();
        }

        [Test]
        public void StartsWith_IndexGreaterLengthAndStringNotEmpty_ReturnFalse()
        {
            var serachResult = "123".StartsWith("abc", 4);

            serachResult.Should().BeFalse();
        }
    }
}
