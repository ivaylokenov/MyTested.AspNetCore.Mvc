namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.DataTests
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class DistributedCacheBuilderTests
    {
        public DistributedCacheBuilderTests()
        {
            MyApplication
               .StartsFrom<DefaultStartup>()
               .WithServices(services =>
               {
                   services.AddDistributedMemoryCache();

                   services
                       .AddMvc()
                       .PartManager
                       .ApplicationParts
                       .Add(new AssemblyPart(this.GetType().Assembly));
               });
        }

        [Fact]
        public void InterfaceDescription()
        {
            MyController<MemoryCacheController>
                .Instance()
                .WithDistributedCache(distributedCache => distributedCache
                    .WithEntry("Normal", "NormalValid")
                    .WithEntry("Another", "AnotherValid"))
                    .WithEntries(new Dictionary<object, object>
                        {
                            ["first"] = "firstValue",
                            ["second"] = "secondValue",
                            ["third"] = "thirdValue"
                        })
                    .WithEntries(new Dictionary<object, object>
                    {
                        ["fourth"] = "fourthValue",
                        ["fifth"] = "fifthValue",
                        ["sixth"] = "sixthValue"
                    })
                    .AndAlso()
                    .WithEntries(new Dictionary<object, object>
                    {
                        ["seventh"] = "seventhValue",
                        ["eight"] = "eightValue",
                    })
                .AndAlso()
                .WithEntry("YetAnother", "YetAnotherValue")
                .Calling(c => c.FullMemoryCacheAction(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok();
        }
    }
}
