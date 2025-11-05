namespace SafeChat.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string SenderName { get; set; }
        public int ChatRoomId { get; set; }
        public DateTime Timestamp { get; set; }

        // Navigation property
        public ChatRoom ChatRoom { get; set; }
    }
}