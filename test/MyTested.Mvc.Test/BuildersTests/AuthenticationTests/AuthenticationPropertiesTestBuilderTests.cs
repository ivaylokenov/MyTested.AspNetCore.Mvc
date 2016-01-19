namespace MyTested.Mvc.Tests.BuildersTests.AuthenticationTests
{
    using System;
    using System.Collections.Generic;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class AuthenticationPropertiesTestBuilderTests
    {
        [Fact]
        public void WithAllowRefreshShouldNotThrowExceptionWithValidValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge()
                .WithAuthenticationProperties(auth => auth.WithAllowRefresh(true));
        }
        
        [Fact]
        public void WithAllowRefreshShouldThrowExceptionWithInvalidValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithAllowRefresh(false));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have allow refresh value of 'False', but in fact found 'True'.");
        }

        [Fact]
        public void WithAllowRefreshShouldThrowExceptionWithInvalidValueEmptyOriginal()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithEmptyAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithAllowRefresh(false));
                },
                "When calling ChallengeWithEmptyAuthenticationProperties action in MvcController expected authentication properties to have allow refresh value of 'False', but in fact found 'null'.");
        }

        [Fact]
        public void WithAllowRefreshShouldThrowExceptionWithInvalidNullValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithAllowRefresh(null));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to not have allow refresh value, but in fact found 'True'.");
        }

        [Fact]
        public void WithExpiresShouldNotThrowExceptionWithValidValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge()
                .WithAuthenticationProperties(auth => auth.WithExpires(new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1))));
        }

        [Fact]
        public void WithExpiresShouldThrowExceptionWithInvalidValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithExpires(new DateTimeOffset(new DateTime(2015, 1, 1, 1, 1, 1))));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have expires value of '12/31/2014 11:01:01 PM +00:00', but in fact found '12/31/2015 11:01:01 PM +00:00'.");
        }

        [Fact]
        public void WithExpiresShouldThrowExceptionWithInvalidValueEmptyOriginal()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithEmptyAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithExpires(new DateTimeOffset(new DateTime(2015, 1, 1, 1, 1, 1))));
                },
                "When calling ChallengeWithEmptyAuthenticationProperties action in MvcController expected authentication properties to have expires value of '12/31/2014 11:01:01 PM +00:00', but in fact found 'null'.");
        }

        [Fact]
        public void WithExpiresShouldThrowExceptionWithInvalidValueNullValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithExpires(null));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to not have expires value, but in fact found '12/31/2015 11:01:01 PM +00:00'.");
        }

        [Fact]
        public void WithIsPersistentShouldNotThrowExceptionWithValidValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge()
                .WithAuthenticationProperties(auth => auth.WithIsPersistent(true));
        }

        [Fact]
        public void WithIsPersistentShouldThrowExceptionWithInvalidValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithIsPersistent(false));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have is persistent value of 'False', but in fact found 'True'.");
        }

        [Fact]
        public void WithIssuedShouldNotThrowExceptionWithValidValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge()
                .WithAuthenticationProperties(auth => auth.WithIssued(new DateTimeOffset(new DateTime(2015, 1, 1, 1, 1, 1))));
        }

        [Fact]
        public void WithIssuedShouldThrowExceptionWithInvalidValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithIssued(new DateTimeOffset(new DateTime(2014, 1, 1, 1, 1, 1))));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have issued value of '12/31/2013 11:01:01 PM +00:00', but in fact found '12/31/2014 11:01:01 PM +00:00'.");
        }

        [Fact]
        public void WithIssuedShouldThrowExceptionWithInvalidValueEmptyOriginal()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithEmptyAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithIssued(new DateTimeOffset(new DateTime(2014, 1, 1, 1, 1, 1))));
                },
                "When calling ChallengeWithEmptyAuthenticationProperties action in MvcController expected authentication properties to have issued value of '12/31/2013 11:01:01 PM +00:00', but in fact found 'null'.");
        }

        [Fact]
        public void WithIssuedShouldThrowExceptionWithInvalidNullValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithIssued(null));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to not have issued value, but in fact found '12/31/2014 11:01:01 PM +00:00'.");
        }

        [Fact]
        public void WithItemShouldNotThrowExceptionWithValidItemKey()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge()
                .WithAuthenticationProperties(auth => auth.WithItem("TestKeyItem"));
        }

        [Fact]
        public void WithItemShouldThrowExceptionWithInvalidValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithItem("TestItem"));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have item with key 'TestItem', but such was not found.");
        }

        [Fact]
        public void WithItemAndValueShouldNotThrowExceptionWithValidItemKey()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge()
                .WithAuthenticationProperties(auth => auth.WithItem("TestKeyItem", "TestValueItem"));
        }

        [Fact]
        public void WithItemAndValueShouldThrowExceptionWithInvalidKey()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithItem("TestItem", "TestValueItem"));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have item with key 'TestItem' and value 'TestValueItem', but such was not found.");
        }

        [Fact]
        public void WithItemAndValueShouldThrowExceptionWithInvalidValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithItem("TestKeyItem", "TestItem"));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have item with key 'TestKeyItem' and value 'TestItem', but the value was 'TestValueItem'.");
        }

        [Fact]
        public void WithItemsShouldNotThrowExceptionWithValidValues()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge()
                .WithAuthenticationProperties(auth => auth.WithItems(new Dictionary<string, string>
                {
                    { "TestKeyItem", "TestValueItem" },
                    { "AnotherTestKeyItem", "AnotherTestValueItem" },
                }));
        }

        [Fact]
        public void WithItemsShouldThrowExceptionWithInvalidCount()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithItems(new Dictionary<string, string>
                        {
                            { "TestKeyItem", "TestValueItem" }
                        }));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have 1 custom item, but in fact found 2.");
        }

        [Fact]
        public void WithItemsShouldThrowExceptionWithInvalidMoreThanOneCount()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithItems(new Dictionary<string, string>
                        {
                            { "TestKeyItem", "TestValueItem" },
                            { "AnotherTestKeyItem", "TestValueItem" },
                            { "YetAnotherTestKeyItem", "TestValueItem" }
                        }));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have 3 custom items, but in fact found 2.");
        }

        [Fact]
        public void WithItemsShouldNotThrowExceptionWithInvalidValues()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithItems(new Dictionary<string, string>
                        {
                            { "TestKeyItem", "TestItem" },
                            { "AnotherTestKeyItem", "AnotherTestValueItem" },
                        }));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have item with key 'TestKeyItem' and value 'TestItem', but the value was 'TestValueItem'.");
        }

        [Fact]
        public void WithRedirectUriShouldNotThrowExceptionWithValidValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ForbidWithAuthenticationProperties())
                .ShouldReturn()
                .Forbid()
                .WithAuthenticationProperties(auth => auth.WithRedirectUri("test"));
        }

        [Fact]
        public void WithRedirectUriShouldThrowExceptionWithInvalidValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithRedirectUri("another"));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have 'another' redirect URI, but in fact found 'test'.");
        }

        [Fact]
        public void WithRedirectUriShouldThrowExceptionWithInvalidValueEmptyOriginal()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ForbidWithEmptyAuthenticationProperties())
                        .ShouldReturn()
                        .Forbid()
                        .WithAuthenticationProperties(auth => auth.WithRedirectUri("another"));
                },
                "When calling ForbidWithEmptyAuthenticationProperties action in MvcController expected authentication properties to have 'another' redirect URI, but in fact found 'null'.");
        }

        [Fact]
        public void WithRedirectUriShouldThrowExceptionWithInvalidNullValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(auth => auth.WithRedirectUri(null));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to not have redirect URI value, but in fact found 'test'.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge()
                .WithAuthenticationProperties(auth => auth
                    .WithItem("TestKeyItem", "TestValueItem")
                    .AndAlso()
                    .WithItem("AnotherTestKeyItem", "AnotherTestValueItem"));
        }
    }
}
