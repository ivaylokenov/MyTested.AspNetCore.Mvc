namespace MyTested.Mvc.Test.Setups.Common
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Formatters;

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
