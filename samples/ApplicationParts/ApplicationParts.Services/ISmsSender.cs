using System.Threading.Tasks;

namespace ApplicationParts.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
