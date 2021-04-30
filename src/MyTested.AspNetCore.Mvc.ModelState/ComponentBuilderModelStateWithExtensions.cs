namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Models;
    using Builders.Models;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Contains extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderModelStateWithExtensions
    {
        /// <summary>
        /// Used for providing a model state to the <see cref="ModelStateDictionary"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="modelStateTestBuilder">
        /// Action setting the <see cref="WithModelStateBuilder"/> by using <see cref="IWithModelStateBuilder"/>.
        /// </param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithModelState<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IWithModelStateBuilder> modelStateTestBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            modelStateTestBuilder(new WithModelStateBuilder(actualBuilder.TestContext as ActionTestContext));

            return actualBuilder.Builder;
        }
    }
}
