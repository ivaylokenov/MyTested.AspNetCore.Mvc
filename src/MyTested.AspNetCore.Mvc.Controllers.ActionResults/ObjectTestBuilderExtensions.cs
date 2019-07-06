namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.Object;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;

    /// <summary>
    /// Contains extension methods for <see cref="IObjectTestBuilder"/>.
    /// </summary>
    public static class ObjectTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="ObjectResult"/>
        /// contains <see cref="IOutputFormatter"/> of the provided type.
        /// </summary>
        /// <param name="objectTestBuilder">
        /// Instance of <see cref="IObjectTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndObjectTestBuilder"/>.</returns>
        public static IAndObjectTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>(
            this IObjectTestBuilder objectTestBuilder)
            where TOutputFormatter : IOutputFormatter
            => objectTestBuilder
                .ContainingOutputFormatterOfType<IAndObjectTestBuilder, TOutputFormatter>();
    }
}
