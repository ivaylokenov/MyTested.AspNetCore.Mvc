namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Base;
    using Builders.Contracts.Controllers;
    using Builders.Controllers;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>
    /// extension methods for <see cref="IControllerBuilder{TController}"/>.
    /// </summary>
    public static class ControllerBuilderDataAnnotationsExtensions
    {
        /// <summary>
        /// Disables <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> validation for the action call.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="controllerBuilder">Instance of <see cref="IControllerBuilder{TController}"/> type.</param>
        /// <returns>The same controller builder.</returns>
        public static TBuilder WithoutValidation<TController, TBuilder>(
            this IBaseControllerBuilder<TController, TBuilder> controllerBuilder)
            where TController : class
            where TBuilder : IBaseTestBuilder
        {
            var actualControllerBuilder = (BaseControllerBuilder<TController, TBuilder>)controllerBuilder;

            actualControllerBuilder.EnabledModelStateValidation = false;

            return actualControllerBuilder.Builder;
        }
    }
}
