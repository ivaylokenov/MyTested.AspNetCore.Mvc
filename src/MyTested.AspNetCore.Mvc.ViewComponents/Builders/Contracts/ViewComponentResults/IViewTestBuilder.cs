namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ViewComponentResults
{
    using System;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using And;
    using Base;

    /// <summary>
    /// Used for testing <see cref="ViewViewComponentResult"/>.
    /// </summary>
    public interface IViewTestBuilder : IBaseTestBuilderWithResponseModel
    {
        /// <summary>
        /// Tests whether the <see cref="ViewViewComponentResult"/> passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions
        /// for the <see cref="ViewViewComponentResult"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Passing(Action<ViewViewComponentResult> assertions);

        /// <summary>
        /// Tests whether the <see cref="ViewViewComponentResult"/> passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the <see cref="ViewViewComponentResult"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Passing(Func<ViewViewComponentResult, bool> predicate);
    }
}
