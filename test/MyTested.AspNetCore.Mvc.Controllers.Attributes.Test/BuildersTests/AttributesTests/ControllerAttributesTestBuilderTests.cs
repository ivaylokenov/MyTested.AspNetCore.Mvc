namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AttributesTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class ControllerAttributesTestBuilderTests
    {
        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttribute()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/test"));
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttributeAndCaseDifference()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/Test"));
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttributeByUsingBuilder()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .ChangingRouteTo(route => route
                        .WithOrder(1)
                        .AndAlso()
                        .WithName("TestRouteAttributes")
                        .AndAlso()
                        .WithTemplate("api/Test")));
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithControllerWithTheAttributeAndWrongTemplate()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.ChangingRouteTo("api/another"));
                },
                "When testing AttributesController was expected to have RouteAttribute with 'api/another' template, but in fact found 'api/test'.");
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttributeAndCorrectName()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/test", withName: "TestRouteAttributes"));
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithActionWithTheAttributeAndWrongName()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.ChangingRouteTo("api/test", withName: "AnotherRoute"));
                },
                "When testing AttributesController was expected to have RouteAttribute with 'AnotherRoute' name, but in fact found 'TestRouteAttributes'.");
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithActionWithTheAttributeAndCorrectOrder()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/test", withOrder: 1));
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithActionWithTheAttributeAndWrongOrder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.ChangingRouteTo("api/test", withOrder: 2));
                },
                "When testing AttributesController was expected to have RouteAttribute with order of 2, but in fact found 1.");
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AreaController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.ChangingRouteTo("api/test"));
                },
                "When testing AreaController was expected to have RouteAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingAreaShouldNotThrowExceptionWithActionWithTheAttribute()
        {
            MyController<AreaController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.SpecifyingArea("CustomArea"));
        }

        [Fact]
        public void SpecifyingAreaShouldThrowExceptionWithActionWithTheAttributeAndWrongName()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AreaController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingArea("AnotherArea"));
                },
                "When testing AreaController was expected to have 'AnotherArea' area, but in fact found 'CustomArea'.");
        }

        [Fact]
        public void SpecifyingAreaToShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingArea("CustomArea"));
                },
                "When testing MvcController was expected to have AreaAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingConsumptionShouldNotThrowExceptionWithCorrectAttributeAndContentType()
        {
            MyController<ApiController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.SpecifyingConsumption("application/xml"));
        }

        [Fact]
        public void SpecifyingConsumptionShouldThrowExceptionWithMissingAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingConsumption("application/xml"));
                },
                "When testing MvcController was expected to have ConsumesAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingConsumptionShouldThrowExceptionWithCorrectAttributeAndWrongContentType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingConsumption("wrong-content-type"));
                },
                "When testing ApiController was expected to have ConsumesAttribute with 'wrong-content-type' content type, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingConsumptionShouldNotThrowExceptionWithCorrectAttributeAndContentTypes()
        {
            MyController<ApiController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.SpecifyingConsumption("application/xml", "application/json"));
        }

        [Fact]
        public void SpecifyingConsumptionShouldThrowExceptionWithCorrectAttributeAndMoreContentTypes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingConsumption("application/xml", "application/json", "application/pdf"));
                },
                "When testing ApiController was expected to have ConsumesAttribute with 3 content types, but in fact found 2.");
        }

        [Fact]
        public void SpecifyingConsumptionShouldThrowExceptionWithCorrectAttributeAndOneWrongContentType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingConsumption("application/xml", "wrong-content-type"));
                },
                "When testing ApiController was expected to have ConsumesAttribute with 'wrong-content-type' content type, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingConsumptionShouldNotThrowExceptionWithCorrectAttributeAndContentTypesAsList()
        {
            MyController<ApiController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.SpecifyingConsumption(new List<string>
                {
                    "application/xml",
                    "application/json"
                }));
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeAndContentType()
        {
            MyController<ApiController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.SpecifyingProduction("application/xml"));
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithMissingAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingProduction("application/xml"));
                },
                "When testing MvcController was expected to have ProducesAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithCorrectAttributeAndWrongContentType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingProduction("wrong-content-type"));
                },
                "When testing ApiController was expected to have ProducesAttribute with 'wrong-content-type' content type, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeAndContentTypes()
        {
            MyController<ApiController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.SpecifyingProduction("application/xml", "application/json"));
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithCorrectAttributeAndMoreContentTypes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingProduction("application/xml", "application/json", "application/pdf"));
                },
                "When testing ApiController was expected to have ProducesAttribute with 3 content types, but in fact found 2.");
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithCorrectAttributeAndOneWrongContentType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingProduction("application/xml", "wrong-content-type"));
                },
                "When testing ApiController was expected to have ProducesAttribute with 'wrong-content-type' content type, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeAndContentTypesAsList()
        {
            MyController<ApiController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.SpecifyingProduction(new List<string>
                {
                    "application/xml",
                    "application/json"
                }));
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeAndCorrectType()
        {
            MyController<ApiController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingProduction(typeof(ResponseModel)));
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithCorrectAttributeAndIncorrectType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingProduction(typeof(RequestModel)));
                },
                "When testing ApiController was expected to have ProducesAttribute with 'RequestModel' type, but in fact found 'ResponseModel'.");
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithMissingAttributeWithType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingProduction(typeof(ResponseModel), "application/xml"));
                },
                "When testing MvcController was expected to have ProducesAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeTypeAndContentTypes()
        {
            MyController<ApiController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.SpecifyingProduction(typeof(ResponseModel), "application/xml", "application/json"));
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithCorrectAttributeTypeAndMoreContentTypes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingProduction(typeof(ResponseModel), "application/xml", "application/json", "application/pdf"));
                },
                "When testing ApiController was expected to have ProducesAttribute with 3 content types, but in fact found 2.");
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithCorrectAttributeTypeAndOneWrongContentType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingProduction(typeof(ResponseModel), "application/xml", "wrong-content-type"));
                },
                "When testing ApiController was expected to have ProducesAttribute with 'wrong-content-type' content type, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeTypeAndContentTypesAsList()
        {
            MyController<ApiController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.SpecifyingProduction(typeof(ResponseModel), new List<string>
                {
                    "application/xml",
                    "application/json"
                }));
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeTypeAndUsingBuilder()
        {
            MyController<ApiController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingProduction(production => production
                        .WithType(typeof(ResponseModel))
                        .WithContentTypes("application/xml", "application/json")
                        .WithOrder(1)));
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeTypeAndUsingBuilderForOrder()
        {
            MyController<ApiController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingProduction(production => production
                        .WithOrder(1)));
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeTypeAndUsingBuilderWithContentTypeList()
        {
            MyController<ApiController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingProduction(production => production
                        .WithType(typeof(ResponseModel))
                        .AndAlso()
                        .WithContentTypes(new List<string> { "application/xml", "application/json" })
                        .AndAlso()
                        .WithOrder(1)));
        }

        [Fact]
        public void SpecifyingProductionShouldThrowExceptionWithCorrectAttributeAndWrongOrder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingProduction(production => production
                                .WithOrder(2)));
                },
                "When testing ApiController was expected to have ProducesAttribute with order of 2, but in fact found 1.");
        }

        [Fact]
        public void RequiringHttpsShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.RequiringHttps());
        }

        [Fact]
        public void RequiringHttpsShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.RequiringHttps());
                },
                "When testing MvcController was expected to have RequireHttpsAttribute, but in fact such was not found.");
        }

        [Fact]
        public void RequiringHttpsShouldNotThrowExceptionWithTheAttributeAndCorrectValue()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.RequiringHttps(false));
        }

        [Fact]
        public void RequiringHttpsShouldThrowExceptionWithActionWithoutTheAttributeAndIncorrectPermanentValue()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.RequiringHttps(true));
                },
                "When testing AttributesController was expected to have RequireHttpsAttribute with permanent redirect, but in fact it was a temporary one.");
        }

        [Fact]
        public void AllowingAnonymousRequestsShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.AllowingAnonymousRequests());
        }

        [Fact]
        public void AllowingAnonymousRequestsShouldThrowExceptionWithControllerWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.AllowingAnonymousRequests());
                },
                "When testing MvcController was expected to have AllowAnonymousAttribute, but in fact such was not found.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.RestrictingForAuthorizedRequests());
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithControllerWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.RestrictingForAuthorizedRequests());
                },
                "When testing AttributesController was expected to have AuthorizeAttribute, but in fact such was not found.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithTheAttributeWithCorrectRoles()
        {
            MyController<MvcController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.RestrictingForAuthorizedRequests(withAllowedRoles: "Admin,Moderator"));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithControllerWithoutTheAttributeWithIncorrectRoles()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.RestrictingForAuthorizedRequests(withAllowedRoles: "Admin"));
                },
                "When testing MvcController was expected to have AuthorizeAttribute with allowed 'Admin' roles, but in fact found 'Admin,Moderator'.");
        }

        [Fact]
        public void AddingFormatShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.AddingFormat());
        }

        [Fact]
        public void AddingFormatShouldThrowExceptionWithControllerWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.AddingFormat());
                },
                "When testing AttributesController was expected to have FormatFilterAttribute, but in fact such was not found.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.CachingResponse());
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithControllerWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.CachingResponse());
                },
                "When testing MvcController was expected to have ResponseCacheAttribute, but in fact such was not found.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectDuration()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.CachingResponse(60));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectDuration()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.CachingResponse(30));
                },
                "When testing AttributesController was expected to have ResponseCacheAttribute with duration of 30 seconds, but in fact found 60.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectCacheProfileName()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.CachingResponse("Test Profile Controller"));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectCacheProfileName()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.CachingResponse("Wrong Profile"));
                },
                "When testing AttributesController was expected to have ResponseCacheAttribute with 'Wrong Profile' cache profile name, but in fact found 'Test Profile Controller'.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectDurationWithBuilder()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithDuration(60)));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectDurationWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithDuration(30)));
                },
                "When testing AttributesController was expected to have ResponseCacheAttribute with duration of 30 seconds, but in fact found 60.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectLocationWithBuilder()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithLocation(ResponseCacheLocation.Any)));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectLocationWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithLocation(ResponseCacheLocation.Client)));
                },
                "When testing AttributesController was expected to have ResponseCacheAttribute with 'Client' location, but in fact found 'Any'.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectNoStoreWithBuilder()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithNoStore(false)));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectNoStoreWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithNoStore(true)));
                },
                "When testing AttributesController was expected to have ResponseCacheAttribute with no store value of 'True', but in fact found 'False'.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectVaryByHeaderWithBuilder()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithVaryByHeader("Test Header Controller")));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectVaryByHeaderWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithVaryByHeader("Wrong Header")));
                },
                "When testing AttributesController was expected to have ResponseCacheAttribute with vary by header value of 'Wrong Header', but in fact found 'Test Header Controller'.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectVaryByQueryKeyWithBuilder()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithVaryByQueryKey("FirstQueryController")));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectVaryByQueryKeyWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithVaryByQueryKey("Wrong Query Key")));
                },
                "When testing AttributesController was expected to have ResponseCacheAttribute with vary by query string key value of 'Wrong Query Key', but in fact such was not found.");
        }


        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithCorrectAttributeAndCorrectVaryQueryKeysWithBuilder()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithVaryByQueryKeys("FirstQueryController", "SecondQueryController")));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithCorrectAttributeAndIncorrectVaryQueryKeysCountWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithVaryByQueryKeys("FirstQueryController", "SecondQueryController", "ThirdQueryController")));
                },
                "When testing AttributesController was expected to have ResponseCacheAttribute with 3 vary by query string key values, but in fact found 2.");
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithCorrectAttributeAndIncorrectVaryQueryKeysSingleCountWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithVaryByQueryKeys("FirstQueryController")));
                },
                "When testing AttributesController was expected to have ResponseCacheAttribute with 1 vary by query string key value, but in fact found 2.");
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithCorrectAttributeAndOneIncorrectVaryQueryKeyWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                        .CachingResponse(responseCache => responseCache
                            .WithVaryByQueryKeys("FirstQueryController", "WrongQuery")));
                },
                "When testing AttributesController was expected to have ResponseCacheAttribute with vary by query string key value of 'WrongQuery', but in fact such was not found.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithCorrectAttributeAndVaryQueryKeyAsList()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithVaryByQueryKeys(new List<string> { "FirstQueryController", "SecondQueryController" })));
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectCacheProfileNameWithBuilder()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithCacheProfileName("Test Profile Controller")));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectCacheProfileNameWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithCacheProfileName("Wrong Profile")));
                },
                "When testing AttributesController was expected to have ResponseCacheAttribute with 'Wrong Profile' cache profile name, but in fact found 'Test Profile Controller'.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectOrderWithBuilder()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithOrder(3)));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectOrderWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithOrder(1)));
                },
                "When testing AttributesController was expected to have ResponseCacheAttribute with order of 1, but in fact found 3.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectValuesWithBuilder()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithOrder(3)
                        .AndAlso()
                        .WithCacheProfileName("Test Profile Controller")
                        .AndAlso()
                        .WithVaryByQueryKeys("FirstQueryController", "SecondQueryController")
                        .AndAlso()
                        .WithVaryByHeader("Test Header Controller")
                        .AndAlso()
                        .WithNoStore(false)
                        .AndAlso()
                        .WithLocation(ResponseCacheLocation.Any)
                        .AndAlso()
                        .WithDuration(60)));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectValuesWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithCacheProfileName("Test Profile Controller")
                                .AndAlso()
                                .WithVaryByQueryKeys("FirstQueryController", "SecondQueryController")
                                .AndAlso()
                                .WithOrder(1)
                                .AndAlso()
                                .WithVaryByHeader("Test Header Controller")
                                .AndAlso()
                                .WithNoStore(false)
                                .AndAlso()
                                .WithLocation(ResponseCacheLocation.Any)
                                .AndAlso()
                                .WithDuration(60)));
                },
                "When testing AttributesController was expected to have ResponseCacheAttribute with order of 1, but in fact found 3.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithBufferBody(true)));
                },
                "When testing MvcController was expected to have RequestFormLimitsAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectBufferBody()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithBufferBody(true)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectBufferBody()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithBufferBody(false)));
                },
                "When testing AttributesController was expected to have RequestFormLimitsAttribute with buffer body value of 'False', but in fact found 'True'.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectMemoryBufferThreshold()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithMemoryBufferThreshold(3)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectMemoryBufferThreshold()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithMemoryBufferThreshold(0)));
                },
                "When testing AttributesController was expected to have RequestFormLimitsAttribute with memory buffer threshold value of 0, but in fact found 3.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectBufferBodyLengthLimit()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithBufferBodyLengthLimit(1)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectBufferBodyLengthLimit()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithBufferBodyLengthLimit(0)));
                },
                "When testing AttributesController was expected to have RequestFormLimitsAttribute with buffer body length limit of 0, but in fact found 1.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectValueCountLimit()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithValueCountLimit(9)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectValueCountLimit()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithValueCountLimit(0)));
                },
                "When testing AttributesController was expected to have RequestFormLimitsAttribute with value count limit of 0, but in fact found 9.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectKeyLengthLimit()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithKeyLengthLimit(2)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectKeyLengthLimit()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithKeyLengthLimit(0)));
                },
                "When testing AttributesController was expected to have RequestFormLimitsAttribute with key length limit of 0, but in fact found 2.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectValueLengthLimit()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithValueLengthLimit(10)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectValueLengthLimit()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithValueLengthLimit(0)));
                },
                "When testing AttributesController was expected to have RequestFormLimitsAttribute with value length limit of 0, but in fact found 10.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectMultipartBoundaryLengthLimit()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithMultipartBoundaryLengthLimit(5)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectMultipartBoundaryLengthLimit()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithMultipartBoundaryLengthLimit(0)));
                },
                "When testing AttributesController was expected to have RequestFormLimitsAttribute with multipart boundary length limit of 0, but in fact found 5.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectMultipartHeadersCountLimit()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithMultipartHeadersCountLimit(6)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectMultipartHeadersCountLimit()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithMultipartHeadersCountLimit(0)));
                },
                "When testing AttributesController was expected to have RequestFormLimitsAttribute with multipart headers count limit of 0, but in fact found 6.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectMultipartHeadersLengthLimit()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithMultipartHeadersLengthLimit(7)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectMultipartHeadersLengthLimit()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithMultipartHeadersLengthLimit(0)));
                },
                "When testing AttributesController was expected to have RequestFormLimitsAttribute with multipart headers length limit of 0, but in fact found 7.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectMultipartBodyLengthLimit()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithMultipartBodyLengthLimit(4)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectMultipartBodyLengthLimit()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithMultipartBodyLengthLimit(0)));
                },
                "When testing AttributesController was expected to have RequestFormLimitsAttribute with multipart body length limit of 0, but in fact found 4.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectOrder()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithOrder(8)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectOrder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithOrder(10)));
                },
                "When testing AttributesController was expected to have RequestFormLimitsAttribute with order of 10, but in fact found 8.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectBuilder()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithBufferBody(true)
                        .AndAlso()
                        .WithBufferBodyLengthLimit(1)
                        .AndAlso()
                        .WithKeyLengthLimit(2)
                        .AndAlso()
                        .WithMemoryBufferThreshold(3)
                        .AndAlso()
                        .WithMultipartBodyLengthLimit(4)
                        .AndAlso()
                        .WithMultipartBoundaryLengthLimit(5)
                        .AndAlso()
                        .WithMultipartHeadersCountLimit(6)
                        .AndAlso()
                        .WithMultipartHeadersLengthLimit(7)
                        .AndAlso()
                        .WithOrder(8)
                        .AndAlso()
                        .WithValueCountLimit(9)
                        .AndAlso()
                        .WithValueLengthLimit(10)
                        .AndAlso()
                        .WithOrder(8)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithBufferBody(true)
                                .AndAlso()
                                .WithBufferBodyLengthLimit(1)
                                .AndAlso()
                                .WithKeyLengthLimit(2)
                                .AndAlso()
                                .WithMemoryBufferThreshold(3)
                                .AndAlso()
                                .WithMultipartBodyLengthLimit(4)
                                .AndAlso()
                                .WithMultipartBoundaryLengthLimit(5)
                                .AndAlso()
                                .WithMultipartHeadersCountLimit(6)
                                .AndAlso()
                                .WithMultipartHeadersLengthLimit(7)
                                .AndAlso()
                                .WithOrder(8)
                                .AndAlso()
                                .WithValueCountLimit(9)
                                .AndAlso()
                                .WithValueLengthLimit(10)
                                .AndAlso()
                                .WithOrder(10)));
                },
                "When testing AttributesController was expected to have RequestFormLimitsAttribute with order of 10, but in fact found 8.");
        }

        [Fact]
        public void SettingRequestSizeLimitToShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.SettingRequestSizeLimitTo(2048));
        }

        [Fact]
        public void SettingRequestSizeLimitToShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.SettingRequestSizeLimitTo(2048));
                },
                "When testing MvcController was expected to have RequestSizeLimitAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SettingRequestSizeLimitToShouldThrowExceptionWithActionWithTheAttributeAndIncorrectBytes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.SettingRequestSizeLimitTo(1024));
                },
                "When testing AttributesController was expected to have RequestSizeLimitAttribute with request size limit of 1024 bytes, but in fact found 2048.");
        }

        [Fact]
        public void DisablingRequestSizeLimitShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.DisablingRequestSizeLimit());
        }

        [Fact]
        public void DisablingRequestSizeLimitShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.DisablingRequestSizeLimit());
                },
                "When testing MvcController was expected to have DisableRequestSizeLimitAttribute, but in fact such was not found.");
        }

        [Fact]
        public void IndicatingControllerShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<AreaController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.IndicatingControllerExplicitly());
        }

        [Fact]
        public void IndicatingControllerShouldThrowExceptionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.IndicatingControllerExplicitly());
                },
                "When testing MvcController was expected to have ControllerAttribute, but in fact such was not found.");
        }

        [Fact]
        public void IndicatingApiControllerShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<ApiController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.IndicatingApiController());
        }

        [Fact]
        public void IndicatingApiControllerShouldThrowExceptionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.IndicatingApiController());
                },
                "When testing MvcController was expected to have ApiControllerAttribute, but in fact such was not found.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectlyAttributes()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .AllowingAnonymousRequests()
                    .AndAlso()
                    .ChangingRouteTo("api/test"));
        }
    }
}
