using System;
using System.Linq;
using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class HeaderHandler : ISubstringHandler
    {
        private readonly MdSettings settings;
        private readonly ISubstringHandler innerHandler;
        private readonly string[] headerSequences;

        public HeaderHandler(MdSettings settings)
        {
            this.settings = settings;
            headerSequences = Enumerable
                .Range(1, 6)
                .Select(size => Enumerable.Repeat('#', size))
                .Select(headerSequence => string.Join("", headerSequence))
                .OrderByDescending(headerSequence => headerSequence.Length)
                .ToArray();

            innerHandler = new FirstWorkHandler(
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

            var currentHeaderSequence = headerSequences.First(reader.IsLocatedOn);

            reader.Read(currentHeaderSequence.Length);
            var headerText = new FirstWorkHandler(new EscapeSkipHandler(), new CharHandler()).HandleUntil(r => r.IsLocatedOn("\r\n"), reader);
            var processedHeaderText = innerHandler.HandleUntil(r => r.AtEndOfString, new StringReader(headerText));

            var headerSize = currentHeaderSequence.Length;
            var headerTag = settings.TagProvider.GetTag($"h{headerSize}");

            return headerTag.Wrap(processedHeaderText);
        }

        public bool CanHandle(StringReader reader)
        {
            var headerSequence = headerSequences.FirstOrDefault(reader.IsLocatedOn);
            if (headerSequence == null)
                return false;

            var context = reader.GetContext(headerSequence);
            return context.Left == "" || context.Left.Length > 2 && (context.Left.Substring(context.Left.Length - 2) == "\r\n");
        }
    }
}