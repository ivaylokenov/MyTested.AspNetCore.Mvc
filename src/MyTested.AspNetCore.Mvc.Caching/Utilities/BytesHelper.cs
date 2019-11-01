namespace MyTested.AspNetCore.Mvc.Utilities
{
    using System.Text;

    /// <summary>
    /// Contains helper methods that provide byte representations of types.
    /// </summary>
    public static class BytesHelper
    {
        /// <summary>
        /// Returns the byte representation of an UTF8 string.
        /// </summary>
        /// <param name="value">The string to get the byte representation of.</param>
        /// <returns></returns>
        public static byte[] GetBytes(string value) => Encoding.UTF8.GetBytes(value);
    }
}
