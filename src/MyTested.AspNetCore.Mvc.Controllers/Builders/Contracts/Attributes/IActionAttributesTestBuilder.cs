namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Routing;

    using HttpMethod = System.Net.Http.HttpMethod;

    /// <summary>
    /// Used for testing action attributes.
    /// </summary>
    public interface IActionAttributesTestBuilder : IControllerActionAttributesTestBuilder<IAndActionAttributesTestBuilder>
    {
        /// <summary>
        /// Tests whether the action attributes contain <see cref="Microsoft.AspNetCore.Mvc.ActionNameAttribute"/>.
        /// </summary>
        /// <param name="actionName">Expected overridden name of the action.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        IAndActionAttributesTestBuilder ChangingActionNameTo(string actionName);
        
        /// <summary>
        /// Tests whether the action attributes contain <see cref="Microsoft.AspNetCore.Mvc.NonActionAttribute"/>.
        /// </summary>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        IAndActionAttributesTestBuilder DisablingActionCall();
        
        /// <summary>
        /// Tests whether the action attributes restrict the request to a specific HTTP method (<see cref="Microsoft.AspNetCore.Mvc.AcceptVerbsAttribute"/> or the specific <see cref="Microsoft.AspNetCore.Mvc.HttpGetAttribute"/>, <see cref="Microsoft.AspNetCore.Mvc.HttpPostAttribute"/>, etc.).
        /// </summary>
        /// <typeparam name="THttpMethod">Attribute of type <see cref="IActionHttpMethodProvider"/>.</typeparam>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        IAndActionAttributesTestBuilder RestrictingForHttpMethod<THttpMethod>()
            where THttpMethod : Attribute, IActionHttpMethodProvider, new();

        /// <summary>
        /// Tests whether the action attributes restrict the request to a specific HTTP method (<see cref="Microsoft.AspNetCore.Mvc.AcceptVerbsAttribute"/> or the specific <see cref="Microsoft.AspNetCore.Mvc.HttpGetAttribute"/>, <see cref="Microsoft.AspNetCore.Mvc.HttpPostAttribute"/>, etc.).
        /// </summary>
        /// <param name="httpMethod">HTTP method provided as string.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        IAndActionAttributesTestBuilder RestrictingForHttpMethod(string httpMethod);

        /// <summary>
        /// Tests whether the action attributes restrict the request to a specific HTTP method (<see cref="Microsoft.AspNetCore.Mvc.AcceptVerbsAttribute"/> or the specific <see cref="Microsoft.AspNetCore.Mvc.HttpGetAttribute"/>, <see cref="Microsoft.AspNetCore.Mvc.HttpPostAttribute"/>, etc.).
        /// </summary>
        /// <param name="httpMethod">HTTP method provided as <see cref="HttpMethod"/> class.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        IAndActionAttributesTestBuilder RestrictingForHttpMethod(HttpMethod httpMethod);

        /// <summary>
        /// Tests whether the action attributes restrict the request to a specific HTTP methods (<see cref="Microsoft.AspNetCore.Mvc.AcceptVerbsAttribute"/> or the specific <see cref="Microsoft.AspNetCore.Mvc.HttpGetAttribute"/>, <see cref="Microsoft.AspNetCore.Mvc.HttpPostAttribute"/>, etc.).
        /// </summary>
        /// <param name="httpMethods">HTTP methods provided as collection of strings.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        IAndActionAttributesTestBuilder RestrictingForHttpMethods(IEnumerable<string> httpMethods);

        /// <summary>
        /// Tests whether the action attributes restrict the request to a specific HTTP methods (<see cref="Microsoft.AspNetCore.Mvc.AcceptVerbsAttribute"/> or the specific <see cref="Microsoft.AspNetCore.Mvc.HttpGetAttribute"/>, <see cref="Microsoft.AspNetCore.Mvc.HttpPostAttribute"/>, etc.).
        /// </summary>
        /// <param name="httpMethods">HTTP methods provided as string parameters.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        IAndActionAttributesTestBuilder RestrictingForHttpMethods(params string[] httpMethods);

        /// <summary>
        /// Tests whether the action attributes restrict the request to a specific HTTP methods (<see cref="Microsoft.AspNetCore.Mvc.AcceptVerbsAttribute"/> or the specific <see cref="Microsoft.AspNetCore.Mvc.HttpGetAttribute"/>, <see cref="Microsoft.AspNetCore.Mvc.HttpPostAttribute"/>, etc.).
        /// </summary>
        /// <param name="httpMethods">HTTP methods provided as collection of <see cref="HttpMethod"/> classes.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        IAndActionAttributesTestBuilder RestrictingForHttpMethods(IEnumerable<HttpMethod> httpMethods);

        /// <summary>
        /// Tests whether the action attributes restrict the request to a specific HTTP methods (<see cref="Microsoft.AspNetCore.Mvc.AcceptVerbsAttribute"/> or the specific <see cref="Microsoft.AspNetCore.Mvc.HttpGetAttribute"/>, <see cref="Microsoft.AspNetCore.Mvc.HttpPostAttribute"/>, etc.).
        /// </summary>
        /// <param name="httpMethods">HTTP methods provided as parameters of <see cref="HttpMethod"/> class.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        IAndActionAttributesTestBuilder RestrictingForHttpMethods(params HttpMethod[] httpMethods);
    }
}
