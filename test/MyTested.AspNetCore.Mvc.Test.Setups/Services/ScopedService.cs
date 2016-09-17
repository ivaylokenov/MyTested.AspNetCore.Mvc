namespace MyTested.AspNetCore.Mvc.Test.Setups.Services
{
    public class ScopedService : IScopedService
    {
        public ScopedService()
        {
            this.Value = "Default";
        }

        public string Value { get; set; }
    }
}
