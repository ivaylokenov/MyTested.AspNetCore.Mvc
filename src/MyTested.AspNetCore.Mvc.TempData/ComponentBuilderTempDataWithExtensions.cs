namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Base;
    using Builders.Contracts.Data;
    using Builders.Base;
    using Builders.Data;
    using Internal.TestContexts;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary"/> extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderTempDataWithExtensions
    {
        /// <summary>
        /// Sets initial values to the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary"/> on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="tempDataBuilder">Action setting the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary"/> values by using <see cref="IWithTempDataBuilder"/>.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithTempData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IWithTempDataBuilder> tempDataBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            actualBuilder.TestContext.ComponentPreparationDelegate += () =>
            {
                tempDataBuilder(new WithTempDataBuilder(actualBuilder.TestContext.GetTempData()));
            };
            
            return actualBuilder.Builder;
        }
    }
}
