namespace MyTested.AspNetCore.Mvc.Utilities
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class AsyncHelper
    {
        private static readonly TaskFactory taskFactory = new TaskFactory(
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskContinuationOptions.None,
            TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func) 
            => taskFactory
                .StartNew(func)
                .Unwrap()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

        public static void RunSync(Func<Task> func) 
            => taskFactory
                .StartNew(func)
                .Unwrap()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
    }
}
