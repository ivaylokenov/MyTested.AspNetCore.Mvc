﻿namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Invocations
{
    using System;
    using And;
    using ViewComponentResults;

    /// <summary>
    /// Used for testing returned view component result.
    /// </summary>
    /// <typeparam name="TInvocationResult">Result from invoked view component in ASP.NET Core MVC.</typeparam>
    public interface IViewComponentShouldReturnTestBuilder<TInvocationResult>
        : IBaseShouldReturnTestBuilder<TInvocationResult, IAndTestBuilder>
    {
        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ContentViewComponentResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Content();

        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ContentViewComponentResult"/> with expected content.
        /// </summary>
        /// <param name="content">Expected content as string.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Content(string content);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ContentViewComponentResult"/> passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions for the content.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Content(Action<string> assertions);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ContentViewComponentResult"/> passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the content.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Content(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Html.IHtmlContent"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder HtmlContent();

        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Html.IHtmlContent"/> with expected content.
        /// </summary>
        /// <param name="htmlContent">Expected HTML content as string.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder HtmlContent(string htmlContent);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Html.IHtmlContent"/> passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions for the HTML content.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder HtmlContent(Action<string> assertions);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Html.IHtmlContent"/> passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the HTML content.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder HtmlContent(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/> with the default view name.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder View();

        /// <summary>       
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/> with the default view name.
        /// </summary>
        /// <param name="viewTestBuilder">Builder for testing the view result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder View(Action<IViewTestBuilder> viewTestBuilder);
    }
}
