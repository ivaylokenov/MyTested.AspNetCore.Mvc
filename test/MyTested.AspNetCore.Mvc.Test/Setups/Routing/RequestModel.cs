namespace MyTested.AspNetCore.Mvc.Test.Setups.Routing
{
    using System.ComponentModel.DataAnnotations;

    public class RequestModel
    {
        public int Integer { get; set; }

        [Required]
        public string String { get; set; }

        public void SomeMethod()
        {
        }
    }
}
