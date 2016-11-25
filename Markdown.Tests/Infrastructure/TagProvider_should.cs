using FluentAssertions;
using Markdown.Infrastructure;
using NUnit.Framework;

namespace Markdown.Tests.Infrastructure
{
    public class TagProvider_should
    {
        [Test]
        public void AddCssClass_WhenCreatedWithCssClas()
        {
            var provider = new TagProvider("test");
            var tag = provider.GetTag("p");

            tag.Wrap("content").Should().Be("<p class='test'>content</p>");
        }
    }
}