namespace MyTested.AspNetCore.Mvc.Test.Setups.ActionResults
{
    using Microsoft.AspNetCore.Mvc;

    public class CustomActionResult : OkObjectResult
    {
        public CustomActionResult(string value) 
            : base(value)
        {
        }

        public string CustomProperty { get; set; }
    }
}
