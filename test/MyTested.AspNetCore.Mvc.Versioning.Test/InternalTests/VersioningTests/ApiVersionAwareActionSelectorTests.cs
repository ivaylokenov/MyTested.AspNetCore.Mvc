namespace MyTested.AspNetCore.Mvc.Test.InternalTests.VersioningTests
{
    using System;
    using Asp.Versioning;
    using Internal.Versioning;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Abstractions;
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
    }
}
