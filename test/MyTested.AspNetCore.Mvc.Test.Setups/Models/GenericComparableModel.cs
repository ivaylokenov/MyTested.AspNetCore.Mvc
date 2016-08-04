namespace MyTested.AspNetCore.Mvc.Test.Setups.Models
{
    using System;

    public class GenericComparableModel : IComparable<GenericComparableModel>
    {
        public int Integer { get; set; }

        public string String { get; set; }

        public int CompareTo(GenericComparableModel other)
        {
            if (this.Integer < other.Integer)
            {
                return -1;
            }
            else if (this.Integer > other.Integer)
            {
                return 1;
            }

            return 0;
        }
    }
}
