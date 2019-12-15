namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
    using Builders.ActionResults.View;
    using Builders.Contracts.ActionResults.View;
    using Exceptions;
    using Microsoft.AspNetCore.Routing;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IViewComponentTestBuilder" />.
    /// </summary>
    public static class ViewComponentTestBuilderExtensions
    {
        private const string ArgumentsName = "arguments";

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/>
        /// has the same name as the provided one.
        /// </summary>
        /// <param name="viewComponentTestBuilder">
        /// Instance of <see cref="IViewComponentTestBuilder"/> type.
        /// </param>
        /// <param name="viewComponentName">Expected view component name.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        public static IAndViewComponentTestBuilder WithName(
            this IViewComponentTestBuilder viewComponentTestBuilder,
            string viewComponentName)
        {
            var actualBuilder = GetActualBuilder(viewComponentTestBuilder);

            var actualViewComponentName = actualBuilder
                .ActionResult
                .ViewComponentName;

            if (viewComponentName != actualViewComponentName)
            {
                throw ViewResultAssertionException.ForNameEquality(
                    actualBuilder.TestContext.ExceptionMessagePrefix,
                    "view component",
                    viewComponentName,
                    actualViewComponentName);
            }

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/>
        /// is of the same type as the provided one.
        /// </summary>
        /// <param name="viewComponentTestBuilder">
        /// Instance of <see cref="IViewComponentTestBuilder"/> type.
        /// </param>
        /// <param name="viewComponentType">Expected view component type.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        public static IAndViewComponentTestBuilder OfType(
            this IViewComponentTestBuilder viewComponentTestBuilder,
            Type viewComponentType)
        {
            var actualBuilder = GetActualBuilder(viewComponentTestBuilder);

            var actualViewComponentType = actualBuilder
                .ActionResult
                .ViewComponentType;

            if (viewComponentType != actualViewComponentType)
            {
                var (expectedViewComponentName, actualViewComponentName) = 
                    (viewComponentType, actualViewComponentType).GetTypeComparisonNames();

                throw ViewResultAssertionException.ForNameEquality(
                    actualBuilder.TestContext.ExceptionMessagePrefix,
                    "view component",
                    expectedViewComponentName,
                    actualViewComponentName);
            }

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/>
        /// is of the same type as the provided one.
        /// </summary>
        /// <typeparam name="TViewComponentType">Expected view component type.</typeparam>
        /// <param name="viewComponentTestBuilder">
        /// Instance of <see cref="IViewComponentTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        public static IAndViewComponentTestBuilder OfType<TViewComponentType>(
            this IViewComponentTestBuilder viewComponentTestBuilder)
            => viewComponentTestBuilder
                .OfType(typeof(TViewComponentType));

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> will be
        /// invoked with an argument with the same name as the provided one.
        /// </summary>
        /// <param name="viewComponentTestBuilder">
        /// Instance of <see cref="IViewComponentTestBuilder"/> type.
        /// </param>
        /// <param name="name">Expected argument name.</param>
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
        /// <param name="name">Expected argument name.</param>
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
        /// <typeparam name="TArgument">Expected argument type.</typeparam>
        /// <param name="argument">Expected argument value.</param>
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
        /// <typeparam name="TArgument">Expected argument type.</typeparam>
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
        /// <typeparam name="TArgument">Expected argument type.</typeparam>
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
        /// <param name="arguments">Expected arguments object.</param>
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
        /// <param name="arguments">Expected arguments object as dictionary.</param>
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
