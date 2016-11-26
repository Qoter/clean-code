using System;
using System.Collections.Generic;
using System.Linq;
using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class NumberedListHandler : ISubstringHandler
    {
        private readonly MdSettings settings;
        private readonly ISubstringHandler listItemHandler;

        public NumberedListHandler(MdSettings settings)
        {
            this.settings = settings;
            listItemHandler = new FirstWorkHandler(
                new EscapeHandler(),
                new LinkHandler(settings),
                new StrongHandler(settings),
                new EmphasisHandler(settings),
                new CharHandler());
        }

        public string HandleSubstring(StringReader reader)
        {
            if (!CanHandle(reader))
                throw new ArgumentException();

            var listItems = new List<string>();
            while (reader.IsLocatedOn("1. "))
            {
                var item = reader.ReadLine();
                var clearItem = item.TrimEnd('\r', '\n').Substring("1. ".Length);
                var processedItem = listItemHandler.HandleUntil(r => r.AtEndOfText, new StringReader(clearItem));
                listItems.Add(processedItem);
            }

            var olTag = settings.TagProvider.GetTag("ol");
            var liTag = settings.TagProvider.GetTag("li");

            return olTag.Wrap(string.Join("", listItems.Select(liTag.Wrap)));
        }

        public bool CanHandle(StringReader reader)
        {
            return reader.AtStartOfText && reader.IsLocatedOn("1. ");
        }
    }
}
