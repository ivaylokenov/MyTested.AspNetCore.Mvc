namespace Blog.Test.Mocks
{
    using System;
    using Moq;
    using Services;

    public class DateTimeProviderMock
    {
        public static IDateTimeProvider Create
        {
            get
            {
                var moq = new Mock<IDateTimeProvider>();

                moq.Setup(m => m.Now()).Returns(new DateTime(1, 1, 1));

                return moq.Object;
            }
        }
    }
}
