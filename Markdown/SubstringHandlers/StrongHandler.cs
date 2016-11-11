using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class StrongHandler : BorderedTagHandler
    {
        protected override string Border { get; } = "__";
        protected override Tag Tag { get; } = Tag.Strong;

        protected override string HandleBeforeClosedBorder(StringReader reader)
        {
            var simpleTextHandler = new FirstWorkHandler(new EscapeHandler(), new CharHandler());
            simpleTextHandler.SetStopRule(IsOnClosedBorder);
            var innerText = simpleTextHandler.HandleSubstring(reader);
            
            var emphasisInsideHandler = new FirstWorkHandler(new EscapeHandler(), new EmphasisHandler(), new CharHandler());
            return emphasisInsideHandler.HandleString(innerText);
        }

    }
}