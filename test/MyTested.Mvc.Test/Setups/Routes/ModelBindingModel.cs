namespace MyTested.Mvc.Test.Setups.Routes
{
    using Microsoft.AspNetCore.Mvc;

    public class ModelBindingModel
    {
        [FromBody]
        public RequestModel Body { get; set; }

        [FromForm(Name = "MyField")]
        public string Form { get; set; }

        [FromQuery(Name = "MyQuery")]
        public string Query { get; set; }

        [FromRoute(Name = "id")]
        public int Route { get; set; }

        [FromHeader(Name = "MyHeader")]
        public string Header { get; set; }
    }
}
