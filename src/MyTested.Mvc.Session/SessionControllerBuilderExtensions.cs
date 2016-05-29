namespace MyTested.Mvc
{
    using System;
    using Builders.Contracts.Controllers;
    using Builders.Contracts.Data;
    using Builders.Controllers;
    using Builders.Data;

    /// <summary>
    /// Contains session extension methods for <see cref="IControllerBuilder{TController}"/>.
    /// </summary>
    public static class SessionControllerBuilderExtensions
    {
        /// <summary>
        /// Sets initial values to the HTTP <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <param name="controllerBuilder">Instance of <see cref="IControllerBuilder{TController}"/> type.</param>
        /// <param name="sessionBuilder">Action setting the <see cref="Microsoft.AspNetCore.Http.ISession"/> values by using <see cref="ISessionBuilder"/>.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        public static IAndControllerBuilder<TController> WithSession<TController>(
            this IControllerBuilder<TController> controllerBuilder,
            Action<ISessionBuilder> sessionBuilder)
            where TController : class
        {
            var actualControllerBuilder = (ControllerBuilder<TController>)controllerBuilder;

            sessionBuilder(new SessionBuilder(actualControllerBuilder.TestContext.Session));

            return actualControllerBuilder;
        }
    }
}
