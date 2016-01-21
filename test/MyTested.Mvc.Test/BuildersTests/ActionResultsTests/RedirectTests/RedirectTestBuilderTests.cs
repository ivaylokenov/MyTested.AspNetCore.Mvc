namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.RedirectTests
{
    using Exceptions;
    using Microsoft.AspNet.Mvc;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using System;
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
        public void AtLocationWithStringShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect()
                .ToUrl("http://somehost.com/someuri/1?query=Test");
        }

        [Fact]
        public void AtLocationWithStringShouldThrowExceptionIfTheLocationIsIncorrect()
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
        public void AtLocationWithStringShouldThrowExceptionIfTheLocationIsNotValid()
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
        public void AtLocationWithUriShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect()
                .ToUrl(new Uri("http://somehost.com/someuri/1?query=Test"));
        }

        [Fact]
        public void AtLocationWithUriShouldThrowExceptionIfTheLocationIsIncorrect()
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
        public void AtLocationWithBuilderShouldNotThrowExceptionIfTheLocationIsCorrect()
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
        public void AtLocationWithBuilderShouldThrowExceptionIfTheLocationIsIncorrect()
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
                "When calling RedirectActionWithUri action in MvcController expected redirect result URI to equal the provided one, but was in fact different.");
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
                .WithRouteValue("id");
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
                        .WithRouteValue("incorrect");
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result route values to have item with key 'incorrect', but such was not found.");
        }

        [Fact]
        public void WithRouteValueWithValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect()
                .WithRouteValue("id", 1);
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
                        .WithRouteValue("incorrect", 1);
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result route values to have item with 'incorrect' key and the provided value, but such was not found.");
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
                        .WithRouteValue("id", 2);
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result route values to have item with 'id' key and the provided value, but the value was different.");
        }

        [Fact]
        public void WithRouteValuesWithObjectShouldNotThrowExceptionWithCorrectRouteValues()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect()
                .WithRouteValues(new { id = 1, text = "sometext" });
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
                        .WithRouteValues(new { id = 1 });
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result route values to have 1 item, but in fact found 2.");
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
                        .WithRouteValues(new { id = 1, second = 5, another = "test" });
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result route values to have 3 items, but in fact found 2.");
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
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect()
                .To<NoAttributesController>(c => c.WithParameter(1));
        }

        [Fact]
        public void AtShouldWorkCorrectlyWithCorrectVoidActionCall()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.RedirectToRouteVoidAction())
                .ShouldReturn()
                .Redirect()
                .To<NoAttributesController>(c => c.VoidAction());
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
    }
}
