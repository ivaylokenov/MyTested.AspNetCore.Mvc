namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.RedirectTests
{
    using System;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Utilities;
    using Xunit;

    public class RedirectTestBuilderTests
    {
        [Fact]
        public void PermanentShouldNotThrowExceptionWhenRedirectIsPermanent()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectPermanentAction())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .Permanent());
        }

        [Fact]
        public void PermanentShouldThrowExceptionWhenRedirectIsNotPermanent()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectToActionResult())
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .Permanent());
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result to be permanent, but in fact it was not.");
        }

        [Fact]
        public void ToUrlWithStringShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ToUrl("http://somehost.com/someuri/1?query=Test"));
        }

        [Fact]
        public void ToUrlWithStringShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectActionWithUri())
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .ToUrl("http://somehost.com/"));
                },
                "When calling RedirectActionWithUri action in MvcController expected redirect result location to be 'http://somehost.com/', but instead received 'http://somehost.com/someuri/1?query=Test'.");
        }

        [Fact]
        public void ToUrlWithStringShouldThrowExceptionIfTheLocationIsNotValid()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectActionWithUri())
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .ToUrl("http://somehost!@#?Query==true"));
                },
                "When calling RedirectActionWithUri action in MvcController expected redirect result location to be URI valid, but instead received 'http://somehost!@#?Query==true'.");
        }

        [Fact]
        public void ToUrlPassingShouldNotThrowExceptionWithValidPredicate()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ToUrlPassing(url => url.StartsWith("http://somehost.com/")));
        }

        [Fact]
        public void ToUrlPassingShouldThrowExceptionWithInvalidPredicate()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectActionWithUri())
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .ToUrlPassing(url => url.StartsWith("http://other.com/")));
                },
                "When calling RedirectActionWithUri action in MvcController expected redirect result location ('http://somehost.com/someuri/1?query=Test') to pass the given predicate, but it failed.");
        }

        [Fact]
        public void ToUrlPassingShouldNotThrowExceptionWithValidaAssertions()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ToUrlPassing(url =>
                    {
                        Assert.StartsWith("http://somehost.com/", url);
                    }));
        }
        
        [Fact]
        public void ToUrlWithUriShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ToUrl(new Uri("http://somehost.com/someuri/1?query=Test")));
        }

        [Fact]
        public void ToUrlWithUriShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectActionWithUri())
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .ToUrl(new Uri("http://somehost.com/")));
                },
                "When calling RedirectActionWithUri action in MvcController expected redirect result location to be 'http://somehost.com/', but instead received 'http://somehost.com/someuri/1?query=Test'.");
        }

        [Fact]
        public void ToUrlWithBuilderShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect(redirect => redirect
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
                            .WithQuery("?query=Test")));
        }

        [Fact]
        public void ToUrlWithBuilderShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectActionWithUri())
                        .ShouldReturn()
                        .Redirect(redirect => redirect
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
                                    .WithQuery("?query=Test")));
                },
                "When calling RedirectActionWithUri action in MvcController expected redirect result URI to be 'http://somehost12.com/someuri/1?query=Test', but was in fact 'http://somehost.com/someuri/1?query=Test'.");
        }

        [Fact]
        public void AtActionShouldNotThrowExceptionWithCorrectActionName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ToAction("MyAction"));
        }

        [Fact]
        public void AtActionShouldThrowExceptionWithIncorrectActionName()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                    .Instance()
                    .Calling(c => c.RedirectToActionResult())
                    .ShouldReturn()
                    .Redirect(redirect => redirect
                        .ToAction("Action"));
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result to have 'Action' action name, but instead received 'MyAction'.");
        }

        [Fact]
        public void AtControllerShouldNotThrowExceptionWithCorrectControllerName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ToController("MyController"));
        }

        [Fact]
        public void AtControllerShouldThrowExceptionWithIncorrectControllerName()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                    .Instance()
                    .Calling(c => c.RedirectToActionResult())
                    .ShouldReturn()
                    .Redirect(redirect => redirect
                        .ToController("Controller"));
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result to have 'Controller' controller name, but instead received 'MyController'.");
        }

        [Fact]
        public void WithRouteNameShouldNotThrowExceptionWithCorrectRouteName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToRouteAction())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ToRoute("Redirect"));
        }

        [Fact]
        public void WithRouteNameShouldThrowExceptionWithIncorrectRouteName()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectToRouteAction())
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .ToRoute("MyRedirect"));
                },
                "When calling RedirectToRouteAction action in MvcController expected redirect result to have 'MyRedirect' route name, but instead received 'Redirect'.");
        }

        [Fact]
        public void WithRouteValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ContainingRouteKey("id"));
        }
        
        [Fact]
        public void WithRouteValueWithValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ContainingRouteValue(1));
        }

        [Fact]
        public void WithRouteValueOfTypeWithValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ContainingRouteValueOfType<string>());
        }

        [Fact]
        public void WithRouteValueOfTypeWithKeyWithValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ContainingRouteValueOfType<int>("id"));
        }
        
        [Fact]
        public void WithRouteValueShouldThrowExceptionWithIncorrectRouteValue()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectToActionResult())
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .ContainingRouteKey("incorrect"));
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result route values to have entry with 'incorrect' key, but such was not found.");
        }

        [Fact]
        public void WithRouteValueWithKeyAndValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ContainingRouteValue("id", 1));
        }

        [Fact]
        public void WithRouteValueWithValueShouldThrowExceptionWithIncorrectRouteKey()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectToActionResult())
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .ContainingRouteValue("incorrect", 1));
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result route values to have entry with 'incorrect' key and the provided value, but such was not found.");
        }

        [Fact]
        public void WithRouteValueWithValueShouldThrowExceptionWithIncorrectRouteValue()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectToActionResult())
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .ContainingRouteValue("id", 2));
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result route values to have entry with 'id' key and the provided value, but the value was different. Expected a value of '2', but in fact it was '1'.");
        }

        [Fact]
        public void WithRouteValuesWithObjectShouldNotThrowExceptionWithCorrectRouteValues()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ContainingRouteValues(new
                    {
                        id = 1,
                        text = "sometext"
                    }));
        }

        [Fact]
        public void WithRouteValuesWithObjectShouldThrowExceptionWithIncorrectRouteValues()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectToActionResult())
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .ContainingRouteValues(new { id = 1 }));
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result route values to have 1 entry, but in fact found 2.");
        }

        [Fact]
        public void WithRouteValuesWithObjectShouldThrowExceptionWithIncorrectMoreRouteValues()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectToActionResult())
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .ContainingRouteValues(new
                            {
                                id = 1,
                                second = 5,
                                another = "test"
                            }));
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result route values to have 3 entries, but in fact found 2.");
        }

        [Fact]
        public void WithCustomUrlHelperShouldNotThrowExceptionWithCorrectUrlHelper()
        {
            var urlHelper = TestObjectFactory.GetCustomUrlHelper();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToActionWithCustomUrlHelperResult(urlHelper))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .WithUrlHelper(urlHelper));
        }

        [Fact]
        public void WithCustomUrlHelperShouldThrowExceptionWithIncorrectUrlHelper()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    var urlHelper = TestObjectFactory.GetCustomUrlHelper();

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectToActionWithCustomUrlHelperResult(urlHelper))
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .WithUrlHelper(null));
                },
                "When calling RedirectToActionWithCustomUrlHelperResult action in MvcController expected redirect result URL helper to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void WithCustomUrlHelperOfTypeShouldNotThrowExceptionWithCorrectUrlHelper()
        {
            var urlHelper = TestObjectFactory.GetCustomUrlHelper();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToActionWithCustomUrlHelperResult(urlHelper))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .WithUrlHelperOfType<CustomUrlHelper>());
        }

        [Fact]
        public void WithCustomUrlHelperOfTypeShouldThrowExceptionWithIncorrectUrlHelper()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    var urlHelper = TestObjectFactory.GetCustomUrlHelper();

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectToActionWithCustomUrlHelperResult(urlHelper))
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .WithUrlHelperOfType<IUrlHelper>());
                },
                "When calling RedirectToActionWithCustomUrlHelperResult action in MvcController expected redirect result URL helper to be of IUrlHelper type, but instead received CustomUrlHelper.");
        }

        [Fact]
        public void WithCustomUrlHelperOfTypeShouldThrowExceptionWithNoUrlHelper()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectToActionResult())
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .WithUrlHelperOfType<IUrlHelper>());
                },
                "When calling RedirectToActionResult action in MvcController expected redirect result URL helper to be of IUrlHelper type, but instead received null.");
        }

        [Fact]
        public void EverySpecificMethodShouldThrowExceptionWhenSuchPropertyIsNotFound()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectToRouteAction())
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .ToController("Controller"));
                },
                "When calling RedirectToRouteAction action in MvcController expected redirect result to contain controller name, but such could not be found.");
        }
        
        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ToAction("MyAction")
                    .AndAlso()
                    .ToController("MyController"));
        }


        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionFunction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .Passing(r => r.ActionName == "MyAction"));
        }

        [Fact]
        public void PassingShouldThrowAnExceptionOnAnIncorrectAssertion()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectToActionResult())
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .Passing(r => r.ActionName == string.Empty));
                },
                $"When calling RedirectToActionResult action in MvcController expected the RedirectToActionResult to pass the given predicate, but it failed.");
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionAction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .Passing(r =>
                    {
                        const string expectedActionName = "MyAction";
                        var actualActionName = r.ActionName;
                        if (actualActionName != expectedActionName)
                        {
                            throw new InvalidAssertionException(
                               string.Format("Expected {0} to have {1} equal to {2}, but it was {3}.",
                                    r.GetType().ToFriendlyTypeName(),
                                    nameof(r.ActionName),
                                    expectedActionName,
                                    actualActionName));
                        };
                    }));
        }
    }
}
