namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AttributesTests
{
    using System;
    using System.Collections.Generic;
    using Exceptions;
    using Microsoft.AspNetCore.Authorization;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    using Microsoft.AspNetCore.Mvc;
    using Setups.Models;

    public class ControllerAttributesTestBuilderTests
    {
        [Fact]
        public void ContainingAttributeOfTypeShouldNotThrowExceptionWithControllerWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.ContainingAttributeOfType<AuthorizeAttribute>());
        }

        [Fact]
        public void ContainingAttributeOfTypeShouldThrowExceptionWithControllerWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.ContainingAttributeOfType<AllowAnonymousAttribute>());
                },
                "When testing MvcController was expected to have AllowAnonymousAttribute, but in fact such was not found.");
        }

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
        public void PassingForShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyController<MvcController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .PassingFor<AuthorizeAttribute>(authorize => Assert.Equal("Admin,Moderator", authorize.Roles)));
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectAssertions()
        {
            Assert.ThrowsAny<Exception>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .PassingFor<AuthorizeAttribute>(authorize => Assert.Equal("Admin", authorize.Roles)));
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
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .PassingFor<ActionNameAttribute>(authorize => Assert.Equal("Admin", authorize.Name)));
                },
                "When testing MvcController was expected to have ActionNameAttribute, but in fact such was not found.");
        }
        
        [Fact]
        public void PassingForShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyController<MvcController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .PassingFor<AuthorizeAttribute>(authorize => authorize.Roles == "Admin,Moderator"));
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectPredicate()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .PassingFor<AuthorizeAttribute>(authorize => authorize.Roles == "Admin"));
                },
                "When testing MvcController was expected to have AuthorizeAttribute passing the given predicate, but it failed.");
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectAttributeWithPredicate()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .PassingFor<AuthorizeAttribute>(authorize => authorize.Roles == "Admin,Moderator")
                            .AndAlso()
                            .PassingFor<ActionNameAttribute>(authorize => authorize.Name == "Admin"));
                },
                "When testing MvcController was expected to have ActionNameAttribute, but in fact such was not found.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes
                    => attributes
                        .AllowingAnonymousRequests()
                        .AndAlso()
                        .ChangingRouteTo("api/test"));
        }
    }
}
