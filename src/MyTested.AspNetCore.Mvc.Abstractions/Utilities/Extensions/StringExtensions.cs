namespace MyTested.AspNetCore.Mvc.Abstractions.Utilities.Extensions
{
    using System.Globalization;

    public static class StringExtensions
    {
        public static string CapitalizeAndJoin(this string text)
        {
            if (text == null)
            {
                return text;
            }

            var capitalizedTextParts = CultureInfo
                .CurrentCulture
                .TextInfo
                .ToTitleCase(text)
                .Split();

            return string.Join(string.Empty, capitalizedTextParts);
        }
    }
}
