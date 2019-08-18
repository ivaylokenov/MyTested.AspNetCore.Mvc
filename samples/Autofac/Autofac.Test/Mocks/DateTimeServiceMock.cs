namespace Autofac.Test.Mocks
{
    using System;
    using Web.Services;

    public class DateTimeServiceMock : IDateTimeService
    {
        public DateTime GetTime() => new DateTime(2020, 1, 1, 1, 1, 1, DateTimeKind.Utc);
    }
}
