namespace MyTested.Mvc.Test.Setups.Common
{
    using System;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class CustomJsonTraceWriter : ITraceWriter
    {
#if NET451
        public TraceLevel LevelFilter
        {
            get
            {
                return TraceLevel.Error;
            }
        }

        public void Trace(TraceLevel level, string message, Exception ex)
        {
        }
#else
        public TraceLevel LevelFilter
        {
            get
            {
                return TraceLevel.Error;
            }
        }

        public void Trace(TraceLevel level, string message, Exception ex)
        {
        }
#endif
    }
}
