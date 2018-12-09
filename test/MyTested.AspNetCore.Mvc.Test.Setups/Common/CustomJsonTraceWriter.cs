namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using System;
    using System.Diagnostics;
    using Newtonsoft.Json.Serialization;

    public class CustomJsonTraceWriter : ITraceWriter
    {
        public TraceLevel LevelFilter => TraceLevel.Error;

        public void Trace(TraceLevel level, string message, Exception ex)
        {
        }
    }
}
