namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.CreatedTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Exceptions;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class CreatedTestBuilderTests
    {
        [Fact]
        public void AtLocationWithStringShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .AtLocation("http://somehost.com/someuri/1?query=Test");
        }

        [Fact]
        public void AtLocationWithStringShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CreatedAction())
                        .ShouldReturn()
                        .Created()
                        .AtLocation("http://somehost.com/");
                },
                "When calling CreatedAction action in MvcController expected created result location to be 'http://somehost.com/', but instead received 'http://somehost.com/someuri/1?query=Test'.");
        }

        [Fact]
        public void AtLocationWithStringShouldThrowExceptionIfTheLocationIsNotValid()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CreatedAction())
                        .ShouldReturn()
                        .Created()
                        .AtLocation("http://somehost!@#?Query==true");
                },
                "When calling CreatedAction action in MvcController expected created result location to be URI valid, but instead received 'http://somehost!@#?Query==true'.");
        }

        [Fact]
        public void AtLocationWithUriShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .AtLocation(new Uri("http://somehost.com/someuri/1?query=Test"));
        }

        [Fact]
        public void AtLocationWithUriShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CreatedAction())
                        .ShouldReturn()
                        .Created()
                        .AtLocation(new Uri("http://somehost.com/"));
                },
                "When calling CreatedAction action in MvcController expected created result location to be 'http://somehost.com/', but instead received 'http://somehost.com/someuri/1?query=Test'.");
        }

        [Fact]
        public void AtLocationWithBuilderShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .AtLocation(location =>
                    location
                        .WithHost("somehost.com")
                        .AndAlso()
                        .WithAbsolutePath("/someuri/1")
                        .AndAlso()
                        .WithPort(80)
                        .AndAlso()
                        .WithScheme("http")
                        .AndAlso()
                        .WithFragment(string.Empty)
                        .AndAlso()
                        .WithQuery("?query=Test"));
        }

        [Fact]
        public void AtLocationWithBuilderShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CreatedAction())
                        .ShouldReturn()
                        .Created()
                        .AtLocation(location =>
                            location
                                .WithHost("somehost12.com")
                                .AndAlso()
                                .WithAbsolutePath("/someuri/1")
                                .AndAlso()
                                .WithPort(80)
                                .AndAlso()
                                .WithScheme("http")
                                .AndAlso()
                                .WithFragment(string.Empty)
                                .AndAlso()
                                .WithQuery("?query=Test"));
                },
                "When calling CreatedAction action in MvcController expected created result URI to equal the provided one, but was in fact different.");
        }

        [Fact]
        public void AtActionShouldNotThrowExceptionWithCorrectActionName()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAtActionResult())
                .ShouldReturn()
                .Created()
                .AtAction("MyAction");
        }

        [Fact]
        public void AtActionShouldThrowExceptionWithIncorrectActionName()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.CreatedAtActionResult())
                    .ShouldReturn()
                    .Created()
                    .AtAction("Action");
                },
                "When calling CreatedAtActionResult action in MvcController expected created result to have 'Action' action name, but instead received 'MyAction'.");
        }

        [Fact]
        public void AtControllerShouldNotThrowExceptionWithCorrectControllerName()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAtActionResult())
                .ShouldReturn()
                .Created()
                .AtController("MyController");
        }

        [Fact]
        public void AtControllerShouldThrowExceptionWithIncorrectControllerName()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.CreatedAtActionResult())
                    .ShouldReturn()
                    .Created()
                    .AtController("Controller");
                },
                "When calling CreatedAtActionResult action in MvcController expected created result to have 'Controller' controller name, but instead received 'MyController'.");
        }

        [Fact]
        public void WithRouteNameShouldNotThrowExceptionWithCorrectRouteName()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAtRouteAction())
                .ShouldReturn()
                .Created()
                .WithRouteName("Redirect");
        }

        [Fact]
        public void WithRouteNameShouldThrowExceptionWithIncorrectRouteName()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CreatedAtRouteAction())
                        .ShouldReturn()
                        .Created()
                        .WithRouteName("MyRedirect");
                },
                "When calling CreatedAtRouteAction action in MvcController expected created result to have 'MyRedirect' route name, but instead received 'Redirect'.");
        }

        [Fact]
        public void WithRouteValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAtActionResult())
                .ShouldReturn()
                .Created()
                .WithRouteValue("id");
        }

        [Fact]
        public void WithRouteValueShouldThrowExceptionWithIncorrectRouteValue()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CreatedAtActionResult())
                        .ShouldReturn()
                        .Created()
                        .WithRouteValue("incorrect");
                },
                "When calling CreatedAtActionResult action in MvcController expected created result route values to have item with key 'incorrect', but such was not found.");
        }

        [Fact]
        public void WithRouteValueWithValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAtActionResult())
                .ShouldReturn()
                .Created()
                .WithRouteValue("id", 1);
        }

        [Fact]
        public void WithRouteValueWithValueShouldThrowExceptionWithIncorrectRouteKey()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CreatedAtActionResult())
                        .ShouldReturn()
                        .Created()
                        .WithRouteValue("incorrect", 1);
                },
                "When calling CreatedAtActionResult action in MvcController expected created result route values to have item with 'incorrect' key and the provided value, but such was not found.");
        }

        [Fact]
        public void WithRouteValueWithValueShouldThrowExceptionWithIncorrectRouteValue()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CreatedAtActionResult())
                        .ShouldReturn()
                        .Created()
                        .WithRouteValue("id", 2);
                },
                "When calling CreatedAtActionResult action in MvcController expected created result route values to have item with 'id' key and the provided value, but the value was different.");
        }

        [Fact]
        public void WithRouteValuesWithObjectShouldNotThrowExceptionWithCorrectRouteValues()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAtActionResult())
                .ShouldReturn()
                .Created()
                .WithRouteValues(new { id = 1, text = "sometext" });
        }

        [Fact]
        public void WithRouteValuesWithObjectShouldThrowExceptionWithIncorrectRouteValues()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CreatedAtActionResult())
                        .ShouldReturn()
                        .Created()
                        .WithRouteValues(new { id = 1 });
                },
                "When calling CreatedAtActionResult action in MvcController expected created result route values to have 1 item, but in fact found 2.");
        }

        [Fact]
        public void WithRouteValuesWithObjectShouldThrowExceptionWithIncorrectMoreRouteValues()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CreatedAtActionResult())
                        .ShouldReturn()
                        .Created()
                        .WithRouteValues(new { id = 1, second = 5, another = "test" });
                },
                "When calling CreatedAtActionResult action in MvcController expected created result route values to have 3 items, but in fact found 2.");
        }

        [Fact]
        public void WithCustomUrlHelperShouldNotThrowExceptionWithCorrectUrlHelper()
        {
            var urlHelper = TestObjectFactory.GetCustomUrlHelper();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAtActionWithCustomHelperResult(urlHelper))
                .ShouldReturn()
                .Created()
                .WithUrlHelper(urlHelper);
        }

        [Fact]
        public void WithCustomUrlHelperShouldThrowExceptionWithIncorrectUrlHelper()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    var urlHelper = TestObjectFactory.GetCustomUrlHelper();

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CreatedAtActionWithCustomHelperResult(urlHelper))
                        .ShouldReturn()
                        .Created()
                        .WithUrlHelper(null);
                },
                "When calling CreatedAtActionWithCustomHelperResult action in MvcController expected created result UrlHelper to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void WithCustomUrlHelperOfTypeShouldNotThrowExceptionWithCorrectUrlHelper()
        {
            var urlHelper = TestObjectFactory.GetCustomUrlHelper();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAtActionWithCustomHelperResult(urlHelper))
                .ShouldReturn()
                .Created()
                .WithUrlHelperOfType<CustomUrlHelper>();
        }

        [Fact]
        public void WithCustomUrlHelperOfTypeShouldThrowExceptionWithIncorrectUrlHelper()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    var urlHelper = TestObjectFactory.GetCustomUrlHelper();

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CreatedAtActionWithCustomHelperResult(urlHelper))
                        .ShouldReturn()
                        .Created()
                        .WithUrlHelperOfType<IUrlHelper>();
                },
                "When calling CreatedAtActionWithCustomHelperResult action in MvcController expected created result UrlHelper to be of IUrlHelper type, but instead received CustomUrlHelper.");
        }

        [Fact]
        public void WithCustomUrlHelperOfTypeShouldThrowExceptionWithNoUrlHelper()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CreatedAtActionResult())
                        .ShouldReturn()
                        .Created()
                        .WithUrlHelperOfType<IUrlHelper>();
                },
                "When calling CreatedAtActionResult action in MvcController expected created result UrlHelper to be of IUrlHelper type, but instead received null.");
        }

        [Fact]
        public void EverySpecificMethodShouldThrowExceptionWhenSuchPropertyIsNotFound()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CreatedAtRouteAction())
                        .ShouldReturn()
                        .Created()
                        .AtController("Controller");
                },
                "When calling CreatedAtRouteAction action in MvcController expected created result to contain controller name, but it could not be found.");
        }

        [Fact]
        public void WithResponseModelOfTypeShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .WithResponseModelOfType<ICollection<ResponseModel>>();
        }

        [Fact]
        public void AtShouldWorkCorrectlyWithCorrectActionCall()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAtRouteAction())
                .ShouldReturn()
                .Created()
                .At<NoAttributesController>(c => c.WithParameter(1));
        }

        [Fact]
        public void AtShouldWorkCorrectlyWithCorrectVoidActionCall()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAtRouteVoidAction())
                .ShouldReturn()
                .Created()
                .At<NoAttributesController>(c => c.VoidAction());
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAtActionResult())
                .ShouldReturn()
                .Created()
                .AtAction("MyAction")
                .AndAlso()
                .AtController("MyController");
        }

        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullCreatedAction())
                .ShouldReturn()
                .Created()
                .WithStatusCode(201);
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullCreatedAction())
                .ShouldReturn()
                .Created()
                .WithStatusCode(HttpStatusCode.Created);
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullCreatedAction())
                        .ShouldReturn()
                        .Created()
                        .WithStatusCode(HttpStatusCode.OK);
                },
                "When calling FullCreatedAction action in MvcController expected created result to have 200 (OK) status code, but instead received 201 (Created).");
        }

        [Fact]
        public void ContainingContentTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullCreatedAction())
                .ShouldReturn()
                .Created()
                .ContainingContentType(ContentType.ApplicationJson);
        }

        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullCreatedAction())
                .ShouldReturn()
                .Created()
                .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson));
        }

        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.FullCreatedAction())
                       .ShouldReturn()
                       .Created()
                       .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationOctetStream));
                },
                "When calling FullCreatedAction action in MvcController expected created result content types to contain application/octet-stream, but such was not found.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullCreatedAction())
                .ShouldReturn()
                .Created()
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
                .Calling(c => c.FullCreatedAction())
                .ShouldReturn()
                .Created()
                .ContainingContentTypes(ContentType.ApplicationJson, ContentType.ApplicationXml);
        }

        [Fact]
        public void ContainingContentTypesStringValueShouldNotThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullCreatedAction())
                        .ShouldReturn()
                        .Created()
                        .ContainingContentTypes(new List<string>
                        {
                            ContentType.ApplicationOctetStream,
                            ContentType.ApplicationXml
                        });
                },
                "When calling FullCreatedAction action in MvcController expected created result content types to contain application/octet-stream, but none was found.");
        }
        
        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullCreatedAction())
                        .ShouldReturn()
                        .Created()
                        .ContainingContentTypes(new List<string>
                        {
                            ContentType.ApplicationXml
                        });
                },
                "When calling FullCreatedAction action in MvcController expected created result content types to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullCreatedAction())
                        .ShouldReturn()
                        .Created()
                        .ContainingContentTypes(new List<string>
                        {
                            ContentType.ApplicationXml,
                            ContentType.ApplicationJson,
                            ContentType.ApplicationZip
                        });
                },
                "When calling FullCreatedAction action in MvcController expected created result content types to have 3 items, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullCreatedAction())
                .ShouldReturn()
                .Created()
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
                .Calling(c => c.FullCreatedAction())
                .ShouldReturn()
                .Created()
                .ContainingContentTypes(new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml));
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullCreatedAction())
                        .ShouldReturn()
                        .Created()
                        .ContainingContentTypes(new List<MediaTypeHeaderValue>
                        {
                            new MediaTypeHeaderValue(ContentType.ApplicationOctetStream),
                            new MediaTypeHeaderValue(ContentType.ApplicationXml)
                        });
                },
                "When calling FullCreatedAction action in MvcController expected created result content types to contain application/octet-stream, but none was found.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullCreatedAction())
                        .ShouldReturn()
                        .Created()
                        .ContainingContentTypes(new List<MediaTypeHeaderValue>
                        {
                            new MediaTypeHeaderValue(ContentType.ApplicationXml)
                        });
                },
                "When calling FullCreatedAction action in MvcController expected created result content types to have 1 item, but instead found 2.");
        }
        
        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueValueShouldNotThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullCreatedAction())
                        .ShouldReturn()
                        .Created()
                        .ContainingContentTypes(new List<MediaTypeHeaderValue>
                        {
                            new MediaTypeHeaderValue(ContentType.ApplicationXml),
                            new MediaTypeHeaderValue(ContentType.ApplicationJson),
                            new MediaTypeHeaderValue(ContentType.ApplicationZip)
                        });
                },
                "When calling FullCreatedAction action in MvcController expected created result content types to have 3 items, but instead found 2.");
        }

        [Fact]
        public void ContainingOutputFormatterShouldNotThrowExceptionWithCorrectValue()
        {
            var formatter = TestObjectFactory.GetOutputFormatter();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedActionWithFormatter(formatter))
                .ShouldReturn()
                .Created()
                .ContainingOutputFormatter(formatter);
        }
        
        [Fact]
        public void ContainingOutputFormatterShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    var formatter = TestObjectFactory.GetOutputFormatter();

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CreatedActionWithFormatter(formatter))
                        .ShouldReturn()
                        .Created()
                        .ContainingOutputFormatter(new JsonOutputFormatter());
                },
                "When calling CreatedActionWithFormatter action in MvcController expected created result output formatters to contain the provided formatter, but such was not found.");
        }
        
        [Fact]
        public void ContainingOutputFormatterOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullCreatedAction())
                .ShouldReturn()
                .Created()
                .ContainingOutputFormatterOfType<JsonOutputFormatter>();
        }

        [Fact]
        public void ContainingOutputFormatterOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullCreatedAction())
                        .ShouldReturn()
                        .Created()
                        .ContainingOutputFormatterOfType<IOutputFormatter>();
                },
                "When calling FullCreatedAction action in MvcController expected created result output formatters to contain formatter of IOutputFormatter type, but such was not found.");
        }
        
        [Fact]
        public void ContainingOutputFormattersShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullCreatedAction())
                .ShouldReturn()
                .Created()
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
                .Calling(c => c.FullCreatedAction())
                .ShouldReturn()
                .Created()
                .ContainingOutputFormatters(new JsonOutputFormatter(), new CustomOutputFormatter());
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullCreatedAction())
                        .ShouldReturn()
                        .Created()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            new JsonOutputFormatter(),
                            new HttpNotAcceptableOutputFormatter()
                        });
                },
                "When calling FullCreatedAction action in MvcController expected created result output formatters to contain formatter of HttpNotAcceptableOutputFormatter type, but none was found.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullCreatedAction())
                        .ShouldReturn()
                        .Created()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            new JsonOutputFormatter()
                        });
                },
                "When calling FullCreatedAction action in MvcController expected created result output formatters to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullCreatedAction())
                        .ShouldReturn()
                        .Created()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            new JsonOutputFormatter(),
                            new CustomOutputFormatter(),
                            new JsonOutputFormatter()
                        });
                },
                "When calling FullCreatedAction action in MvcController expected created result output formatters to have 3 items, but instead found 2.");
        }

        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            var actionResult = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .AndProvideTheActionResult();

            Assert.NotNull(actionResult);
            Assert.IsAssignableFrom<CreatedResult>(actionResult);
        }
    }
}
