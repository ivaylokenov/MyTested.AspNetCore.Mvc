namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;
    using Xunit.Sdk;

    public class ShouldReturnActionResultTests
    {
        [Fact]
        public void ShouldReturnActionResultShouldNotThrowExceptionWhenResultIsIActionResultInterface()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultInterface())
                .ShouldReturn()
                .ActionResult();
        }

        [Fact]
        public void ShouldReturnActionResultShouldNotThrowExceptionWhenResultIsActionResultBaseClass()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultBaseClass())
                .ShouldReturn()
                .ActionResult();
        }

        [Fact]
        public void ShouldReturnActionResultShouldNotThrowExceptionWhenResultIsActionResultOfT()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfAnonymousType())
                .ShouldReturn()
                .ActionResult();
        }

        [Fact]
        public void ShouldReturnActionResultShouldThrowExceptionWhenWhenResultIsNotActionResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AnonymousResult())
                        .ShouldReturn()
                        .ActionResult();
                },
                "When calling AnonymousResult action in MvcController expected result to be IActionResult or ActionResult<TValue>, but instead received AnonymousType<Int32, String, AnonymousType<Boolean>>.");
        }

        [Fact]
        public void ShouldReturnActionResultWithDetailsShouldNotThrowExceptionWhenResultIsIActionResultInterface()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultInterface())
                .ShouldReturn()
                .ActionResult(result => result
                    .Ok(okResult => okResult
                        .Passing(ok => ok.Value?.GetType() == typeof(ResponseModel))));
        }

        [Fact]
        public void ShouldReturnActionResultWithDetailsShouldNotThrowExceptionWhenResultIsActionResultBaseClass()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultBaseClass())
                .ShouldReturn()
                .ActionResult(result => result
                    .Ok(okResult => okResult
                        .Passing(ok => ok.Value?.GetType() == typeof(ResponseModel))));
        }

        [Fact]
        public void ShouldReturnActionResultWithDetailsShouldNotThrowExceptionWhenResultIsActionResultOfT()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfT(int.MaxValue))
                .ShouldReturn()
                .ActionResult(result => result
                    .Ok(okResult => okResult
                        .Passing(ok => ok.Value?.GetType() == typeof(ResponseModel))));
        }

        [Fact]
        public void ShouldReturnActionResultWithDetailsAndInnerBuilderShouldThrowExceptionWhenWhenResultIsNotActionResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AnonymousResult())
                        .ShouldReturn()
                        .ActionResult(result => result
                            .Ok());
                },
                "When calling AnonymousResult action in MvcController expected result to be IActionResult or ActionResult<TValue>, but instead received AnonymousType<Int32, String, AnonymousType<Boolean>>.");
        }

        [Fact]
        public void ShouldReturnActionResultWithDetailsShouldThrowExceptionWhenWhenResultIsNotActionResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .ActionResult(result => result
                            .Ok());
                },
                "When calling BadRequestAction action in MvcController expected result to be OkResult, but instead received BadRequestResult.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTShouldNotThrowExceptionWhenResultIsActionResultOfTWithActionResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfT(0))
                .ShouldReturn()
                .ActionResult<ResponseModel>();
        }

        [Fact]
        public void ShouldReturnActionResultOfTShouldNotThrowExceptionWhenResultIsActionResultOfTWithModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfT(1))
                .ShouldReturn()
                .ActionResult<ResponseModel>();
        }

        [Fact]
        public void ShouldReturnActionResultOfTShouldThrowExceptionWhenResultIsIActionResultInterface()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultInterface())
                        .ShouldReturn()
                        .ActionResult<ResponseModel>();
                },
                "When calling ActionResultInterface action in MvcController expected result to be ActionResult<ResponseModel> or Task<ActionResult<ResponseModel>>, but instead received IActionResult.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTShouldThrowExceptionWhenResultIsActionResultBaseClass()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultBaseClass())
                        .ShouldReturn()
                        .ActionResult<ResponseModel>();
                },
                "When calling ActionResultBaseClass action in MvcController expected result to be ActionResult<ResponseModel> or Task<ActionResult<ResponseModel>>, but instead received ActionResult.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTShouldThrowExceptionWhenResultIsActionResultOfWrongModel()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultOfT(0))
                        .ShouldReturn()
                        .ActionResult<RequestModel>();
                },
                "When calling ActionResultOfT action in MvcController expected result to be ActionResult<RequestModel> or Task<ActionResult<RequestModel>>, but instead received ActionResult<ResponseModel>.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTWithDetailsShouldNotThrowExceptionWhenResultIsActionResultOfTWithActionResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfT(0))
                .ShouldReturn()
                .ActionResult<ResponseModel>(result => result
                    .BadRequest());
        }

        [Fact]
        public void ShouldReturnActionResultOfTWithDetailsShouldNotThrowExceptionWhenResultIsActionResultOfTWithModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfT(int.MaxValue))
                .ShouldReturn()
                .ActionResult<ResponseModel>(result => result
                    .Ok(okResult => okResult
                        .Passing(ok => ok.Value?.GetType() == typeof(ResponseModel))));
        }

        [Fact]
        public void ShouldReturnActionResultOfTWithDetailsShouldThrowExceptionWhenResultIsIActionResultInterface()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultInterface())
                        .ShouldReturn()
                        .ActionResult<ResponseModel>(result => result
                            .Ok());
                },
                "When calling ActionResultInterface action in MvcController expected result to be ActionResult<ResponseModel> or Task<ActionResult<ResponseModel>>, but instead received IActionResult.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTWithDetailsShouldThrowExceptionWhenResultIsActionResultBaseClass()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultBaseClass())
                        .ShouldReturn()
                        .ActionResult<ResponseModel>(result => result
                            .Ok());
                },
                "When calling ActionResultBaseClass action in MvcController expected result to be ActionResult<ResponseModel> or Task<ActionResult<ResponseModel>>, but instead received ActionResult.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTWithDetailsShouldThrowExceptionWhenResultIsActionResultOfWrongModel()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultOfT(0))
                        .ShouldReturn()
                        .ActionResult<RequestModel>(result => result
                            .Ok());
                },
                "When calling ActionResultOfT action in MvcController expected result to be ActionResult<RequestModel> or Task<ActionResult<RequestModel>>, but instead received ActionResult<ResponseModel>.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTWithResultDetailsShouldNotThrowExceptionWhenResultIsActionResultOfTWithCorrectResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfT(1))
                .ShouldReturn()
                .ActionResult<ResponseModel>(result => result
                    .Passing(model => model.IntegerValue == 1));
        }

        [Fact]
        public void ShouldReturnActionResultOfTWithResultDetailsShouldThrowExceptionWhenResultIsActionResultOfTWithIncorrectResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultOfT(1))
                        .ShouldReturn()
                        .ActionResult<ResponseModel>(result => result
                            .Passing(model => model.IntegerValue == 2));
                },
                "When calling ActionResultOfT action in MvcController expected the ResponseModel to pass the given predicate, but it failed.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTWithResultDetailsShouldNotThrowExceptionWhenResultIsActionResultOfTWithCorrectAssertion()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfT(1))
                .ShouldReturn()
                .ActionResult<ResponseModel>(result => result
                    .Passing(model =>
                    {
                        Assert.True(model.IntegerValue == 1);
                    }));
        }

        [Fact]
        public void ShouldReturnActionResultOfTWithResultDetailsShouldThrowExceptionWhenResultIsActionResultOfTWithIncorrectAssertion()
        {
            Assert.Throws<TrueException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultOfT(1))
                        .ShouldReturn()
                        .ActionResult<ResponseModel>(result => result
                            .Passing(model =>
                            {
                                Assert.True(model.IntegerValue == 2);
                            }));
                });
        }

        [Fact]
        public void ShouldReturnActionResultShouldWorkCorrectlyWithShouldPassFotTheMethod()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultInterface())
                .ShouldReturn()
                .ActionResult()
                .AndAlso()
                .ShouldPassForThe<OkObjectResult>(ok => ok
                    .Value.GetType() == typeof(ResponseModel));
        }

        [Fact]
        public void ShouldReturnActionResultOfTShouldWorkCorrectlyWithShouldPassFotTheMethodAndActionResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfT(int.MaxValue))
                .ShouldReturn()
                .ActionResult()
                .AndAlso()
                .ShouldPassForThe<OkObjectResult>(ok => ok
                    .Value.GetType() == typeof(ResponseModel));
        }

        [Fact]
        public void ShouldReturnActionResultOfTShouldWorkCorrectlyWithShouldPassForTheMethodAndObjectResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfT(1))
                .ShouldReturn()
                .ActionResult<ResponseModel>()
                .AndAlso()
                .ShouldPassForThe<ObjectResult>(model => model
                    .Value.GetType() == typeof(ResponseModel));
        }

        [Fact]
        public void ShouldReturnActionResultOfTShouldWorkCorrectlyWithShouldPassForTheMethodAndModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfT(1))
                .ShouldReturn()
                .ActionResult<ResponseModel>()
                .AndAlso()
                .ShouldPassForThe<ResponseModel>(model => model.IntegerValue == 1);
        }

        [Fact]
        public void ShouldReturnActionResultAsyncShouldNotThrowExceptionWhenResultIsIActionResultInterface()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultInterfaceAsync())
                .ShouldReturn()
                .ActionResult();
        }

        [Fact]
        public void ShouldReturnActionResultAsyncShouldNotThrowExceptionWhenResultIsActionResultBaseClass()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultBaseClassAsync())
                .ShouldReturn()
                .ActionResult();
        }

        [Fact]
        public void ShouldReturnActionResultAsyncWithDetailsShouldNotThrowExceptionWhenResultIsIActionResultInterface()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultInterfaceAsync())
                .ShouldReturn()
                .ActionResult(result => result
                    .Ok(okResult => okResult
                        .Passing(ok => ok.Value?.GetType() == typeof(ResponseModel))));
        }

        [Fact]
        public void ShouldReturnActionResultAsyncWithDetailsShouldNotThrowExceptionWhenResultIsActionResultBaseClass()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultBaseClassAsync())
                .ShouldReturn()
                .ActionResult(result => result
                    .Ok(okResult => okResult
                        .Passing(ok => ok.Value?.GetType() == typeof(ResponseModel))));
        }

        [Fact]
        public void ShouldReturnActionResultAsyncWithDetailsShouldNotThrowExceptionWhenResultIsActionResultOfT()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfTAsync(int.MaxValue))
                .ShouldReturn()
                .ActionResult(result => result
                    .Ok(okResult => okResult
                        .Passing(ok => ok.Value?.GetType() == typeof(ResponseModel))));
        }

        [Fact]
        public void ShouldReturnActionResultAsyncWithDetailsShouldThrowExceptionWhenWhenResultIsNotActionResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AsyncOkResultAction())
                        .ShouldReturn()
                        .ActionResult(result => result
                            .BadRequest());
                },
                "When calling AsyncOkResultAction action in MvcController expected result to be BadRequestResult, but instead received OkResult.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncShouldNotThrowExceptionWhenResultIsActionResultOfTWithActionResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfTAsync(0))
                .ShouldReturn()
                .ActionResult<ResponseModel>();
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncShouldNotThrowExceptionWhenResultIsActionResultOfTWithModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfTAsync(1))
                .ShouldReturn()
                .ActionResult<ResponseModel>();
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncShouldThrowExceptionWhenResultIsIActionResultInterface()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultInterfaceAsync())
                        .ShouldReturn()
                        .ActionResult<ResponseModel>();
                },
                "When calling ActionResultInterfaceAsync action in MvcController expected result to be ActionResult<ResponseModel> or Task<ActionResult<ResponseModel>>, but instead received Task<IActionResult>.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncShouldThrowExceptionWhenResultIsActionResultBaseClass()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultBaseClassAsync())
                        .ShouldReturn()
                        .ActionResult<ResponseModel>();
                },
                "When calling ActionResultBaseClassAsync action in MvcController expected result to be ActionResult<ResponseModel> or Task<ActionResult<ResponseModel>>, but instead received Task<ActionResult>.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncShouldThrowExceptionWhenResultIsActionResultOfWrongModel()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultOfTAsync(0))
                        .ShouldReturn()
                        .ActionResult<RequestModel>();
                },
                "When calling ActionResultOfTAsync action in MvcController expected result to be ActionResult<RequestModel> or Task<ActionResult<RequestModel>>, but instead received Task<ActionResult<ResponseModel>>.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncWithDetailsShouldNotThrowExceptionWhenResultIsActionResultOfTWithActionResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfTAsync(0))
                .ShouldReturn()
                .ActionResult<ResponseModel>(result => result
                    .BadRequest());
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncWithDetailsShouldNotThrowExceptionWhenResultIsActionResultOfTWithModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfTAsync(int.MaxValue))
                .ShouldReturn()
                .ActionResult<ResponseModel>(result => result
                    .Ok(okResult => okResult
                        .Passing(ok => ok.Value?.GetType() == typeof(ResponseModel))));
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncWithDetailsShouldThrowExceptionWhenResultIsIActionResultInterface()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultInterfaceAsync())
                        .ShouldReturn()
                        .ActionResult<ResponseModel>(result => result
                            .Ok());
                },
                "When calling ActionResultInterfaceAsync action in MvcController expected result to be ActionResult<ResponseModel> or Task<ActionResult<ResponseModel>>, but instead received Task<IActionResult>.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncWithDetailsShouldThrowExceptionWhenResultIsActionResultBaseClass()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultBaseClassAsync())
                        .ShouldReturn()
                        .ActionResult<ResponseModel>(result => result
                            .Ok());
                },
                "When calling ActionResultBaseClassAsync action in MvcController expected result to be ActionResult<ResponseModel> or Task<ActionResult<ResponseModel>>, but instead received Task<ActionResult>.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncWithDetailsShouldThrowExceptionWhenResultIsActionResultOfWrongModel()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultOfTAsync(0))
                        .ShouldReturn()
                        .ActionResult<RequestModel>(result => result
                            .Ok());
                },
                "When calling ActionResultOfTAsync action in MvcController expected result to be ActionResult<RequestModel> or Task<ActionResult<RequestModel>>, but instead received Task<ActionResult<ResponseModel>>.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncWithResultDetailsShouldNotThrowExceptionWhenResultIsActionResultOfTWithCorrectResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfTAsync(1))
                .ShouldReturn()
                .ActionResult<ResponseModel>(result => result
                    .Passing(model => model.IntegerValue == 1));
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncWithResultDetailsShouldThrowExceptionWhenResultIsActionResultOfTWithIncorrectResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultOfTAsync(1))
                        .ShouldReturn()
                        .ActionResult<ResponseModel>(result => result
                            .Passing(model => model.IntegerValue == 2));
                },
                "When calling ActionResultOfTAsync action in MvcController expected the ResponseModel to pass the given predicate, but it failed.");
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncWithResultDetailsShouldNotThrowExceptionWhenResultIsActionResultOfTWithCorrectAssertion()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfTAsync(1))
                .ShouldReturn()
                .ActionResult<ResponseModel>(result => result
                    .Passing(model =>
                    {
                        Assert.True(model.IntegerValue == 1);
                    }));
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncWithResultDetailsShouldThrowExceptionWhenResultIsActionResultOfTWithIncorrectAssertion()
        {
            Assert.Throws<TrueException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionResultOfTAsync(1))
                        .ShouldReturn()
                        .ActionResult<ResponseModel>(result => result
                            .Passing(model =>
                            {
                                Assert.True(model.IntegerValue == 2);
                            }));
                });
        }

        [Fact]
        public void ShouldReturnActionResultAsyncShouldWorkCorrectlyWithShouldPassFotTheMethod()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultInterfaceAsync())
                .ShouldReturn()
                .ActionResult()
                .AndAlso()
                .ShouldPassForThe<OkObjectResult>(ok => ok
                    .Value.GetType() == typeof(ResponseModel));
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncShouldWorkCorrectlyWithShouldPassFotTheMethodAndActionResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfTAsync(int.MaxValue))
                .ShouldReturn()
                .ActionResult()
                .AndAlso()
                .ShouldPassForThe<OkObjectResult>(ok => ok
                    .Value.GetType() == typeof(ResponseModel));
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncShouldWorkCorrectlyWithShouldPassForTheMethodAndObjectResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfTAsync(1))
                .ShouldReturn()
                .ActionResult<ResponseModel>()
                .AndAlso()
                .ShouldPassForThe<ObjectResult>(model => model
                    .Value.GetType() == typeof(ResponseModel));
        }

        [Fact]
        public void ShouldReturnActionResultOfTAsyncShouldWorkCorrectlyWithShouldPassForTheMethodAndModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfTAsync(1))
                .ShouldReturn()
                .ActionResult<ResponseModel>()
                .AndAlso()
                .ShouldPassForThe<ResponseModel>(model => model.IntegerValue == 1);
        }
    }
}
