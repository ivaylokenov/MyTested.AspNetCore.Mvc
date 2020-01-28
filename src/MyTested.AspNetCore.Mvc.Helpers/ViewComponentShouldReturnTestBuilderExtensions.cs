namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.And;
    using Builders.Contracts.Invocations;

    /// <summary>
    /// Contains extension methods for <see cref="IViewComponentShouldReturnTestBuilder{TInvocationResult}"/>.
    /// </summary>
    public static class ViewComponentShouldReturnTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/>
        /// with the same view name as the provided one.
        /// </summary>
        /// <typeparam name="TInvocationResult">Type of invocation result.</typeparam>
        /// <param name="builder">Instance of <see cref="IViewComponentShouldReturnTestBuilder{TInvocationResult}"/> type.</param>
        /// <param name="viewName">Expected view name.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder View<TInvocationResult>(
            this IViewComponentShouldReturnTestBuilder<TInvocationResult> builder,
            string viewName)
            => builder
                .View(view => view
                    .WithName(viewName));

        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/>
        /// with the provided deeply equal model object.
        /// </summary>
        /// <typeparam name="TInvocationResult">Type of invocation result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="builder">Instance of <see cref="IViewComponentShouldReturnTestBuilder{TInvocationResult}"/> type.</param>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder View<TInvocationResult, TModel>(
            this IViewComponentShouldReturnTestBuilder<TInvocationResult> builder,
            TModel model) 
            => builder
                .View(view => view
                    .WithDefaultName()
                    .WithModel(model));

        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/>
        /// with the provided view name and deeply equal model object.
        /// </summary>
        /// <typeparam name="TInvocationResult">Type of invocation result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="builder">Instance of <see cref="IViewComponentShouldReturnTestBuilder{TInvocationResult}"/> type.</param>
        /// <param name="viewName">Expected view name.</param>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder View<TInvocationResult, TModel>(
            this IViewComponentShouldReturnTestBuilder<TInvocationResult> builder,
            string viewName,
            TModel model)
            => builder
                .View(view => view
                    .WithName(viewName)
                    .WithModel(model));
    }
}
