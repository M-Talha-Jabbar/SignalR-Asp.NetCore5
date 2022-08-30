using System.Threading.Tasks;

namespace Server.ClientContracts
{
    public interface IClient
    {
        Task ReceiveMessage(string user, string message);
    }
}
