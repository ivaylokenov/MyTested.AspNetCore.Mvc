namespace Blog.Web.Infrastructure
{
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public static class ViewDataDictionaryExtensions
    {
        private const string TitleKey = "Title";

        public static void SetTitle(this ViewDataDictionary viewDataDictionary, string title)
            => viewDataDictionary[TitleKey] = title;
        
        public static string GetTitle(this ViewDataDictionary viewDataDictionary)
            => viewDataDictionary[TitleKey] as string;
    }
}
