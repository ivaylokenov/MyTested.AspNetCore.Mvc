namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public static class ControllerTestContextViewFeaturesExtensions
    {
        public static ITempDataDictionary GetTempData(this ControllerTestContext controllerTestContext)
            => controllerTestContext.ComponentAs<Controller>()?.TempData
            ?? ViewFeaturesControllerPropertyHelper
                .GetViewFeatureProperties(controllerTestContext.Component.GetType())
                .TempDataGetter(controllerTestContext.Component);

        public static ViewDataDictionary GetViewData(this ControllerTestContext controllerTestContext)
            => controllerTestContext.ComponentAs<Controller>()?.ViewData
            ?? ViewFeaturesControllerPropertyHelper
                .GetViewFeatureProperties(controllerTestContext.Component.GetType())
                .ViewDataGetter(controllerTestContext.Component);
    }
}
