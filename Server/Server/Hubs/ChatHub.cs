using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Server.Hubs
{
    public class ChatHub : Hub // The Hub class manages connections, groups, and messaging.
    {
        public async Task SendMessage(string user, string message) // Clients can call methods that are defined as public.
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        // SignalR handles the serialization and deserialization of complex objects and arrays in your parameters and return values.

        // Hubs are transient, so:
        // 1) Don't store state in a property on the hub class. Every hub method call is executed on a new hub instance.
        // 2) Use await when calling asynchronous methods that depend on the hub staying alive. For example, a method such as Clients.All.SendAsync(...) can fail if it's called without await and the hub method completes before SendAsync finishes.
    }
}
