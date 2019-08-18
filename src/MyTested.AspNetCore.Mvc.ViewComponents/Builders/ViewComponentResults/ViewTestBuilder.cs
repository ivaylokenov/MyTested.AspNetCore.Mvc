namespace MyTested.AspNetCore.Mvc.Builders.ViewComponentResults
{
    using System;
    using Base;
    using Contracts.ViewComponentResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Exceptions;

    /// <summary>
    /// Used for testing <see cref="ViewViewComponentResult"/>.
    /// </summary>
    public class ViewTestBuilder : BaseTestBuilderWithResponseModel, IAndViewTestBuilder
    {
        public ViewTestBuilder(ActionTestContext testContext)
            : base(testContext) 
            => this.ViewResult = testContext.MethodResultAs<ViewViewComponentResult>();
        
        public ViewViewComponentResult ViewResult { get; private set; }
        
        /// <inheritdoc />
        public IViewTestBuilder AndAlso() => this;

        public override object GetActualModel()
            => this.TestContext.MethodResultAs<ViewViewComponentResult>()?.ViewData?.Model;

        public override Type GetModelReturnType() => this.GetActualModel()?.GetType();

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
