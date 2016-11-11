namespace Markdown.Infrastructure
{
    public static class NullableCharExtensions
    {
        public static bool IsPrintable(this char? symbol) => symbol.HasValue && !char.IsWhiteSpace(symbol.Value);

        public static bool IsWhiteSpace(this char? symbol) => symbol.HasValue && char.IsWhiteSpace(symbol.Value);
    }
}