namespace MyTested.Mvc.Test.Setups.Routes
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
