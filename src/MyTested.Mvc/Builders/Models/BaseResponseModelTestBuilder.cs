namespace MyTested.Mvc.Builders.Models
{
    using System;
    using Base;
    using Contracts.Models;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Utilities;

    /// <summary>
    /// Base class for all response model test builders.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC 6 controller.</typeparam>
    public abstract class BaseResponseModelTestBuilder<TActionResult>
        : BaseTestBuilderWithActionResult<TActionResult>, IBaseResponseModelTestBuilder
    {
        private const string ErrorMessage = "When calling {0} action in {1} expected response model {2} to be the given model, but in fact it was a different.";
        private const string OfTypeErrorMessage = "When calling {0} action in {1} expected response model to be of {2} type, but instead received {3}.";

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseResponseModelTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        protected BaseResponseModelTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
            this.ErrorMessageFormat = ErrorMessage;
            this.OfTypeErrorMessageFormat = OfTypeErrorMessage;
        }

        /// <summary>
        /// Gets or sets the error message format for the response model assertions.
        /// </summary>
        /// <value>String value.</value>
        protected string ErrorMessageFormat { get; set; }

        /// <summary>
        /// Gets or sets the error message format for the response model type assertions.
        /// </summary>
        /// <value>String value.</value>
        protected string OfTypeErrorMessageFormat { get; set; }

        /// <summary>
        /// Tests whether certain type of response model is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        public IModelDetailsTestBuilder<TResponseModel> WithResponseModelOfType<TResponseModel>()
        {
            var actionResultType = this.ActionResult.GetType();
            var objectResultType = typeof(ObjectResult);

            var actualResponseDataType = actionResultType;
            var expectedResponseDataType = typeof(TResponseModel);
            
            var objectResultIsAssignable = Reflection.AreAssignable(
                objectResultType,
                actionResultType);

            if (!objectResultIsAssignable)
            {
                // action result do not inherit ObjectResult
                actualResponseDataType = this.GetNonObjectResultModelType();
            }
            else
            {
                actualResponseDataType = (this.ActionResult as ObjectResult)?.Value?.GetType();
            }
            
            var responseDataTypeIsAssignable = Reflection.AreAssignable(
                    expectedResponseDataType,
                    actualResponseDataType);

            if (!responseDataTypeIsAssignable)
            {
                throw new ResponseModelAssertionException(string.Format(
                    this.OfTypeErrorMessageFormat,
                    this.ActionName,
                    this.Controller.GetName(),
                    typeof(TResponseModel).ToFriendlyTypeName(),
                    actualResponseDataType.ToFriendlyTypeName()));
            }

            return new ModelDetailsTestBuilder<TResponseModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.GetActualModel<TResponseModel>());
        }

        /// <summary>
        /// Tests whether a deeply equal object to the provided one is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IModelDetailsTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(TResponseModel expectedModel)
        {
            this.WithResponseModelOfType<TResponseModel>();

            var actualModel = this.GetActualModel<TResponseModel>();
            if (Reflection.AreNotDeeplyEqual(expectedModel, actualModel))
            {
                throw new ResponseModelAssertionException(string.Format(
                            this.ErrorMessageFormat,
                            this.ActionName,
                            this.Controller.GetName(),
                            typeof(TResponseModel).ToFriendlyTypeName()));
            }

            return new ModelDetailsTestBuilder<TResponseModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                actualModel);
        }

        private Type GetNonObjectResultModelType()
        {
            if (this.ActionResult is JsonResult)
            {
                return (this.ActionResult as JsonResult)?.Value?.GetType();
            }

            if (this.ActionResult is ViewResult)
            {
                return (this.ActionResult as ViewResult)?.Model?.GetType();
            }

            if (this.ActionResult is PartialViewResult)
            {
                return (this.ActionResult as PartialViewResult)?.ViewData?.Model?.GetType();
            }

            return null;
        }
    }
}
