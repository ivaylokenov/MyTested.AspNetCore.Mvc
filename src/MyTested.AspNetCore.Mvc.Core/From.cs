namespace MyTested.AspNetCore.Mvc
{
    using Internal.Application;

    /// <summary>
    /// Provides easy resolving of expression method argument values from the application <see cref="System.IServiceProvider"/>.
    /// </summary>
    public static class From
    {
        /// <summary>
        /// Indicates that a argument should be resolved from the application <see cref="System.IServiceProvider"/> in a method call lambda expression.
        /// </summary>
        /// <typeparam name="TService">Type of service.</typeparam>
        /// <returns>Service implementation.</returns>
        public static TService Services<TService>() => TestServiceProvider.GetRequiredService<TService>();
    }
}
