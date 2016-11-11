using System;
using static System.String;

namespace Markdown
{
    public static class StringExtensions
    {
        public static bool StartsWith(this string str, string searchString, int startIndex)
        {
            if (startIndex >= str.Length)
                return searchString == Empty;

            var matchLength = Math.Min(searchString.Length, str.Length - startIndex);
            return str.Substring(startIndex, matchLength) == searchString;
        }
    }
}
