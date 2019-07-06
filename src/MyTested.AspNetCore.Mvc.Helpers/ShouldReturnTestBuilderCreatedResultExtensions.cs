namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>, <see cref = "Microsoft.AspNetCore.Mvc.CreatedAtActionResult" />
    /// and <see cref="Microsoft.AspNetCore.Mvc.CreatedAtRouteResult"/> extension
    /// methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderCreatedResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>
        /// with the same URI and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="uri">Expected URI.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Created<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string uri,
            TModel model)
            => shouldReturnTestBuilder
                .Created(result => result
                    .AtLocation(uri)
                    .WithModel(model));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>
        /// with the same URI and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="uri">Expected URI.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Created<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            Uri uri,
            TModel model)
            => shouldReturnTestBuilder
                .Created(result => result
                    .AtLocation(uri)
                    .WithModel(model));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.CreatedAtActionResult"/>
        /// with the same action name and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder CreatedAtAction<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            TModel model)
            => shouldReturnTestBuilder
                .Created(result => result
                    .AtAction(actionName)
                    .WithModel(model));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.CreatedAtActionResult"/>
        /// with the same action name, route values, and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder CreatedAtAction<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            object routeValues,
            TModel model)
            => shouldReturnTestBuilder
                .Created(result => result
                    .AtAction(actionName)
                    .ContainingRouteValues(routeValues)
                    .WithModel(model));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.CreatedAtActionResult"/>
        /// with the same action name, controller name, and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder CreatedAtAction<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            string controllerName,
            TModel model)
            => shouldReturnTestBuilder
                .Created(result => result
                    .AtAction(actionName)
                    .AtController(controllerName)
                    .WithModel(model));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.CreatedAtActionResult"/>
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
        public static IAndTestBuilder CreatedAtAction<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            string controllerName,
            object routeValues,
            TModel model)
            => shouldReturnTestBuilder
                .Created(result => result
                    .AtAction(actionName)
                    .AtController(controllerName)
                    .ContainingRouteValues(routeValues)
                    .WithModel(model));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.CreatedAtRouteResult"/>
        /// with the same route name and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeName">Expected route name.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder CreatedAtRoute<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string routeName,
            TModel model)
            => shouldReturnTestBuilder
                .Created(result => result
                    .AtRoute(routeName)
                    .WithModel(model));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.CreatedAtRouteResult"/>
        /// with the same route values and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder CreatedAtRoute<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            object routeValues,
            TModel model)
            => shouldReturnTestBuilder
                .Created(result => result
                    .ContainingRouteValues(routeValues)
                    .WithModel(model));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.CreatedAtRouteResult"/>
        /// with the same route name, route values, and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeName">Expected route name.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder CreatedAtRoute<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string routeName,
            object routeValues,
            TModel model)
            => shouldReturnTestBuilder
                .Created(result => result
                    .AtRoute(routeName)
                    .ContainingRouteValues(routeValues)
                    .WithModel(model));
    }
}
