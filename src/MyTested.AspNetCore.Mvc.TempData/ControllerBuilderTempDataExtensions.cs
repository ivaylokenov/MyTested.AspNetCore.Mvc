namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Controllers;
    using Builders.Contracts.Data;
    using Builders.Controllers;
    using Builders.Data;
    using Internal.TestContexts;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary"/> extension methods for <see cref="IControllerBuilder{TController}"/>.
    /// </summary>
    public static class ControllerBuilderTempDataExtensions
    {
        /// <summary>
        /// Sets initial values to the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary"/> on the tested controller.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <param name="controllerBuilder">Instance of <see cref="IControllerBuilder{TController}"/> type.</param>
        /// <param name="tempDataBuilder">Action setting the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary"/> values by using <see cref="ITempDataBuilder"/>.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        public static IAndControllerBuilder<TController> WithTempData<TController>(
            this IControllerBuilder<TController> controllerBuilder,
            Action<ITempDataBuilder> tempDataBuilder)
            where TController : class
        {
            var actualControllerBuilder = (ControllerBuilder<TController>)controllerBuilder;

            actualControllerBuilder.TestContext.ComponentPreparationAction += () =>
            {
                tempDataBuilder(new TempDataBuilder(actualControllerBuilder.TestContext.GetTempData()));
            };
            
            return actualControllerBuilder;
        }
    }
}
