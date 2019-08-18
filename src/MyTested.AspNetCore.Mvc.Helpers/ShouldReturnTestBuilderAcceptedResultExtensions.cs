namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.AcceptedResult"/>, <see cref = "Microsoft.AspNetCore.Mvc.AcceptedAtActionResult" />
    /// and <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtRouteResult"/> extension
    /// methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderAcceptedResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedResult"/>
        /// with the same deeply equal model as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Accepted<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            TModel model)
            => shouldReturnTestBuilder
                .Accepted(result => result
                    .WithModel(model));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedResult"/>
        /// with the same URI as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="uri">Expected URI.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Accepted<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string uri)
            => shouldReturnTestBuilder
                .Accepted(result => result
                    .AtLocation(uri));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedResult"/>
        /// with the same URI as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="uri">Expected URI.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Accepted<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            Uri uri)
            => shouldReturnTestBuilder
                .Accepted(result => result
                    .AtLocation(uri));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedResult"/>
        /// with the same URI and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="uri">Expected URI.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Accepted<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string uri,
            TModel model)
            => shouldReturnTestBuilder
                .Accepted(result => result
                    .AtLocation(uri)
                    .WithModel(model));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedResult"/>
        /// with the same URI and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="uri">Expected URI.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Accepted<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            Uri uri,
            TModel model)
            => shouldReturnTestBuilder
                .Accepted(result => result
                    .AtLocation(uri)
                    .WithModel(model));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtActionResult"/>
        /// with the same action name as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder AcceptedAtAction<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName)
            => shouldReturnTestBuilder
                .Accepted(result => result
                    .AtAction(actionName));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtActionResult"/>
        /// with the same action and controller names as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder AcceptedAtAction<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            string controllerName)
            => shouldReturnTestBuilder
                .Accepted(result => result
                    .AtAction(actionName)
                    .AtController(controllerName));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtActionResult"/>
        /// with the same action name and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder AcceptedAtAction<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            TModel model)
            => shouldReturnTestBuilder
                .Accepted(result => result
                    .AtAction(actionName)
                    .WithModel(model));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtActionResult"/>
        /// with the same action name, controller name, and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder AcceptedAtAction<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            string controllerName,
            TModel model)
            => shouldReturnTestBuilder
                .Accepted(result => result
                    .AtAction(actionName)
                    .AtController(controllerName)
                    .WithModel(model));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtActionResult"/>
        /// with the same action name, route values, and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder AcceptedAtAction<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            object routeValues,
            TModel model)
            => shouldReturnTestBuilder
                .Accepted(result => result
                    .AtAction(actionName)
                    .ContainingRouteValues(routeValues)
                    .WithModel(model));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtActionResult"/>
        /// with the same action name, controller name, route values, and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder AcceptedAtAction<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            string controllerName,
            object routeValues,
            TModel model)
            => shouldReturnTestBuilder
                .Accepted(result => result
                    .AtAction(actionName)
                    .AtController(controllerName)
                    .ContainingRouteValues(routeValues)
                    .WithModel(model));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtRouteResult"/>
        /// with the same route values as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder AcceptedAtRoute<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            object routeValues)
             => shouldReturnTestBuilder
                 .Accepted(result => result
                     .ContainingRouteValues(routeValues));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtRouteResult"/>
        /// with the same route name as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeName">Expected route name.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder AcceptedAtRoute<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string routeName)
            => shouldReturnTestBuilder
                .Accepted(result => result
                    .AtRoute(routeName));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtRouteResult"/>
        /// with the same route name and route values as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeName">Expected route name.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder AcceptedAtRoute<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string routeName,
            object routeValues)
            => shouldReturnTestBuilder
                .Accepted(result => result
                    .AtRoute(routeName)
                    .ContainingRouteValues(routeValues));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtRouteResult"/>
        /// with the same route values and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder AcceptedAtRoute<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            object routeValues, 
            TModel model)
            => shouldReturnTestBuilder
                .Accepted(result => result
                    .ContainingRouteValues(routeValues)
                    .WithModel(model));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtRouteResult"/>
        /// with the same route name, route values, and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeName">Expected route name.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder AcceptedAtRoute<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string routeName,
            object routeValues,
            TModel model)
            => shouldReturnTestBuilder
                .Accepted(result => result
                    .AtRoute(routeName)
                    .ContainingRouteValues(routeValues)
                    .WithModel(model));
    }
}
