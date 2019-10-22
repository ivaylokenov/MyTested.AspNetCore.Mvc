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
    public static class ComponentBuilderModelStateWithExtensions
    {
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
