namespace MyTested.AspNetCore.Mvc
{
    using System.Collections.Generic;
    using Builders.ActionResults.View;
    using Builders.Contracts.ActionResults.View;
    using Microsoft.AspNetCore.Routing;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IViewComponentTestBuilder" />.
    /// </summary>
    public static class ViewComponentTestBuilderExtensions
    {
        private const string ArgumentsName = "arguments";

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> will be
        /// invoked with an argument with the same name as the provided one.
        /// </summary>
        /// <param name="viewComponentTestBuilder">
        /// Instance of <see cref="IViewComponentTestBuilder"/> type.
        /// </param>
        /// <param name="name">Name of the argument.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        public static IAndViewComponentTestBuilder ContainingArgumentWithName(
            this IViewComponentTestBuilder viewComponentTestBuilder,
            string name)
        {
            var actualBuilder = GetActualBuilder(viewComponentTestBuilder);

            DictionaryValidator.ValidateStringKey(
                ArgumentsName,
                actualBuilder.ViewComponentArguments,
                name,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> will be
        /// invoked with an argument deeply equal to the provided one.
        /// </summary>
        /// <param name="viewComponentTestBuilder">
        /// Instance of <see cref="IViewComponentTestBuilder"/> type.
        /// </param>
        /// <param name="name">Name of the argument.</param>
        /// <param name="value">Expected argument value.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        public static IAndViewComponentTestBuilder ContainingArgument(
            this IViewComponentTestBuilder viewComponentTestBuilder,
            string name,
            object value)
        {
            var actualBuilder = GetActualBuilder(viewComponentTestBuilder);

            DictionaryValidator.ValidateStringKeyAndValue(
                ArgumentsName,
                actualBuilder.ViewComponentArguments,
                name,
                value,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> will be
        /// invoked with an argument equal to the provided one.
        /// </summary>
        /// <param name="viewComponentTestBuilder">
        /// Instance of <see cref="IViewComponentTestBuilder"/> type.
        /// </param>
        /// <typeparam name="TArgument">Type of the argument.</typeparam>
        /// <param name="argument">Argument object.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        public static IAndViewComponentTestBuilder ContainingArgument<TArgument>(
            this IViewComponentTestBuilder viewComponentTestBuilder,
            TArgument argument)
        {
            var actualBuilder = GetActualBuilder(viewComponentTestBuilder);

            DictionaryValidator.ValidateValue(
                ArgumentsName,
                actualBuilder.ViewComponentArguments,
                argument,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> will be
        /// invoked with an argument of the provided type.
        /// </summary>
        /// <param name="viewComponentTestBuilder">
        /// Instance of <see cref="IViewComponentTestBuilder"/> type.
        /// </param>
        /// <typeparam name="TArgument">Type of the argument.</typeparam>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        public static IAndViewComponentTestBuilder ContainingArgumentOfType<TArgument>(
            this IViewComponentTestBuilder viewComponentTestBuilder)
        {
            var actualBuilder = GetActualBuilder(viewComponentTestBuilder);

            DictionaryValidator.ValidateValueOfType<TArgument>(
                ArgumentsName,
                actualBuilder.ViewComponentArguments,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> will be
        /// invoked with an argument of the provided type and the given name.
        /// </summary>
        /// <param name="viewComponentTestBuilder">
        /// Instance of <see cref="IViewComponentTestBuilder"/> type.
        /// </param>
        /// <typeparam name="TArgument">Type of the argument.</typeparam>
        /// <param name="name">Name of the argument.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        public static IAndViewComponentTestBuilder ContainingArgumentOfType<TArgument>(
            this IViewComponentTestBuilder viewComponentTestBuilder,
            string name)
        {
            var actualBuilder = GetActualBuilder(viewComponentTestBuilder);

            DictionaryValidator.ValidateStringKeyAndValueOfType<TArgument>(
                ArgumentsName,
                actualBuilder.ViewComponentArguments,
                name,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> will be
        /// invoked with the provided arguments.
        /// </summary>
        /// <param name="viewComponentTestBuilder">
        /// Instance of <see cref="IViewComponentTestBuilder"/> type.
        /// </param>
        /// <param name="arguments">Arguments object.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        public static IAndViewComponentTestBuilder ContainingArguments(
            this IViewComponentTestBuilder viewComponentTestBuilder,
            object arguments)
            => viewComponentTestBuilder
                .ContainingArguments(new RouteValueDictionary(arguments));

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> will be
        /// invoked with the provided arguments.
        /// </summary>
        /// <param name="viewComponentTestBuilder">
        /// Instance of <see cref="IViewComponentTestBuilder"/> type.
        /// </param>
        /// <param name="arguments">Argument objects as dictionary.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        public static IAndViewComponentTestBuilder ContainingArguments(
            this IViewComponentTestBuilder viewComponentTestBuilder,
            IDictionary<string, object> arguments)
        {
            var actualBuilder = GetActualBuilder(viewComponentTestBuilder);

            DictionaryValidator.ValidateValues(
                ArgumentsName,
                actualBuilder.ViewComponentArguments,
                arguments,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
        }
        
        private static ViewComponentTestBuilder GetActualBuilder(
            IViewComponentTestBuilder viewComponentTestBuilder)
            => (ViewComponentTestBuilder)viewComponentTestBuilder;
    }
}
