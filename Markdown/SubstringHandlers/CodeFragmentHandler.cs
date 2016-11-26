using System;
using System.Collections.Generic;
using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class CodeFragmentHandler : ISubstringHandler
    {
        private readonly MdSettings settings;

        public CodeFragmentHandler(MdSettings settings)
        {
            this.settings = settings;
        }

        public string HandleSubstring(StringReader reader)
        {
            if (!CanHandle(reader))
                throw new ArgumentException();

            var codeLines = new List<string>();
            while (CanHandle(reader))
            {
                codeLines.Add(reader.ReadLine().Substring("    ".Length));
            }

            var preTag = settings.TagProvider.GetTag("pre");
            var codeTag = settings.TagProvider.GetTag("code");

            return preTag.Wrap(codeTag.Wrap(string.Join("", codeLines)));
        }

        public bool CanHandle(StringReader reader)
        {
            return reader.AtStartOfLine && reader.IsLocatedOn("    ");
        }
    }
}