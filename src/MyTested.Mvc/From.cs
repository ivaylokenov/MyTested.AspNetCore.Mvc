namespace MyTested.Mvc
{
    using Internal.Application;

    /// <summary>
    /// Provides easy resolving of expression method argument values from the application services.
    /// </summary>
    public static class From
    {
        /// <summary>
        /// Indicates that a argument should be resolved from the application services in method call lambda expression.
        /// </summary>
        /// <typeparam name="TService">Type of service.</typeparam>
        /// <returns>Service implementation.</returns>
        public static TService Services<TService>() => TestServiceProvider.GetService<TService>();
    }
}
