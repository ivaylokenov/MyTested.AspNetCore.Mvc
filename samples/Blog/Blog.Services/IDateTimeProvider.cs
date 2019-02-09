namespace Blog.Services
{
    using System;

    public interface IDateTimeProvider
    {
        DateTime Now();
    }
}
