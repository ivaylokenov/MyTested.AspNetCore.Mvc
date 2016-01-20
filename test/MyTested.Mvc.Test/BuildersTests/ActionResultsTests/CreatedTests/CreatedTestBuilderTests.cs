namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.CreatedTests
{
    using System;
    using System.Collections.Generic;
    using Exceptions;
    using Microsoft.AspNet.Mvc;
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
                    var urlHelper = TestObjectFactory.GetCustomUrlHelper();

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
    }
}
