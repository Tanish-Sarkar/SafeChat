namespace SafeChat.Models
{
    public class UserChatName
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ChatRoomId { get; set; }
        public string ChatName { get; set; }
    }
}