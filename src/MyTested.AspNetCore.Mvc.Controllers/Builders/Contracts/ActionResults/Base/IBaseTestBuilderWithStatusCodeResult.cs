namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using System.Net;
    using Contracts.Base;

    /// <summary>
    /// Base interface for all test builders with <see cref="Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult"/>.
    /// </summary>
    /// <typeparam name="TStatusCodeResultTestBuilder">Type of status code result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithStatusCodeResult<TStatusCodeResultTestBuilder> : IBaseTestBuilderWithActionResult
        where TStatusCodeResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult"/>
        /// has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code as integer.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult"/> test builder.</returns>
        TStatusCodeResultTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult"/>
        /// has the same status code as the provided <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> enumeration.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult"/> test builder.</returns>
        TStatusCodeResultTestBuilder WithStatusCode(HttpStatusCode statusCode);
    }
}
