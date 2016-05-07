namespace MyTested.Mvc.Builders.Contracts.ShouldPassFor
{
    using System;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Test builder allowing additional assertions on various components.
    /// </summary>
    public interface IShouldPassForTestBuilder
    {
        /// <summary>
        /// Tests whether the <see cref="HttpContext"/> passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing assertions on the <see cref="HttpContext"/>.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilder"/>.</returns>
        IShouldPassForTestBuilder TheHttpContext(Action<HttpContext> assertions);

        /// <summary>
        /// Tests whether the <see cref="HttpContext"/> passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the <see cref="HttpContext"/>.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilder"/>.</returns>
        IShouldPassForTestBuilder TheHttpContext(Func<HttpContext, bool> predicate);

        /// <summary>
        /// Tests whether the <see cref="HttpRequest"/> passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing assertions on the <see cref="HttpRequest"/>.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilder"/>.</returns>
        IShouldPassForTestBuilder TheHttpRequest(Action<HttpRequest> assertions);

        /// <summary>
        /// Tests whether the <see cref="HttpRequest"/> passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the <see cref="HttpRequest"/>.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilder"/>.</returns>
        IShouldPassForTestBuilder TheHttpRequest(Func<HttpRequest, bool> predicate);
    }
}
