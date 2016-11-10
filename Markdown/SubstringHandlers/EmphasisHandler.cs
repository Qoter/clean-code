using System;

namespace Markdown.SubstringHandlers
{
    public class EmphasisHandler : ISubstringHandler
    {
        private readonly HandlerCombiner innerTextHandler;

        public EmphasisHandler()
        {
            innerTextHandler = new HandlerCombiner(new EscapeHandler(), new CharHandler());
            innerTextHandler.SetStopRule(IsEndOfEmphasis);
        }

        public string HandleSubstring(StringReader reader)
        {
            if (!CanHandle(reader))
                throw new InvalidOperationException("Can't read emphasis substring");

            reader.Read("_".Length);
            var innerText = innerTextHandler.HandleSubstring(reader);

            if (!IsEndOfEmphasis(reader))
                return "_" + innerText;

            reader.Read("_".Length);
            return Tag.Emphasis.Wrap(innerText);
        }

        private static bool IsEndOfEmphasis(StringReader reader)
        {
            return reader.OnCharacter('_') && !reader.InsideWord && !reader.PreviousChar.IsWhiteSpace();
        }

        public bool CanHandle(StringReader reader)
        {
            return reader.OnCharacter('_') && !reader.NextChar.IsWhiteSpace() && !reader.InsideWord;
        }
    }
}