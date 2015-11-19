namespace MyTested.Mvc.Tests.Setups.Models
{
    using System;

    public class ComparableModel : IComparable
    {
        public int Integer { get; set; }

        public string String { get; set; }

        public int CompareTo(object obj)
        {
            var objAsComparableModel = (ComparableModel)obj;
            if (this.Integer < objAsComparableModel.Integer)
            {
                return -1;
            }
            else if (this.Integer > objAsComparableModel.Integer)
            {
                return 1;
            }

            return 0;
        }
    }
}
