namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Invocations;
    using Builders.Contracts.ViewComponentResults;

    /// <summary>
    /// Contains extension methods for <see cref="IViewComponentShouldReturnTestBuilder{TInvocationResult}"/>.
    /// </summary>
    public static class ViewComponentShouldReturnTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/>
        /// with the provided deeply equal model object.
        /// </summary>
        /// <typeparam name="TInvocationResult">Type of invocation result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="builder">Instance of <see cref="IViewComponentShouldReturnTestBuilder{TInvocationResult}"/> type.</param>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IAndViewTestBuilder"/> type.</returns>
        public static IAndViewTestBuilder View<TInvocationResult, TModel>(
            this IViewComponentShouldReturnTestBuilder<TInvocationResult> builder,
            TModel model) 
            => builder.View(null, model);

        /// <summary>
        /// Tests whether the view component result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/>
        /// with the provided view name and deeply equal model object.
        /// </summary>
        /// <typeparam name="TInvocationResult">Type of invocation result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="builder">Instance of <see cref="IViewComponentShouldReturnTestBuilder{TInvocationResult}"/> type.</param>
        /// <param name="viewName">Expected view name.</param>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IAndViewTestBuilder"/> type.</returns>
        public static IAndViewTestBuilder View<TInvocationResult, TModel>(
            this IViewComponentShouldReturnTestBuilder<TInvocationResult> builder,
            string viewName,
            TModel model)
        {
            var viewTestBuilder = builder.View(viewName);
            viewTestBuilder.WithModel(model);
            return viewTestBuilder;
        }
    }
}
