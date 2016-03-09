namespace MyTested.Mvc.Test.BuildersTests.ActionResultsTests.LocalRedirectTests
{
    using System;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class LocalRedirectTestBuilderTests
    {
        [Fact]
        public void PermanentShouldNotThrowExceptionWhenRedirectIsPermanent()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.LocalRedirectPermanentAction())
                .ShouldReturn()
                .LocalRedirect()
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
                        .Calling(c => c.LocalRedirectAction())
                        .ShouldReturn()
                        .LocalRedirect()
                        .Permanent();
                },
                "When calling LocalRedirectAction action in MvcController expected local redirect result to be permanent, but in fact it was not.");
        }

        [Fact]
        public void ToUrlWithStringShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.LocalRedirectAction())
                .ShouldReturn()
                .LocalRedirect()
                .ToUrl("/local/test");
        }

        [Fact]
        public void ToUrlWithStringShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.LocalRedirectAction())
                        .ShouldReturn()
                        .LocalRedirect()
                        .ToUrl("/local");
                },
                "When calling LocalRedirectAction action in MvcController expected local redirect result location to be '/local', but instead received '/local/test'.");
        }

        [Fact]
        public void ToUrlWithStringShouldThrowExceptionIfTheLocationIsNotValid()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.LocalRedirectAction())
                        .ShouldReturn()
                        .LocalRedirect()
                        .ToUrl("http://somehost!@#?Query==true");
                },
                "When calling LocalRedirectAction action in MvcController expected local redirect result location to be URI valid, but instead received 'http://somehost!@#?Query==true'.");
        }

        [Fact]
        public void ToUrlWithUriShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.LocalRedirectAction())
                .ShouldReturn()
                .LocalRedirect()
                .ToUrl(new Uri("/local/test", UriKind.Relative));
        }

        [Fact]
        public void ToUrlWithUriShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.LocalRedirectAction())
                        .ShouldReturn()
                        .LocalRedirect()
                        .ToUrl(new Uri("/local", UriKind.Relative));
                },
                "When calling LocalRedirectAction action in MvcController expected local redirect result location to be '/local', but instead received '/local/test'.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.LocalRedirectPermanentAction())
                .ShouldReturn()
                .LocalRedirect()
                .Permanent()
                .AndAlso()
                .ToUrl("/local/test");
        }
    }
}
