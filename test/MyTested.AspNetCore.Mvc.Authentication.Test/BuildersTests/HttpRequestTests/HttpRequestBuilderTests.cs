namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.HttpRequestTests
{
    using Microsoft.AspNetCore.Http;
    using MyTested.AspNetCore.Mvc.Test.Setups.Controllers;
    using System.Collections.Generic;
    using Xunit;

    public class HttpRequestBuilderTests
    {
        [Fact]
        public void WithAuthenticatedUserShouldPopulateUserNameAndRolesFromListProperly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithUser("TestUserName", new List<string>
                    {
                        "Administrator",
                        "Moderator"
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(0, builtRequest.Query.Count);
                    Assert.False(builtRequest.HasFormContentType);
                    Assert.Equal(0, builtRequest.Cookies.Count);
                });
        }

        [Fact]
        public void WithAuthenticatedUserShouldPopulateUserNameAndRolesProperly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithUser("TestUserName", new string[]
                    {
                        "Administrator",
                        "Moderator"
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(0, builtRequest.Query.Count);
                    Assert.False(builtRequest.HasFormContentType);
                    Assert.Equal(0, builtRequest.Cookies.Count);
                });
        }

        [Fact]
        public void WithUserShouldPopulateUserIdentifierAndNameAndRolesProperly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithUser("TestIdentifier", "TestUserName", new List<string>
                    {
                        "Administrator",
                        "Moderator"
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(0, builtRequest.Query.Count);
                    Assert.False(builtRequest.HasFormContentType);
                    Assert.Equal(0, builtRequest.Cookies.Count);
                });
        }

        [Fact]
        public void WithAuthenticatedUserShouldPopulateUserIdentifierAndNameAndRolesProperly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithUser("TestIdentifier", "TestUserName", new string[]
                    {
                        "Administrator",
                        "Moderator"
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(0, builtRequest.Query.Count);
                    Assert.False(builtRequest.HasFormContentType);
                    Assert.Equal(0, builtRequest.Cookies.Count);
                });
        }

        [Fact]
        public void WithAuthenticatedUserShouldPopulateUserRolesProperly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithUser(new List<string>
                    {
                        "Administrator",
                        "Moderator"
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(0, builtRequest.Query.Count);
                    Assert.False(builtRequest.HasFormContentType);
                    Assert.Equal(0, builtRequest.Cookies.Count);
                });
        }

        [Fact]
        public void WithAuthenticatedUserShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithUser(user => user
                    .WithIdentity(identity => identity
                        .WithIdentifier("IdentityIdentifier")
                        .WithUsername("IdentityUsername")
                        .InRole("IdentityRole"))))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(0, builtRequest.Query.Count);
                    Assert.False(builtRequest.HasFormContentType);
                    Assert.Equal(0, builtRequest.Cookies.Count);
                });
        }
    }
}
