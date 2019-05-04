namespace Lite.Test
{
    using MyTested.AspNetCore.Mvc;
    using Web.Controllers;
    using Xunit;

    public class ValuesControllerTest
    {
        [Fact]
        public void GetShouldReturnOk()
            => MyMvc
                .Controller<ValuesController>()
                .Calling(c => c.Get())
                .ShouldReturn()
                .Ok();

        [Fact]
        public void GetByIdShouldReturnNotFoundWithNoId()
            => MyMvc
                .Controller<ValuesController>()
                .Calling(c => c.Get(0))
                .ShouldReturn()
                .NotFound();

        [Fact]
        public void GetByIdShouldReturnOkWithId()
            => MyMvc
                .Controller<ValuesController>()
                .Calling(c => c.Get(1))
                .ShouldReturn()
                .Ok();

        [Fact]
        public void PostShouldReturnCreated()
            => MyMvc
                .Controller<ValuesController>()
                .Calling(c => c.Post("Test"))
                .ShouldReturn()
                .Created(created => created
                    .Passing(result => result
                        .Location == "/Created/Mocked"));

        [Fact]
        public void PutShouldReturnCreated()
            => MyMvc
                .Controller<ValuesController>()
                .Calling(c => c.Put(1, "Test"))
                .ShouldReturn()
                .Created(created => created
                    .Passing(result => result
                        .Location == "/Updated/1"));

        [Fact]
        public void DeleteShouldReturnBadRequestWithInvalidId()
            => MyMvc
                .Controller<ValuesController>()
                .Calling(c => c.Delete(0))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void DeleteShouldReturnUnauthorizedWithValidId()
            => MyMvc
                .Controller<ValuesController>()
                .Calling(c => c.Delete(1))
                .ShouldReturn()
                .Unauthorized();
    }
}
