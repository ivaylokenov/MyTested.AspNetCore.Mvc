namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.JsonResult"/>
    /// extension methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderJsonResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.JsonResult"/>
        /// with the same deeply equal model and JSON serializer settings as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <param name="serializerSettings">Expected JSON serializer settings.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Json<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            TModel model,
            JsonSerializerSettings serializerSettings)
            => shouldReturnTestBuilder
                .Json(view => view
                    .WithJsonSerializerSettings(serializerSettings)
                    .WithModel(model));
    }
}
