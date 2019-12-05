namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System.Collections.Generic;

    public class ExecutionTestContext
    {
        public object Controller { get; internal set; }

        public IDictionary<string, object> ActionArguments { get; internal set; }
    }
}
