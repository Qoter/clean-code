﻿using Markdown.Infrastructure;
using Markdown.SubstringHandlers;
using NUnit.Framework;

namespace Markdown.Tests.SubstringHandlers
{
    class LinkHandler_should
    {
        [TestCase("[hello](url)", ExpectedResult = "<a src='url'>hello</a>", TestName = "Simple link")]
        [TestCase("[_hello_](url)", ExpectedResult = "<a src='url'><em>hello</em></a>", TestName = "Title with emphasis")]
        [TestCase("[__hello__](url)", ExpectedResult = "<a src='url'><strong>hello</strong></a>", TestName = "Title with strong")]
        [TestCase("[[hello]](url)", ExpectedResult = "<a src='url'>[hello]</a>", TestName = "Title with square brace inside")]
        [TestCase("[hello](url(()()))", ExpectedResult = "<a src='url(()())'>hello</a>", TestName = "Url with circular brace inside")]
        [TestCase("[hello]()", ExpectedResult = "<a src=''>hello</a>", TestName = "Empty url")]
        [TestCase("[](url)", ExpectedResult = "<a src='url'></a>", TestName = "Empty title")]
        public string HandleLink(string str)
        {
            var reader = new StringReader(str);
            var linkHandler = new LinkHandler();

            return linkHandler.HandleSubstring(reader);
        }
    }
}
