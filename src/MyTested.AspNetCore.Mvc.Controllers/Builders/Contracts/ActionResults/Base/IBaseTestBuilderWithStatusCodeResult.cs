namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using System.Net;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc.Infrastructure;

    /// <summary>
    /// Base interface for all test builders with status code action result.
    /// </summary>
    /// <typeparam name="TStatusCodeResultTestBuilder">Type of status code result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithStatusCodeResult<TStatusCodeResultTestBuilder> : IBaseTestBuilderWithActionResult
        where TStatusCodeResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Tests whether <see cref="IStatusCodeActionResult"/> has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code as integer.</param>
        /// <returns>The same <see cref="IStatusCodeActionResult"/> test builder.</returns>
        TStatusCodeResultTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether <see cref="IStatusCodeActionResult"/> has the same status code as the provided <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> enumeration.</param>
        /// <returns>The same <see cref="IStatusCodeActionResult"/> test builder.</returns>
        TStatusCodeResultTestBuilder WithStatusCode(HttpStatusCode statusCode);
    }
}
