namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AuthenticationTests
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
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge(challenge => challenge
                    .WithAuthenticationProperties(auth => auth
                        .AllowingRefresh(true)));
        }
        
        [Fact]
        public void WithAllowRefreshShouldThrowExceptionWithInvalidValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .AllowingRefresh(false)));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have allow refresh value of 'False', but in fact found 'True'.");
        }

        [Fact]
        public void WithAllowRefreshShouldThrowExceptionWithInvalidValueEmptyOriginal()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithEmptyAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .AllowingRefresh(false)));
                },
                "When calling ChallengeWithEmptyAuthenticationProperties action in MvcController expected authentication properties to have allow refresh value of 'False', but in fact found null.");
        }

        [Fact]
        public void WithAllowRefreshShouldThrowExceptionWithInvalidNullValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .AllowingRefresh(null)));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to not have allow refresh value, but in fact found 'True'.");
        }

        [Fact]
        public void WithExpirationShouldNotThrowExceptionWithValidValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge(challenge => challenge
                    .WithAuthenticationProperties(auth => auth
                        .WithExpiration(new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)))));
        }

        [Fact]
        public void WithExpirationShouldThrowExceptionWithInvalidValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .WithExpiration(new DateTimeOffset(new DateTime(2015, 1, 1, 1, 1, 1, DateTimeKind.Utc)))));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have expiration value of 'Thu, 01 Jan 2015 01:01:01 GMT', but in fact found 'Fri, 01 Jan 2016 01:01:01 GMT'.");
        }

        [Fact]
        public void WithExpirationShouldThrowExceptionWithInvalidValueEmptyOriginal()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithEmptyAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .WithExpiration(new DateTimeOffset(new DateTime(2015, 1, 1, 1, 1, 1, DateTimeKind.Utc)))));
                },
                "When calling ChallengeWithEmptyAuthenticationProperties action in MvcController expected authentication properties to have expiration value of 'Thu, 01 Jan 2015 01:01:01 GMT', but in fact found null.");
        }

        [Fact]
        public void WithExpirationShouldThrowExceptionWithInvalidValueNullValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .WithExpiration(null)));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to not have expiration value, but in fact found 'Fri, 01 Jan 2016 01:01:01 GMT'.");
        }

        [Fact]
        public void WithIsPersistentShouldNotThrowExceptionWithValidValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge(challenge => challenge
                    .WithAuthenticationProperties(auth => auth
                        .Persistent(true)));
        }

        [Fact]
        public void WithIsPersistentShouldThrowExceptionWithInvalidValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .Persistent(false)));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have is persistent value of 'False', but in fact found 'True'.");
        }

        [Fact]
        public void WithIssuedShouldNotThrowExceptionWithValidValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge(challenge => challenge
                    .WithAuthenticationProperties(auth => auth
                        .IssuedOn(new DateTimeOffset(new DateTime(2015, 1, 1, 1, 1, 1, DateTimeKind.Utc)))));
        }

        [Fact]
        public void WithIssuedShouldThrowExceptionWithInvalidValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .IssuedOn(new DateTimeOffset(new DateTime(2014, 1, 1, 1, 1, 1, DateTimeKind.Utc)))));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have issued value of 'Wed, 01 Jan 2014 01:01:01 GMT', but in fact found 'Thu, 01 Jan 2015 01:01:01 GMT'.");
        }

        [Fact]
        public void WithIssuedShouldThrowExceptionWithInvalidValueEmptyOriginal()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithEmptyAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .IssuedOn(new DateTimeOffset(new DateTime(2014, 1, 1, 1, 1, 1, DateTimeKind.Utc)))));
                },
                "When calling ChallengeWithEmptyAuthenticationProperties action in MvcController expected authentication properties to have issued value of 'Wed, 01 Jan 2014 01:01:01 GMT', but in fact found null.");
        }

        [Fact]
        public void WithIssuedShouldThrowExceptionWithInvalidNullValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .IssuedOn(null)));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to not have issued value, but in fact found 'Thu, 01 Jan 2015 01:01:01 GMT'.");
        }

        [Fact]
        public void WithItemShouldNotThrowExceptionWithValidItemKey()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge(challenge => challenge
                    .WithAuthenticationProperties(auth => auth
                        .WithItem("TestKeyItem")));
        }

        [Fact]
        public void WithItemShouldThrowExceptionWithInvalidValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .WithItem("TestItem")));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have item with key 'TestItem', but such was not found.");
        }

        [Fact]
        public void WithItemAndValueShouldNotThrowExceptionWithValidItemKey()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge(challenge => challenge
                    .WithAuthenticationProperties(auth => auth
                        .WithItem("TestKeyItem", "TestValueItem")));
        }

        [Fact]
        public void WithItemAndValueShouldThrowExceptionWithInvalidKey()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .WithItem("TestItem", "TestValueItem")));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have item with key 'TestItem' and value 'TestValueItem', but such was not found.");
        }

        [Fact]
        public void WithItemAndValueShouldThrowExceptionWithInvalidValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .WithItem("TestKeyItem", "TestItem")));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have item with key 'TestKeyItem' and value 'TestItem', but the value was 'TestValueItem'.");
        }

        [Fact]
        public void WithItemsShouldNotThrowExceptionWithValidValues()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge(challenge => challenge
                    .WithAuthenticationProperties(auth => auth
                        .WithItems(new Dictionary<string, string>
                        {
                            { "TestKeyItem", "TestValueItem" },
                            { "AnotherTestKeyItem", "AnotherTestValueItem" },
                        })));
            }

        [Fact]
        public void WithItemsShouldThrowExceptionWithInvalidCount()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .WithItems(new Dictionary<string, string>
                                {
                                    { "TestKeyItem", "TestValueItem" }
                                })));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have 1 custom item, but in fact found 2.");
        }

        [Fact]
        public void WithItemsShouldThrowExceptionWithInvalidMoreThanOneCount()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .WithItems(new Dictionary<string, string>
                                {
                                    { "TestKeyItem", "TestValueItem" },
                                    { "AnotherTestKeyItem", "TestValueItem" },
                                    { "YetAnotherTestKeyItem", "TestValueItem" }
                                })));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have 3 custom items, but in fact found 2.");
        }

        [Fact]
        public void WithItemsShouldNotThrowExceptionWithInvalidValues()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .WithItems(new Dictionary<string, string>
                                {
                                    { "TestKeyItem", "TestItem" },
                                    { "AnotherTestKeyItem", "AnotherTestValueItem" },
                                })));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have item with key 'TestKeyItem' and value 'TestItem', but the value was 'TestValueItem'.");
        }

        [Fact]
        public void WithRedirectUriShouldNotThrowExceptionWithValidValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ForbidWithAuthenticationProperties())
                .ShouldReturn()
                .Forbid(forbid => forbid
                    .WithAuthenticationProperties(auth => auth
                        .WithRedirectUri("test")));
        }

        [Fact]
        public void WithRedirectUriShouldThrowExceptionWithInvalidValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .WithRedirectUri("another")));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to have 'another' redirect URI, but in fact found 'test'.");
        }

        [Fact]
        public void WithRedirectUriShouldThrowExceptionWithInvalidValueEmptyOriginal()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ForbidWithEmptyAuthenticationProperties())
                        .ShouldReturn()
                        .Forbid(forbid => forbid
                            .WithAuthenticationProperties(auth => auth
                                .WithRedirectUri("another")));
                },
                "When calling ForbidWithEmptyAuthenticationProperties action in MvcController expected authentication properties to have 'another' redirect URI, but in fact found null.");
        }

        [Fact]
        public void WithRedirectUriShouldThrowExceptionWithInvalidNullValue()
        {
            Test.AssertException<AuthenticationPropertiesAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(auth => auth
                                .WithRedirectUri(null)));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected authentication properties to not have redirect URI value, but in fact found 'test'.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge(challenge => challenge
                    .WithAuthenticationProperties(auth => auth
                        .WithItem("TestKeyItem", "TestValueItem")
                        .AndAlso()
                        .WithItem("AnotherTestKeyItem", "AnotherTestValueItem")));
        }
    }
}
