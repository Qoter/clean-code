using System;

namespace Markdown.SubstringHandlers
{
    public class EmphasisHandler : ISubstringHandler
    {
        private readonly HandlerCombiner innerTextHandler;

        public EmphasisHandler()
        {
            innerTextHandler = new HandlerCombiner(new EscapeHandler(), new CharHandler());
            innerTextHandler.SetStopRule(reader => reader.OnCharacter('_') && !reader.InsideWord && !reader.PreviousChar.IsWhiteSpace());
        }

        public string HandleSubstring(StringReader reader)
        {
            if (!CanHandle(reader))
                throw new InvalidOperationException("Can't read emphasis substring");

            reader.Read("_".Length);
            var innerText = innerTextHandler.HandleSubstring(reader);
            reader.Read("_".Length);

            return Tag.Emphasis.Wrap(innerText);
        }

        public bool CanHandle(StringReader reader)
        {
            return reader.OnCharacter('_') && !reader.NextChar.IsWhiteSpace() && !reader.InsideWord;
        }
    }
}