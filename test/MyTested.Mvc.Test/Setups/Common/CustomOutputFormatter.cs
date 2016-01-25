namespace MyTested.Mvc.Tests.Setups.Common
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Mvc.Formatters;

    public class CustomOutputFormatter : IOutputFormatter
    {
        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return true;
        }

        public Task WriteAsync(OutputFormatterWriteContext context)
        {
            return Task.FromResult(true);
        }
    }
}
