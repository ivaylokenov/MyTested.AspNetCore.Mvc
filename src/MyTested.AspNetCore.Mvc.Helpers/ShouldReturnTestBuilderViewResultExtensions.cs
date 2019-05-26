namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.View;
    using Builders.Contracts.Actions;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/>
    /// extension methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderViewResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/>
        /// with the provided deeply equal model object.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndViewTestBuilder"/> type.</returns>
        public static IAndViewTestBuilder View<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> builder,
            TModel model) 
            => builder.View(null, model);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/>
        /// with the provided view name and deeply equal model object.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="viewName">Expected view name.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndViewTestBuilder"/> type.</returns>
        public static IAndViewTestBuilder View<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> builder,
            string viewName,
            TModel model)
        {
            var viewTestBuilder = builder.View(viewName);
            viewTestBuilder.WithModel(model);
            return viewTestBuilder;
        }
    }
}
