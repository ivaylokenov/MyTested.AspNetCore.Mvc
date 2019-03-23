namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Created
{
    using System;
    using Base;
    using Contracts.Base;
    using Uri;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>,
    /// <see cref="Microsoft.AspNetCore.Mvc.CreatedAtActionResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.CreatedAtRouteResult"/>.
    /// </summary>
    public interface ICreatedTestBuilder : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithOutputResult<IAndCreatedTestBuilder>, IBaseTestBuilderWithRouteValuesResult<IAndCreatedTestBuilder>
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>
        /// has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder AtLocation(string location);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>
        /// location passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions for the location.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder AtLocationPassing(Action<string> assertions);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>
        /// location passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the location.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder AtLocationPassing(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>
        /// has specific location provided by <see cref="Uri"/>.
        /// </summary>
        /// <param name="location">Expected location as <see cref="Uri"/>.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder AtLocation(Uri location);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>
        /// has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder AtLocation(Action<IUriTestBuilder> uriTestBuilder);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.CreatedAtActionResult"/>
        /// has specific action name.
        /// </summary>
        /// <param name="actionName">Expected action name.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder AtAction(string actionName);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.CreatedAtActionResult"/>
        /// has specific controller name.
        /// </summary>
        /// <param name="controllerName">Expected controller name.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder AtController(string controllerName);
    }
}
