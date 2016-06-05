namespace MyTested.Mvc.Builders.Base
{
    using Contracts.Base;
    using Contracts.Models;
    using Contracts.ShouldPassFor;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using ShouldPassFor;

    /// <summary>
    /// Base class for all test builders with view features.
    /// </summary>
    /// <typeparam name="TViewResult">Type of view result - <see cref="ViewResult"/>, <see cref="PartialViewResult"/> or <see cref="ViewComponentResult"/>.</typeparam>
    public abstract class BaseTestBuilderWithViewFeature<TViewResult>
        : BaseTestBuilderWithResponseModel<TViewResult>, IBaseTestBuilderWithViewFeature
        where TViewResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithViewFeature{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseTestBuilderWithViewFeature(ControllerTestContext testContext)
            : base(testContext)
        {
        }
        
        /// <inheritdoc />
        public IModelDetailsTestBuilder<TModel> WithModel<TModel>(TModel model) => this.WithResponseModel(model);

        /// <inheritdoc />
        public IModelDetailsTestBuilder<TModel> WithModelOfType<TModel>() => this.WithResponseModelOfType<TModel>();

        protected override object GetActualModel()
        {
            if (this.ActionResult is ViewResult)
            {
                return (this.ActionResult as ViewResult).Model;
            }

            return (this.ActionResult as PartialViewResult)?.ViewData?.Model;
        }

        /// <inheritdoc />
        IShouldPassForTestBuilderWithActionResult<ActionResult> IBaseTestBuilderWithActionResult<ActionResult>.ShouldPassFor()
            => new ShouldPassForTestBuilderWithActionResult<ActionResult>(this.TestContext);
    }
}
