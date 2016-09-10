namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    public class StringComponent : ViewComponent
    {
        public string Invoke() => "TestString";
    }
}
