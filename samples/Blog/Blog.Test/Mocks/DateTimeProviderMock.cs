namespace Blog.Test.Mocks
{
    using System;
    using Services;

    public class DateTimeProviderMock : IDateTimeProvider
    {
        public DateTime Now() => new DateTime(1, 1, 1);
    }
}
