using System;

namespace Markdown.SubstringHandlers
{
    public class EmphasisHandler : BorderedTagHandler
    {
        protected override string Border { get; } = "_";
        protected override Tag Tag { get; } = Tag.Emphasis;

        public EmphasisHandler() : base(new ISubstringHandler[] {new EscapeHandler(), new CharHandler()})
        {
        }
    }
}