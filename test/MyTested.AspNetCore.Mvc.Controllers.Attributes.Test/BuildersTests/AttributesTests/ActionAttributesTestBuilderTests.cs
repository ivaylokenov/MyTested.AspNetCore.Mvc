namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AttributesTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups.Pipelines;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Setups.ActionFilters;
    using Xunit;

    using HttpMethod = System.Net.Http.HttpMethod;

    public class ActionAttributesTestBuilderTests
    {
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
                        .OfType(typeof(RequestModel))
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
                        .OfType(typeof(RequestModel))
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
        public void SpecifyingMiddlewareShouldNotThrowExceptionWithCorrectAttribute()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SpecifyingMiddleware(typeof(MyPipeline)));
        }

        [Fact]
        public void SpecifyingMiddlewareShouldThrowExceptionWithMissingAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SpecifyingMiddleware(typeof(MyPipeline)));
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have MiddlewareFilterAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingMiddlewareShouldThrowExceptionWithCorrectAttributeAndWrongConfigurationType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SpecifyingMiddleware(typeof(MyOtherPipeline)));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have MiddlewareFilterAttribute with 'MyOtherPipeline' type, but in fact found 'MyPipeline'.");
        }

        [Fact]
        public void SpecifyingMiddlewareShouldNotThrowExceptionWithCorrectAttributeConfigurationType()
        {
            MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SpecifyingMiddleware(middleware => middleware
                                .OfType(typeof(MyPipeline))));
        }

        [Fact]
        public void SpecifyingMiddlewareShouldThrowExceptionWithWrongConfigurationType()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SpecifyingMiddleware(middleware => middleware
                                .OfType(typeof(MyOtherPipeline))));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have MiddlewareFilterAttribute with 'MyOtherPipeline' type, but in fact found 'MyPipeline'.");
        }

        [Fact]
        public void SpecifyingMiddlewareShouldThrowExceptionWithCorrectAttributeAndWrongOrder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SpecifyingMiddleware(middleware => middleware.WithOrder(1)));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have MiddlewareFilterAttribute with order of 1, but in fact found 2.");
        }

        [Fact]
        public void SpecifyingMiddlewareShouldNotThrowExceptionWithCorrectAttributeTypeAndUsingBuilderForOrder()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SpecifyingMiddleware(middleware => middleware.WithOrder(2)));
        }

        [Fact]
        public void SpecifyingMiddlewareShouldNotThrowExceptionWithCorrectAttributeConfigTypeAndUsingBuilderForOrder()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SpecifyingMiddleware(middleware => middleware
                        .OfType(typeof(MyPipeline))
                        .AndAlso()
                        .WithOrder(2)));
        }

        [Fact]
        public void SpecifyingMiddlewareShouldThrowExceptionWithCorrectAttributeConfigTypeAndUsingBuilderForOrderWithWrongOrder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SpecifyingMiddleware(middleware => middleware
                                .OfType(typeof(MyPipeline))
                                .AndAlso()
                                .WithOrder(1)));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have MiddlewareFilterAttribute with order of 1, but in fact found 2.");
        }

        [Fact]
        public void WithServiceFilterShouldNotThrowExceptionWithCorrectAttribute()
        {
            MyApplication.StartsFrom<TestStartup>();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .WithServiceFilter(typeof(MyActionFilter)));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithServiceFilterShouldThrowExceptionWithMissingAttribute()
        {
            MyApplication.StartsFrom<TestStartup>();

            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .WithServiceFilter(typeof(MyActionFilter)));
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have ServiceFilterAttribute, but in fact such was not found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithServiceFilterShouldThrowExceptionWithCorrectAttributeAndWrongConfigurationType()
        {
            MyApplication.StartsFrom<TestStartup>();

            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .WithServiceFilter(typeof(MyOtherActionFilter)));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ServiceFilterAttribute with 'MyOtherActionFilter' type, but in fact found 'MyActionFilter'.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithServiceFilterShouldNotThrowExceptionWithCorrectServiceFilterType()
        {
            MyApplication.StartsFrom<TestStartup>();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .WithServiceFilter(filter => filter
                        .OfType(typeof(MyActionFilter))));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithServiceFilterShouldThrowExceptionWithWrongServiceFilterType()
        {
            MyApplication.StartsFrom<TestStartup>();

            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .WithServiceFilter(filter => filter
                                .OfType(typeof(MyOtherActionFilter))));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ServiceFilterAttribute with 'MyOtherActionFilter' type, but in fact found 'MyActionFilter'.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithServiceFilterShouldThrowExceptionWithCorrectAttributeAndWrongOrder()
        {
            MyApplication.StartsFrom<TestStartup>();

            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .WithServiceFilter(filter => filter.WithOrder(1)));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ServiceFilterAttribute with order of 1, but in fact found 2.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithServiceFilterShouldNotThrowExceptionWithCorrectAttributeTypeAndUsingBuilderForOrder()
        {
            MyApplication.StartsFrom<TestStartup>();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .WithServiceFilter(filter => filter.WithOrder(2)));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithServiceFilterShouldNotThrowExceptionWithCorrectAttributeServiceTypeAndUsingBuilderForOrder()
        {
            MyApplication.StartsFrom<TestStartup>();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .WithServiceFilter(filter => filter
                        .OfType(typeof(MyActionFilter))
                        .AndAlso()
                        .WithOrder(2)));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithServiceFilterShouldThrowExceptionWithCorrectAttributeServiceTypeAndUsingBuilderForOrderWithWrongOrder()
        {
            MyApplication.StartsFrom<TestStartup>();

            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .WithServiceFilter(filter => filter
                                .OfType(typeof(MyActionFilter))
                                .AndAlso()
                                .WithOrder(1)));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ServiceFilterAttribute with order of 1, but in fact found 2.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void RequiringHttpsShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<AttributesController>
                .Instance()
                .Calling(c => c.WithAttributesAndParameters(With.Any<int>()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RequiringHttps());
        }

        [Fact]
        public void RequiringHttpsShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.RequiringHttps());
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have RequireHttpsAttribute, but in fact such was not found.");
        }

        [Fact]
        public void RequiringHttpsShouldNotThrowExceptionWithTheAttributeAndCorrectValue()
        {
            MyController<AttributesController>
                .Instance()
                .Calling(c => c.WithAttributesAndParameters(With.Any<int>()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RequiringHttps(true));
        }

        [Fact]
        public void RequiringHttpsShouldThrowExceptionWithActionWithoutTheAttributeAndIncorrectPermanentValue()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .Calling(c => c.WithAttributesAndParameters(With.Any<int>()))
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.RequiringHttps(false));
                },
                "When calling WithAttributesAndParameters action in AttributesController expected action to have RequireHttpsAttribute with temporary redirect, but in fact it was a permanent one.");
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
        public void CachingResponseShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.CachingResponse());
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithControllerWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AntiForgeryToken())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.CachingResponse());
                },
                "When calling AntiForgeryToken action in MvcController expected action to have ResponseCacheAttribute, but in fact such was not found.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectDuration()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.CachingResponse(30));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectDuration()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.CachingResponse(60));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ResponseCacheAttribute with duration of 60 seconds, but in fact found 30.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectCacheProfileName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.CachingResponse("Test Profile"));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectCacheProfileName()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.CachingResponse("Wrong Profile"));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ResponseCacheAttribute with 'Wrong Profile' cache profile name, but in fact found 'Test Profile'.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectDurationWithBuilder()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithDuration(30)));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectDurationWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithDuration(60)));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ResponseCacheAttribute with duration of 60 seconds, but in fact found 30.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectLocationWithBuilder()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithLocation(ResponseCacheLocation.Client)));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectLocationWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithLocation(ResponseCacheLocation.Any)));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ResponseCacheAttribute with 'Any' location, but in fact found 'Client'.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectNoStoreWithBuilder()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithNoStore(true)));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectNoStoreWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithNoStore(false)));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ResponseCacheAttribute with no store value of 'False', but in fact found 'True'.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectVaryByHeaderWithBuilder()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithVaryByHeader("Test Header")));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectVaryByHeaderWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithVaryByHeader("Wrong Header")));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ResponseCacheAttribute with vary by header value of 'Wrong Header', but in fact found 'Test Header'.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectVaryByQueryKeyWithBuilder()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithVaryByQueryKey("FirstQuery")));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectVaryByQueryKeyWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithVaryByQueryKey("Wrong Query Key")));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ResponseCacheAttribute with vary by query string key value of 'Wrong Query Key', but in fact such was not found.");
        }


        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithCorrectAttributeAndCorrectVaryQueryKeysWithBuilder()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithVaryByQueryKeys("FirstQuery", "SecondQuery")));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithCorrectAttributeAndIncorrectVaryQueryKeysCountWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithVaryByQueryKeys("FirstQuery", "SecondQuery", "ThirdQuery")));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ResponseCacheAttribute with 3 vary by query string key values, but in fact found 2.");
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithCorrectAttributeAndIncorrectVaryQueryKeysSingleCountWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithVaryByQueryKeys("FirstQuery")));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ResponseCacheAttribute with 1 vary by query string key value, but in fact found 2.");
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithCorrectAttributeAndOneIncorrectVaryQueryKeyWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                        .CachingResponse(responseCache => responseCache
                            .WithVaryByQueryKeys("FirstQuery", "WrongQuery")));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ResponseCacheAttribute with vary by query string key value of 'WrongQuery', but in fact such was not found.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithCorrectAttributeAndVaryQueryKeyAsList()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithVaryByQueryKeys(new List<string> { "FirstQuery", "SecondQuery" })));
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectCacheProfileNameWithBuilder()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithCacheProfileName("Test Profile")));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectCacheProfileNameWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithCacheProfileName("Wrong Profile")));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ResponseCacheAttribute with 'Wrong Profile' cache profile name, but in fact found 'Test Profile'.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectOrderWithBuilder()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithOrder(2)));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectOrderWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithOrder(1)));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ResponseCacheAttribute with order of 1, but in fact found 2.");
        }

        [Fact]
        public void CachingResponseShouldNotThrowExceptionWithTheAttributeAndCorrectValuesWithBuilder()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .CachingResponse(responseCache => responseCache
                        .WithOrder(2)
                        .AndAlso()
                        .WithCacheProfileName("Test Profile")
                        .AndAlso()
                        .WithVaryByQueryKeys("FirstQuery", "SecondQuery")
                        .AndAlso()
                        .WithVaryByHeader("Test Header")
                        .AndAlso()
                        .WithNoStore(true)
                        .AndAlso()
                        .WithLocation(ResponseCacheLocation.Client)
                        .AndAlso()
                        .WithDuration(30)));
        }

        [Fact]
        public void CachingResponseShouldThrowExceptionWithTheAttributeAndIncorrectValuesWithBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .CachingResponse(responseCache => responseCache
                                .WithCacheProfileName("Test Profile")
                                .AndAlso()
                                .WithOrder(3)
                                .AndAlso()
                                .WithVaryByQueryKeys("FirstQuery", "SecondQuery")
                                .AndAlso()
                                .WithVaryByHeader("Test Header")
                                .AndAlso()
                                .WithNoStore(true)
                                .AndAlso()
                                .WithLocation(ResponseCacheLocation.Client)
                                .AndAlso()
                                .WithDuration(30)));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have ResponseCacheAttribute with order of 3, but in fact found 2.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithBufferBody(true)));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have RequestFormLimitsAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectBufferBody()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RequestFormLimits())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithBufferBody(false)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectBufferBody()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RequestFormLimits())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithBufferBody(true)));
                },
                "When calling RequestFormLimits action in MvcController expected action to have RequestFormLimitsAttribute with buffer body value of 'True', but in fact found 'False'.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectMemoryBufferThreshold()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RequestFormLimits())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithMemoryBufferThreshold(30)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectMemoryBufferThreshold()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RequestFormLimits())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithMemoryBufferThreshold(0)));
                },
                "When calling RequestFormLimits action in MvcController expected action to have RequestFormLimitsAttribute with memory buffer threshold value of 0, but in fact found 30.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectBufferBodyLengthLimit()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RequestFormLimits())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithBufferBodyLengthLimit(10)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectBufferBodyLengthLimit()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RequestFormLimits())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithBufferBodyLengthLimit(0)));
                },
                "When calling RequestFormLimits action in MvcController expected action to have RequestFormLimitsAttribute with buffer body length limit of 0, but in fact found 10.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectValueCountLimit()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RequestFormLimits())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithValueCountLimit(90)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectValueCountLimit()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RequestFormLimits())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithValueCountLimit(0)));
                },
                "When calling RequestFormLimits action in MvcController expected action to have RequestFormLimitsAttribute with value count limit of 0, but in fact found 90.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectKeyLengthLimit()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RequestFormLimits())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithKeyLengthLimit(20)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectKeyLengthLimit()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RequestFormLimits())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithKeyLengthLimit(0)));
                },
                "When calling RequestFormLimits action in MvcController expected action to have RequestFormLimitsAttribute with key length limit of 0, but in fact found 20.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectValueLengthLimit()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RequestFormLimits())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithValueLengthLimit(100)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectValueLengthLimit()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RequestFormLimits())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithValueLengthLimit(0)));
                },
                "When calling RequestFormLimits action in MvcController expected action to have RequestFormLimitsAttribute with value length limit of 0, but in fact found 100.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectMultipartBoundaryLengthLimit()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RequestFormLimits())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithMultipartBoundaryLengthLimit(50)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectMultipartBoundaryLengthLimit()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RequestFormLimits())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithMultipartBoundaryLengthLimit(0)));
                },
                "When calling RequestFormLimits action in MvcController expected action to have RequestFormLimitsAttribute with multipart boundary length limit of 0, but in fact found 50.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectMultipartHeadersCountLimit()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RequestFormLimits())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithMultipartHeadersCountLimit(60)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectMultipartHeadersCountLimit()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RequestFormLimits())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithMultipartHeadersCountLimit(0)));
                },
                "When calling RequestFormLimits action in MvcController expected action to have RequestFormLimitsAttribute with multipart headers count limit of 0, but in fact found 60.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectMultipartHeadersLengthLimit()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RequestFormLimits())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithMultipartHeadersLengthLimit(70)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectMultipartHeadersLengthLimit()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RequestFormLimits())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithMultipartHeadersLengthLimit(0)));
                },
                "When calling RequestFormLimits action in MvcController expected action to have RequestFormLimitsAttribute with multipart headers length limit of 0, but in fact found 70.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectMultipartBodyLengthLimit()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RequestFormLimits())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithMultipartBodyLengthLimit(40)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectMultipartBodyLengthLimit()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RequestFormLimits())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithMultipartBodyLengthLimit(0)));
                },
                "When calling RequestFormLimits action in MvcController expected action to have RequestFormLimitsAttribute with multipart body length limit of 0, but in fact found 40.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectOrder()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RequestFormLimits())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithOrder(80)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectOrder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RequestFormLimits())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithOrder(100)));
                },
                "When calling RequestFormLimits action in MvcController expected action to have RequestFormLimitsAttribute with order of 100, but in fact found 80.");
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldNotThrowExceptionWithTheAttributeAndCorrectBuilder()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RequestFormLimits())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                        .WithBufferBody(false)
                        .AndAlso()
                        .WithBufferBodyLengthLimit(10)
                        .AndAlso()
                        .WithKeyLengthLimit(20)
                        .AndAlso()
                        .WithMemoryBufferThreshold(30)
                        .AndAlso()
                        .WithMultipartBodyLengthLimit(40)
                        .AndAlso()
                        .WithMultipartBoundaryLengthLimit(50)
                        .AndAlso()
                        .WithMultipartHeadersCountLimit(60)
                        .AndAlso()
                        .WithMultipartHeadersLengthLimit(70)
                        .AndAlso()
                        .WithOrder(80)
                        .AndAlso()
                        .WithValueCountLimit(90)
                        .AndAlso()
                        .WithValueLengthLimit(100)
                        .AndAlso()
                        .WithOrder(80)));
        }

        [Fact]
        public void SettingRequestFormLimitsToShouldThrowExceptionWithTheAttributeAndIncorrectBuilder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RequestFormLimits())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .SettingRequestFormLimitsTo(requestFormLimits => requestFormLimits
                                .WithBufferBody(false)
                                .AndAlso()
                                .WithBufferBodyLengthLimit(10)
                                .AndAlso()
                                .WithKeyLengthLimit(20)
                                .AndAlso()
                                .WithMemoryBufferThreshold(30)
                                .AndAlso()
                                .WithMultipartBodyLengthLimit(40)
                                .AndAlso()
                                .WithMultipartBoundaryLengthLimit(50)
                                .AndAlso()
                                .WithMultipartHeadersCountLimit(60)
                                .AndAlso()
                                .WithMultipartHeadersLengthLimit(70)
                                .AndAlso()
                                .WithOrder(80)
                                .AndAlso()
                                .WithValueCountLimit(90)
                                .AndAlso()
                                .WithValueLengthLimit(100)
                                .AndAlso()
                                .WithOrder(100)));
                },
                "When calling RequestFormLimits action in MvcController expected action to have RequestFormLimitsAttribute with order of 100, but in fact found 80.");
        }

        [Fact]
        public void SettingRequestSizeLimitToShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RequestSizeLimit())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.SettingRequestSizeLimitTo(1024));
        }

        [Fact]
        public void SettingRequestSizeLimitToShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AntiForgeryToken())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.SettingRequestSizeLimitTo(1024));
                },
                "When calling AntiForgeryToken action in MvcController expected action to have RequestSizeLimitAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SettingRequestSizeLimitToShouldThrowExceptionWithActionWithTheAttributeAndIncorrectBytes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RequestSizeLimit())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.SettingRequestSizeLimitTo(2048));
                },
                "When calling RequestSizeLimit action in MvcController expected action to have RequestSizeLimitAttribute with request size limit of 2048 bytes, but in fact found 1024.");
        }

        [Fact]
        public void DisablingRequestSizeLimitShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DisabledRequestSizeLimit())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.DisablingRequestSizeLimit());
        }

        [Fact]
        public void DisablingRequestSizeLimitShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AntiForgeryToken())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.DisablingRequestSizeLimit());
                },
                "When calling AntiForgeryToken action in MvcController expected action to have DisableRequestSizeLimitAttribute, but in fact such was not found.");
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
