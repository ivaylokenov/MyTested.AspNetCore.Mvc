namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.View
{
    using Base;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Exceptions;
    using Internal;

    /// <summary>
    /// Base class for all test builders with view features.
    /// </summary>
    /// <typeparam name="TViewResult">Type of view result - <see cref="ViewResult"/>,
    /// <see cref="PartialViewResult"/> or <see cref="ViewComponentResult"/>.</typeparam>
    public abstract class BaseTestBuilderWithViewFeatureResult<TViewResult> : BaseTestBuilderWithResponseModel<TViewResult>
        where TViewResult : ActionResult
    {
        private readonly string viewFeatureType;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithViewFeatureResult{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext">
        /// <see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.
        /// </param>
        /// <param name="viewFeatureType">View feature type name.</param>
        protected BaseTestBuilderWithViewFeatureResult(
            ControllerTestContext testContext,
            string viewFeatureType)
            : base(testContext)
            => this.viewFeatureType = viewFeatureType;
        
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

        /// <summary>
        /// Throws new <see cref="ViewResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => throw new ViewResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                this.viewFeatureType,
                propertyName,
                expectedValue,
                actualValue));
    }
}
