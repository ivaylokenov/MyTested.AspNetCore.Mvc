namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Controllers;
    using Builders.Controllers;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> extension methods for <see cref="IControllerBuilder{TController}"/>.
    /// </summary>
    public static class ControllerBuilderDataAnnotationsExtensions
    {
        /// <summary>
        /// Disables <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> validation for the action call.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <param name="controllerBuilder">Instance of <see cref="IControllerBuilder{TController}"/> type.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        public static IAndControllerBuilder<TController> WithoutValidation<TController>(
            this IControllerBuilder<TController> controllerBuilder)
            where TController : class
        {
            var actualControllerBuilder = (ControllerBuilder<TController>)controllerBuilder;

            actualControllerBuilder.EnabledValidation = false;

            return actualControllerBuilder;
        }
    }
}
