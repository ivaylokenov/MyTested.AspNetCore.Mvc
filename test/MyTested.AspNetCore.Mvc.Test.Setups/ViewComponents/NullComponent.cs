namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    public class NullComponent : ViewComponent
    {
        public string Invoke() => null;
    }
}
