namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public static class ControllerTestContextViewFeaturesExtensions
    {
        internal static TController ControllerAs<TController>(this ControllerTestContext controllerTestContext)
            where TController : class => controllerTestContext.Controller as TController;

        public static ITempDataDictionary GetTempData(this ControllerTestContext controllerTestContext)
            => controllerTestContext.ControllerAs<Controller>()?.TempData
            ?? ViewFeaturesControllerPropertyHelper
                .GetViewFeatureProperties(controllerTestContext.Controller.GetType())
                .TempDataGetter(controllerTestContext.Controller);

        public static ViewDataDictionary GetViewData(this ControllerTestContext controllerTestContext)
            => controllerTestContext.ControllerAs<Controller>()?.ViewData
            ?? ViewFeaturesControllerPropertyHelper
                .GetViewFeatureProperties(controllerTestContext.Controller.GetType())
                .ViewDataGetter(controllerTestContext.Controller);
    }
}
