namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using System;
    using System.Text;

    public static class EncodingExtensions
    {
        public static string GetName(this Encoding encoding)
        {
            if (encoding == null)
            {
                return "null";
            }

            var fullEncodingName = encoding.ToString();
            var lastIndexOfDot = fullEncodingName.LastIndexOf(".", StringComparison.Ordinal);
            return fullEncodingName.Substring(lastIndexOfDot + 1).Replace("Encoding", string.Empty);
        }
    }
}
