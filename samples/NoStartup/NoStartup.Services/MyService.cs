namespace NoStartup.Services
{
    public class MyService : IService
    {
        public string[] GetData()
        {
            return new[] 
            {
                "First",
                "Second",
                "Third"
            };
        }
    }
}
