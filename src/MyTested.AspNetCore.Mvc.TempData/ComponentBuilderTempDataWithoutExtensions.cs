namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
    using Builders.Contracts.Base;
    using Builders.Contracts.Data;
    using Builders.Base;
    using Builders.Data;
    using Internal.TestContexts;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary"/> extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderTempDataWithoutExtensions
    {
        /// <summary>
        /// Remove entity by providing its key from <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="key">The key of the entity to be deleted.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutTempData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            string key)
            where TBuilder : IBaseTestBuilder
            => builder
                .WithoutTempData(tempData => tempData
                .WithoutEntry(key));

        /// <summary>
        /// Remove entities by providing their keys from <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="keys">The keys of the entities to be deleted.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutTempData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            params string[] keys)
            where TBuilder : IBaseTestBuilder
            => builder
                .WithoutTempData(tempData => tempData
                .WithoutEntries(keys));

        /// <summary>
        /// Remove entities by providing their keys from <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="keys">The keys of the entities to be deleted.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutTempData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            IEnumerable<string> keys)
            where TBuilder : IBaseTestBuilder
            => builder
                .WithoutTempData(tempData => tempData
                .WithoutEntries(keys));

        /// <summary>
        /// Removing all entities from <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutTempData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
            => builder
                .WithoutTempData(tempData => tempData
                .WithoutEntries());

        /// <summary>
        /// Remove values from <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary"/> on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="tempDataBuilder">Action setting the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary"/> values by using <see cref="IWithTempDataBuilder"/>.</param>
        /// <returns>The same component builder.</returns>
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
