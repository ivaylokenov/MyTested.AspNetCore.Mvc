namespace Lite.Web.Services
{
    public class Data : IData
    {
        public string[] Get() => new[] { "Real", "Data", "From", "Database" };
    }
}
