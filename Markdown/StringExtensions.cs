using System;

namespace Markdown
{
    public static class StringExtensions
    {
        public static bool MatchWith(this string str, string matchValue, int startIndex=0)
        {
            var matchLength = Math.Min(matchValue.Length, str.Length - startIndex);
            return str.Substring(startIndex, matchLength) == matchValue;
        }
    }
}
