namespace MyTested.Mvc.Tests.Setups.Common
{
    using System;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.ViewEngines;

    public class CustomViewEngine : IViewEngine
    {
        public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            return null;
        }

        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            return null;
        }
    }
}
