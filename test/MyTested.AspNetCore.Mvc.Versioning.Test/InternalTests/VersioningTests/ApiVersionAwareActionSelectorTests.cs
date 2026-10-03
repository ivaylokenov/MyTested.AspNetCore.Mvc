namespace MyTested.AspNetCore.Mvc.Test.InternalTests.VersioningTests
{
    using System;
    using System.Collections.Generic;
    using Asp.Versioning;
    using Internal.Versioning;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.ActionConstraints;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Options;
    using Setups;
    using Setups.Common;
    using Xunit;

    public class ApiVersionAwareActionSelectorTests
    {
        [Fact]
        public void ConstructorShouldThrowExceptionWithNullActionSelector()
        {
            Test.AssertException<NullReferenceException>(
                () =>
                {
                    _ = new ApiVersionAwareActionSelector(null, Options.Create(new ApiVersioningOptions()));
                },
                "actionSelector cannot be null.");
        }

        [Fact]
        public void ConstructorShouldThrowExceptionWithNullOptionsAccessor()
        {
            Test.AssertException<NullReferenceException>(
                () =>
                {
                    _ = new ApiVersionAwareActionSelector(new CustomActionSelector(), null);
                },
                "optionsAccessor cannot be null.");
        }

        [Fact]
        public void SelectCandidatesShouldReturnTheCandidatesOfTheDecoratedActionSelector()
        {
            var candidates = new[] { new ActionDescriptor() };

            var actionSelector = new ApiVersionAwareActionSelector(
                new CustomActionSelector { Candidates = candidates },
                Options.Create(new ApiVersioningOptions()));

            var result = actionSelector.SelectCandidates(new RouteContext(new DefaultHttpContext()));

            Assert.Same(candidates, result);
        }

        [Fact]
        public void SelectBestCandidateShouldUseTheDecoratedActionSelectorWithUnversionedCandidates()
        {
            var candidate = new ActionDescriptor();

            var actionSelector = new ApiVersionAwareActionSelector(
                new CustomActionSelector(),
                Options.Create(new ApiVersioningOptions()));

            var result = actionSelector.SelectBestCandidate(
                new RouteContext(new DefaultHttpContext()),
                new[] { candidate });

            Assert.Same(candidate, result);
        }

        [Fact]
        public void SelectBestCandidateShouldSelectImplicitlyMappedActionWhenExplicitlyMappedActionsHaveOtherHttpMethods()
        {
            var explicitGet = CreateVersionedAction(HttpMethods.Get, new ApiVersion(2, 0));
            var implicitPut = CreateVersionedAction(HttpMethods.Put);

            var actionSelector = new ApiVersionAwareActionSelector(
                new HttpMethodActionSelector(),
                Options.Create(new ApiVersioningOptions()));

            var result = actionSelector.SelectBestCandidate(
                CreateRouteContext(HttpMethods.Put, new ApiVersion(2, 0)),
                new[] { explicitGet, implicitPut });

            Assert.Same(implicitPut, result);
        }

        [Fact]
        public void SelectBestCandidateShouldPreferExplicitlyMappedActionWithTheSameHttpMethod()
        {
            var implicitGet = CreateVersionedAction(HttpMethods.Get);
            var explicitGet = CreateVersionedAction(HttpMethods.Get, new ApiVersion(2, 0));
            var implicitPut = CreateVersionedAction(HttpMethods.Put);

            var actionSelector = new ApiVersionAwareActionSelector(
                new HttpMethodActionSelector(),
                Options.Create(new ApiVersioningOptions()));

            var result = actionSelector.SelectBestCandidate(
                CreateRouteContext(HttpMethods.Get, new ApiVersion(2, 0)),
                new[] { implicitGet, implicitPut, explicitGet });

            Assert.Same(explicitGet, result);
        }

        private static RouteContext CreateRouteContext(string httpMethod, ApiVersion requestedApiVersion)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = httpMethod;
            httpContext.ApiVersioningFeature.RequestedApiVersion = requestedApiVersion;

            return new RouteContext(httpContext);
        }

        private static ActionDescriptor CreateVersionedAction(string httpMethod, params ApiVersion[] mappedApiVersions)
        {
            var declaredApiVersions = new[] { new ApiVersion(1, 0), new ApiVersion(2, 0) };
            var noApiVersions = Array.Empty<ApiVersion>();

            var apiModel = new ApiVersionModel(
                declaredApiVersions,
                declaredApiVersions,
                noApiVersions,
                noApiVersions,
                noApiVersions);

            var endpointModel = mappedApiVersions.Length == 0
                ? ApiVersionModel.Empty
                : new ApiVersionModel(
                    mappedApiVersions,
                    declaredApiVersions,
                    noApiVersions,
                    noApiVersions,
                    noApiVersions);

            return new ActionDescriptor
            {
                ActionConstraints = new List<IActionConstraintMetadata>
                {
                    new HttpMethodActionConstraint(new[] { httpMethod })
                },
                EndpointMetadata = new List<object>
                {
                    new ApiVersionMetadata(apiModel, endpointModel)
                }
            };
        }
    }
}
