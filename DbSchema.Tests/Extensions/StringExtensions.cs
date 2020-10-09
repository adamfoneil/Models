using System.Text.RegularExpressions;

namespace DbSchema.Tests
{
    public static class StringExtensions
    {
        public static string ReplaceWhitespace(this string input)
        {
            return Regex.Replace(input, @"\s+", " ");
        }
    }
}
