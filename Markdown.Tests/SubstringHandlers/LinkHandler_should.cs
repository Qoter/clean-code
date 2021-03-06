﻿using System;
using FluentAssertions;
using Markdown.Infrastructure;
using Markdown.SubstringHandlers;
using NUnit.Framework;

namespace Markdown.Tests.SubstringHandlers
{
    public class LinkHandler_should
    {
        [TestCase("[hello](url)", ExpectedResult = "<a href='url'>hello</a>", TestName = "Simple link")]
        [TestCase("[_hello_](url)", ExpectedResult = "<a href='url'><em>hello</em></a>", TestName = "Title with emphasis")]
        [TestCase("[__hello__](url)", ExpectedResult = "<a href='url'><strong>hello</strong></a>",TestName = "Title with strong")]
        [TestCase("[[hello]](url)", ExpectedResult = "<a href='url'>[hello]</a>", TestName = "Title with square brace inside")]
        [TestCase("[hello](url(()()))", ExpectedResult = "<a href='url(()())'>hello</a>", TestName = "Url with circular brace inside")]
        [TestCase("[hello]()", ExpectedResult = "<a href=''>hello</a>", TestName = "Empty url")]
        [TestCase("[](url)", ExpectedResult = "<a href='url'></a>", TestName = "Empty title")]
        [TestCase("[a  \r\nb](abc)", ExpectedResult = "<a href='abc'>a<br />b</a>", TestName = "Line break inside link name")]
        public string HandleLink(string str)
        {
            var reader = new StringReader(str);
            var linkHandler = new LinkHandler(MdSettings.Default);

            return linkHandler.HandleSubstring(reader);
        }


        [Test]
        public void SupportBaseUrl()
        {
            var reader = new StringReader("[hello](index.html)");
            var linkHandelr = new LinkHandler(new MdSettings(new Uri("http://hello")));

            linkHandelr.HandleSubstring(reader).Should().Be("<a href='http://hello/index.html'>hello</a>");
        }
    }
}