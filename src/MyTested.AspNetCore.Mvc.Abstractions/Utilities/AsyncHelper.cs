namespace MyTested.AspNetCore.Mvc.Utilities
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class AsyncHelper
    {
        private static readonly TaskFactory TaskFactory = new TaskFactory(
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskContinuationOptions.None,
            TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func) 
            => TaskFactory
                .StartNew(func)
                .Unwrap()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

        public static void RunSync(Func<Task> func) 
            => TaskFactory
                .StartNew(func)
                .Unwrap()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
    }
}
