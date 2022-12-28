namespace MyTested.AspNetCore.Mvc.Internal.Razor
{
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.Primitives;

    public class TestActionDescriptorChangeProvider : IActionDescriptorChangeProvider
    {
        public IChangeToken GetChangeToken() => ChangeTokenMock.Instance;
    }
}
