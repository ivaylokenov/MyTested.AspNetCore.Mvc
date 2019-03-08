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
    using System;

    public class ActionAttributesTestBuilderTests
    {
        [Fact]
        public void ContainingAttributeOfTypeShouldNotThrowExceptionWithActionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.ContainingAttributeOfType<HttpGetAttribute>());
        }

        [Fact]
        public void ContainingAttributeOfTypeShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.ContainingAttributeOfType<HttpPatchAttribute>());
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have HttpPatchAttribute, but in fact such was not found.");
        }

        [Fact]
        public void ChangingActionNameToShouldNotThrowExceptionWithActionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                        .Instance()
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
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.ChangingActionNameTo("NormalAction"));
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have ActionNameAttribute, but in fact such was not found.");
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithActionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.ChangingRouteTo("/api/test"));
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithActionWithTheAttributeAndCasingDifference()
        {
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.ChangingRouteTo("/api/another"));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have RouteAttribute with '/api/another' template, but in fact found '/api/test'.");
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithActionWithTheAttributeAndCorrectName()
        {
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.ChangingRouteTo("/api/test", withName: "AnotherRoute"));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have RouteAttribute with 'AnotherRoute' name, but in fact found 'TestRoute'.");
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithActionWithTheAttributeAndCorrectOrder()
        {
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                        .Instance()
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
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.ChangingRouteTo("/api/test"));
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have RouteAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingAreaShouldNotThrowExceptionWithActionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OtherAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.SpecifyingArea("InArea"));
        }

        [Fact]
        public void SpecifyingAreaShouldThrowExceptionWithActionWithTheAttributeAndWrongName()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OtherAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.SpecifyingArea("AnotherArea"));
                },
                "When calling OtherAttributes action in MvcController expected action to have 'AnotherArea' area, but in fact found 'InArea'.");
        }

        [Fact]
        public void SpecifyingAreaToShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.SpecifyingArea("InArea"));
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have AreaAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingConsumptionShouldNotThrowExceptionWithCorrectAttributeAndContentType()
        {
            MyController<ApiController>
                .Instance()
                .Calling(c => c.Post())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SpecifyingConsumption("application/pdf"));
        }

        [Fact]
        public void SpecifyingConsumptionShouldThrowExceptionWithMissingAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.SpecifyingConsumption("application/xml"));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ConsumesAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingConsumptionShouldThrowExceptionWithCorrectAttributeAndWrongContentType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .Calling(c => c.Post())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.SpecifyingConsumption("wrong-content-type"));
                },
                "When calling Post action in ApiController expected action to have ConsumesAttribute with 'wrong-content-type' content type, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingConsumptionShouldNotThrowExceptionWithCorrectAttributeAndContentTypes()
        {
            MyController<ApiController>
                .Instance()
                .Calling(c => c.Post())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SpecifyingConsumption("application/pdf", "application/javascript"));
        }

        [Fact]
        public void SpecifyingConsumptionShouldThrowExceptionWithCorrectAttributeAndMoreContentTypes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .Calling(c => c.Post())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SpecifyingConsumption("application/xml", "application/json", "application/pdf"));
                },
                "When calling Post action in ApiController expected action to have ConsumesAttribute with 3 content types, but in fact found 2.");
        }

        [Fact]
        public void SpecifyingConsumptionShouldThrowExceptionWithCorrectAttributeAndOneWrongContentType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .Calling(c => c.Post())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SpecifyingConsumption("application/pdf", "wrong-content-type"));
                },
                "When calling Post action in ApiController expected action to have ConsumesAttribute with 'wrong-content-type' content type, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingConsumptionShouldNotThrowExceptionWithCorrectAttributeAndContentTypesAsList()
        {
            MyController<ApiController>
                .Instance()
                .Calling(c => c.Post())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SpecifyingConsumption(new List<string>
                    {
                        "application/pdf",
                        "application/javascript"
                    }));
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeAndContentType()
        {
            MyController<ApiController>
                .Instance()
                .Calling(c => c.Post())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SpecifyingProduction("application/pdf"));
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithMissingAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SpecifyingProduction("application/xml"));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ProducesAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithCorrectAttributeAndWrongContentType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .Calling(c => c.Post())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SpecifyingProduction("wrong-content-type"));
                },
                "When calling Post action in ApiController expected action to have ProducesAttribute with 'wrong-content-type' content type, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeAndContentTypes()
        {
            MyController<ApiController>
                .Instance()
                .Calling(c => c.Post())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SpecifyingProduction("application/pdf", "application/javascript"));
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithCorrectAttributeAndMoreContentTypes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .Calling(c => c.Post())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SpecifyingProduction("application/xml", "application/json", "application/pdf"));
                },
                "When calling Post action in ApiController expected action to have ProducesAttribute with 3 content types, but in fact found 2.");
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithCorrectAttributeAndOneWrongContentType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .Calling(c => c.Post())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SpecifyingProduction("application/xml", "wrong-content-type"));
                },
                "When calling Post action in ApiController expected action to have ProducesAttribute with 'wrong-content-type' content type, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeAndContentTypesAsList()
        {
            MyController<ApiController>
                .Instance()
                .Calling(c => c.Post())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.SpecifyingProduction(new List<string>
                {
                    "application/javascript",
                    "application/pdf"
                }));
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeAndCorrectType()
        {
            MyController<ApiController>
                .Instance()
                .Calling(c => c.Post())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SpecifyingProduction(typeof(RequestModel)));
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithCorrectAttributeAndIncorrectType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .Calling(c => c.Post())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SpecifyingProduction(typeof(ResponseModel)));
                },
                "When calling Post action in ApiController expected action to have ProducesAttribute with 'ResponseModel' type, but in fact found 'RequestModel'.");
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithMissingAttributeWithType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.SpecifyingProduction(typeof(ResponseModel), "application/xml"));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ProducesAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeTypeAndContentTypes()
        {
            MyController<ApiController>
                .Instance()
                .Calling(c => c.Post())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SpecifyingProduction(typeof(RequestModel), "application/pdf", "application/javascript"));
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithCorrectAttributeTypeAndMoreContentTypes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .Calling(c => c.Post())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SpecifyingProduction(typeof(RequestModel), "application/xml", "application/json", "application/pdf"));
                },
                "When calling Post action in ApiController expected action to have ProducesAttribute with 3 content types, but in fact found 2.");
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithCorrectAttributeTypeAndOneWrongContentType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .Calling(c => c.Post())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SpecifyingProduction(typeof(RequestModel), "application/pdf", "wrong-content-type"));
                },
                "When calling Post action in ApiController expected action to have ProducesAttribute with 'wrong-content-type' content type, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeTypeAndContentTypesAsList()
        {
            MyController<ApiController>
                .Instance()
                .Calling(c => c.Post())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.SpecifyingProduction(typeof(RequestModel), new List<string>
                {
                    "application/pdf",
                    "application/javascript"
                }));
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeTypeAndUsingBuilder()
        {
            MyController<ApiController>
                .Instance()
                .Calling(c => c.Post())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SpecifyingProduction(production => production
                        .WithType(typeof(RequestModel))
                        .WithContentTypes("application/javascript", "application/pdf")
                        .WithOrder(2)));
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeTypeAndUsingBuilderForOrder()
        {
            MyController<ApiController>
                .Instance()
                .Calling(c => c.Post())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SpecifyingProduction(production => production
                        .WithOrder(2)));
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeTypeAndUsingBuilderWithContentTypeList()
        {
            MyController<ApiController>
                .Instance()
                .Calling(c => c.Post())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SpecifyingProduction(production => production
                        .WithType(typeof(RequestModel))
                        .AndAlso()
                        .WithContentTypes(new List<string> { "application/pdf", "application/javascript" })
                        .AndAlso()
                        .WithOrder(2)));
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithCorrectAttributeAndWrongOrder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .Calling(c => c.Post())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SpecifyingProduction(production => production
                                .WithOrder(1)));
                },
                "When calling Post action in ApiController expected action to have ProducesAttribute with order of 1, but in fact found 2.");
        }

        [Fact]
        public void AllowingAnonymousRequestsShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.AllowingAnonymousRequests());
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have AllowAnonymousAttribute, but in fact such was not found.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.RestrictingForAuthorizedRequests());
                },
                "When calling VariousAttributesAction action in MvcController expected action to have AuthorizeAttribute, but in fact such was not found.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithTheAttributeWithCorrectRoles()
        {
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.RestrictingForAuthorizedRequests(withAllowedRoles: "Admin"));
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have AuthorizeAttribute with allowed 'Admin' roles, but in fact found 'Admin,Moderator'.");
        }
        
        [Fact]
        public void AddingFormatShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<ApiController>
                .Instance()
                .Calling(c => c.Post())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.AddingFormat());
        }

        [Fact]
        public void AddingFormatCallShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .Calling(c => c.Get())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.AddingFormat());
                },
                "When calling Get action in ApiController expected action to have FormatFilterAttribute, but in fact such was not found.");
        }

        [Fact]
        public void DisablingActionCallShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.DisablingActionCall());
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have NonActionAttribute, but in fact such was not found.");
        }
        
        [Fact]
        public void RestrictingForHttpMethodWithGenericShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForHttpMethod<HttpGetAttribute>());
        }

        [Fact]
        public void RestrictingForHttpMethodWithStringShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForHttpMethod("GET"));
        }

        [Fact]
        public void RestrictingForHttpMethodWithHttpMethodClassShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForHttpMethod(HttpMethod.Get));
        }

        [Fact]
        public void RestrictingForHttpMethodWithListOfStringsShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForHttpMethods(new List<string> { "GET", "HEAD" }));
        }

        [Fact]
        public void RestrictingForHttpMethodWithParamsOfStringsShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForHttpMethods("GET", "HEAD"));
        }

        [Fact]
        public void RestrictingForHttpMethodWithListOfHttpMethodsShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForHttpMethods(new List<HttpMethod> { HttpMethod.Get, HttpMethod.Head }));
        }

        [Fact]
        public void RestrictingForHttpMethodWithParamsOfHttpMethodShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForHttpMethods(HttpMethod.Get, HttpMethod.Head));
        }

        [Fact]
        public void RestrictingForHttpMethodWithListOfHttpMethodsShouldWorkCorrectlyWithDoubleAttributes()
        {
            MyController<MvcController>
                .Instance()
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
                    MyController<MvcController>
                       .Instance()
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
        public void PassingForShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .PassingFor<RouteAttribute>(route => Assert.Equal("/api/test", route.Template)));
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectAssertions()
        {
            Assert.ThrowsAny<Exception>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .PassingFor<RouteAttribute>(route => Assert.Equal("/invalid", route.Template)));
                });
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OtherAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .PassingFor<ActionNameAttribute>(authorize => Assert.Equal("Admin", authorize.Name)));
                },
                "When calling OtherAttributes action in MvcController expected action to have ActionNameAttribute, but in fact such was not found.");
        }

        [Fact]
        public void PassingForShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .PassingFor<RouteAttribute>(route => route.Template == "/api/test"));
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectPredicate()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .PassingFor<RouteAttribute>(route => route.Template == "/invalid"));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have RouteAttribute passing the given predicate, but it failed.");
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectAttributeWithPredicate()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OtherAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .PassingFor<ActionNameAttribute>(authorize => authorize.Name == "Admin"));
                },
                "When calling OtherAttributes action in MvcController expected action to have ActionNameAttribute, but in fact such was not found.");
        }

        [Fact]
        public void WithNoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.EmptyActionWithParameters(With.No<int>(), With.No<RequestModel>()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes.ChangingActionNameTo("Test"));
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes =>
                    attributes
                        .AllowingAnonymousRequests()
                        .AndAlso()
                        .DisablingActionCall()
                        .ChangingActionNameTo("NormalAction"));
        }
    }
}
