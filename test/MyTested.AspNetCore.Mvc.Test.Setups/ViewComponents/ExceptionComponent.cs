namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    public class ExceptionComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            throw new IndexOutOfRangeException("Test exception message");
        }
    }
}
