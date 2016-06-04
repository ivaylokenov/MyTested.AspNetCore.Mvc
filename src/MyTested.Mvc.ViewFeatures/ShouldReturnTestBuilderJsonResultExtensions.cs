namespace MyTested.Mvc
{
    using Builders.ActionResults.Json;
    using Builders.Actions.ShouldReturn;
    using Builders.Contracts.ActionResults.Json;
    using Builders.Contracts.Actions;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contains <see cref="JsonResult"/> extension methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderJsonResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="JsonResult"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <returns>Test builder of <see cref="IJsonTestBuilder"/> type.</returns>
        public static IJsonTestBuilder Json<TActionResult>(this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder)
        {
            var actualShouldReturnTestBuilder = (ShouldReturnTestBuilder<TActionResult>)shouldReturnTestBuilder;

            actualShouldReturnTestBuilder.TestContext.ActionResult = actualShouldReturnTestBuilder.GetReturnObject<JsonResult>();

            return new JsonTestBuilder(actualShouldReturnTestBuilder.TestContext);
        }
    }
}
