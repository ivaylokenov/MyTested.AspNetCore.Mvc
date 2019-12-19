﻿namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.RoutingTests
{
    using Exceptions;
    using Setups;
    using Setups.Routing;
    using Xunit;

    public class RouteTestBuilderTests
    {
        [Fact]
        public void ShouldMapShouldExecuteAuthorizationFiltersAndShouldValidateJustRoutes()
        {
            MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Normal/AuthorizedAction")
                    .WithUser())
                .To<NormalController>(c => c.AuthorizedAction());
        }

        [Fact]
        public void ShouldMapShouldThrowExceptionIfAuthorizationFiltersAreNotSet()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyPipeline
                        .Configuration()
                        .ShouldMap("/Normal/AuthorizedAction")
                        .To<NormalController>(c => c.AuthorizedAction());
                },
                "Expected route '/Normal/AuthorizedAction' to match AuthorizedAction action in NormalController but exception was thrown when trying to invoke the pipeline: 'No authenticationScheme was specified, and there was no DefaultChallengeScheme found. The default schemes can be set using either AddAuthentication(string defaultScheme) or AddAuthentication(Action<AuthenticationOptions> configureOptions).'.");
        }

        [Fact]
        public void ShouldMapShouldExecuteActionFiltersAndShouldValidateRoutes()
        {
            MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Normal/FiltersAction")
                    .WithAntiForgeryToken())
                .To<NormalController>(c => c.FiltersAction());
        }

        [Fact]
        public void ShouldMapShouldExecuteActionFiltersAndShouldValidateRoutesAndModelBinding()
        {
            MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Normal/FiltersActionWithModelBinding/1")
                    .WithMethod(HttpMethod.Post)
                    .WithAntiForgeryToken()
                    .WithJsonBody(new
                    {
                        Integer = 1,
                        String = "Text"
                    }))
                .To<NormalController>(c => c.FiltersActionWithModelBinding(
                    1,
                    new RequestModel
                    {
                        Integer = 1,
                        String = "Text"
                    }));
        }

        [Fact]
        public void ShouldMapShouldThrowExceptionIfFiltersAreNotSet()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyPipeline
                        .Configuration()
                        .ShouldMap("/Normal/FiltersAction")
                        .To<NormalController>(c => c.FiltersAction());
                },
                "Expected route '/Normal/FiltersAction' to match FiltersAction action in NormalController but action could not be invoked because of the declared filters - ValidateAntiForgeryTokenAttribute (Action), UnsupportedContentTypeFilter (Global), SaveTempDataAttribute (Global), ControllerActionFilter (Controller). Either a filter is setting the response result before the action itself, or you must set the request properties so that they will pass through the pipeline.");
        }

        [Fact]
        public void ShouldMapShouldExecuteCustomActionFiltersAndShouldValidateRoutes()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyPipeline
                        .Configuration()
                        .ShouldMap("/Normal/CustomFiltersAction?throw=true")
                        .To<NormalController>(c => c.CustomFiltersAction());
                },
                "Expected route '/Normal/CustomFiltersAction' to match CustomFiltersAction action in NormalController but exception was thrown when trying to invoke the pipeline: 'Exception of type 'System.Exception' was thrown.'.");
        }
    }
}
