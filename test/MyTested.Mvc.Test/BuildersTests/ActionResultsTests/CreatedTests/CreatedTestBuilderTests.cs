namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.CreatedTests
{
    using System;
    using System.Collections.Generic;
    using Exceptions;
    using Setups;
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
            Test.AssertException<CreatedResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.CreatedAction())
                    .ShouldReturn()
                    .Created()
                    .AtLocation("http://somehost.com/");
            }, "When calling CreatedAction action in MvcController expected created result location to be http://somehost.com/, but instead received http://somehost.com/someuri/1?query=Test.");
        }

        [Fact]
        public void AtLocationWithStringShouldThrowExceptionIfTheLocationIsNotValid()
        {
            Test.AssertException<CreatedResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.CreatedAction())
                    .ShouldReturn()
                    .Created()
                    .AtLocation("http://somehost!@#?Query==true");
            }, "When calling CreatedAction action in MvcController expected created result location to be URI valid, but instead received http://somehost!@#?Query==true.");
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
            Test.AssertException<CreatedResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.CreatedAction())
                    .ShouldReturn()
                    .Created()
                    .AtLocation(new Uri("http://somehost.com/"));
            }, "When calling CreatedAction action in MvcController expected created result location to be http://somehost.com/, but instead received http://somehost.com/someuri/1?query=Test.");
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
            Test.AssertException<CreatedResultAssertionException>(() =>
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
            }, "When calling CreatedAction action in MvcController expected created result URI to equal the provided one, but was in fact different.");
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
    }
}
