namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ViewComponents
{
    using Base;

    /// <summary>
    /// Used for building the view component which will be tested.
    /// </summary>
    /// <typeparam name="TViewComponent">Class representing ASP.NET Core MVC view component.</typeparam>
    public interface IViewComponentBuilder<TViewComponent> : IBaseTestBuilderWithComponentBuilder<IAndViewComponentBuilder<TViewComponent>>
        where TViewComponent : class
    {
        /// <summary>
        /// Used for testing view component additional details.
        /// </summary>
        /// <returns>Test builder of <see cref="IViewComponentTestBuilder"/> type.</returns>
        IViewComponentTestBuilder ShouldHave();
    }
}
