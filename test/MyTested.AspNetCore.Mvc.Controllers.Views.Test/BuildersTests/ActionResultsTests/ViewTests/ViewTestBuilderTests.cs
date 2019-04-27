namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ViewTests
{
    using System.Collections.Generic;
    using System.Net;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class ViewTestBuilderTests
    {
        [Fact]
        public void WithModelShouldNotThrowExceptionWithCorrectModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexView())
                .ShouldReturn()
                .View(view => view
                    .WithModel(TestObjectFactory.GetListOfResponseModels()));
        }

        [Fact]
        public void WithModelShouldThrowExceptionWithNullModel()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.DefaultView())
                        .ShouldReturn()
                        .View(view => view
                            .WithModel(TestObjectFactory.GetListOfResponseModels()));
                },
                "When calling DefaultView action in MvcController expected response model to be of List<ResponseModel> type, but instead received null.");
        }

        [Fact]
        public void WithModelShouldThrowExceptionWithExpectedNullModel()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.IndexView())
                        .ShouldReturn()
                        .View(view => view
                            .WithModel((string)null));
                },
                "When calling IndexView action in MvcController expected response model to be of String type, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void WithModelShouldThrowExceptionWithIncorrectModel()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    var model = TestObjectFactory.GetListOfResponseModels();
                    model[0].IntegerValue = int.MaxValue;

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.IndexView())
                        .ShouldReturn()
                        .View(view => view
                            .WithModel(model));
                },
                "When calling IndexView action in MvcController expected response model List<ResponseModel> to be the given model, but in fact it was a different one.");
        }

        [Fact]
        public void WithModelOfTypeShouldNotThrowExceptionWithCorrectType()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexView())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<ResponseModel>>());
        }

        [Fact]
        public void WithStatusCodeAsIntShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewResult())
                .ShouldReturn()
                .View(view => view
                    .WithStatusCode(500));
        }

        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewResult())
                .ShouldReturn()
                .View(view => view
                    .WithStatusCode(HttpStatusCode.InternalServerError));
        }

        [Fact]
        public void WithStatusCodeShouldThrowExceptionWhenActionReturnsWrongStatusCode()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CustomViewResult())
                        .ShouldReturn()
                        .View(view => view
                            .WithStatusCode(HttpStatusCode.NotFound));
                },
                "When calling CustomViewResult action in MvcController expected view result to have 404 (NotFound) status code, but instead received 500 (InternalServerError).");
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithString()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewResult())
                .ShouldReturn()
                .View(view => view
                    .WithContentType(ContentType.ApplicationXml));
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewResult())
                .ShouldReturn()
                .View(view => view
                    .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationXml)));
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValueConstant()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewResult())
                .ShouldReturn()
                .View(view => view
                    .WithContentType(ContentType.ApplicationXml));
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithMediaTypeHeaderValue()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CustomViewResult())
                        .ShouldReturn()
                        .View(view => view
                            .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson)));
                },
                "When calling CustomViewResult action in MvcController expected view result ContentType to be 'application/json', but instead received 'application/xml'.");
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValue()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CustomViewResult())
                        .ShouldReturn()
                        .View(view => view
                            .WithContentType((MediaTypeHeaderValue)null));
                },
                "When calling CustomViewResult action in MvcController expected view result ContentType to be null, but instead received 'application/xml'.");
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValueAndNullActual()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View(view => view
                    .WithContentType((MediaTypeHeaderValue)null));
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithMediaTypeHeaderValueAndNullActual()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.DefaultView())
                        .ShouldReturn()
                        .View(view => view
                            .WithContentType(new MediaTypeHeaderValue(TestObjectFactory.MediaType)));
                },
                "When calling DefaultView action in MvcController expected view result ContentType to be 'application/json', but instead received null.");
        }
        
        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewResult())
                .ShouldReturn()
                .View(view => view
                    .WithContentType(ContentType.ApplicationXml)
                    .AndAlso()
                    .WithStatusCode(500));
        }
        
        [Fact]
        public void WithModelShouldNotThrowExceptionWithCorrectModelForPartials()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexPartialView())
                .ShouldReturn()
                .PartialView(partialView => partialView
                    .WithModel(TestObjectFactory.GetListOfResponseModels()));
        }

        [Fact]
        public void WithModelShouldThrowExceptionWithNullModelForPartials()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.DefaultPartialView())
                        .ShouldReturn()
                        .PartialView(partialView => partialView
                            .WithModel(TestObjectFactory.GetListOfResponseModels()));
                },
                "When calling DefaultPartialView action in MvcController expected response model to be of List<ResponseModel> type, but instead received null.");
        }

        [Fact]
        public void WithModelShouldThrowExceptionWithExpectedNullModelForPartials()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.IndexPartialView())
                        .ShouldReturn()
                        .PartialView(partialView => partialView
                            .WithModel((string)null));
                },
                "When calling IndexPartialView action in MvcController expected response model to be of String type, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void WithModelShouldThrowExceptionWithIncorrectModelForPartials()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    var model = TestObjectFactory.GetListOfResponseModels();
                    model[0].IntegerValue = int.MaxValue;

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.IndexPartialView())
                        .ShouldReturn()
                        .PartialView(partialView => partialView
                            .WithModel(model));
                },
                "When calling IndexPartialView action in MvcController expected response model List<ResponseModel> to be the given model, but in fact it was a different one.");
        }

        [Fact]
        public void WithModelOfTypeShouldNotThrowExceptionWithCorrectTypeForPartials()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexPartialView())
                .ShouldReturn()
                .PartialView(partialView => partialView
                    .WithModelOfType<List<ResponseModel>>());
        }

        [Fact]
        public void WithStatusCodeAsIntShouldNotThrowExceptionWhenActionReturnsCorrectStatusCodeForPartials()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomPartialViewResult())
                .ShouldReturn()
                .PartialView(partialView => partialView
                    .WithStatusCode(500));
        }

        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWhenActionReturnsCorrectStatusCodeForPartials()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomPartialViewResult())
                .ShouldReturn()
                .PartialView(partialView => partialView
                    .WithStatusCode(HttpStatusCode.InternalServerError));
        }

        [Fact]
        public void WithStatusCodeShouldThrowExceptionWhenActionReturnsWrongStatusCodeForPartials()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CustomPartialViewResult())
                        .ShouldReturn()
                        .PartialView(partialView => partialView
                            .WithStatusCode(HttpStatusCode.NotFound));
                },
                "When calling CustomPartialViewResult action in MvcController expected partial view result to have 404 (NotFound) status code, but instead received 500 (InternalServerError).");
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithStringForPartials()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomPartialViewResult())
                .ShouldReturn()
                .PartialView(partialView => partialView
                    .WithContentType(ContentType.ApplicationXml));
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValueForPartials()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomPartialViewResult())
                .ShouldReturn()
                .PartialView(partialView => partialView
                    .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationXml)));
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValueConstantForPartials()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomPartialViewResult())
                .ShouldReturn()
                .PartialView(partialView => partialView
                    .WithContentType(ContentType.ApplicationXml));
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithMediaTypeHeaderValueForPartials()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CustomPartialViewResult())
                        .ShouldReturn()
                        .PartialView(partialView => partialView
                            .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson)));
                },
                "When calling CustomPartialViewResult action in MvcController expected partial view result ContentType to be 'application/json', but instead received 'application/xml'.");
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValueForPartials()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CustomPartialViewResult())
                        .ShouldReturn()
                        .PartialView(partialView => partialView
                            .WithContentType((MediaTypeHeaderValue)null));
                },
                "When calling CustomPartialViewResult action in MvcController expected partial view result ContentType to be null, but instead received 'application/xml'.");
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValueAndNullActualForPartials()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultPartialView())
                .ShouldReturn()
                .PartialView(partialView => partialView
                    .WithContentType((MediaTypeHeaderValue)null));
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithMediaTypeHeaderValueAndNullActualForPartials()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.DefaultPartialView())
                        .ShouldReturn()
                        .PartialView(partialView => partialView
                            .WithContentType(new MediaTypeHeaderValue(TestObjectFactory.MediaType)));
                },
                "When calling DefaultPartialView action in MvcController expected partial view result ContentType to be 'application/json', but instead received null.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectlyForViews()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewResult())
                .ShouldReturn()
                .View(view => view
                    .WithContentType(ContentType.ApplicationXml)
                    .AndAlso()
                    .WithStatusCode(500));
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectlyForPartials()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomPartialViewResult())
                .ShouldReturn()
                .PartialView(partialView => partialView
                    .WithContentType(ContentType.ApplicationXml)
                    .AndAlso()
                    .WithStatusCode(500));
        }

        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View()
                .AndAlso()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<ViewResult>(actionResult);
                });
        }
        
        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectlyWithPartial()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultPartialView())
                .ShouldReturn()
                .PartialView()
                .AndAlso()
                .ShouldPassForThe<ActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<PartialViewResult>(actionResult);
                });
        }
    }
}
