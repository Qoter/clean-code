using System;
using FluentAssertions;
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
        public void MatchWith_WithoutStrartIndex_ReturnSameAsStartsWithResult(string str, string substring)
        {
            str.MatchWith(substring).Should().Be(str.StartsWith(substring));
        }

        [TestCase("01234", "23", 2)]
        [TestCase("01234", "4", 4)]
        [TestCase("01234", "", 3)]
        public void MatchWith_RealyMatch_ReturnTrue(string str, string substrin, int startIndex)
        {
            str.MatchWith(substrin, startIndex).Should().BeTrue();
        }

        [TestCase("01234", "123", 2)]
        [TestCase("01234", "123", 4)]
        [TestCase("01234", "123", 5)]
        public void MatchWith_NotMatch_ReturnFalse(string str, string substrin, int startIndex)
        {
            str.MatchWith(substrin, startIndex).Should().BeFalse();
        }

        [Test]
        public void MatchWith_StartIndexGreaterThenLength_ThrowArgumentOutOfRange()
        {
            Action tryMatch = () => "123".MatchWith("123", 4);

            tryMatch.ShouldThrow<ArgumentOutOfRangeException>();
        }
    }
}
