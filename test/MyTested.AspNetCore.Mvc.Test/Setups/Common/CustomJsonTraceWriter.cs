namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using System;
    using Newtonsoft.Json.Serialization;

#if NET451
    using System.Diagnostics;
#else
    using TraceLevel = Newtonsoft.Json.TraceLevel;
#endif

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
