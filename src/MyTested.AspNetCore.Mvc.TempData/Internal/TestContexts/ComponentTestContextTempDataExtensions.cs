namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public static class ComponentTestContextTempDataExtensions
    {
        public static ITempDataDictionary GetTempData(this ComponentTestContext testContext)
            => testContext.ComponentAs<Controller>()?.TempData
            ?? testContext.ComponentAs<ViewComponent>()?.ViewContext?.TempData
            ?? TempDataPropertyHelper
                .GetTempDataProperties(testContext.Component.GetType())
                .TempDataGetter(testContext.Component);
    }
}
