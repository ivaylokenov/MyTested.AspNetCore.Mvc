namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using System;
    using Contracts.Base;
    using Contracts.Models;
    using Internal;
    using Internal.TestContexts;
    using Exceptions;
    using Models;
    using Utilities;

    public abstract class BaseTestBuilderWithResponseModel : BaseTestBuilderWithActionContext, IBaseTestBuilderWithResponseModel
    {
        public BaseTestBuilderWithResponseModel(ActionTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets or sets the error message format for the response model assertions.
        /// </summary>
        /// <value>String value.</value>
        protected string ErrorMessageFormat { get; set; } = ExceptionMessageFormats.ResponseModel;

        /// <summary>
        /// Gets or sets the error message format for the response model type assertions.
        /// </summary>
        /// <value>String value.</value>
        protected string OfTypeErrorMessageFormat { get; set; } = ExceptionMessageFormats.ResponseModelOfType;

        /// <inheritdoc />
        public IAndModelDetailsTestBuilder<TModel> WithModelOfType<TModel>()
        {
            var actualResponseDataType = this.GetReturnType();
            var expectedResponseDataType = typeof(TModel);

            var responseDataTypeIsAssignable = Reflection.AreAssignable(
                    expectedResponseDataType,
                    actualResponseDataType);

            if (!responseDataTypeIsAssignable)
            {
                throw new ResponseModelAssertionException(string.Format(
                    this.OfTypeErrorMessageFormat,
                    this.TestContext.ExceptionMessagePrefix,
                    typeof(TModel).ToFriendlyTypeName(),
                    actualResponseDataType.ToFriendlyTypeName()));
            }

            this.TestContext.Model = this.GetActualModel<TModel>();
            return new ModelDetailsTestBuilder<TModel>(this.TestContext);
        }

        /// <inheritdoc />
        public IAndModelDetailsTestBuilder<TModel> WithModel<TModel>(TModel expectedModel)
        {
            this.WithModelOfType<TModel>();

            var actualModel = this.GetActualModel<TModel>();
            if (Reflection.AreNotDeeplyEqual(expectedModel, actualModel))
            {
                throw new ResponseModelAssertionException(string.Format(
                    this.ErrorMessageFormat,
                    this.TestContext.ExceptionMessagePrefix,
                    typeof(TModel).ToFriendlyTypeName()));
            }

            this.TestContext.Model = actualModel;
            return new ModelDetailsTestBuilder<TModel>(this.TestContext);
        }

        protected abstract TModel GetActualModel<TModel>();

        protected abstract Type GetReturnType();
    }
}
