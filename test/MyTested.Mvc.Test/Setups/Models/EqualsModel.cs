namespace MyTested.Mvc.Tests.Setups.Models
{
    public class EqualsModel
    {
        public int Integer { get; set; }

        public string String { get; set; }

        public override bool Equals(object obj)
        {
            return this.Integer == ((EqualsModel)obj).Integer;
        }

        public override int GetHashCode()
        {
            return this.Integer.GetHashCode() ^ this.String.GetHashCode();
        }
    }
}
