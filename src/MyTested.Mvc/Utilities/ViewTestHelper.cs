namespace MyTested.Mvc.Utilities
{
    public static class ViewTestHelper
    {
        public static string GetFriendlyViewName(string viewName)
        {
            return viewName == null ? "the default one" : $"'{viewName}'";
        }
    }
}
