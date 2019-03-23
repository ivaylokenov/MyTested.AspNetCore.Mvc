namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using Contracts.Base;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Exceptions;

    /// <summary>
    /// Base class for all test builders with view features.
    /// </summary>
    /// <typeparam name="TViewResult">Type of view result - <see cref="ViewResult"/>, <see cref="PartialViewResult"/> or <see cref="ViewComponentResult"/>.</typeparam>
    public abstract class BaseTestBuilderWithViewFeature<TViewResult> : BaseTestBuilderWithResponseModel<TViewResult>
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
        
        public override object GetActualModel()
        {
            if (this.ActionResult is ViewResult)
            {
                return (this.ActionResult as ViewResult).Model;
            }

            return (this.ActionResult as PartialViewResult)?.ViewData?.Model;
        }

        public override void ValidateNoModel()
        {
            if (this.GetActualModel() != null)
            {
                throw new ResponseModelAssertionException(string.Format(
                    "{0} to not have a view model but in fact such was found.",
                    this.TestContext.ExceptionMessagePrefix));
            }
        }
    }
}
