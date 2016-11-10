using System;

namespace Markdown
{
    public static class NullableCharExtensions
    {
        public static bool IsDigitOrLetter(this char? symbol) => symbol.HasValue && char.IsLetterOrDigit(symbol.Value);

        public static bool IsWhiteSpace(this char? symbol) => symbol.HasValue && char.IsWhiteSpace(symbol.Value);
    }
}
