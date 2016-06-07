namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AttributesTests
{
    using System.Collections.Generic;
    using System.Net.Http;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class ActionAttributesTestBuilderTests
    {
        [Fact]
        public void ContainingAttributeOfTypeShouldNotThrowExceptionWithActionWithTheAttribute()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.ContainingAttributeOfType<HttpGetAttribute>());
        }

        public void ContainingAttributeOfTypeShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.ContainingAttributeOfType<HttpPatchAttribute>());
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have HttpPatchAttribute, but in fact such was not found.");
        }

        [Fact]
        public void ChangingActionNameToShouldNotThrowExceptionWithActionWithTheAttribute()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.ChangingActionNameTo("NormalAction"));
        }

        [Fact]
        public void ChangingActionNameToShouldThrowExceptionWithActionWithTheAttributeAndWrongName()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.ChangingActionNameTo("AnotherAction"));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ActionNameAttribute with 'AnotherAction' name, but in fact found 'NormalAction'.");
        }

        [Fact]
        public void ChangingActionNameToShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.ChangingActionNameTo("NormalAction"));
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have ActionNameAttribute, but in fact such was not found.");
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithActionWithTheAttribute()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.ChangingRouteTo("/api/test"));
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithActionWithTheAttributeAndCasingDifference()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.ChangingRouteTo("/api/Test"));
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithActionWithTheAttributeAndWrongTemplate()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.ChangingRouteTo("/api/another"));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have RouteAttribute with '/api/another' template, but in fact found '/api/test'.");
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithActionWithTheAttributeAndCorrectName()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.ChangingRouteTo("/api/test", withName: "TestRoute"));
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithActionWithTheAttributeAndWrongName()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.ChangingRouteTo("/api/test", withName: "AnotherRoute"));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have RouteAttribute with 'AnotherRoute' name, but in fact found 'TestRoute'.");
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithActionWithTheAttributeAndCorrectOrder()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.ChangingRouteTo("/api/test", withOrder: 1));
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithActionWithTheAttributeAndWrongOrder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.ChangingRouteTo("/api/test", withOrder: 2));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have RouteAttribute with order of 2, but in fact found 1.");
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.ChangingRouteTo("/api/test"));
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have RouteAttribute, but in fact such was not found.");
        }

        [Fact]
        public void AllowingAnonymousRequestsShouldNotThrowExceptionWithTheAttribute()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.AllowingAnonymousRequests());
        }

        [Fact]
        public void AllowingAnonymousRequestsShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.AllowingAnonymousRequests());
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have AllowAnonymousAttribute, but in fact such was not found.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithTheAttribute()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForAuthorizedRequests());
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.RestrictingForAuthorizedRequests());
                },
                "When calling VariousAttributesAction action in MvcController expected action to have AuthorizeAttribute, but in fact such was not found.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithTheAttributeWithCorrectRoles()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForAuthorizedRequests(withAllowedRoles: "Admin,Moderator"));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithActionWithoutTheAttributeWithIncorrectRoles()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.RestrictingForAuthorizedRequests(withAllowedRoles: "Admin"));
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have AuthorizeAttribute with allowed 'Admin' roles, but in fact found 'Admin,Moderator'.");
        }

        [Fact]
        public void DisablingActionCallShouldNotThrowExceptionWithTheAttribute()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.DisablingActionCall());
        }

        [Fact]
        public void DisablingActionCallShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.DisablingActionCall());
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have NonActionAttribute, but in fact such was not found.");
        }
        
        [Fact]
        public void RestrictingForHttpMethodWithGenericShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForHttpMethod<HttpGetAttribute>());
        }

        [Fact]
        public void RestrictingForHttpMethodWithStringShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForHttpMethod("GET"));
        }

        [Fact]
        public void RestrictingForHttpMethodWithHttpMethodClassShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForHttpMethod(HttpMethod.Get));
        }

        [Fact]
        public void RestrictingForHttpMethodWithListOfStringsShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForHttpMethods(new List<string> { "GET", "HEAD" }));
        }

        [Fact]
        public void RestrictingForHttpMethodWithParamsOfStringsShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForHttpMethods("GET", "HEAD"));
        }

        [Fact]
        public void RestrictingForHttpMethodWithListOfHttpMethodsShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForHttpMethods(new List<HttpMethod> { HttpMethod.Get, HttpMethod.Head }));
        }

        [Fact]
        public void RestrictingForHttpMethodWithParamsOfHttpMethodShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForHttpMethods(HttpMethod.Get, HttpMethod.Head));
        }

        [Fact]
        public void RestrictingForHttpMethodWithListOfHttpMethodsShouldWorkCorrectlyWithDoubleAttributes()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForHttpMethods(new List<HttpMethod>
                {
                    HttpMethod.Get,
                    HttpMethod.Post,
                    HttpMethod.Delete
                }));
        }

        [Fact]
        public void RestrictingForHttpMethodWithListOfHttpMethodsShouldWorkCorrectlyWithDoubleAttributesAndIncorrectMethods()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.VariousAttributesAction())
                       .ShouldHave()
                       .ActionAttributes(attributes => attributes.RestrictingForHttpMethods(new List<HttpMethod>
                       {
                            HttpMethod.Get,
                            HttpMethod.Head,
                            HttpMethod.Delete
                       }));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have attribute restricting requests for HTTP 'HEAD' method, but in fact none was found.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes =>
                    attributes
                        .AllowingAnonymousRequests()
                        .AndAlso()
                        .DisablingActionCall()
                        .ChangingActionNameTo("NormalAction"));
        }

        [Fact]
        public void WithNoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.EmptyActionWithParameters(With.No<int>(), With.No<RequestModel>()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes.ChangingActionNameTo("Test"));
        }
    }
}
