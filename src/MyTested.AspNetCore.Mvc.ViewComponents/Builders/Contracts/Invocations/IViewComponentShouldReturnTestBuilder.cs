namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Invocations
{
    using System;
    using Base;
    using ViewComponentResults;

    /// <summary>
    /// Used for testing returned view component result.
    /// </summary>
    /// <typeparam name="TInvocationResult">Result from invoked view component in ASP.NET Core MVC.</typeparam>
    public interface IViewComponentShouldReturnTestBuilder<TInvocationResult>
        : IBaseShouldReturnTestBuilder<TInvocationResult, IBaseTestBuilderWithViewComponentResult<TInvocationResult>>
    {
        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ContentViewComponentResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithViewComponentResult{TInvocationResult}"/> type.</returns>
        IBaseTestBuilderWithViewComponentResult<TInvocationResult> Content();

        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ContentViewComponentResult"/> with expected content.
        /// </summary>
        /// <param name="content">Expected content as string.</param>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithViewComponentResult{TInvocationResult}"/> type.</returns>
        IBaseTestBuilderWithViewComponentResult<TInvocationResult> Content(string content);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ContentViewComponentResult"/> passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the content.</param>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithViewComponentResult{TInvocationResult}"/> type.</returns>
        IBaseTestBuilderWithViewComponentResult<TInvocationResult> Content(Action<string> assertions);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ContentViewComponentResult"/> passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the content.</param>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithViewComponentResult{TInvocationResult}"/> type.</returns>
        IBaseTestBuilderWithViewComponentResult<TInvocationResult> Content(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Html.IHtmlContent"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithViewComponentResult{TInvocationResult}"/> type.</returns>
        IBaseTestBuilderWithViewComponentResult<TInvocationResult> HtmlContent();

        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Html.IHtmlContent"/> with expected content.
        /// </summary>
        /// <param name="htmlContent">Expected HTML content as string.</param>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithViewComponentResult{TInvocationResult}"/> type.</returns>
        IBaseTestBuilderWithViewComponentResult<TInvocationResult> HtmlContent(string htmlContent);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Html.IHtmlContent"/> passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the HTML content.</param>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithViewComponentResult{TInvocationResult}"/> type.</returns>
        IBaseTestBuilderWithViewComponentResult<TInvocationResult> HtmlContent(Action<string> assertions);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Html.IHtmlContent"/> passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the HTML content.</param>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithViewComponentResult{TInvocationResult}"/> type.</returns>
        IBaseTestBuilderWithViewComponentResult<TInvocationResult> HtmlContent(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/> with the default view name.
        /// </summary>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        IViewTestBuilder View();

        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/> with the provided view name.
        /// </summary>
        /// <param name="viewName">Expected view name.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        IViewTestBuilder View(string viewName);

        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/> with the provided deeply equal model object.
        /// </summary>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        IViewTestBuilder View<TModel>(TModel model);

        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/> with the provided view name and deeply equal model object.
        /// </summary>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="viewName">Expected view name.</param>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        IViewTestBuilder View<TModel>(string viewName, TModel model);
    }
}
