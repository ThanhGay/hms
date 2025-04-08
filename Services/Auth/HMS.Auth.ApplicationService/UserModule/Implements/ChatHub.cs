using Microsoft.AspNetCore.SignalR;

namespace HMS.Auth.ApplicationService.UserModule.Implements
{
    public class ChatHub : Hub
    {

        public async Task SendMessage(string conversationId, string user, string message)
        {
            await Clients.Group(conversationId).SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinRoom(string conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);
            await Clients.Group(conversationId).SendAsync("UserJoined", Context.ConnectionId);
        }
    }
}
