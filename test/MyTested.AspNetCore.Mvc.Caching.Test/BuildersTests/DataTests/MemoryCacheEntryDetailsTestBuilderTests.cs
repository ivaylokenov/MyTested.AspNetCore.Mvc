namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.DataTests
{
    using System;
    using Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class MemoryCacheEntryDetailsTestBuilderTests : IDisposable
    {
        public MemoryCacheEntryDetailsTestBuilderTests()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());
        }

        [Fact]
        public void WithValidShouldNotThrowExceptionWithCorrectValueAssertions()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(memoryCache => memoryCache
                    .ContainingEntry(entry => entry
                        .WithKey("test")
                        .WithValueOfType<string>()
                        .Passing(v => Assert.True(v.StartsWith("val")))))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithValidShouldNotThrowExceptionWithCorrectValuePredicate()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(memoryCache => memoryCache
                    .ContainingEntry(entry => entry
                        .WithKey("test")
                        .WithValueOfType<string>()
                        .Passing(v => v.StartsWith("val"))))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithValidShouldThrowExceptionWithIncorrectValuePredicate()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddMemoryCacheAction())
                        .ShouldHave()
                        .MemoryCache(memoryCache => memoryCache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .AndAlso()
                                .WithValueOfType<string>()
                                .Passing(v => v.StartsWith("inv"))))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with 'test' key and value passing the given predicate, but it failed.");
        }

        public void Dispose()
        {
            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
