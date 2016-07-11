namespace MyTested.AspNetCore.Mvc.Internal
{
#if NET451
    using System.Runtime.Remoting;
    using System.Runtime.Remoting.Messaging;
#else
    using System.Threading;
#endif

    public class TestLocal<T> where T : class
    {
#if NET451
        private static readonly string Key = $"__{typeof(T).FullName}_Current__";

        public T Value
        {
            get
            {
                var handle = CallContext.GetData(Key) as ObjectHandle;
                return (T)handle?.Unwrap();
            }
            set
            {
                CallContext.SetData(Key, new ObjectHandle(value));
            }
        }
#else
        private readonly ThreadLocal<T> current = new ThreadLocal<T>();

        public T Value
        {
            get { return current.Value; }
            set { current.Value = value; }
        }
#endif
    }
}
