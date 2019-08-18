namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using System;
    using Contracts.Base;
    using Internal;
    using Internal.TestContexts;
    using Exceptions;
    using Utilities;

    public abstract class BaseTestBuilderWithResponseModel : BaseTestBuilderWithActionContext, IBaseTestBuilderWithResponseModel
    {
        protected BaseTestBuilderWithResponseModel(ActionTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets or sets the error message format for the response model assertions.
        /// </summary>
        /// <value>String value.</value>
        public string ErrorMessageFormat { get; set; } = ExceptionMessages.ResponseModelFormat;

        /// <summary>
        /// Gets or sets the error message format for the response model type assertions.
        /// </summary>
        /// <value>String value.</value>
        public string OfTypeErrorMessageFormat { get; set; } = ExceptionMessages.ResponseModelOfTypeFormat;
        
        public virtual TModel GetActualModel<TModel>()
        {
            try
            {
                return (TModel)this.GetActualModel();
            }
            catch (InvalidCastException)
            {
                throw new ResponseModelAssertionException(string.Format(
                    "{0} response model to be a {1}, but instead received null.",
                    this.TestContext.ExceptionMessagePrefix,
                    typeof(TModel).ToFriendlyTypeName()));
            }
        }

        public abstract object GetActualModel();

        public abstract Type GetModelReturnType();

        public abstract void ValidateNoModel();
    }
}
