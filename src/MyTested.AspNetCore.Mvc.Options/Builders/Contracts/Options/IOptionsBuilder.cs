namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Options
{
    using System;

    /// <summary>
    /// Used for building configuration options.
    /// </summary>
    public interface IOptionsBuilder
    {
        /// <summary>
        /// Sets initial values to the provided configuration options.
        /// </summary>
        /// <typeparam name="TOptions">Type of configuration options to set up.</typeparam>
        /// <param name="optionsSetup">Action setting the configuration options.</param>
        void For<TOptions>(Action<TOptions> optionsSetup) where TOptions : class, new();
    }
}
