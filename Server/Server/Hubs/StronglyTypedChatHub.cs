using Microsoft.AspNetCore.SignalR;
using Server.ClientContracts;
using System.Threading.Tasks;

namespace Server.Hubs
{
    public class StronglyTypedChatHub : Hub<IClient>
    {
        // A drawback of using SendAsync is that it relies on a magic string to specify the client method to be called. 
        // This leaves code open to runtime errors if the method name is misspelled or missing from the client.
        // An alternative to using SendAsync is to strongly type the Hub with Hub<T>.
        public async Task SendMessageToGroup(string user, string group, string message)
        {
            await Clients.Group(group).ReceiveMessage(user, $"{user}: {message}");
        }

        // So here the client methods have been extracted out into an interface called IClient.
        // Using Hub<IClient> enables compile-time checking of the client methods. This prevents issues caused by using magic strings, since Hub<T> can only provide access to the methods defined in the interface.
        // Using a strongly typed Hub<T> disables the ability to use SendAsync. Any methods defined on the interface (in our case i.e. IClient) can still be defined as asynchronous. In fact, each of these methods should return a Task. 

        public async Task AddToGroup(string user, string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);

            await Clients.Caller.ReceiveMessage(user, $"You have joined the group {group}.");

            await Clients.OthersInGroup(group).ReceiveMessage(user, $"{user} has joined the group {group}.");

            await Clients.Group(group).ReceiveGroupMembers(State.AddToState(user, group));
        }

        public async Task RemoveFromGroup(string user, string group)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);

            await Clients.Group(group).ReceiveMessage(user, $"{user} has left the group {group}.");

            State.RemoveFromState(user, group);

            await Clients.Group(group).ReceiveGroupMembers(State.CurrentState(group));
        }
    }
}
