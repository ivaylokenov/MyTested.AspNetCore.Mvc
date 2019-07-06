namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.BadRequest;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;

    /// <summary>
    /// Contains extension methods for <see cref="IBadRequestTestBuilder"/>.
    /// </summary>
    public static class BadRequestTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="BadRequestObjectResult"/>
        /// contains <see cref="IOutputFormatter"/> of the provided type.
        /// </summary>
        /// <param name="badRequestTestBuilder">
        /// Instance of <see cref="IBadRequestTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndBadRequestTestBuilder"/>.</returns>
        public static IAndBadRequestTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>(
            this IBadRequestTestBuilder badRequestTestBuilder)
            where TOutputFormatter : IOutputFormatter
            => badRequestTestBuilder
                .ContainingOutputFormatterOfType<IAndBadRequestTestBuilder, TOutputFormatter>();
    }
}
