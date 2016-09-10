namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ViewComponents
{
    /// <summary>
    /// Used for adding AndAlso() method to the view component builder.
    /// </summary>
    /// <typeparam name="TViewComponent">Class representing ASP.NET Core MVC view component.</typeparam>
    public interface IAndViewComponentBuilder<TViewComponent> : IViewComponentBuilder<TViewComponent>
        where TViewComponent : class
    {
        /// <summary>
        /// AndAlso method for better readability when building view component instance.
        /// </summary>
        /// <returns>The same <see cref="IAndViewComponentBuilder{TViewComponent}"/>.</returns>
        IAndViewComponentBuilder<TViewComponent> AndAlso();
    }
}
