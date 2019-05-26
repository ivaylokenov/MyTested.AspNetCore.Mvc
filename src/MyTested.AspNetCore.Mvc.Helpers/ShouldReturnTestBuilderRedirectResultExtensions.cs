namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.RedirectResult"/>, <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>,
    /// <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/> and <see cref="Microsoft.AspNetCore.Mvc.RedirectToPageResult"/> extension
    /// methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderRedirectResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectResult"/>
        /// with the same redirect URL as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="url">Expected redirect URL.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Redirect<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string url)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToUrl(url)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectResult"/>
        /// with a permanent redirect and the same URL as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="url">Expected redirect URL.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectPermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string url)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToUrl(url)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectResult"/>
        /// with a preserved method and the same URL as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="url">Expected redirect URL.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectPreserveMethod<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string url)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToUrl(url)
                    .Permanent(false)
                    .PreservingMethod());

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectResult"/>
        /// with a preserved method, permanent redirect, and the same URL as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="url">Expected redirect URL.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectPermanentPreserveMethod<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string url)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToUrl(url)
                    .Permanent()
                    .PreservingMethod());

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// with the same action name as the called one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToAction<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToAction(null)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// with the same action name as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToAction<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder, 
            string actionName)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToAction(actionName)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// with the same action name and route values as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToAction<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName, 
            object routeValues)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToAction(actionName)
                    .ContainingRouteValues(routeValues)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// with the same action and controller names as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToAction<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            string controllerName)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToAction(actionName)
                    .ToController(controllerName)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// with the same action name, controller name, and route values as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToAction<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            string controllerName,
            object routeValues)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToAction(actionName)
                    .ToController(controllerName)
                    .ContainingRouteValues(routeValues)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// with the same action name, controller name, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToAction<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            string controllerName,
            string fragment)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToAction(actionName)
                    .ToController(controllerName)
                    .WithFragment(fragment)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// with the same action name, controller name, route values, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToAction<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            string controllerName,
            object routeValues,
            string fragment)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToAction(actionName)
                    .ToController(controllerName)
                    .ContainingRouteValues(routeValues)
                    .WithFragment(fragment)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// with a preserved method and the same action name,
        /// controller name, route values, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToActionPreserveMethod<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName = null,
            string controllerName = null,
            object routeValues = null,
            string fragment = null)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToAction(actionName)
                    .ToController(controllerName)
                    .ContainingRouteValues(routeValues)
                    .WithFragment(fragment)
                    .Permanent(false)
                    .PreservingMethod());
        
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// with a permanent redirect and the same action name as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToActionPermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToAction(actionName)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// with a permanent redirect and the same action name and route values as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToActionPermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            object routeValues)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToAction(actionName)
                    .ContainingRouteValues(routeValues)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// with a permanent redirect and the same action and controller names as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToActionPermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            string controllerName)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToAction(actionName)
                    .ToController(controllerName)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// with a permanent redirect and the same action name,
        /// controller name, and route values as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToActionPermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            string controllerName,
            object routeValues)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToAction(actionName)
                    .ToController(controllerName)
                    .ContainingRouteValues(routeValues)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// with a permanent redirect and the same action name,
        /// controller name, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToActionPermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            string controllerName,
            string fragment)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToAction(actionName)
                    .ToController(controllerName)
                    .WithFragment(fragment)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// with a permanent redirect and the same action name, controller name,
        /// route values, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToActionPermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName,
            string controllerName,
            object routeValues,
            string fragment)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToAction(actionName)
                    .ToController(controllerName)
                    .ContainingRouteValues(routeValues)
                    .WithFragment(fragment)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// with a preserved method, permanent redirect, and the same action name,
        /// controller name, route values, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToActionPermanentPreserveMethod<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string actionName = null,
            string controllerName = null,
            object routeValues = null,
            string fragment = null)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToAction(actionName)
                    .ToController(controllerName)
                    .ContainingRouteValues(routeValues)
                    .WithFragment(fragment)
                    .Permanent()
                    .PreservingMethod());

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>
        /// with the same route name as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeName">Expected route name.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToRoute<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string routeName)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToRoute(routeName)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>
        /// with the same route values as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToRoute<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            object routeValues)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ContainingRouteValues(routeValues)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>
        /// with the same route name and route values as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeName">Expected route name.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToRoute<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder, 
            string routeName,
            object routeValues)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToRoute(routeName)
                    .ContainingRouteValues(routeValues)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>
        /// with the same route name and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeName">Expected route name.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToRoute<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string routeName,
            string fragment)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToRoute(routeName)
                    .WithFragment(fragment)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>
        /// with the same route name, route values, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeName">Expected route name.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToRoute<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string routeName,
            object routeValues,
            string fragment)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToRoute(routeName)
                    .ContainingRouteValues(routeValues)
                    .WithFragment(fragment)
                    .Permanent(false)
                    .PreservingMethod(false));
        
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>
        /// with a preserved method and the same route name, route values, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeName">Expected route name.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToRoutePreserveMethod<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string routeName = null,
            object routeValues = null,
            string fragment = null)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToRoute(routeName)
                    .ContainingRouteValues(routeValues)
                    .WithFragment(fragment)
                    .Permanent(false)
                    .PreservingMethod());

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>
        /// with a permanent redirect and the same route name as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeName">Expected route name.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToRoutePermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string routeName)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToRoute(routeName)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>
        /// with a permanent redirect and the same route values as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToRoutePermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            object routeValues)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ContainingRouteValues(routeValues)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>
        /// with a permanent redirect and the same route name, and route values as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeName">Expected route name.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToRoutePermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string routeName,
            object routeValues)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToRoute(routeName)
                    .ContainingRouteValues(routeValues)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>
        /// with a permanent redirect and the same route name, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeName">Expected route name.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToRoutePermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string routeName,
            string fragment)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToRoute(routeName)
                    .WithFragment(fragment)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>
        /// with a permanent redirect and the same route name, route values, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeName">Expected route name.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToRoutePermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string routeName,
            object routeValues,
            string fragment)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToRoute(routeName)
                    .ContainingRouteValues(routeValues)
                    .WithFragment(fragment)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>
        /// with a preserved method, permanent redirect, and the same route name,
        /// route values, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="routeName">Expected route name.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToRoutePermanentPreserveMethod<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string routeName = null,
            object routeValues = null,
            string fragment = null)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToRoute(routeName)
                    .ContainingRouteValues(routeValues)
                    .WithFragment(fragment)
                    .Permanent()
                    .PreservingMethod());

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToPageResult"/>
        /// with the same page name as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="pageName">Expected page name.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToPage<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string pageName)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToPage(pageName)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToPageResult"/>
        /// with the same page name and route values as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="pageName">Expected page name.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToPage<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string pageName,
            object routeValues)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToPage(pageName)
                    .ContainingRouteValues(routeValues)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToPageResult"/>
        /// with the same page name and page handler as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="pageName">Expected page name.</param>
        /// <param name="pageHandler">Expected page handler.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToPage<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string pageName,
            string pageHandler)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToPage(pageName)
                    .WithPageHandler(pageHandler)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToPageResult"/>
        /// with the same page name, page handler, and route values as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="pageName">Expected page name.</param>
        /// <param name="pageHandler">Expected page handler.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToPage<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string pageName,
            string pageHandler,
            object routeValues)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToPage(pageName)
                    .WithPageHandler(pageHandler)
                    .ContainingRouteValues(routeValues)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToPageResult"/>
        /// with the same page name, page handler, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="pageName">Expected page name.</param>
        /// <param name="pageHandler">Expected page handler.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToPage<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string pageName,
            string pageHandler,
            string fragment)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToPage(pageName)
                    .WithPageHandler(pageHandler)
                    .WithFragment(fragment)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToPageResult"/>
        /// with the same page name, page handler, route values, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="pageName">Expected page name.</param>
        /// <param name="pageHandler">Expected page handler.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToPage<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string pageName,
            string pageHandler,
            object routeValues,
            string fragment)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToPage(pageName)
                    .WithPageHandler(pageHandler)
                    .ContainingRouteValues(routeValues)
                    .WithFragment(fragment)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToPageResult"/>
        /// with a permanent redirect and the same page name as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="pageName">Expected page name.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToPagePermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string pageName)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToPage(pageName)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToPageResult"/>
        /// with a permanent redirect and the same page name, and route values as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="pageName">Expected page name.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToPagePermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string pageName,
            object routeValues)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToPage(pageName)
                    .ContainingRouteValues(routeValues)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToPageResult"/>
        /// with a permanent redirect and the same page name, and page handler as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="pageName">Expected page name.</param>
        /// <param name="pageHandler">Expected page handler.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToPagePermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string pageName,
            string pageHandler)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToPage(pageName)
                    .WithPageHandler(pageHandler)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToPageResult"/>
        /// with a permanent redirect and the same page name, page handler, and route values as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="pageName">Expected page name.</param>
        /// <param name="pageHandler">Expected page handler.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToPagePermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string pageName,
            string pageHandler,
            object routeValues)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToPage(pageName)
                    .WithPageHandler(pageHandler)
                    .ContainingRouteValues(routeValues)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToPageResult"/>
        /// with a permanent redirect and the same page name, page handler, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="pageName">Expected page name.</param>
        /// <param name="pageHandler">Expected page handler.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToPagePermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string pageName,
            string pageHandler,
            string fragment)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToPage(pageName)
                    .WithPageHandler(pageHandler)
                    .WithFragment(fragment)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToPageResult"/>
        /// with a permanent redirect and the same page name, page handler, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="pageName">Expected page name.</param>
        /// <param name="pageHandler">Expected page handler.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToPagePermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string pageName,
            string pageHandler,
            object routeValues,
            string fragment)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToPage(pageName)
                    .WithPageHandler(pageHandler)
                    .ContainingRouteValues(routeValues)
                    .WithFragment(fragment)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToPageResult"/>
        /// with a preserved method and the same page name, page handler, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="pageName">Expected page name.</param>
        /// <param name="pageHandler">Expected page handler.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToPagePreserveMethod<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string pageName,
            string pageHandler = null,
            object routeValues = null,
            string fragment = null)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToPage(pageName)
                    .WithPageHandler(pageHandler)
                    .ContainingRouteValues(routeValues)
                    .WithFragment(fragment)
                    .Permanent(false)
                    .PreservingMethod());

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectToPageResult"/>
        /// with a preserved method, permanent redirect, and
        /// the same page name, page handler, and fragment as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="pageName">Expected page name.</param>
        /// <param name="pageHandler">Expected page handler.</param>
        /// <param name="routeValues">Expected route values.</param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder RedirectToPagePermanentPreserveMethod<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string pageName,
            string pageHandler = null,
            object routeValues = null,
            string fragment = null)
            => shouldReturnTestBuilder
                .Redirect(result => result
                    .ToPage(pageName)
                    .WithPageHandler(pageHandler)
                    .ContainingRouteValues(routeValues)
                    .WithFragment(fragment)
                    .Permanent()
                    .PreservingMethod());
    }
}
