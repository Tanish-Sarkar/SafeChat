namespace SafeChat.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public string? InviteCode { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}