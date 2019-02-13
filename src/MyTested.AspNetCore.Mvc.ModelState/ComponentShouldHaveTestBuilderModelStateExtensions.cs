namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Linq;
    using Builders.Contracts.Models;
    using Builders.Models;
    using Exceptions;
    using Utilities.Validators;
    using Builders.Contracts.Base;
    using Builders.Base;
    using Internal.TestContexts;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> extension methods for <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentShouldHaveTestBuilderModelStateExtensions
    {
        /// <summary>
        /// Tests whether the component has specific <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/> type.</param>
        /// <param name="modelStateTestBuilder">Builder for testing specific <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.</param>
        /// <returns>The same component should have test builder.</returns>
        public static TBuilder ModelState<TBuilder>(
            this IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> builder,
            Action<IModelStateTestBuilder> modelStateTestBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            modelStateTestBuilder(new ModelStateTestBuilder(actualBuilder.TestContext as ActionTestContext));

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Tests whether the component has valid <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> with no errors.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/> type.</param>
        /// <returns>The same component should have test builder.</returns>
        public static TBuilder ValidModelState<TBuilder>(this IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            ModelStateValidator.CheckValidModelState(actualBuilder.TestContext as ActionTestContext);

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Tests whether the action has invalid <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/> type.</param>
        /// <param name="withNumberOfErrors">Expected number of <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.
        /// If default null is provided, the test builder checks only if any errors are found.</param>
        /// <returns>The same component should have test builder.</returns>
        public static TBuilder InvalidModelState<TBuilder>(
            this IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> builder,
            int? withNumberOfErrors = null)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            var actualModelStateErrors = (actualBuilder.TestContext as ActionTestContext)?.ModelState.Values.SelectMany(c => c.Errors).Count();
            if (actualModelStateErrors == 0
                || (withNumberOfErrors != null && actualModelStateErrors != withNumberOfErrors))
            {
                throw new ModelErrorAssertionException(string.Format(
                    "{0} to have invalid model state{1}, {2}.",
                    actualBuilder.TestContext.ExceptionMessagePrefix,
                    withNumberOfErrors == null ? string.Empty : $" with {withNumberOfErrors} errors",
                    withNumberOfErrors == null ? "but was in fact valid" : $"but in fact contained {actualModelStateErrors}"));
            }

            return actualBuilder.Builder;
        }
    }
}
