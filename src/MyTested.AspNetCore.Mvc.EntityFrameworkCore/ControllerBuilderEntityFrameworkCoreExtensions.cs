namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Data;
    using Builders.Controllers;
    using Builders.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Contains <see cref="DbContext"/> extension methods for <see cref="IControllerBuilder{TController}"/>.
    /// </summary>
    public static class ControllerBuilderEntityFrameworkCoreExtensions
    {
        /// <summary>
        /// Sets initial values to the <see cref="DbContext"/> on the tested controller.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <param name="controllerBuilder">Instance of <see cref="IControllerBuilder{TController}"/> type.</param>
        /// <param name="dbContextBuilder">Action setting the <see cref="DbContext"/> by using <see cref="IDbContextBuilder"/>.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        public static IControllerBuilder<TController> WithDbContext<TController>(
            this IControllerBuilder<TController> controllerBuilder,
            Action<IDbContextBuilder> dbContextBuilder)
            where TController : class
        {
            var actualControllerBuilder = (ControllerBuilder<TController>)controllerBuilder;

            dbContextBuilder(new DbContextBuilder(actualControllerBuilder.TestContext));

            return actualControllerBuilder;
        }
    }
}
