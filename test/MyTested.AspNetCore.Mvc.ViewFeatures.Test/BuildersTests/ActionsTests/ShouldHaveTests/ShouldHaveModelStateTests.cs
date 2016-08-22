namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
{
    using System;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class ShouldHaveModelStateTests
    {
        [Fact]
        public void ShouldHaveModelStateForShouldChainCorrectly()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .ModelState(modelState => modelState.For<RequestModel>()
                    .ContainingNoErrorFor(r => r.NonRequiredString)
                    .ContainingErrorFor(r => r.Integer)
                    .ContainingErrorFor(r => r.RequiredString));
        }

        [Fact]
        public void AndProvideModelShouldThrowExceptionWhenIsCalledOnTheRequest()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                        .ShouldHave()
                        .ModelState(modelState => modelState.For<RequestModel>()
                            .ContainingNoErrorFor(r => r.NonRequiredString)
                            .ContainingErrorFor(r => r.Integer)
                            .ContainingErrorFor(r => r.RequiredString))
                        .ShouldPassForThe<ResponseModel>(model => model != null);
                },
                "AndProvideTheModel can be used when there is response model from the action.");
        }
    }
}
