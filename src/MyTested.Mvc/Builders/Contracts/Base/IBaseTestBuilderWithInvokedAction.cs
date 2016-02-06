namespace MyTested.Mvc.Builders.Contracts.Base
{
    using Microsoft.AspNetCore.Http;
    using System;

    /// <summary>
    /// Base interface for test builders with caught exception.
    /// </summary>
    public interface IBaseTestBuilderWithInvokedAction : IBaseTestBuilderWithAction
    {
        /// <summary>
        /// Gets the thrown exception in the tested action.
        /// </summary>
        /// <returns>The exception instance or null, if no exception was caught.</returns>
        Exception AndProvideTheCaughtException();

        /// <summary>
        /// Gets the HTTP response after the tested action is executed.
        /// </summary>
        /// <returns>The HTTP response.</returns>
        HttpResponse AndProvideTheHttpResponse();
    }
}
