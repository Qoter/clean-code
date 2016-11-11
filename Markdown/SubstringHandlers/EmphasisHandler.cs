using System;
using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class EmphasisHandler : BorderedTagHandler
    {
        protected override string Border { get; } = "_";
        protected override Tag Tag { get; } = Tag.Emphasis;

        protected override string HandleBeforeClosedBorder(StringReader reader)
        {
            var simpleTextHandler = new FirstWorkHandler(new EscapeHandler(), new CharHandler());
            simpleTextHandler.SetStopRule(IsOnClosedBorder);

            return simpleTextHandler.HandleSubstring(reader);
        }
    }
}