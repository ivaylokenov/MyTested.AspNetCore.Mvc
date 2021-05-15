namespace MyTested.AspNetCore.Mvc
{
    using Builders.And;
    using Builders.Base;
    using Builders.Contracts.And;
    using Builders.Contracts.Base;
    using Builders.Contracts.Models;
    using Builders.Models;
    using Exceptions;
    using System;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Contains model extension methods for <see cref="IBaseTestBuilderWithResponseModel"/>.
    /// </summary>
    public static class BaseTestBuilderWithResponseModelExtensions
    {
        /// <summary>
        /// Tests whether no model is returned from the invoked action.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithResponseModel"/> type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder WithNoModel(this IBaseTestBuilderWithResponseModel builder)
        {
            var actualBuilder = (BaseTestBuilderWithResponseModel)builder;

            actualBuilder.ValidateNoModel();

            return new AndTestBuilder(actualBuilder.TestContext);
        }

        /// <summary>
        /// Tests whether model of the given type is returned from the invoked method.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithResponseModel"/> type.</param>
        /// <param name="modelType">Expected model type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder WithModelOfType(
            this IBaseTestBuilderWithResponseModel builder,
            Type modelType)
        {
            var actualBuilder = (BaseTestBuilderWithResponseModel)builder;
            
            InvocationResultValidator.ValidateInvocationResultType(
                actualBuilder.TestContext,
                modelType,
                canBeAssignable: true,
                allowDifferentGenericTypeDefinitions: true,
                typeOfActualReturnValue: actualBuilder.GetModelReturnType());

            actualBuilder.TestContext.Model = actualBuilder.GetActualModel();

            return new AndTestBuilder(actualBuilder.TestContext);
        }

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
            
            var actualModelType = actualBuilder.GetModelReturnType();
            var expectedModelType = typeof(TModel);

            var modelIsAssignable = Reflection.AreAssignable(
                    expectedModelType,
                    actualModelType);
            
            if (!modelIsAssignable)
            {
                var (expectedModelName, actualModelName) = (expectedModelType, actualModelType).GetTypeComparisonNames();

                throw new ResponseModelAssertionException(string.Format(
                    actualBuilder.OfTypeErrorMessageFormat,
                    actualBuilder.TestContext.ExceptionMessagePrefix,
                    expectedModelName,
                    actualModelName));
            }

            actualBuilder.TestContext.Model = actualBuilder.GetActualModel<TModel>();

            return new ModelDetailsTestBuilder<TModel>(actualBuilder.TestContext);
        }

        /// <summary>
        /// Tests whether model of the given type is returned from the invoked method and whether the provided predicate is passing.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithResponseModel"/> type.</param>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="predicate">Predicate testing the model.</param>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TModel}"/>.</returns>
        public static IAndModelDetailsTestBuilder<TModel> WithModelOfType<TModel>(
            this IBaseTestBuilderWithResponseModel builder,
            Func<TModel, bool> predicate)
        {
            var actualBuilder = (BaseTestBuilderWithResponseModel)builder;

            actualBuilder.WithModelOfType<TModel>();

            if (!predicate(actualBuilder.TestContext.ModelAs<TModel>()))
            {
                throw new ResponseModelAssertionException(string.Format(
                    "{0} response model {1} to pass the given predicate, but it failed.",
                    actualBuilder.TestContext.ExceptionMessagePrefix,
                    typeof(TModel).ToFriendlyTypeName()));
            }

            actualBuilder.TestContext.Model = actualBuilder.GetActualModel<TModel>();

            return new ModelDetailsTestBuilder<TModel>(actualBuilder.TestContext);
        }

        /// <summary>
        /// Tests whether model of the given type is returned from the invoked method and whether the provided assertions are passing.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithResponseModel"/> type.</param>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="assertions">Method containing all assertions for the model.</param>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TModel}"/>.</returns>
        public static IAndModelDetailsTestBuilder<TModel> WithModelOfType<TModel>(
            this IBaseTestBuilderWithResponseModel builder,
            Action<TModel> assertions)
        {
            var actualBuilder = (BaseTestBuilderWithResponseModel)builder;

            actualBuilder.WithModelOfType<TModel>();

            assertions(actualBuilder.TestContext.ModelAs<TModel>());

            actualBuilder.TestContext.Model = actualBuilder.GetActualModel<TModel>();

            return new ModelDetailsTestBuilder<TModel>(actualBuilder.TestContext);
        }

        /// <summary>
        /// Tests whether a deeply equal object to the provided one is returned from the invoked method.
        /// </summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithResponseModel"/> type.</param>
        /// <param name="model">Expected model to be returned from the action result.</param>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TModel}"/>.</returns>
        public static IAndModelDetailsTestBuilder<TModel> WithModel<TModel>(
            this IBaseTestBuilderWithResponseModel builder,
            TModel model)
        {
            var actualBuilder = (BaseTestBuilderWithResponseModel)builder;

            var modelType = typeof(TModel);
            var anonymousModel = Reflection.IsAnonymousType(modelType);

            if (!anonymousModel)
            {
                actualBuilder.WithModelOfType<TModel>();
            }

            var actualModel = anonymousModel 
                ? actualBuilder.GetActualModel()
                : actualBuilder.GetActualModel<TModel>();

            if (Reflection.AreNotDeeplyEqual(model, actualModel, out var result))
            {
                throw new ResponseModelAssertionException(string.Format(
                    actualBuilder.ErrorMessageFormat,
                    actualBuilder.TestContext.ExceptionMessagePrefix,
                    modelType.ToFriendlyTypeName(),
                    result));
            }

            actualBuilder.TestContext.Model = actualModel;

            return new ModelDetailsTestBuilder<TModel>(actualBuilder.TestContext);
        }
    }
}
