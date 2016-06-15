namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders;
    using Builders.Contracts;
    using Builders.Controllers;

    /// <summary>
    /// Contains configuration options extension methods for <see cref="IControllerBuilder{TController}"/>.
    /// </summary>
    public static class ControllerBuilderOptionsExtensions
    {
        /// <summary>
        /// Sets initial values to the configuration options on the tested controller.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <param name="controllerBuilder">Instance of <see cref="IControllerBuilder{TController}"/> type.</param>
        /// <param name="optionsBuilder">Action setting the configuration options by using <see cref="IOptionsBuilder"/>.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        public static IControllerBuilder<TController> WithOptions<TController>(
            this IControllerBuilder<TController> controllerBuilder,
            Action<IOptionsBuilder> optionsBuilder)
            where TController : class
        {
            var actualControllerBuilder = (ControllerBuilder<TController>)controllerBuilder;

            optionsBuilder(new OptionsBuilder(actualControllerBuilder.TestContext));

            return actualControllerBuilder;
        }
    }
}
