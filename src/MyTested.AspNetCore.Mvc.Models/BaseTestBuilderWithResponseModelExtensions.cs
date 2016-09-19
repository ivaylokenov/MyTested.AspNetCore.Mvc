namespace MyTested.AspNetCore.Mvc
{
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Models;
    using Builders.Models;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Contains model extension methods for <see cref="IBaseTestBuilderWithResponseModel"/>.
    /// </summary>
    public static class BaseTestBuilderWithResponseModelExtensions
    {
        /// <summary>
        /// Tests whether model of the given type is returned from the invoked method.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithResponseModel"/> type.</param>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TModel}"/>.</returns>
        public static IAndModelDetailsTestBuilder<TModel> WithModelOfType<TModel>(
            this IBaseTestBuilderWithResponseModel builder)
        {
            var actualBuilder = (BaseTestBuilderWithResponseModel)builder;
            
            var actualResponseDataType = actualBuilder.GetReturnType();
            var expectedResponseDataType = typeof(TModel);

            var responseDataTypeIsAssignable = Reflection.AreAssignable(
                    expectedResponseDataType,
                    actualResponseDataType);

            if (!responseDataTypeIsAssignable)
            {
                throw new ResponseModelAssertionException(string.Format(
                    actualBuilder.OfTypeErrorMessageFormat,
                    actualBuilder.TestContext.ExceptionMessagePrefix,
                    typeof(TModel).ToFriendlyTypeName(),
                    actualResponseDataType.ToFriendlyTypeName()));
            }

            actualBuilder.TestContext.Model = actualBuilder.GetActualModel<TModel>();
            return new ModelDetailsTestBuilder<TModel>(actualBuilder.TestContext);
        }

        /// <summary>
        /// Tests whether a deeply equal object to the provided one is returned from the invoked method.
        /// </summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithResponseModel"/> type.</param>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TModel}"/>.</returns>
        public static IAndModelDetailsTestBuilder<TModel> WithModel<TModel>(
            this IBaseTestBuilderWithResponseModel builder,
            TModel expectedModel)
        {
            var actualBuilder = (BaseTestBuilderWithResponseModel)builder;

            actualBuilder.WithModelOfType<TModel>();

            var actualModel = actualBuilder.GetActualModel<TModel>();
            if (Reflection.AreNotDeeplyEqual(expectedModel, actualModel))
            {
                throw new ResponseModelAssertionException(string.Format(
                    actualBuilder.ErrorMessageFormat,
                    actualBuilder.TestContext.ExceptionMessagePrefix,
                    typeof(TModel).ToFriendlyTypeName()));
            }

            actualBuilder.TestContext.Model = actualModel;
            return new ModelDetailsTestBuilder<TModel>(actualBuilder.TestContext);
        }
    }
}
