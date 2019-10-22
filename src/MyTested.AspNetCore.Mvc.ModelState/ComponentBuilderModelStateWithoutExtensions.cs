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
    public static class ComponentBuilderModelStateWithoutExtensions
    {
        /// <summary>
        /// Removes the provided key from <see cref="ModelStateDictionary"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="key">The model state key.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutModelState<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            string key)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutModelState(state => state.WithoutModelState(key));

        /// <summary>
        /// Removes all keys and values from <see cref="ModelStateDictionary"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutModelState<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutModelState(state => state.WithoutModelState());

        /// <summary>
        /// Used for removing a model state from <see cref="ModelStateDictionary"/> by using the builder.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="modelStateTestBuilder">
        /// Action setting the <see cref="WithoutModelStateBuilder"/> by using <see cref="IWithoutModelStateBuilder"/>.
        /// </param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutModelState<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IWithoutModelStateBuilder> modelStateTestBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            modelStateTestBuilder(new WithoutModelStateBuilder(actualBuilder.TestContext as ActionTestContext));

            return actualBuilder.Builder;
        }
    }
}
