using Microsoft.AspNetCore.SignalR;
using SafeChat.Data;
using SafeChat.Models;

namespace SafeChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(int roomId, string userName, string message)
        {
            // Save message to database
            var newMessage = new Message
            {
                Content = message,
                SenderName = userName,
                ChatRoomId = roomId,
                Timestamp = DateTime.UtcNow
            };

            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();

            // Send message to all users in the room
            await Clients.Group(roomId.ToString())
                .SendAsync("ReceiveMessage", userName, message, newMessage.Timestamp.ToString("HH:mm"));
        }

        public async Task JoinRoom(int roomId, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());

            await Clients.Group(roomId.ToString())
                .SendAsync("UserJoined", userName);
        }

        public async Task LeaveRoom(int roomId, string userName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());

            await Clients.Group(roomId.ToString())
                .SendAsync("UserLeft", userName);
        }

        public async Task NotifyTyping(int roomId, string userName)
        {
            await Clients.OthersInGroup(roomId.ToString())
                .SendAsync("UserTyping", userName);
        }
    }
}