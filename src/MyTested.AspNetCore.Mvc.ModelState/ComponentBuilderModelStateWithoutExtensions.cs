namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Models;
    using Builders.Models;
    using Internal.TestContexts;

    /// <summary>
    /// Contains extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderModelStateWithoutExtensions
    {
        public static TBuilder WithoutModelState<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutModelState(state => state.WithoutModelState());

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
