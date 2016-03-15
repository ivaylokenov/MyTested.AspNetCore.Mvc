namespace MyTested.Mvc.Test.Setups.Common
{
    using System.Collections;

    public class CustomEqualityComparer : IEqualityComparer
    {
        public new bool Equals(object x, object y)
        {
            return true;
        }

        public int GetHashCode(object obj)
        {
            return 0;
        }
    }
}
