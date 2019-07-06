namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Created
{
    using System;
    using And;
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="CreatedResult"/>,
    /// <see cref="CreatedAtActionResult"/>
    /// or <see cref="CreatedAtRouteResult"/>.
    /// </summary>
    public interface ICreatedTestBuilder 
        : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithOutputResult<IAndCreatedTestBuilder>, 
        IBaseTestBuilderWithRouteValuesResult<IAndCreatedTestBuilder>,
        IBaseTestBuilderWithActionResult<CreatedResult>,
        IBaseTestBuilderWithActionResult<CreatedAtRouteResult>
    {
        /// <summary>
        /// Tests whether the <see cref="CreatedAtActionResult"/> passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions
        /// for the <see cref="CreatedAtActionResult"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Passing(Action<CreatedAtActionResult> assertions);

        /// <summary>
        /// Tests whether the <see cref="CreatedAtActionResult"/> passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the <see cref="CreatedAtActionResult"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Passing(Func<CreatedAtActionResult, bool> predicate);
    }
}
