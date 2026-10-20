namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/mixed-method-versioning")]
    public class MixedMethodVersioningController : ControllerBase
    {
        [HttpGet("{id:int}")]
        [MapToApiVersion("2.0")]
        public IActionResult Details(int id) => this.Ok();

        [HttpGet("{id:int}")]
        [MapToApiVersion("1.0")]
        public IActionResult DetailsV1(int id) => this.Ok();

        [HttpPut("{id:int}")]
        public IActionResult Edit(int id) => this.Ok();

        [HttpDelete("{id:int}")]
        [MapToApiVersion("2.0")]
        public IActionResult Delete(int id) => this.Ok();
    }
}
