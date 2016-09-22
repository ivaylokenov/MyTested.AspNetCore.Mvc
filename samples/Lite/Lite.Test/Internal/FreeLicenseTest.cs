namespace Lite.Test.Internal
{
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Web.Controllers;
    using Xunit;

    public class FreeLicenseTest
    {
        [Fact]
        public async Task UsingOnlyLitePackageShouldNotRequireLicense()
            => await Task.Run(async () =>
            {
                var tasks = new List<Task>();

                for (int i = 0; i < 200; i++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        MyController<ValuesController>
                            .Instance()
                            .Calling(c => c.Get())
                            .ShouldReturn()
                            .Ok();
                    }));
                }

                await Task.WhenAll(tasks);
            });
    }
}
