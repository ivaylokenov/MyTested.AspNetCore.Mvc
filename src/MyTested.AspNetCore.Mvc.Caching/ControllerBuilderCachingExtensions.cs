namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Controllers;
    using Builders.Contracts.Data;
    using Builders.Controllers;
    using Builders.Data;

    /// <summary>
    /// Contains <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> extension methods for <see cref="IControllerBuilder{TController}"/>.
    /// </summary>
    public static class ControllerBuilderCachingExtensions
    {
        /// <summary>
        /// Sets initial values to the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> service.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <param name="controllerBuilder">Instance of <see cref="IControllerBuilder{TController}"/> type.</param>
        /// <param name="memoryCacheBuilder">Action setting the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> values by using <see cref="IMemoryCacheBuilder"/>.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        public static IAndControllerBuilder<TController> WithMemoryCache<TController>(
            this IControllerBuilder<TController> controllerBuilder,
            Action<IMemoryCacheBuilder> memoryCacheBuilder)
            where TController : class
        {
            var actualControllerBuilder = (ControllerBuilder<TController>)controllerBuilder;

            memoryCacheBuilder(new MemoryCacheBuilder(actualControllerBuilder.TestContext.HttpContext.RequestServices));

            return actualControllerBuilder;
        }
    }
}
