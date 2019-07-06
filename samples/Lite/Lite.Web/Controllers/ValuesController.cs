namespace Lite.Web.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IData data;

        public ValuesController(IData data) => this.data = data;

        [HttpGet]
        public IActionResult Get() => this.Ok(this.data.Get());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
            {
                return this.NotFound();
            }

            return this.Ok(this.data.Get().FirstOrDefault());
        }
        
        [HttpPost]
        public IActionResult Post([FromBody]string value) => this.Created($"/Created/{this.data.Get().FirstOrDefault()}", value);

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value) => this.Created($"/Updated/{id}", value);

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return this.BadRequest();
            }

            return this.Unauthorized();
        }
    }
}
