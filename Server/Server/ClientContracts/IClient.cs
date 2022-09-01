using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.ClientContracts
{
    // The client methods have been extracted out into an interface called IClient.
    public interface IClient
    {
        Task ReceiveMessage(string user, string message);
        Task ReceiveGroupMembers(IList<string> members);
    }
}
