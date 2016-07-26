namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Linq;
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Data;
    using Builders.Data;
    using Utilities.Validators;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Http.ISession"/> extension methods for <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentShouldHaveTestBuilderSessionExtensions
    {
        /// <summary>
        /// Tests whether the component does not set any <see cref="Microsoft.AspNetCore.Http.ISession"/> entries.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/> type.</param>
        /// <returns>The same component should have test builder.</returns>
        public static TBuilder NoSession<TBuilder>(this IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder>)builder;

            if (actualBuilder.TestContext.Session.Keys.Any())
            {
                DataProviderValidator.ThrowNewDataProviderAssertionExceptionWithNoEntries(
                    actualBuilder.TestContext,
                    SessionTestBuilder.SessionName);
            }

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Tests whether the component sets entries in the <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/> type.</param>
        /// <param name="withNumberOfEntries">Expected number of <see cref="Microsoft.AspNetCore.Http.ISession"/> entries.
        /// If default null is provided, the test builder checks only if any entries are found.</param>
        /// <returns>The same component should have test builder.</returns>
        public static TBuilder Session<TBuilder>(
            this IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> builder, 
            int? withNumberOfEntries = null)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder>)builder;

            DataProviderValidator.ValidateDataProviderNumberOfEntries(
                actualBuilder.TestContext,
                SessionTestBuilder.SessionName,
                withNumberOfEntries,
                actualBuilder.TestContext.Session.Keys.Count());

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Tests whether the component sets specific <see cref="Microsoft.AspNetCore.Http.ISession"/> entries.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/> type.</param>
        /// <param name="sessionTestBuilder">Builder for testing specific <see cref="Microsoft.AspNetCore.Http.ISession"/> entries.</param>
        /// <returns>The same component should have test builder.</returns>
        public static TBuilder Session<TBuilder>(
            this IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> builder, 
            Action<ISessionTestBuilder> sessionTestBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder>)builder;

            sessionTestBuilder(new SessionTestBuilder(actualBuilder.TestContext));

            return actualBuilder.Builder;
        }
    }
}
