namespace MyTested.Mvc.Internal
{
    /// <summary>
    /// Class containing helper logic for view test builders.
    /// </summary>
    public static class ViewTestHelper
    {
        /// <summary>
        /// Gets formatted friendly view name.
        /// </summary>
        /// <param name="viewName">View name to format.</param>
        /// <returns>Formatted string.</returns>
        public static string GetFriendlyViewName(string viewName)
        {
            return viewName == null ? "the default one" : $"'{viewName}'";
        }
    }
}
