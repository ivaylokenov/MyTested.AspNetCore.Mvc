namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.CreatedTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
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
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .AtLocation("http://somehost.com/someuri/1?query=Test");
        }

        [Fact]
        public void AtLocationWithStringShouldNotThrowExceptionIfTheLocationIsCorrectWithPredicate()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CreatedAction())
                        .ShouldReturn()
                        .Created()
                        .AtLocationPassing(location => location.StartsWith("http://somehost.com/someuri/2"));
                },
                "When calling CreatedAction action in MvcController expected created result location ('http://somehost.com/someuri/1?query=Test') to pass the given predicate, but it failed.");
        }

        [Fact]
        public void AtLocationWithStringShouldNotThrowExceptionIfTheLocationIsCorrectWithAssertions()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .AtLocationPassing(location =>
                {
                    Assert.Equal("http://somehost.com/someuri/1?query=Test", location);
                });
        }

        [Fact]
        public void AtLocationWithStringShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
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
                    MyController<MvcController>
                        .Instance()
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
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                        .Instance()
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
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                        .Instance()
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
                "When calling CreatedAction action in MvcController expected created result URI to be 'http://somehost12.com/someuri/1?query=Test', but was in fact 'http://somehost.com/someuri/1?query=Test'.");
        }

        [Fact]
        public void AtActionShouldNotThrowExceptionWithCorrectActionName()
        {
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                    .Instance()
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
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                    .Instance()
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
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                        .Instance()
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
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAtActionResult())
                .ShouldReturn()
                .Created()
                .ContainingRouteKey("id");
        }

        [Fact]
        public void WithRouteValueWithValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAtActionResult())
                .ShouldReturn()
                .Created()
                .ContainingRouteValue(1);
        }
        
        [Fact]
        public void WithRouteValueOfTypeWithValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAtActionResult())
                .ShouldReturn()
                .Created()
                .ContainingRouteValueOfType<string>();
        }
        
        [Fact]
        public void WithRouteValueOfTypeWithKeyWithValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAtActionResult())
                .ShouldReturn()
                .Created()
                .ContainingRouteValueOfType<int>("id");
        }

        [Fact]
        public void WithRouteValueShouldThrowExceptionWithIncorrectRouteValue()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CreatedAtActionResult())
                        .ShouldReturn()
                        .Created()
                        .ContainingRouteKey("incorrect");
                },
                "When calling CreatedAtActionResult action in MvcController expected created result route values to have entry with 'incorrect' key, but such was not found.");
        }

        [Fact]
        public void WithRouteValueWithValueAndKeyShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAtActionResult())
                .ShouldReturn()
                .Created()
                .ContainingRouteValue("id", 1);
        }

        [Fact]
        public void WithRouteValueWithValueShouldThrowExceptionWithIncorrectRouteKey()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CreatedAtActionResult())
                        .ShouldReturn()
                        .Created()
                        .ContainingRouteValue("incorrect", 1);
                },
                "When calling CreatedAtActionResult action in MvcController expected created result route values to have entry with 'incorrect' key and the provided value, but such was not found.");
        }

        [Fact]
        public void WithRouteValueWithValueShouldThrowExceptionWithIncorrectRouteValue()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CreatedAtActionResult())
                        .ShouldReturn()
                        .Created()
                        .ContainingRouteValue("id", 2);
                },
                "When calling CreatedAtActionResult action in MvcController expected created result route values to have entry with 'id' key and the provided value, but the value was different.");
        }

        [Fact]
        public void WithRouteValuesWithObjectShouldNotThrowExceptionWithCorrectRouteValues()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAtActionResult())
                .ShouldReturn()
                .Created()
                .ContainingRouteValues(new { id = 1, text = "sometext" });
        }

        [Fact]
        public void WithRouteValuesWithObjectShouldThrowExceptionWithIncorrectRouteValues()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CreatedAtActionResult())
                        .ShouldReturn()
                        .Created()
                        .ContainingRouteValues(new { id = 1 });
                },
                "When calling CreatedAtActionResult action in MvcController expected created result route values to have 1 entry, but in fact found 2.");
        }

        [Fact]
        public void WithRouteValuesWithObjectShouldThrowExceptionWithIncorrectMoreRouteValues()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CreatedAtActionResult())
                        .ShouldReturn()
                        .Created()
                        .ContainingRouteValues(new { id = 1, second = 5, another = "test" });
                },
                "When calling CreatedAtActionResult action in MvcController expected created result route values to have 3 entries, but in fact found 2.");
        }

        [Fact]
        public void WithCustomUrlHelperShouldNotThrowExceptionWithCorrectUrlHelper()
        {
            var urlHelper = TestObjectFactory.GetCustomUrlHelper();

            MyController<MvcController>
                .Instance()
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

                    MyController<MvcController>
                        .Instance()
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

            MyController<MvcController>
                .Instance()
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

                    MyController<MvcController>
                        .Instance()
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
                    MyController<MvcController>
                        .Instance()
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
                    MyController<MvcController>
                        .Instance()
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
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .WithModelOfType<ICollection<ResponseModel>>();
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
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
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullCreatedAction())
                .ShouldReturn()
                .Created()
                .WithStatusCode(201);
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                        .Instance()
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
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullCreatedAction())
                .ShouldReturn()
                .Created()
                .ContainingContentType(ContentType.ApplicationJson);
        }

        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                       .Instance()
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
            MyController<MvcController>
                .Instance()
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
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                        .Instance()
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
                    MyController<MvcController>
                        .Instance()
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
                    MyController<MvcController>
                        .Instance()
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
            MyController<MvcController>
                .Instance()
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
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                        .Instance()
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
                    MyController<MvcController>
                        .Instance()
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
                    MyController<MvcController>
                        .Instance()
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

            MyController<MvcController>
                .Instance()
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

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CreatedActionWithFormatter(formatter))
                        .ShouldReturn()
                        .Created()
                        .ContainingOutputFormatter(TestObjectFactory.GetOutputFormatter());
                },
                "When calling CreatedActionWithFormatter action in MvcController expected created result output formatters to contain the provided formatter, but such was not found.");
        }
        
        [Fact]
        public void ContainingOutputFormatterOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                        .Instance()
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
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullCreatedAction())
                .ShouldReturn()
                .Created()
                .ContainingOutputFormatters(new List<IOutputFormatter>
                {
                    TestObjectFactory.GetOutputFormatter(),
                    new CustomOutputFormatter()
                });
        }

        [Fact]
        public void ContainingOutputFormattersShouldNotThrowExceptionWithCorrectParametersValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullCreatedAction())
                .ShouldReturn()
                .Created()
                .ContainingOutputFormatters(TestObjectFactory.GetOutputFormatter(), new CustomOutputFormatter());
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullCreatedAction())
                        .ShouldReturn()
                        .Created()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            new CustomOutputFormatter(),
                            new StringOutputFormatter()
                        });
                },
                "When calling FullCreatedAction action in MvcController expected created result output formatters to contain formatter of StringOutputFormatter type, but none was found.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullCreatedAction())
                        .ShouldReturn()
                        .Created()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            TestObjectFactory.GetOutputFormatter()
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
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullCreatedAction())
                        .ShouldReturn()
                        .Created()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            TestObjectFactory.GetOutputFormatter(),
                            new CustomOutputFormatter(),
                            TestObjectFactory.GetOutputFormatter()
                        });
                },
                "When calling FullCreatedAction action in MvcController expected created result output formatters to have 3 items, but instead found 2.");
        }

        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<CreatedResult>(actionResult);
                });
        }
    }
}
