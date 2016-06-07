namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ShouldPassFor
{
    using System;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Test builder allowing additional assertions on various components.
    /// </summary>
    public interface IShouldPassForTestBuilderWithInvokedAction : IShouldPassForTestBuilderWithAction
    {
        /// <summary>
        /// Tests whether the caught <see cref="Exception"/> passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing assertions on the caught <see cref="Exception"/>.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilderWithInvokedAction"/>.</returns>
        IShouldPassForTestBuilderWithInvokedAction TheCaughtException(Action<Exception> assertions);

        /// <summary>
        /// Tests whether the caught <see cref="Exception"/> passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the caught <see cref="Exception"/>.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilderWithInvokedAction"/>.</returns>
        IShouldPassForTestBuilderWithInvokedAction TheCaughtException(Func<Exception, bool> predicate);

        /// <summary>
        /// Tests whether the <see cref="HttpResponse"/> passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing assertions on the <see cref="HttpResponse"/>.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilderWithInvokedAction"/>.</returns>
        IShouldPassForTestBuilderWithInvokedAction TheHttpResponse(Action<HttpResponse> assertions);

        /// <summary>
        /// Tests whether the <see cref="HttpResponse"/> passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the <see cref="HttpResponse"/>.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilderWithInvokedAction"/>.</returns>
        IShouldPassForTestBuilderWithInvokedAction TheHttpResponse(Func<HttpResponse, bool> predicate);
    }
}
