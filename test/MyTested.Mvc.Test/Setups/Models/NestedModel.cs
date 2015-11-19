namespace MyTested.Mvc.Tests.Setups.Models
{
    public class NestedModel
    {
        public int Integer { get; set; }

        public string String { get; set; }

        public NestedModel Nested { get; set; }
    }
}
