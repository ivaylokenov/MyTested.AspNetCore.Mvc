namespace MyTested.AspNetCore.Mvc.Test.Setups.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RequestModel
    {
        [Range(1, int.MaxValue)]
        public int Integer { get; set; }

        [Required]
        public string RequiredString { get; set; }

        public string NonRequiredString { get; set; }

        public int NotValidateInteger { get; set; }
    }
}
