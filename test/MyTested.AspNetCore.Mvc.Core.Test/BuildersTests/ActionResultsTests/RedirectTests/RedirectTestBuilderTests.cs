namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.RedirectTests
{
    using System;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Startups;
    using Xunit;

    public class RedirectTestBuilderTests
    {
        [Fact]
        public void PermanentShouldNotThrowExceptionWhenRedirectIsPermanent()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectPermanentAction())
                .ShouldReturn()
                .Redirect()
                .Permanent();
        }

        [Fact]
        public void PermanentShouldThrowExceptionWhenRedirectIsNotPermanent()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectToActionResult())
                        .ShouldReturn()
                        .Redirect()
                        .Permanent();
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result to be permanent, but in fact it was not.");
        }

        [Fact]
        public void ToUrlWithStringShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect()
                .ToUrl("http://somehost.com/someuri/1?query=Test");
        }

        [Fact]
        public void ToUrlWithStringShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectActionWithUri())
                        .ShouldReturn()
                        .Redirect()
                        .ToUrl("http://somehost.com/");
                },
                "When calling RedirectActionWithUri action in MvcController expected redirect result location to be 'http://somehost.com/', but instead received 'http://somehost.com/someuri/1?query=Test'.");
        }

        [Fact]
        public void ToUrlWithStringShouldThrowExceptionIfTheLocationIsNotValid()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectActionWithUri())
                        .ShouldReturn()
                        .Redirect()
                        .ToUrl("http://somehost!@#?Query==true");
                },
                "When calling RedirectActionWithUri action in MvcController expected redirect result location to be URI valid, but instead received 'http://somehost!@#?Query==true'.");
        }

        [Fact]
        public void ToUrlPassingShouldNotThrowExceptionWithValidaPredicate()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect()
                .ToUrlPassing(url => url.StartsWith("http://somehost.com/"));
        }

        [Fact]
        public void ToUrlPassingShouldThrowExceptionWithInvalidaPredicate()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectActionWithUri())
                        .ShouldReturn()
                        .Redirect()
                        .ToUrlPassing(url => url.StartsWith("http://other.com/"));
                },
                "When calling RedirectActionWithUri action in MvcController expected redirect result location ('http://somehost.com/someuri/1?query=Test') to pass the given predicate, but it failed.");
        }

        [Fact]
        public void ToUrlPassingShouldNotThrowExceptionWithValidaAssertions()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect()
                .ToUrlPassing(url =>
                {
                    Assert.True(url.StartsWith("http://somehost.com/"));
                });
        }
        
        [Fact]
        public void ToUrlWithUriShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect()
                .ToUrl(new Uri("http://somehost.com/someuri/1?query=Test"));
        }

        [Fact]
        public void ToUrlWithUriShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectActionWithUri())
                        .ShouldReturn()
                        .Redirect()
                        .ToUrl(new Uri("http://somehost.com/"));
                },
                "When calling RedirectActionWithUri action in MvcController expected redirect result location to be 'http://somehost.com/', but instead received 'http://somehost.com/someuri/1?query=Test'.");
        }

        [Fact]
        public void ToUrlWithBuilderShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect()
                .ToUrl(location =>
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
        public void ToUrlWithBuilderShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectActionWithUri())
                        .ShouldReturn()
                        .Redirect()
                        .ToUrl(location =>
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
                "When calling RedirectActionWithUri action in MvcController expected redirect result URI to be 'http://somehost12.com/someuri/1?query=Test', but was in fact 'http://somehost.com/someuri/1?query=Test'.");
        }

        [Fact]
        public void AtActionShouldNotThrowExceptionWithCorrectActionName()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect()
                .ToAction("MyAction");
        }

        [Fact]
        public void AtActionShouldThrowExceptionWithIncorrectActionName()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.RedirectToActionResult())
                    .ShouldReturn()
                    .Redirect()
                    .ToAction("Action");
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result to have 'Action' action name, but instead received 'MyAction'.");
        }

        [Fact]
        public void AtControllerShouldNotThrowExceptionWithCorrectControllerName()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect()
                .ToController("MyController");
        }

        [Fact]
        public void AtControllerShouldThrowExceptionWithIncorrectControllerName()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.RedirectToActionResult())
                    .ShouldReturn()
                    .Redirect()
                    .ToController("Controller");
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result to have 'Controller' controller name, but instead received 'MyController'.");
        }

        [Fact]
        public void WithRouteNameShouldNotThrowExceptionWithCorrectRouteName()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToRouteAction())
                .ShouldReturn()
                .Redirect()
                .WithRouteName("Redirect");
        }

        [Fact]
        public void WithRouteNameShouldThrowExceptionWithIncorrectRouteName()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectToRouteAction())
                        .ShouldReturn()
                        .Redirect()
                        .WithRouteName("MyRedirect");
                },
                "When calling RedirectToRouteAction action in MvcController expected redirect result to have 'MyRedirect' route name, but instead received 'Redirect'.");
        }

        [Fact]
        public void WithRouteValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect()
                .ContainingRouteKey("id");
        }
        
        [Fact]
        public void WithRouteValueWithValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect()
                .ContainingRouteValue(1);
        }

        [Fact]
        public void WithRouteValueOfTypeWithValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect()
                .ContainingRouteValueOfType<string>();
        }

        [Fact]
        public void WithRouteValueOfTypeWithKeyWithValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect()
                .ContainingRouteValueOfType<int>("id");
        }
        
        [Fact]
        public void WithRouteValueShouldThrowExceptionWithIncorrectRouteValue()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectToActionResult())
                        .ShouldReturn()
                        .Redirect()
                        .ContainingRouteKey("incorrect");
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result route values to have entry with 'incorrect' key, but such was not found.");
        }

        [Fact]
        public void WithRouteValueWithKeyAndValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect()
                .ContainingRouteValue("id", 1);
        }

        [Fact]
        public void WithRouteValueWithValueShouldThrowExceptionWithIncorrectRouteKey()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectToActionResult())
                        .ShouldReturn()
                        .Redirect()
                        .ContainingRouteValue("incorrect", 1);
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result route values to have entry with 'incorrect' key and the provided value, but such was not found.");
        }

        [Fact]
        public void WithRouteValueWithValueShouldThrowExceptionWithIncorrectRouteValue()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectToActionResult())
                        .ShouldReturn()
                        .Redirect()
                        .ContainingRouteValue("id", 2);
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result route values to have entry with 'id' key and the provided value, but the value was different.");
        }

        [Fact]
        public void WithRouteValuesWithObjectShouldNotThrowExceptionWithCorrectRouteValues()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect()
                .ContainingRouteValues(new { id = 1, text = "sometext" });
        }

        [Fact]
        public void WithRouteValuesWithObjectShouldThrowExceptionWithIncorrectRouteValues()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectToActionResult())
                        .ShouldReturn()
                        .Redirect()
                        .ContainingRouteValues(new { id = 1 });
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result route values to have 1 entry, but in fact found 2.");
        }

        [Fact]
        public void WithRouteValuesWithObjectShouldThrowExceptionWithIncorrectMoreRouteValues()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectToActionResult())
                        .ShouldReturn()
                        .Redirect()
                        .ContainingRouteValues(new { id = 1, second = 5, another = "test" });
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result route values to have 3 entries, but in fact found 2.");
        }

        [Fact]
        public void WithCustomUrlHelperShouldNotThrowExceptionWithCorrectUrlHelper()
        {
            var urlHelper = TestObjectFactory.GetCustomUrlHelper();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToActionWithCustomUrlHelperResult(urlHelper))
                .ShouldReturn()
                .Redirect()
                .WithUrlHelper(urlHelper);
        }

        [Fact]
        public void WithCustomUrlHelperShouldThrowExceptionWithIncorrectUrlHelper()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    var urlHelper = TestObjectFactory.GetCustomUrlHelper();

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectToActionWithCustomUrlHelperResult(urlHelper))
                        .ShouldReturn()
                        .Redirect()
                        .WithUrlHelper(null);
                },
                "When calling RedirectToActionWithCustomUrlHelperResult action in MvcController expected redirect result UrlHelper to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void WithCustomUrlHelperOfTypeShouldNotThrowExceptionWithCorrectUrlHelper()
        {
            var urlHelper = TestObjectFactory.GetCustomUrlHelper();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToActionWithCustomUrlHelperResult(urlHelper))
                .ShouldReturn()
                .Redirect()
                .WithUrlHelperOfType<CustomUrlHelper>();
        }

        [Fact]
        public void WithCustomUrlHelperOfTypeShouldThrowExceptionWithIncorrectUrlHelper()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    var urlHelper = TestObjectFactory.GetCustomUrlHelper();

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectToActionWithCustomUrlHelperResult(urlHelper))
                        .ShouldReturn()
                        .Redirect()
                        .WithUrlHelperOfType<IUrlHelper>();
                },
                "When calling RedirectToActionWithCustomUrlHelperResult action in MvcController expected redirect result UrlHelper to be of IUrlHelper type, but instead received CustomUrlHelper.");
        }

        [Fact]
        public void WithCustomUrlHelperOfTypeShouldThrowExceptionWithNoUrlHelper()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectToActionResult())
                        .ShouldReturn()
                        .Redirect()
                        .WithUrlHelperOfType<IUrlHelper>();
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result UrlHelper to be of IUrlHelper type, but instead received null.");
        }

        [Fact]
        public void EverySpecificMethodShouldThrowExceptionWhenSuchPropertyIsNotFound()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectToRouteAction())
                        .ShouldReturn()
                        .Redirect()
                        .ToController("Controller");
                },
                "When calling RedirectToRouteAction action in MvcController expected redirect result to contain controller name, but it could not be found.");
        }
        
        [Fact]
        public void AtShouldWorkCorrectlyWithCorrectActionCall()
        {
            MyMvc.StartsFrom<RoutesStartup>();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToRouteAction())
                .ShouldReturn()
                .Redirect()
                .To<NoAttributesController>(c => c.WithParameter(1));

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void AtShouldWorkCorrectlyWithCorrectVoidActionCall()
        {
            MyMvc.StartsFrom<RoutesStartup>();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToRouteVoidAction())
                .ShouldReturn()
                .Redirect()
                .To<NoAttributesController>(c => c.VoidAction());

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void AtShouldWorkCorrectlyWithCorrectTaskActionCall()
        {
            MyMvc.StartsFrom<RoutesStartup>();

            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.RedirectToRouteAction())
                        .ShouldReturn()
                        .Redirect()
                        .To<MvcController>(c => c.AsyncOkResultAction());
                },
                "When calling RedirectToRouteAction action in MvcController expected redirect result to have resolved location to '/api/test', but in fact received '/api/Redirect/WithParameter?id=1'.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect()
                .ToAction("MyAction")
                .AndAlso()
                .ToController("MyController");
        }

        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect()
                .ShouldPassFor()
                .TheActionResult(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<RedirectToActionResult>(actionResult);
                });
        }
    }
}
