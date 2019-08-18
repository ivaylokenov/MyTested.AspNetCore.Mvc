namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.Ok;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;

    /// <summary>
    /// Contains extension methods for <see cref="IOkTestBuilder"/>.
    /// </summary>
    public static class OkTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="OkObjectResult"/>
        /// contains <see cref="IOutputFormatter"/> of the provided type.
        /// </summary>
        /// <param name="okTestBuilder">
        /// Instance of <see cref="IOkTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndOkTestBuilder"/>.</returns>
        public static IAndOkTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>(
            this IOkTestBuilder okTestBuilder)
            where TOutputFormatter : IOutputFormatter
            => okTestBuilder
                .ContainingOutputFormatterOfType<IAndOkTestBuilder, TOutputFormatter>();
    }
}
