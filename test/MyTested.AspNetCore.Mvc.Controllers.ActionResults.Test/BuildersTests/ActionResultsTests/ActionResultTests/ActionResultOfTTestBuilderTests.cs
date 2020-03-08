namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ActionResultTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class ActionResultOfTTestBuilderTests
    {
        [Fact]
        public void ShouldReturnActionResultOfTWithDetailsShouldNotThrowExceptionWithEqualResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfT(1))
                .ShouldReturn()
                .ActionResult<ResponseModel>(result => result
                    .EqualTo(new ResponseModel
                    {
                        IntegerValue = 1,
                        StringValue = "Test"
                    }));
        }

        [Fact]
        public void ShouldReturnActionResultOfTWithDetailsShouldThrowExceptionWithIncorrectResult()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultOfT(1))
                        .ShouldReturn()
                        .ActionResult<ResponseModel>(result => result
                            .EqualTo(new ResponseModel
                            {
                                IntegerValue = 2,
                                StringValue = "Test"
                            }));
                },
                "When calling ActionResultOfT action in MvcController expected the response model to be the given model, but in fact it was a different one. Difference occurs at 'ResponseModel.IntegerValue'. Expected a value of '2', but in fact it was '1'.");
        }

        [Fact]
        public void ShouldReturnActionResultOfAsyncTWithDetailsShouldNotThrowExceptionWithEqualResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfTAsync(1))
                .ShouldReturn()
                .ActionResult<ResponseModel>(result => result
                    .EqualTo(new ResponseModel
                    {
                        IntegerValue = 1,
                        StringValue = "Test"
                    }));
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncWithDetailsShouldThrowExceptionWithIncorrectResult()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultOfTAsync(1))
                        .ShouldReturn()
                        .ActionResult<ResponseModel>(result => result
                            .EqualTo(new ResponseModel
                            {
                                IntegerValue = 2,
                                StringValue = "Test"
                            }));
                },
                "When calling ActionResultOfTAsync action in MvcController expected the response model to be the given model, but in fact it was a different one. Difference occurs at 'ResponseModel.IntegerValue'. Expected a value of '2', but in fact it was '1'.");
        }
    }
}
