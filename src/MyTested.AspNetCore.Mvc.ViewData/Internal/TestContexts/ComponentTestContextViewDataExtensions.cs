namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public static class ComponentTestContextViewDataExtensions
    {
        public static ViewDataDictionary GetViewData(this ComponentTestContext testContext)
            => testContext.ComponentAs<Controller>()?.ViewData
            ?? testContext.ComponentAs<ViewComponent>()?.ViewData
            ?? ViewDataPropertyHelper
                .GetViewDataProperties(testContext.Component.GetType())
                .ViewDataGetter(testContext.Component);
    }
}
