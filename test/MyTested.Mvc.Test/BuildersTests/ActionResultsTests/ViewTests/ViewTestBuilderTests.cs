namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.ViewTests
{
    using System.Collections.Generic;
    using System.Net;
    using Exceptions;
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
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.IndexView())
                        .ShouldReturn()
                        .View("Index")
                        .WithModel(model);
                },
                "When calling IndexView action in MvcController expected response model List<ResponseModel> to be the given model, but in fact it was a different.");
        }

        [Fact]
        public void WithModelOfTypeShouldNotThrowExceptionWithCorrectType()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.IndexView())
                .ShouldReturn()
                .View("Index")
                .WithModelOfType<List<ResponseModel>>();
        }

        [Fact]
        public void WithStatusCodeAsIntShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomViewResult())
                .ShouldReturn()
                .View()
                .WithStatusCode(500);
        }

        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomViewResult())
                .ShouldReturn()
                .View()
                .WithContentType(ContentType.ApplicationXml);
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomViewResult())
                .ShouldReturn()
                .View()
                .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationXml));
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValueConstant()
        {
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CustomViewResult())
                        .ShouldReturn()
                        .View()
                        .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson));
                },
                "When calling CustomViewResult action in MvcController expected view result ContentType to be application/json, but instead received application/xml.");
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValue()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CustomViewResult())
                        .ShouldReturn()
                        .View()
                        .WithContentType((MediaTypeHeaderValue)null);
                },
                "When calling CustomViewResult action in MvcController expected view result ContentType to be null, but instead received application/xml.");
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValueAndNullActual()
        {
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.DefaultView())
                        .ShouldReturn()
                        .View()
                        .WithContentType(new MediaTypeHeaderValue(TestObjectFactory.MediaType));
                },
                "When calling DefaultView action in MvcController expected view result ContentType to be application/json, but instead received null.");
        }

        // TODO: Add the same for partial views
    }
}
