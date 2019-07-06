namespace MyTested.AspNetCore.Mvc.Internal
{
    using System.Threading;

    public class TestLocal<T> where T : class
    {
        private readonly ThreadLocal<T> current = new ThreadLocal<T>();

        public T Value
        {
            get => this.current.Value;
            set => this.current.Value = value;
        }
    }
}
