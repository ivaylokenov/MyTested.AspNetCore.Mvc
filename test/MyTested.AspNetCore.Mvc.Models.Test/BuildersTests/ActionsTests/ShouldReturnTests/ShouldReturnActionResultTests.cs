namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class ShouldReturnActionResultTests
    {
        [Fact]
        public void ShouldReturnActionResultOfTWithDetailsShouldNotThrowExceptionWhenResultIsActionResultOfTModelResultOfType()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfT(1))
                .ShouldReturn()
                .ActionResult<ResponseModel>(action => action
                    .ResultOfType<ResponseModel>(result => result
                        .Passing(model => model.IntegerValue == 1)));
        }

        [Fact]
        public void ShouldReturnActionResultOfTWithDetailsShouldNotThrowExceptionWhenResultIsActionResultOfTModelResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfT(1))
                .ShouldReturn()
                .ActionResult<ResponseModel>(action => action
                    .Result(new ResponseModel
                    {
                        IntegerValue = 1,
                        StringValue = "Test"
                    }));
        }

        [Fact]
        public void ShouldReturnActionResultOfTWithDetailsShouldThrowExceptionWhenResultIsActionResultOfTWithWrongModelResultOfType()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultOfT(1))
                        .ShouldReturn()
                        .ActionResult<ResponseModel>(action => action
                            .ResultOfType<ResponseModel>(result => result
                                .Passing(model => model.IntegerValue == 2)));
                },
                "When calling ActionResultOfT action in MvcController expected response model ResponseModel to pass the given predicate, but it failed.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTWithDetailsShouldThrowExceptionWhenResultIsActionResultOfTWithWrongModelResult()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultOfT(1))
                        .ShouldReturn()
                        .ActionResult<ResponseModel>(action => action
                            .Result(new ResponseModel
                            {
                                IntegerValue = 2,
                                StringValue = "Test"
                            }));
                },
                "When calling ActionResultOfT action in MvcController expected the response model to be the given model, but in fact it was a different one. Difference occurs at 'ResponseModel.IntegerValue'. Expected a value of '2', but in fact it was '1'.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncWithDetailsShouldNotThrowExceptionWhenResultIsActionResultOfTModelResultOfType()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfTAsync(1))
                .ShouldReturn()
                .ActionResult<ResponseModel>(action => action
                    .ResultOfType<ResponseModel>(result => result
                        .Passing(model => model.IntegerValue == 1)));
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncWithDetailsShouldNotThrowExceptionWhenResultIsActionResultOfTModelResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfTAsync(1))
                .ShouldReturn()
                .ActionResult<ResponseModel>(action => action
                    .Result(new ResponseModel
                    {
                        IntegerValue = 1,
                        StringValue = "Test"
                    }));
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncWithDetailsShouldThrowExceptionWhenResultIsActionResultOfTWithWrongModelResultOfType()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultOfTAsync(1))
                        .ShouldReturn()
                        .ActionResult<ResponseModel>(action => action
                            .ResultOfType<ResponseModel>(result => result
                                .Passing(model => model.IntegerValue == 2)));
                },
                "When calling ActionResultOfTAsync action in MvcController expected response model ResponseModel to pass the given predicate, but it failed.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncWithDetailsShouldThrowExceptionWhenResultIsActionResultOfTWithWrongModelResult()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultOfTAsync(1))
                        .ShouldReturn()
                        .ActionResult<ResponseModel>(action => action
                            .Result(new ResponseModel
                            {
                                IntegerValue = 2,
                                StringValue = "Test"
                            }));
                },
                "When calling ActionResultOfTAsync action in MvcController expected the response model to be the given model, but in fact it was a different one. Difference occurs at 'ResponseModel.IntegerValue'. Expected a value of '2', but in fact it was '1'.");
        }
    }
}
