using Markdown.SubstringReaders;
using NUnit.Framework;

namespace Markdown.Tests.SubstringReaders
{
    [TestFixture]
    public class EmphasisReader_should
    {
        [TestCase("_text_", ExpectedResult = "<em>text</em>")]
        public string ReadTextInsideUnderscore(string str)
        {
            var tokenizer = new Tokenizer(str);
            var emphasisReader = new EmphasisReader();

            return emphasisReader.ReadSubstring(tokenizer);
        }
    }
}
