namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Options;
    using Builders.Options;

    /// <summary>
    /// Contains configuration options extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ControllerBuilderOptionsExtensions
    {
        /// <summary>
        /// Sets initial values to the configuration options on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="optionsBuilder">Action setting the configuration options by using <see cref="IOptionsBuilder"/>.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithOptions<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IOptionsBuilder> optionsBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            optionsBuilder(new OptionsBuilder(actualBuilder.TestContext));

            return actualBuilder.Builder;
        }
    }
}
