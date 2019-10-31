namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
    using Builders.Contracts.Base;
    using Builders.Contracts.Data;
    using Builders.Base;
    using Builders.Data;
    using Internal.TestContexts;

    public static class ComponentBuilderTempDataWithoutExtensions
    {
        public static TBuilder WithoutTempData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            string key)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutTempData(tempData => tempData.WithoutEntry(key));

        public static TBuilder WithoutTempData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            params string[] keys)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutTempData(tempData => tempData.WithoutEntries(keys));

        public static TBuilder WithoutTempData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            IEnumerable<string> keys)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutTempData(tempData => tempData.WithoutEntries(keys));

        public static TBuilder WithoutTempData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutTempData(tempData => tempData.WithoutAllEntries());

        public static TBuilder WithoutTempData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IWithoutTempDataBuilder> tempDataBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            actualBuilder.TestContext.ComponentPreparationDelegate += () =>
            {
                tempDataBuilder(new WithoutTempDataBuilder(actualBuilder.TestContext.GetTempData()));
            };

            return actualBuilder.Builder;
        }
    }
}
