namespace MyTested.Mvc.Test.BuildersTests.ActionResultsTests.HttpBadRequestTests
{
    using System.Collections.Generic;
    using System.Net;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class HttpBadRequestTestBuilderTests
    {
        [Fact]
        public void WithErrorShouldNotThrowExceptionWithCorrectErrorObject()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestWithCustomError())
                .ShouldReturn()
                .BadRequest()
                .WithError(TestObjectFactory.GetListOfResponseModels());
        }

        [Fact]
        public void WithErrorOfTypeShouldNotThrowExceptionWithCorrectErrorObject()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestWithCustomError())
                .ShouldReturn()
                .BadRequest()
                .WithErrorOfType<List<ResponseModel>>();
        }

        [Fact]
        public void WithNoErrorShouldNotThrowExceptionWithNoError()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .WithNoError();
        }

        [Fact]
        public void WithNoErrorShouldThrowExceptionWithError()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestWithCustomError())
                        .ShouldReturn()
                        .BadRequest()
                        .WithNoError();
                },
                "When calling BadRequestWithCustomError action in MvcController expected bad request result to not have error message, but in fact such was found.");
        }

        [Fact]
        public void WithErrorMessageShouldNotThrowExceptionWhenResultHasErrorMessage()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage();
        }

        [Fact]
        public void WithErrorMessageShouldThrowExceptionWhenResultDoesNotHaveErrorMessage()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .BadRequest()
                        .WithErrorMessage();
                }, 
                "When calling BadRequestAction action in MvcController expected bad request result to contain error object, but it could not be found.");
        }

        [Fact]
        public void WithErrorMessageShouldNotThrowExceptionWhenResultHasCorrentErrorMessage()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage("Bad request");
        }

        [Fact]
        public void WithErrorMessageShouldThrowExceptionWhenResultDoesNotHaveCorrentErrorMessage()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestWithErrorAction())
                        .ShouldReturn()
                        .BadRequest()
                        .WithErrorMessage("Good request");
                }, 
                "When calling BadRequestWithErrorAction action in MvcController expected bad request result with message 'Good request', but instead received 'Bad request'.");
        }

        [Fact]
        public void WithErrorMessageShouldThrowExceptionWhenResultIsNotString()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                        .ShouldReturn()
                        .BadRequest()
                        .WithErrorMessage("Good request");
                },
                "When calling BadRequestWithModelState action in MvcController expected bad request result with error message, but instead received non-string value.");
        }

        [Fact]
        public void WithModelStateShouldNotThrowExceptionWhenModelStateHasDefaultErrors()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                .ShouldReturn()
                .BadRequest()
                .WithModelStateError();
        }

        [Fact]
        public void WithModelStateShouldNotThrowExceptionWhenModelStateHasSameErrors()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
            modelState.AddModelError("RequiredString", "The RequiredString field is required.");

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                .ShouldReturn()
                .BadRequest()
                .WithModelStateError(modelState);
        }

        [Fact]
        public void WithModelStateShouldThrowExceptionWhenModelStateHasDifferentKeys()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
                    var modelState = new ModelStateDictionary();
                    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
                    modelState.AddModelError("String", "The RequiredString field is required.");

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                        .ShouldReturn()
                        .BadRequest()
                        .WithModelStateError(modelState);
                }, 
                "When calling BadRequestWithModelState action in MvcController expected bad request model state dictionary to contain String key, but none found.");
        }

        [Fact]
        public void WithModelStateShouldThrowExceptionWhenErrorIsNotModelState()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    var modelState = new ModelStateDictionary();

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestWithErrorAction())
                        .ShouldReturn()
                        .BadRequest()
                        .WithModelStateError(modelState);
                },
                "When calling BadRequestWithErrorAction action in MvcController expected bad request result with model state dictionary as error, but instead received other type of error.");
        }

        [Fact]
        public void WithModelStateShouldThrowExceptionWhenModelStateHasLessNumberOfKeys()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
                    var modelState = new ModelStateDictionary();
                    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                        .ShouldReturn()
                        .BadRequest()
                        .WithModelStateError(modelState);
                }, 
                "When calling BadRequestWithModelState action in MvcController expected bad request model state dictionary to contain 1 keys, but found 2.");
        }

        [Fact]
        public void WithModelStateShouldThrowExceptionWhenModelStateHasMoreNumberOfKeys()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
                    var modelState = new ModelStateDictionary();
                    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
                    modelState.AddModelError("RequiredString", "The RequiredString field is required.");
                    modelState.AddModelError("NonRequiredString", "The NonRequiredString field is required.");

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                        .ShouldReturn()
                        .BadRequest()
                        .WithModelStateError(modelState);
                }, 
                "When calling BadRequestWithModelState action in MvcController expected bad request model state dictionary to contain 3 keys, but found 2.");
        }

        [Fact]
        public void WithModelStateShouldThrowExceptionWhenModelStateHasWrongError()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
                    var modelState = new ModelStateDictionary();
                    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
                    modelState.AddModelError("RequiredString", "The RequiredString field is not required.");

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                        .ShouldReturn()
                        .BadRequest()
                        .WithModelStateError(modelState);
                },
                "When calling BadRequestWithModelState action in MvcController expected bad request result with message 'The RequiredString field is not required.', but instead received 'The RequiredString field is required.'.");
        }

        [Fact]
        public void WithModelStateShouldThrowExceptionWhenModelStateHasMoreErrors()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
                    var modelState = new ModelStateDictionary();
                    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
                    modelState.AddModelError("RequiredString", "The RequiredString field is not required.");
                    modelState.AddModelError("RequiredString", "The RequiredString field is required.");

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                        .ShouldReturn()
                        .BadRequest()
                        .WithModelStateError(modelState);
                }, 
                "When calling BadRequestWithModelState action in MvcController expected bad request model state dictionary to contain 2 errors for RequiredString key, but found 1.");
        }

        [Fact]
        public void WithModelStateForShouldNotThrowExceptionWhenModelStateHasSameErrors()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
            modelState.AddModelError("RequiredString", "The RequiredString field is required.");

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                .ShouldReturn()
                .BadRequest()
                .WithModelStateErrorFor<RequestModel>()
                .ContainingErrorFor(m => m.Integer).ThatEquals("The field Integer must be between 1 and 2147483647.")
                .AndAlso()
                .ContainingErrorFor(m => m.RequiredString).BeginningWith("The RequiredString")
                .AndAlso()
                .ContainingErrorFor(m => m.RequiredString).EndingWith("required.")
                .AndAlso()
                .ContainingErrorFor(m => m.RequiredString).Containing("field")
                .AndAlso()
                .ContainingNoErrorFor(m => m.NonRequiredString);
        }
        
        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .WithStatusCode(201);
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .WithStatusCode(HttpStatusCode.Created);
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest()
                        .WithStatusCode(HttpStatusCode.OK);
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result to have 200 (OK) status code, but instead received 201 (Created).");
        }

        [Fact]
        public void ContainingContentTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .ContainingContentType(ContentType.ApplicationJson);
        }

        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson));
        }
        
        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.FullHttpBadRequestAction())
                       .ShouldReturn()
                       .BadRequest()
                       .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationOctetStream));
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result content types to contain application/octet-stream, but such was not found.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .ContainingContentTypes(new List<string>
                {
                    ContentType.ApplicationJson,
                    ContentType.ApplicationXml
                });
        }

        [Fact]
        public void ContainingContentTypesAsStringShouldNotThrowExceptionWithCorrectParametersValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .ContainingContentTypes(ContentType.ApplicationJson, ContentType.ApplicationXml);
        }

        [Fact]
        public void ContainingContentTypesStringValueShouldNotThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest()
                        .ContainingContentTypes(new List<string>
                        {
                            ContentType.ApplicationOctetStream,
                            ContentType.ApplicationXml
                        });
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result content types to contain application/octet-stream, but none was found.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest()
                        .ContainingContentTypes(new List<string>
                        {
                            ContentType.ApplicationXml
                        });
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result content types to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest()
                        .ContainingContentTypes(new List<string>
                        {
                            ContentType.ApplicationXml,
                            ContentType.ApplicationJson,
                            ContentType.ApplicationZip
                        });
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result content types to have 3 items, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .ContainingContentTypes(new List<MediaTypeHeaderValue>
                {
                    new MediaTypeHeaderValue(ContentType.ApplicationJson),
                    new MediaTypeHeaderValue(ContentType.ApplicationXml)
                });
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectParametersValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .ContainingContentTypes(new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml));
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest()
                        .ContainingContentTypes(new List<MediaTypeHeaderValue>
                        {
                            new MediaTypeHeaderValue(ContentType.ApplicationOctetStream),
                            new MediaTypeHeaderValue(ContentType.ApplicationXml)
                        });
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result content types to contain application/octet-stream, but none was found.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest()
                        .ContainingContentTypes(new List<MediaTypeHeaderValue>
                        {
                            new MediaTypeHeaderValue(ContentType.ApplicationXml)
                        });
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result content types to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueValueShouldNotThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest()
                        .ContainingContentTypes(new List<MediaTypeHeaderValue>
                        {
                            new MediaTypeHeaderValue(ContentType.ApplicationXml),
                            new MediaTypeHeaderValue(ContentType.ApplicationJson),
                            new MediaTypeHeaderValue(ContentType.ApplicationZip)
                        });
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result content types to have 3 items, but instead found 2.");
        }

        [Fact]
        public void ContainingOutputFormatterShouldNotThrowExceptionWithCorrectValue()
        {
            var formatter = TestObjectFactory.GetOutputFormatter();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.HttpBadRequestActionWithFormatter(formatter))
                .ShouldReturn()
                .BadRequest()
                .ContainingOutputFormatter(formatter);
        }

        [Fact]
        public void ContainingOutputFormatterShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    var formatter = TestObjectFactory.GetOutputFormatter();

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.HttpBadRequestActionWithFormatter(formatter))
                        .ShouldReturn()
                        .BadRequest()
                        .ContainingOutputFormatter(new JsonOutputFormatter());
                },
                "When calling HttpBadRequestActionWithFormatter action in MvcController expected bad request result output formatters to contain the provided formatter, but such was not found.");
        }

        [Fact]
        public void ContainingOutputFormatterOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .ContainingOutputFormatterOfType<JsonOutputFormatter>();
        }

        [Fact]
        public void ContainingOutputFormatterOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest()
                        .ContainingOutputFormatterOfType<IOutputFormatter>();
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result output formatters to contain formatter of IOutputFormatter type, but such was not found.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .ContainingOutputFormatters(new List<IOutputFormatter>
                {
                    new JsonOutputFormatter(),
                    new CustomOutputFormatter()
                });
        }

        [Fact]
        public void ContainingOutputFormattersShouldNotThrowExceptionWithCorrectParametersValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .ContainingOutputFormatters(new JsonOutputFormatter(), new CustomOutputFormatter());
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            new JsonOutputFormatter(),
                            new HttpNotAcceptableOutputFormatter()
                        });
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result output formatters to contain formatter of HttpNotAcceptableOutputFormatter type, but none was found.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            new JsonOutputFormatter()
                        });
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result output formatters to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            new JsonOutputFormatter(),
                            new CustomOutputFormatter(),
                            new JsonOutputFormatter()
                        });
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result output formatters to have 3 items, but instead found 2.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .WithStatusCode(201)
                .AndAlso()
                .ContainingOutputFormatters(new JsonOutputFormatter(), new CustomOutputFormatter());
        }

        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            var actionResult = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .AndProvideTheActionResult();

            Assert.NotNull(actionResult);
            Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
        }
    }
}
