﻿namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class ExecutionTestContext
    {
        public object Controller { get; internal set; }

        public ControllerContext ControllerContext { get; internal set; }

        public IDictionary<string, object> ActionArguments { get; internal set; }
    }
}
