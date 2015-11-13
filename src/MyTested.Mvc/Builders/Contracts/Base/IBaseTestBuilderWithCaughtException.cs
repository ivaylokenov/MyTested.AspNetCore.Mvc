namespace MyTested.Mvc.Builders.Contracts.Base
{
    using System;

    /// <summary>
    /// Base interface for test builders with caught exception.
    /// </summary>
    public interface IBaseTestBuilderWithCaughtException : IBaseTestBuilderWithAction
    {
        /// <summary>
        /// Gets the thrown exception in the tested action.
        /// </summary>
        /// <returns>The exception instance or null, if no exception was caught.</returns>
        Exception AndProvideTheCaughtException();
    }
}
