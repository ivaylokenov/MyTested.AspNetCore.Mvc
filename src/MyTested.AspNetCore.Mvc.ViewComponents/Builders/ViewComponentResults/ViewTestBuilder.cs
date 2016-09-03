namespace MyTested.AspNetCore.Mvc.Builders.ViewComponentResults
{
    using System;
    using Base;
    using Contracts.ViewComponentResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Utilities;
    using Exceptions;

    public class ViewTestBuilder : BaseTestBuilderWithResponseModel, IAndViewTestBuilder
    {
        private readonly ViewViewComponentResult viewResult;

        public ViewTestBuilder(ActionTestContext testContext)
            : base(testContext)
        {
            this.viewResult = testContext.MethodResultAs<ViewViewComponentResult>();
        }

        /// <inheritdoc />
        public IAndViewTestBuilder WithViewEngine(IViewEngine viewEngine)
        {
            var actualViewEngine = this.viewResult.ViewEngine;
            if (viewEngine != actualViewEngine)
            {
                throw ViewViewComponentResultAssertionException
                    .ForViewEngineEquality(this.TestContext.ExceptionMessagePrefix);
            }

            return this;
        }

        /// <inheritdoc />
        public IAndViewTestBuilder WithViewEngineOfType<TViewEngine>()
            where TViewEngine : IViewEngine
        {
            var actualViewEngineType = this.viewResult?.ViewEngine?.GetType();
            var expectedViewEngineType = typeof(TViewEngine);

            if (actualViewEngineType == null
                || Reflection.AreDifferentTypes(expectedViewEngineType, actualViewEngineType))
            {
                throw ViewViewComponentResultAssertionException.ForViewEngineType(
                    this.TestContext.ExceptionMessagePrefix,
                    expectedViewEngineType.ToFriendlyTypeName(),
                    actualViewEngineType.ToFriendlyTypeName());
            }

            return this;
        }

        /// <inheritdoc />
        public IViewTestBuilder AndAlso() => this;

        protected override object GetActualModel()
            => this.TestContext.MethodResultAs<ViewViewComponentResult>()?.ViewData?.Model;

        protected override Type GetReturnType() => this.GetActualModel()?.GetType();
    }
}
