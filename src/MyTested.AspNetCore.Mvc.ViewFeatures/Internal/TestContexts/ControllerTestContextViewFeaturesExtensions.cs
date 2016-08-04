namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public static class ControllerTestContextViewFeaturesExtensions
    {
        public static ITempDataDictionary GetTempData(this ComponentTestContext testContext)
            => testContext.ComponentAs<Controller>()?.TempData
            ?? ViewFeaturesControllerPropertyHelper
                .GetViewFeatureProperties(testContext.Component.GetType())
                .TempDataGetter(testContext.Component);

        public static ViewDataDictionary GetViewData(this ComponentTestContext testContext)
            => testContext.ComponentAs<Controller>()?.ViewData
            ?? ViewFeaturesControllerPropertyHelper
                .GetViewFeatureProperties(testContext.Component.GetType())
                .ViewDataGetter(testContext.Component);
    }
}
