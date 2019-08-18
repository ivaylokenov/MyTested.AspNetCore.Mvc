namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Accepted
{
    using System;
    using And;
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="AcceptedResult"/>,
    /// <see cref="AcceptedAtActionResult"/>
    /// or <see cref="AcceptedAtRouteResult"/>.
    /// </summary>
    public interface IAcceptedTestBuilder
        : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithOutputResult<IAndAcceptedTestBuilder>,
        IBaseTestBuilderWithRouteValuesResult<IAndAcceptedTestBuilder>,
        IBaseTestBuilderWithActionResult<AcceptedResult>,
        IBaseTestBuilderWithActionResult<AcceptedAtRouteResult>
    {
        /// <summary>
        /// Tests whether the <see cref="AcceptedAtActionResult"/> passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions
        /// for the <see cref="AcceptedAtActionResult"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Passing(Action<AcceptedAtActionResult> assertions);

        /// <summary>
        /// Tests whether the <see cref="AcceptedAtActionResult"/> passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the <see cref="AcceptedAtActionResult"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Passing(Func<AcceptedAtActionResult, bool> predicate);
    }
}
