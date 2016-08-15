namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.View
{
    using System.Net;
    using Base;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> or <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/>.
    /// </summary>
    public interface IViewTestBuilder : IBaseTestBuilderWithViewFeature
    {
        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> or <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/>
        /// has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code as integer.</param>
        /// <returns>The same <see cref="IAndViewTestBuilder"/>.</returns>
        IAndViewTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> or <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/>
        /// has the same status code as the provided <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> enumeration.</param>
        /// <returns>The same <see cref="IAndViewTestBuilder"/>.</returns>
        IAndViewTestBuilder WithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> or <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/>
        /// has the same content type as the provided string.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same <see cref="IAndViewTestBuilder"/>.</returns>
        IAndViewTestBuilder WithContentType(string contentType);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> or <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/>
        /// has the same content type as the provided <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="contentType">Content type as <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same <see cref="IAndViewTestBuilder"/>.</returns>
        IAndViewTestBuilder WithContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> or <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/>
        /// has the same <see cref="IViewEngine"/> as the provided one.
        /// </summary>
        /// <param name="viewEngine">View engine of type <see cref="IViewEngine"/>.</param>
        /// <returns>The same <see cref="IAndViewTestBuilder"/>.</returns>
        IAndViewTestBuilder WithViewEngine(IViewEngine viewEngine);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> or <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/>
        /// has the same <see cref="IViewEngine"/> type as the provided one.
        /// </summary>
        /// <typeparam name="TViewEngine">View engine of type IViewEngine.</typeparam>
        /// <returns>The same <see cref="IAndViewTestBuilder"/>.</returns>
        IAndViewTestBuilder WithViewEngineOfType<TViewEngine>()
            where TViewEngine : IViewEngine;
    }
}
