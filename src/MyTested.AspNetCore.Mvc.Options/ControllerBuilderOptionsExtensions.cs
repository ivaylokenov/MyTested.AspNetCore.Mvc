namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders;
    using Builders.Contracts;
    using Builders.Controllers;

    public static class ControllerBuilderOptionsExtensions
    {
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
