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

    public class ViewComponentTestBuilderTests
    {
        [Fact]
        public void WithStatusCodeAsIntShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent()
                .WithStatusCode(500);
        }

        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent()
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
                        .Calling(c => c.CustomViewComponentResult())
                        .ShouldReturn()
                        .ViewComponent()
                        .WithStatusCode(HttpStatusCode.NotFound);
                },
                "When calling CustomViewComponentResult action in MvcController expected view component result to have 404 (NotFound) status code, but instead received 500 (InternalServerError).");
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithString()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent()
                .WithContentType(ContentType.ApplicationXml);
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent()
                .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationXml));
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValueConstant()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent()
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
                        .Calling(c => c.CustomViewComponentResult())
                        .ShouldReturn()
                        .ViewComponent()
                        .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson));
                },
                "When calling CustomViewComponentResult action in MvcController expected view component result ContentType to be 'application/json', but instead received 'application/xml'.");
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValue()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CustomViewComponentResult())
                        .ShouldReturn()
                        .ViewComponent()
                        .WithContentType((MediaTypeHeaderValue)null);
                },
                "When calling CustomViewComponentResult action in MvcController expected view component result ContentType to be null, but instead received 'application/xml'.");
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValueAndNullActual()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent()
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
                        .Calling(c => c.ViewComponentResultByName())
                        .ShouldReturn()
                        .ViewComponent()
                        .WithContentType(new MediaTypeHeaderValue(TestObjectFactory.MediaType));
                },
                "When calling ViewComponentResultByName action in MvcController expected view component result ContentType to be 'application/json', but instead received null.");
        }

        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithValidViewEngine()
        {
            var viewEngine = TestObjectFactory.GetViewEngine();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentWithViewEngine(viewEngine))
                .ShouldReturn()
                .ViewComponent()
                .WithViewEngine(viewEngine);
        }

        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithNullViewEngine()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent()
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
                        .Calling(c => c.ViewComponentWithViewEngine(null))
                        .ShouldReturn()
                        .ViewComponent()
                        .WithViewEngine(new CustomViewEngine());
                },
                "When calling ViewComponentWithViewEngine action in MvcController expected view component result ViewEngine to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithValidViewEngine()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentWithViewEngine(new CustomViewEngine()))
                .ShouldReturn()
                .ViewComponent()
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
                        .Calling(c => c.ViewComponentWithViewEngine(new CustomViewEngine()))
                        .ShouldReturn()
                        .ViewComponent()
                        .WithViewEngineOfType<IViewEngine>();
                },
                "When calling ViewComponentWithViewEngine action in MvcController expected view component result ViewEngine to be of IViewEngine type, but instead received CustomViewEngine.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithNullViewEngine()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ViewComponentResultByName())
                        .ShouldReturn()
                        .ViewComponent()
                        .WithViewEngineOfType<CustomViewEngine>();
                },
                "When calling ViewComponentResultByName action in MvcController expected view component result ViewEngine to be of CustomViewEngine type, but instead received null.");
        }

        [Fact]
        public void WithArgumentShouldNotThrowExceptionWithCorrectArgumentName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByType())
                .ShouldReturn()
                .ViewComponent()
                .ContainingArgumentWithName("model");
        }

        [Fact]
        public void WithArgumentOfTypeAndNameShouldNotThrowExceptionWithCorrectArgumentName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByType())
                .ShouldReturn()
                .ViewComponent()
                .ContainingArgumentOfType<List<ResponseModel>>("model");
        }

        [Fact]
        public void WithArgumentShouldThrowExceptionWithIncorrectArgumentName()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ViewComponentResultByType())
                        .ShouldReturn()
                        .ViewComponent()
                        .ContainingArgumentWithName("id");
                },
                "When calling ViewComponentResultByType action in MvcController expected view component result arguments to have entry with 'id' key, but such was not found.");
        }

        [Fact]
        public void WithArgumentWithSameKeyAndDiffeentValueShouldThrowException()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ViewComponentResultByType())
                        .ShouldReturn()
                        .ViewComponent()
                        .ContainingArgument("model", new { model = 1 });
                },
                "When calling ViewComponentResultByType action in MvcController expected view component result arguments to have entry with 'model' key and the provided value, but the value was different.");
        }

        [Fact]
        public void WithArgumentWithSameKeyAndDiffeentNameShouldThrowException()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ViewComponentResultByType())
                        .ShouldReturn()
                        .ViewComponent()
                        .ContainingArgument("id", new { model = 1 });
                },
                "When calling ViewComponentResultByType action in MvcController expected view component result arguments to have entry with 'id' key and the provided value, but such was not found.");
        }

        [Fact]
        public void WithArgumentShouldNotThrowExceptionWithCorrectArgument()
        {
            var responseModels = TestObjectFactory.GetListOfResponseModels();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByType())
                .ShouldReturn()
                .ViewComponent()
                .ContainingArgument(responseModels);
        }

        [Fact]
        public void WithArgumentShouldNotThrowExceptionWithCorrectArgumentAndName()
        {
            var responseModels = TestObjectFactory.GetListOfResponseModels();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByType())
                .ShouldReturn()
                .ViewComponent()
                .ContainingArgument("model", responseModels);
        }

        [Fact]
        public void WithArgumentShouldThrowExceptionWithIncorrectArgument()
        {
            Test.AssertException<ViewResultAssertionException>(
                   () =>
                   {
                       MyController<MvcController>
                           .Instance()
                           .Calling(c => c.ViewComponentResultByType())
                           .ShouldReturn()
                           .ViewComponent()
                           .ContainingArgument(1);
                   },
                   "When calling ViewComponentResultByType action in MvcController expected view component result arguments to have entry with the provided value, but none was found.");
        }

        [Fact]
        public void WithArgumentShouldNotThrowExceptionWithCorrectArgumentOfType()
        {
            var responseModels = TestObjectFactory.GetListOfResponseModels();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByType())
                .ShouldReturn()
                .ViewComponent()
                .ContainingArgumentOfType<List<ResponseModel>>();
        }

        [Fact]
        public void WithArgumentShouldThrowExceptionWithIncorrectArgumentOfType()
        {
            Test.AssertException<ViewResultAssertionException>(
                   () =>
                   {
                       MyController<MvcController>
                           .Instance()
                           .Calling(c => c.ViewComponentResultByType())
                           .ShouldReturn()
                           .ViewComponent()
                           .ContainingArgumentOfType<int>();
                   },
                   "When calling ViewComponentResultByType action in MvcController expected view component result arguments to have at least one entry of Int32 type, but none was found.");
        }

        [Fact]
        public void WithArgumentsShouldNotThrowExceptionWithCorrectArguments()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent()
                .ContainingArguments(new { id = 1, test = "text" });
        }

        [Fact]
        public void WithArgumentsShouldThrowExceptionWithIncorrectArgumentsCount()
        {
            Test.AssertException<ViewResultAssertionException>(
                    (() =>
                    {
                        MyController<MvcController>
                             .Instance()
                             .Calling(c => c.ViewComponentResultByName())
                             .ShouldReturn()
                             .ViewComponent()
                             .ContainingArguments(new { id = 1, text = "text", incorrect = 15 });
                    }),
                   "When calling ViewComponentResultByName action in MvcController expected view component result arguments to have 3 entries, but in fact found 2.");
        }

        [Fact]
        public void WithArgumentsShouldThrowExceptionWithIncorrectArguments()
        {
            Test.AssertException<ViewResultAssertionException>(
                    (() =>
                    {
                        MyController<MvcController>
                             .Instance()
                             .Calling(c => c.ViewComponentResultByName())
                             .ShouldReturn()
                             .ViewComponent()
                             .ContainingArguments(new { id = 1, test = "incorrect" });
                    }),
                   "When calling ViewComponentResultByName action in MvcController expected view component result arguments to have entry with 'test' key and the provided value, but the value was different.");
        }

        [Fact]
        public void WithArgumentsShouldThrowExceptionWithCorrectArgumentsAsDictionary()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent()
                .ContainingArguments(new Dictionary<string, object> { ["id"] = 1, ["test"] = "text" });
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent()
                .WithContentType(ContentType.ApplicationXml)
                .AndAlso()
                .WithStatusCode(500);
        }

        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<ViewComponentResult>(actionResult);
                });
        }
    }
}
