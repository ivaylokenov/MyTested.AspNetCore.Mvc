namespace MyTested.AspNetCore.Mvc.Test.Internal
{
    using Licensing;
    using Setups.Controllers;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class FreeLicenseTests
    {
        [Fact]
        public async Task UsingLitePackageWithOtherPluginsShouldRequireLicense()
            => await Task.Run(async () =>
            {
                var tasks = new List<Task>();

                for (int i = 0; i < 200; i++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        MyController<MvcController>
                            .Instance()
                            .Calling(c => c.OkResultAction())
                            .ShouldReturn()
                            .Ok();
                    }));
                }

                await Assert.ThrowsAsync<InvalidLicenseException>(async () => await Task.WhenAll(tasks));
            });
    }
}
