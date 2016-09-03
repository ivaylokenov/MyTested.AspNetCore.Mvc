namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ViewComponentResults
{
    using Base;
    using Microsoft.AspNetCore.Mvc.ViewEngines;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/>.
    /// </summary>
    public interface IViewTestBuilder : IBaseTestBuilderWithResponseModel
    {
        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/>
        /// has the same <see cref="IViewEngine"/> as the provided one.
        /// </summary>
        /// <param name="viewEngine">View engine of type <see cref="IViewEngine"/>.</param>
        /// <returns>The same <see cref="IAndViewTestBuilder"/>.</returns>
        IAndViewTestBuilder WithViewEngine(IViewEngine viewEngine);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/>
        /// has the same <see cref="IViewEngine"/> type as the provided one.
        /// </summary>
        /// <typeparam name="TViewEngine">View engine of type <see cref="IViewEngine"/>.</typeparam>
        /// <returns>The same <see cref="IAndViewTestBuilder"/>.</returns>
        IAndViewTestBuilder WithViewEngineOfType<TViewEngine>()
            where TViewEngine : IViewEngine;
    }
}
