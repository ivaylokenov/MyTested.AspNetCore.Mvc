namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public static class ControllerTestContextTempDataExtensions
    {
        public static ITempDataDictionary GetTempData(this ControllerTestContext testContext)
            => testContext.ComponentAs<Controller>()?.TempData
            ?? TempDataControllerPropertyHelper
                .GetTempDataProperties(testContext.Component.GetType())
                .TempDataGetter(testContext.Component);
    }
}
