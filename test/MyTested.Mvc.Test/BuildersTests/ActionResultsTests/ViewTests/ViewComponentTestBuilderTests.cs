namespace MyTested.Mvc.Test.BuildersTests.ActionResultsTests.ViewTests
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
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent()
                .WithStatusCode(500);
        }

        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent()
                .WithContentType(ContentType.ApplicationXml);
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent()
                .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationXml));
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValueConstant()
        {
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentWithViewEngine(viewEngine))
                .ShouldReturn()
                .ViewComponent()
                .WithViewEngine(viewEngine);
        }

        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithNullViewEngine()
        {
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentResultByType())
                .ShouldReturn()
                .ViewComponent()
                .ContainingArgument("model");
        }

        [Fact]
        public void WithArgumentShouldThrowExceptionWithIncorrectArgumentName()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ViewComponentResultByType())
                        .ShouldReturn()
                        .ViewComponent()
                        .ContainingArgument("id");
                },
                "When calling ViewComponentResultByType action in MvcController expected view component result arguments to have entry with key 'id', but such was not found.");
        }

        [Fact]
        public void WithArgumentWithSameKeyAndDiffeentValueShouldThrowException()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentResultByType())
                .ShouldReturn()
                .ViewComponent()
                .ContainingArgument(responseModels);
        }

        [Fact]
        public void WithArgumentShouldThrowExceptionWithIncorrectArgument()
        {
            Test.AssertException<ViewResultAssertionException>(
                   () =>
                   {
                       MyMvc
                           .Controller<MvcController>()
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

            MyMvc
                .Controller<MvcController>()
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
                       MyMvc
                           .Controller<MvcController>()
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
            MyMvc
                .Controller<MvcController>()
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
                        MyMvc
                         .Controller<MvcController>()
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
                        MyMvc
                         .Controller<MvcController>()
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
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent()
                .ContainingArguments(new Dictionary<string, object> { ["id"] = 1, ["test"] = "text" });
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
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
            var actionResult = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent()
                .AndProvideTheActionResult();

            Assert.NotNull(actionResult);
            Assert.IsAssignableFrom<ViewComponentResult>(actionResult);
        }
    }
}
