namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ViewTests
{
    using System.Collections.Generic;
    using System.Net;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Common;
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
                .View("Index")
                .WithModel(TestObjectFactory.GetListOfResponseModels());
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
                        .View()
                        .WithModel(TestObjectFactory.GetListOfResponseModels());
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
                        .View("Index")
                        .WithModel((string)null);
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
                        .View("Index")
                        .WithModel(model);
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
                .View("Index")
                .WithModelOfType<List<ResponseModel>>();
        }

        [Fact]
        public void WithStatusCodeAsIntShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewResult())
                .ShouldReturn()
                .View()
                .WithStatusCode(500);
        }

        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewResult())
                .ShouldReturn()
                .View()
                .WithStatusCode(HttpStatusCode.InternalServerError);
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
                        .View()
                        .WithStatusCode(HttpStatusCode.NotFound);
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
                .View()
                .WithContentType(ContentType.ApplicationXml);
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewResult())
                .ShouldReturn()
                .View()
                .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationXml));
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValueConstant()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewResult())
                .ShouldReturn()
                .View()
                .WithContentType(ContentType.ApplicationXml);
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
                        .View()
                        .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson));
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
                        .View()
                        .WithContentType((MediaTypeHeaderValue)null);
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
                .View()
                .WithContentType((MediaTypeHeaderValue)null);
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
                        .View()
                        .WithContentType(new MediaTypeHeaderValue(TestObjectFactory.MediaType));
                },
                "When calling DefaultView action in MvcController expected view result ContentType to be 'application/json', but instead received null.");
        }
        
        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithValidViewEngine()
        {
            var viewEngine = TestObjectFactory.GetViewEngine();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewWithViewEngine(viewEngine))
                .ShouldReturn()
                .View()
                .WithViewEngine(viewEngine);
        }

        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithNullViewEngine()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View()
                .WithViewEngine(null);
        }
        
        [Fact]
        public void WithViewEngineShouldThrowExceptionWithInvalidViewEngine()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.ViewWithViewEngine(null))
                        .ShouldReturn()
                        .View()
                        .WithViewEngine(new CustomViewEngine());
                },
                "When calling ViewWithViewEngine action in MvcController expected view result ViewEngine to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithValidViewEngine()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewWithViewEngine(new CustomViewEngine()))
                .ShouldReturn()
                .View()
                .WithViewEngineOfType<CustomViewEngine>();
        }

        [Fact]
        public void WithViewEngineOfTypeShouldThrowExceptionWithInvalidViewEngine()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ViewWithViewEngine(new CustomViewEngine()))
                        .ShouldReturn()
                        .View()
                        .WithViewEngineOfType<IViewEngine>();
                },
                "When calling ViewWithViewEngine action in MvcController expected view result ViewEngine to be of IViewEngine type, but instead received CustomViewEngine.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithNullViewEngine()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.DefaultView())
                        .ShouldReturn()
                        .View()
                        .WithViewEngineOfType<CustomViewEngine>();
                },
                "When calling DefaultView action in MvcController expected view result ViewEngine to be of CustomViewEngine type, but instead received null.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewResult())
                .ShouldReturn()
                .View()
                .WithContentType(ContentType.ApplicationXml)
                .AndAlso()
                .WithStatusCode(500);
        }
        
        [Fact]
        public void WithModelShouldNotThrowExceptionWithCorrectModelForPartials()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexPartialView())
                .ShouldReturn()
                .PartialView("_IndexPartial")
                .WithModel(TestObjectFactory.GetListOfResponseModels());
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
                        .PartialView()
                        .WithModel(TestObjectFactory.GetListOfResponseModels());
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
                        .PartialView("_IndexPartial")
                        .WithModel((string)null);
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
                        .PartialView("_IndexPartial")
                        .WithModel(model);
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
                .PartialView("_IndexPartial")
                .WithModelOfType<List<ResponseModel>>();
        }

        [Fact]
        public void WithStatusCodeAsIntShouldNotThrowExceptionWhenActionReturnsCorrectStatusCodeForPartials()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomPartialViewResult())
                .ShouldReturn()
                .PartialView()
                .WithStatusCode(500);
        }

        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWhenActionReturnsCorrectStatusCodeForPartials()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomPartialViewResult())
                .ShouldReturn()
                .PartialView()
                .WithStatusCode(HttpStatusCode.InternalServerError);
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
                        .PartialView()
                        .WithStatusCode(HttpStatusCode.NotFound);
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
                .PartialView()
                .WithContentType(ContentType.ApplicationXml);
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValueForPartials()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomPartialViewResult())
                .ShouldReturn()
                .PartialView()
                .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationXml));
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValueConstantForPartials()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomPartialViewResult())
                .ShouldReturn()
                .PartialView()
                .WithContentType(ContentType.ApplicationXml);
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
                        .PartialView()
                        .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson));
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
                        .PartialView()
                        .WithContentType((MediaTypeHeaderValue)null);
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
                .PartialView()
                .WithContentType((MediaTypeHeaderValue)null);
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
                        .PartialView()
                        .WithContentType(new MediaTypeHeaderValue(TestObjectFactory.MediaType));
                },
                "When calling DefaultPartialView action in MvcController expected partial view result ContentType to be 'application/json', but instead received null.");
        }
        
        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithValidViewEngineForPartials()
        {
            var viewEngine = TestObjectFactory.GetViewEngine();

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.PartialViewWithViewEngine(viewEngine))
                .ShouldReturn()
                .PartialView()
                .WithViewEngine(viewEngine);
        }

        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithNullViewEngineForPartials()
        {
            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.DefaultPartialView())
                .ShouldReturn()
                .PartialView()
                .WithViewEngine(null);
        }

        [Fact]
        public void WithViewEngineShouldThrowExceptionWithInvalidViewEngineForPartials()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.PartialViewWithViewEngine(null))
                        .ShouldReturn()
                        .PartialView()
                        .WithViewEngine(new CustomViewEngine());
                },
                "When calling PartialViewWithViewEngine action in MvcController expected partial view result ViewEngine to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithValidViewEngineForPartials()
        {
            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.PartialViewWithViewEngine(new CustomViewEngine()))
                .ShouldReturn()
                .PartialView()
                .WithViewEngineOfType<CustomViewEngine>();
        }

        [Fact]
        public void WithViewEngineOfTypeShouldThrowExceptionWithInvalidViewEngineForPartials()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.PartialViewWithViewEngine(new CustomViewEngine()))
                        .ShouldReturn()
                        .PartialView()
                        .WithViewEngineOfType<IViewEngine>();
                },
                "When calling PartialViewWithViewEngine action in MvcController expected partial view result ViewEngine to be of IViewEngine type, but instead received CustomViewEngine.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithNullViewEngineForPartials()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.DefaultPartialView())
                        .ShouldReturn()
                        .PartialView()
                        .WithViewEngineOfType<CustomViewEngine>();
                },
                "When calling DefaultPartialView action in MvcController expected partial view result ViewEngine to be of CustomViewEngine type, but instead received null.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectlyForPartials()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomPartialViewResult())
                .ShouldReturn()
                .PartialView()
                .WithContentType(ContentType.ApplicationXml)
                .AndAlso()
                .WithStatusCode(500);
        }

        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<ViewResult>(actionResult);
                });
        }
    }
}
