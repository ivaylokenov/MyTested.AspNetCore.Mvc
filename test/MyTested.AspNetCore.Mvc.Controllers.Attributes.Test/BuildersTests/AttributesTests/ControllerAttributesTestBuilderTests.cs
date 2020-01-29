namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AttributesTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Setups.ActionFilters;
    using Setups.Pipeline;
    using Xunit;

    public class ControllerAttributesTestBuilderTests
    {
        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttribute()
        {
            MyController<AttributesController>
                .ShouldHave()
                .Attributes(attributes => attributes.SpecifyingRoute("api/test"));
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttributeAndCaseDifference()
        {
            MyController<AttributesController>
                .ShouldHave()
                .Attributes(attributes => attributes.SpecifyingRoute("api/Test"));
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttributeByUsingBuilder()
        {
            MyController<AttributesController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingRoute(route => route
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingRoute("api/another"));
                },
                "When testing AttributesController was expected to have RouteAttribute with 'api/another' template, but in fact found 'api/test'.");
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttributeAndCorrectName()
        {
            MyController<AttributesController>
                .ShouldHave()
                .Attributes(attributes => attributes.SpecifyingRoute("api/test", withName: "TestRouteAttributes"));
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithActionWithTheAttributeAndWrongName()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingRoute("api/test", withName: "AnotherRoute"));
                },
                "When testing AttributesController was expected to have RouteAttribute with 'AnotherRoute' name, but in fact found 'TestRouteAttributes'.");
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithActionWithTheAttributeAndCorrectOrder()
        {
            MyController<AttributesController>
                .ShouldHave()
                .Attributes(attributes => attributes.SpecifyingRoute("api/test", withOrder: 1));
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithActionWithTheAttributeAndWrongOrder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingRoute("api/test", withOrder: 2));
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingRoute("api/test"));
                },
                "When testing AreaController was expected to have RouteAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingAreaShouldNotThrowExceptionWithActionWithTheAttribute()
        {
            MyController<AreaController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingArea("CustomArea"));
                },
                "When testing MvcController was expected to have AreaAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingConsumptionShouldNotThrowExceptionWithCorrectAttributeAndContentType()
        {
            MyController<ApiController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingConsumption("wrong-content-type"));
                },
                "When testing ApiController was expected to have ConsumesAttribute with 'wrong-content-type' content type, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingConsumptionShouldNotThrowExceptionWithCorrectAttributeAndContentTypes()
        {
            MyController<ApiController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingConsumption("application/xml", "wrong-content-type"));
                },
                "When testing ApiController was expected to have ConsumesAttribute with 'wrong-content-type' content type, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingConsumptionShouldNotThrowExceptionWithCorrectAttributeAndContentTypesAsList()
        {
            MyController<ApiController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingProduction("wrong-content-type"));
                },
                "When testing ApiController was expected to have ProducesAttribute with 'wrong-content-type' content type, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeAndContentTypes()
        {
            MyController<ApiController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingProduction("application/xml", "wrong-content-type"));
                },
                "When testing ApiController was expected to have ProducesAttribute with 'wrong-content-type' content type, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeAndContentTypesAsList()
        {
            MyController<ApiController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingProduction(typeof(ResponseModel), "application/xml"));
                },
                "When testing MvcController was expected to have ProducesAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeTypeAndContentTypes()
        {
            MyController<ApiController>
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
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingProduction(production => production
                        .OfType(typeof(ResponseModel))
                        .WithContentTypes("application/xml", "application/json")
                        .WithOrder(1)));
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeTypeAndUsingBuilderForOrder()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingProduction(production => production
                        .WithOrder(1)));
        }

        [Fact]
        public void SpecifyingProductionShouldNotThrowExceptionWithCorrectAttributeTypeAndUsingBuilderWithContentTypeList()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingProduction(production => production
                        .OfType(typeof(ResponseModel))
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
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingProduction(production => production
                                .WithOrder(2)));
                },
                "When testing ApiController was expected to have ProducesAttribute with order of 2, but in fact found 1.");
        }

        [Fact]
        public void SpecifyingResponseProductionShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingResponseProduction(200));
        }

        [Fact]
        public void SpecifyingResponseProductionShouldThrowExceptionWithMissingAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingResponseProduction(200));
                },
                "When testing MvcController was expected to have ProducesResponseTypeAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingResponseProductionShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingResponseProduction(404));
                },
                "When testing ApiController was expected to have ProducesResponseTypeAttribute with '404' status code, but in fact found '200'.");
        }

        [Fact]
        public void SpecifyingResponseProductionShouldNotThrowExceptionWithCorrectAttributeType()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingResponseProduction(typeof(ResponseModel)));
        }

        [Fact]
        public void SpecifyingResponseProductionShouldThrowExceptionWithIncorrectAttributeType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingResponseProduction(typeof(RequestModel)));
                },
                "When testing ApiController was expected to have ProducesResponseTypeAttribute with 'RequestModel' type, but in fact found 'ResponseModel'.");
        }

        [Fact]
        public void SpecifyingResponseProductionShouldThrowExceptionWithMissingAttributeWithType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingResponseProduction(typeof(ResponseModel)));
                },
                "When testing MvcController was expected to have ProducesResponseTypeAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingResponseProductionShouldNotThrowExceptionWithCorrectAttributeTypeAndStatusCode()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingResponseProduction(typeof(ResponseModel), 200));
        }

        [Fact]
        public void SpecifyingResponseProductionShouldThrowExceptionWithCorrectAttributeTypeAndWrongStatusCode()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingResponseProduction(typeof(ResponseModel), 404));
                },
                "When testing ApiController was expected to have ProducesResponseTypeAttribute with '404' status code, but in fact found '200'.");
        }

        [Fact]
        public void SpecifyingResponseProductionShouldThrowExceptionWithWrongAttributeTypeAndCorrectStatusCode()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingResponseProduction(typeof(RequestModel), 200));
                },
                "When testing ApiController was expected to have ProducesResponseTypeAttribute with 'RequestModel' type, but in fact found 'ResponseModel'.");
        }

        [Fact]
        public void SpecifyingResponseProductionShouldThrowExceptionWithWrongAttributeTypeAndWrongStatusCode()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingResponseProduction(typeof(RequestModel), 404));
                },
                "When testing ApiController was expected to have ProducesResponseTypeAttribute with 'RequestModel' type, but in fact found 'ResponseModel'.");
        }

        [Fact]
        public void SpecifyingResponseProductionShouldThrowExceptionWithMissingAttributeWithTypeAndStatusCode()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingResponseProduction(typeof(ResponseModel), 200));
                },
                "When testing MvcController was expected to have ProducesResponseTypeAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingResponseProductionShouldNotThrowExceptionWithCorrectAttributeStatusCodeUsingBuilder()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingResponseProduction(responseProduction => responseProduction
                        .WithStatusCode(200)));
        }

        [Fact]
        public void SpecifyingResponseProductionShouldThrowExceptionWithWrongAttributeStatusCodeUsingBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingResponseProduction(responseProduction => responseProduction
                                .WithStatusCode(404)));
                },
                "When testing ApiController was expected to have ProducesResponseTypeAttribute with '404' status code, but in fact found '200'.");
        }

        [Fact]
        public void SpecifyingResponseProductionShouldThrowExceptionWithMissingAttributeWithStatusCodeUsingBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingResponseProduction(responseProduction => responseProduction
                                .WithStatusCode(200)));
                },
                "When testing MvcController was expected to have ProducesResponseTypeAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingResponseProductionShouldNotThrowExceptionWithCorrectAttributeTypeUsingBuilder()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingResponseProduction(responseProduction => responseProduction
                        .OfType(typeof(ResponseModel))));
        }

        [Fact]
        public void SpecifyingResponseProductionShouldThrowExceptionWithMissingAttributeUsingBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingResponseProduction(responseProduction => responseProduction
                                .OfType(typeof(ResponseModel))));
                },
                "When testing MvcController was expected to have ProducesResponseTypeAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingResponseProductionShouldThrowExceptionWithWrongAttributeTypeUsingBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingResponseProduction(responseProduction => responseProduction
                                .OfType(typeof(RequestModel))));
                },
                "When testing ApiController was expected to have ProducesResponseTypeAttribute with 'RequestModel' type, but in fact found 'ResponseModel'.");
        }

        [Fact]
        public void SpecifyingResponseProductionShouldNotThrowExceptionWithCorrectAttributeTypeAndStatusCodeUsingBuilder()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingResponseProduction(responseProduction => responseProduction
                        .WithStatusCode(200)
                        .OfType(typeof(ResponseModel))));
        }

        [Fact]
        public void SpecifyingResponseProductionShouldThrowExceptionWithIncorrectAttributeTypeAndStatusCodeUsingBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingResponseProduction(responseProduction => responseProduction
                                .WithStatusCode(200)
                                .OfType(typeof(RequestModel))));
                },
                "When testing ApiController was expected to have ProducesResponseTypeAttribute with 'RequestModel' type, but in fact found 'ResponseModel'.");
        }

        [Fact]
        public void SpecifyingResponseProductionShouldThrowExceptionWithCorrectAttributeTypeAndWrongStatusCodeUsingBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingResponseProduction(responseProduction => responseProduction
                                .WithStatusCode(404)
                                .OfType(typeof(ResponseModel))));
                },
                "When testing ApiController was expected to have ProducesResponseTypeAttribute with '404' status code, but in fact found '200'.");
        }

        [Fact]
        public void SpecifyingResponseProductionShouldNotThrowExceptionWithCorrectAttributeTypeAndAlsoWithStatusCodeUsingBuilder()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingResponseProduction(responseProduction => responseProduction
                        .WithStatusCode(200)
                        .AndAlso()
                        .OfType(typeof(ResponseModel))));
        }

        [Fact]
        public void SpecifyingMiddlewareShouldNotThrowExceptionWithCorrectAttribute()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingMiddleware(typeof(Pipeline)));
        }

        [Fact]
        public void SpecifyingMiddlewareShouldThrowExceptionWithMissingAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingMiddleware(typeof(Pipeline)));
                },
                "When testing AttributesController was expected to have MiddlewareFilterAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingMiddlewareShouldThrowExceptionWithCorrectAttributeAndWrongConfigurationType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingMiddleware(typeof(OtherPipeline)));
                },
                "When testing ApiController was expected to have MiddlewareFilterAttribute with 'OtherPipeline' type, but in fact found 'Pipeline'.");
        }

        [Fact]
        public void SpecifyingMiddlewareShouldNotThrowExceptionWithCorrectAttributeConfigurationType()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingMiddleware(middleware => middleware
                        .OfType(typeof(Pipeline))));
        }

        [Fact]
        public void SpecifyingMiddlewareShouldThrowExceptionWithWrongConfigurationType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingMiddleware(middleware => middleware
                                .OfType(typeof(OtherPipeline))));
                },
                "When testing ApiController was expected to have MiddlewareFilterAttribute with 'OtherPipeline' type, but in fact found 'Pipeline'.");
        }

        [Fact]
        public void SpecifyingMiddlewareShouldThrowExceptionWithCorrectAttributeAndWrongOrder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingMiddleware(middleware => middleware.WithOrder(1)));
                },
                "When testing ApiController was expected to have MiddlewareFilterAttribute with order of 1, but in fact found 2.");
        }

        [Fact]
        public void SpecifyingMiddlewareShouldNotThrowExceptionWithCorrectAttributeTypeAndUsingBuilderForOrder()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingMiddleware(middleware => middleware.WithOrder(2)));
        }

        [Fact]
        public void SpecifyingMiddlewareShouldNotThrowExceptionWithCorrectAttributeConfigTypeAndUsingBuilderForOrder()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .SpecifyingMiddleware(middleware => middleware
                        .OfType(typeof(Pipeline))
                        .AndAlso()
                        .WithOrder(2)));
        }

        [Fact]
        public void SpecifyingMiddlewareShouldThrowExceptionWithCorrectAttributeConfigTypeAndUsingBuilderForOrderWithWrongOrder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .SpecifyingMiddleware(middleware => middleware
                                .OfType(typeof(Pipeline))
                                .AndAlso()
                                .WithOrder(1)));
                },
                "When testing ApiController was expected to have MiddlewareFilterAttribute with order of 1, but in fact found 2.");
        }

        [Fact]
        public void WithTypeFilterShouldNotThrowExceptionWithCorrectAttribute()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .WithTypeFilter(typeof(CustomActionFilterWithArgs)));
        }

        [Fact]
        public void WithTypeFilterShouldThrowExceptionWithMissingAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .WithTypeFilter(typeof(CustomActionFilterWithArgs)));
                },
                "When testing AttributesController was expected to have TypeFilterAttribute, but in fact such was not found.");
        }

        [Fact]
        public void WithTypeFilterShouldThrowExceptionWithCorrectAttributeAndWrongImplementationType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .WithTypeFilter(typeof(OtherActionFilterWithArgs)));
                },
                "When testing ApiController was expected to have TypeFilterAttribute with 'OtherActionFilterWithArgs' type, but in fact found 'CustomActionFilterWithArgs'.");
        }

        [Fact]
        public void WithTypeFilterShouldNotThrowExceptionWithCorrectImplementationType()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .WithTypeFilter(filter => filter
                        .OfType(typeof(CustomActionFilterWithArgs))));
        }

        [Fact]
        public void WithTypeFilterShouldThrowExceptionWithWrongImplementationType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .WithTypeFilter(filter => filter
                                .OfType(typeof(OtherActionFilterWithArgs))));
                },
                "When testing ApiController was expected to have TypeFilterAttribute with 'OtherActionFilterWithArgs' type, but in fact found 'CustomActionFilterWithArgs'.");
        }

        [Fact]
        public void WithTypeFilterShouldThrowExceptionWithCorrectAttributeAndWrongOrder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .WithTypeFilter(filter => filter.WithOrder(1)));
                },
                "When testing ApiController was expected to have TypeFilterAttribute with order of 1, but in fact found 2.");
        }

        [Fact]
        public void WithTypeFilterShouldNotThrowExceptionWithCorrectAttributeTypeAndUsingBuilderForOrder()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .WithTypeFilter(filter => filter.WithOrder(2)));
        }

        [Fact]
        public void WithTypeFilterShouldNotThrowExceptionWithCorrectAttributeImplementationTypeAndUsingBuilderForOrder()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .WithTypeFilter(filter => filter
                        .OfType(typeof(CustomActionFilterWithArgs))
                        .AndAlso()
                        .WithOrder(2)));
        }

        [Fact]
        public void WithTypeFilterShouldThrowExceptionWithCorrectAttributeImplementationTypeAndUsingBuilderForOrderWithWrongOrder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .WithTypeFilter(filter => filter
                                .OfType(typeof(CustomActionFilterWithArgs))
                                .AndAlso()
                                .WithOrder(1)));
                },
                "When testing ApiController was expected to have TypeFilterAttribute with order of 1, but in fact found 2.");
        }

        [Fact]
        public void WithTypeFilterShouldNotThrowExceptionWithCorrectAttributeImplementationTypeAndUsingBuilderForArguments()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .WithTypeFilter(filter => filter
                        .OfType(typeof(CustomActionFilterWithArgs))
                        .AndAlso()
                        .WithArguments(new object[]
                        {
                            10
                        })));
        }

        [Fact]
        public void WithTypeFilterShouldThrowExceptionWithCorrectAttributeImplementationTypeAndUsingBuilderForForArgumentsWithWrongArgs()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .WithTypeFilter(filter => filter
                                .OfType(typeof(CustomActionFilterWithArgs))
                                .AndAlso()
                                .WithArguments(new object[]
                                {
                                    1
                                })));
                },
                "When testing ApiController was expected to have TypeFilterAttribute with argument with the provided value, but in fact such was not found.");
        }

        [Fact]
        public void WithTypeFilterShouldThrowExceptionWithCorrectAttributeAndWrongArgumentsValue()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .WithTypeFilter(filter => filter.WithArguments(new object[]
                            {
                                1
                            })));
                },
                "When testing ApiController was expected to have TypeFilterAttribute with argument with the provided value, but in fact such was not found.");
        }

        [Fact]
        public void WithTypeFilterShouldNotThrowExceptionWithCorrectAttributeTypeAndUsingBuilderForArguments()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .WithTypeFilter(filter => filter.WithArguments(new object[]
                    {
                        10
                    })));
        }

        [Fact]
        public void WithServiceFilterShouldNotThrowExceptionWithCorrectAttribute()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .WithServiceFilter(typeof(CustomActionFilter)));
        }

        [Fact]
        public void WithServiceFilterShouldThrowExceptionWithMissingAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .WithServiceFilter(typeof(CustomActionFilter)));
                },
                "When testing AttributesController was expected to have ServiceFilterAttribute, but in fact such was not found.");
        }

        [Fact]
        public void WithServiceFilterShouldThrowExceptionWithCorrectAttributeAndWrongServiceType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .WithServiceFilter(typeof(OtherActionFilter)));
                },
                "When testing ApiController was expected to have ServiceFilterAttribute with 'OtherActionFilter' type, but in fact found 'CustomActionFilter'.");
        }

        [Fact]
        public void WithServiceFilterShouldNotThrowExceptionWithCorrectServiceFilterType()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .WithServiceFilter(filter => filter
                        .OfType(typeof(CustomActionFilter))));
        }

        [Fact]
        public void WithServiceFilterShouldThrowExceptionWithWrongServiceFilterType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .WithServiceFilter(filter => filter
                                .OfType(typeof(OtherActionFilter))));
                },
                "When testing ApiController was expected to have ServiceFilterAttribute with 'OtherActionFilter' type, but in fact found 'CustomActionFilter'.");
        }

        [Fact]
        public void WithServiceFilterShouldThrowExceptionWithCorrectAttributeAndWrongOrder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .WithServiceFilter(filter => filter.WithOrder(1)));
                },
                "When testing ApiController was expected to have ServiceFilterAttribute with order of 1, but in fact found 2.");
        }

        [Fact]
        public void WithServiceFilterShouldNotThrowExceptionWithCorrectAttributeTypeAndUsingBuilderForOrder()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .WithServiceFilter(filter => filter.WithOrder(2)));
        }

        [Fact]
        public void WithServiceFilterShouldNotThrowExceptionWithCorrectAttributeServiceTypeAndUsingBuilderForOrder()
        {
            MyController<ApiController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .WithServiceFilter(filter => filter
                        .OfType(typeof(CustomActionFilter))
                        .AndAlso()
                        .WithOrder(2)));
        }

        [Fact]
        public void WithServiceFilterShouldThrowExceptionWithCorrectAttributeServiceTypeAndUsingBuilderForOrderWithWrongOrder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<ApiController>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .WithServiceFilter(filter => filter
                                .OfType(typeof(CustomActionFilter))
                                .AndAlso()
                                .WithOrder(1)));
                },
                "When testing ApiController was expected to have ServiceFilterAttribute with order of 1, but in fact found 2.");
        }

        [Fact]
        public void RequiringHttpsShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<AttributesController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.RequiringHttps());
                },
                "When testing MvcController was expected to have RequireHttpsAttribute, but in fact such was not found.");
        }

        [Fact]
        public void RequiringHttpsShouldNotThrowExceptionWithTheAttributeAndCorrectValue()
        {
            MyController<AttributesController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.RequiringHttps(true));
                },
                "When testing AttributesController was expected to have RequireHttpsAttribute with permanent redirect, but in fact it was a temporary one.");
        }

        [Fact]
        public void AllowingAnonymousRequestsShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<AttributesController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.AllowingAnonymousRequests());
                },
                "When testing MvcController was expected to have AllowAnonymousAttribute, but in fact such was not found.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<MvcController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.RestrictingForAuthorizedRequests());
                },
                "When testing AttributesController was expected to have AuthorizeAttribute, but in fact such was not found.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithTheAttributeWithCorrectRoles()
        {
            MyController<MvcController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .RestrictingForAuthorizedRequests(withAllowedRoles: "Admin"));
                },
                "When testing MvcController was expected to have AuthorizeAttribute with 'Admin' roles, but in fact found 'Admin,Moderator'.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithCorrectAuthenticationScheme()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithAuthenticationSchemes("Cookies")));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithIncorrectAuthenticationScheme()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<AuthorizationController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithAuthenticationSchemes("JWTBearer")));
            },
            "When testing AuthorizationController was expected to have AuthorizeAttribute with 'JWTBearer' authentication schemes, but in fact found 'Cookies'.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithMultipleAuthenticationSchemes()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<AuthorizationController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithAuthenticationSchemes("Cookies, JWTBearer")));
            },
            "When testing AuthorizationController was expected to have AuthorizeAttribute with 'Cookies, JWTBearer' authentication schemes, but in fact found 'Cookies'.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithEmptyAuthenticationScheme()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithAuthenticationSchemes(string.Empty)));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithNullAuthenticationScheme()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithAuthenticationSchemes(null)));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithCorrectPolicyAndAlsoWithCorrectAuthenticationScheme()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithPolicy("Admin")
                        .AndAlso()
                        .WithAuthenticationSchemes("Cookies")));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithWithEmptyPolicyAndAlsoIncorrectAuthenticationScheme()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<AuthorizationController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithPolicy(string.Empty)
                            .AndAlso()
                            .WithAuthenticationSchemes("Cookies, JWTBearer")));
            },
            "When testing AuthorizationController was expected to have AuthorizeAttribute with 'Cookies, JWTBearer' authentication schemes, but in fact found 'Cookies'.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithCorrectPolicy()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithPolicy("Admin")));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithIncorrectPolicy()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<AuthorizationController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithPolicy("MyPolicy")));
            },
            "When testing AuthorizationController was expected to have AuthorizeAttribute with 'MyPolicy' policy, but in fact found 'Admin'.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithoutDefinedPolicy()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<MvcController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithPolicy("Admin")));
            },
            "When testing MvcController was expected to have AuthorizeAttribute with 'Admin' policy, but in fact found ''.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithEmptyPolicy()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithPolicy(string.Empty)));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithNullPolicy()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithPolicy(null)));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithCorrectAuthenticationSchemeAndAlsoWithPolicy()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithAuthenticationSchemes("Cookies")
                        .AndAlso()
                        .WithPolicy("Admin")));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithCorrectAuthenticationSchemeAndAlsoWithIncorrectPolicy()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<AuthorizationController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithAuthenticationSchemes("Cookies")
                            .AndAlso()
                            .WithPolicy("MyPolicy")));
            },
            "When testing AuthorizationController was expected to have AuthorizeAttribute with 'MyPolicy' policy, but in fact found 'Admin'.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithCorrectRole()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithRole("Admin")));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithIncorrectRole()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<AuthorizationController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithRole("Moderator")));
            },
            "When testing AuthorizationController was expected to have AuthorizeAttribute with 'Moderator' roles, but in fact found 'Admin'.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithoutDefinedRole()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<ApiController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithRole("Admin")));
            },
            "When testing ApiController was expected to have AuthorizeAttribute with 'Admin' roles, but in fact found ''.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithEmptyRole()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithRole(string.Empty)));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithNullRole()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithRole(null)));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithCorrectAuthenticationSchemeAndAlsoWithRole()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithAuthenticationSchemes("Cookies")
                        .AndAlso()
                        .WithRole("Admin")));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithCorrectAuthenticationSchemeAndAlsoWithIncorrectRole()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<AuthorizationController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithAuthenticationSchemes("Cookies")
                            .AndAlso()
                            .WithRole("Moderator")));
            },
            "When testing AuthorizationController was expected to have AuthorizeAttribute with 'Moderator' roles, but in fact found 'Admin'.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithCorrectRoles()
        {
            MyController<MvcController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithRoles(new List<string>() { "Admin", "Moderator" })));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithIncorrectRoles()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<MvcController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithRoles(new List<string>() { "Admin", "Student" })));
            },
            "When testing MvcController was expected to have AuthorizeAttribute with 'Admin,Student' roles, but in fact found 'Admin,Moderator'.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithoutDefinedRoles()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<ApiController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithRoles(new List<string>() { "Admin" })));
            },
            "When testing ApiController was expected to have AuthorizeAttribute with 'Admin' roles, but in fact found ''.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithEmptyRoles()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithRoles(new List<string>())));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithNullRoles()
        {
            List<string> roles = null;

            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithRoles(roles)));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithCorrectAuthenticationSchemeAndAlsoWithRoles()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithAuthenticationSchemes("Cookies")
                        .AndAlso()
                        .WithRoles(new List<string>() { "Admin" })));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithCorrectAuthenticationSchemeAndAlsoWithIncorrectRoles()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<AuthorizationController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithAuthenticationSchemes("Cookies")
                            .AndAlso()
                            .WithRoles(new List<string>() { "Moderator" })));
            },
            "When testing AuthorizationController was expected to have AuthorizeAttribute with 'Moderator' roles, but in fact found 'Admin'.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithCorrectParamsRoles()
        {
            MyController<MvcController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithRoles("Admin", "Moderator")));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithCorrectSingleParamsRoles()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithRoles("Admin")));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithIncorrectParamsRoles()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<MvcController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithRoles("Admin", "Student")));
            },
            "When testing MvcController was expected to have AuthorizeAttribute with 'Admin,Student' roles, but in fact found 'Admin,Moderator'.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithoutDefinedParamsRoles()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<ApiController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithRoles("Admin")));
            },
            "When testing ApiController was expected to have AuthorizeAttribute with 'Admin' roles, but in fact found ''.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithEmptyParamsRoles()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithRoles()));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithNullParamsRoles()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithRoles(null)));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithMultipleNullParamsRoles()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<AuthorizationController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithRoles(null, null)));
            },
            "When testing AuthorizationController was expected to have AuthorizeAttribute with ',' roles, but in fact found 'Admin'.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithEmptyStringParamsRoles()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<AuthorizationController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithRoles(string.Empty)));
            },
            "When testing AuthorizationController was expected to have AuthorizeAttribute with '' roles, but in fact found 'Admin'.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithMultipleEmptyParamsRoles()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<AuthorizationController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithRoles(string.Empty, string.Empty)));
            },
            "When testing AuthorizationController was expected to have AuthorizeAttribute with ',' roles, but in fact found 'Admin'.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithCorrectAuthenticationSchemeAndAlsoWithParamsRoles()
        {
            MyController<AuthorizationController>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(authorization => authorization
                        .WithAuthenticationSchemes("Cookies")
                        .AndAlso()
                        .WithRoles("Admin")));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithCorrectAuthenticationSchemeAndAlsoWithIncorrectParamsRoles()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyController<AuthorizationController>
                    .ShouldHave()
                    .Attributes(attributes => attributes
                        .RestrictingForAuthorizedRequests(authorization => authorization
                            .WithAuthenticationSchemes("Cookies")
                            .AndAlso()
                            .WithRoles("Moderator")));
            },
            "When testing AuthorizationController was expected to have AuthorizeAttribute with 'Moderator' roles, but in fact found 'Admin'.");
        }

        [Fact]
        public void AddingFormatShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<MvcController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.AddingFormat());
                },
                "When testing AttributesController was expected to have FormatFilterAttribute, but in fact such was not found.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<AttributesController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.CachingResponse());
                },
                "When testing MvcController was expected to have ResponseCacheAttribute, but in fact such was not found.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectDuration()
        {
            MyController<AttributesController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.CachingResponse(30));
                },
                "When testing AttributesController was expected to have ResponseCacheAttribute with duration of 30 seconds, but in fact found 60.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectCacheProfileName()
        {
            MyController<AttributesController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.CachingResponse("Wrong Profile"));
                },
                "When testing AttributesController was expected to have ResponseCacheAttribute with 'Wrong Profile' cache profile name, but in fact found 'Test Profile Controller'.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectDurationWithBuilder()
        {
            MyController<AttributesController>
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
                .ShouldHave()
                .Attributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithVaryByQueryKeys(new List<string> { "FirstQueryController", "SecondQueryController" })));
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectCacheProfileNameWithBuilder()
        {
            MyController<AttributesController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.SettingRequestSizeLimitTo(1024));
                },
                "When testing AttributesController was expected to have RequestSizeLimitAttribute with request size limit of 1024 bytes, but in fact found 2048.");
        }

        [Fact]
        public void DisablingRequestSizeLimitShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<AttributesController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.DisablingRequestSizeLimit());
                },
                "When testing MvcController was expected to have DisableRequestSizeLimitAttribute, but in fact such was not found.");
        }

        [Fact]
        public void IndicatingControllerShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<AreaController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.IndicatingControllerExplicitly());
                },
                "When testing MvcController was expected to have ControllerAttribute, but in fact such was not found.");
        }

        [Fact]
        public void IndicatingApiControllerShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<ApiController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes.IndicatingApiController());
                },
                "When testing MvcController was expected to have ApiControllerAttribute, but in fact such was not found.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<AttributesController>
                .ShouldHave()
                .Attributes(attributes
                    => attributes
                        .AllowingAnonymousRequests()
                        .AndAlso()
                        .SpecifyingRoute("api/test"));
        }
    }
}
