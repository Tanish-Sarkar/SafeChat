using SafeChat.Models;

namespace SafeChat.Data
{
    public class DbSeeder
    {
        public static async Task SeedPublicRooms(ApplicationDbContext context)
        {
            // Check if any public rooms exist
            if (!context.ChatRooms.Any(r => !r.IsPrivate))
            {
                var publicRooms = new List<ChatRoom>
                {
                    new ChatRoom
                    {
                        Name = "General Chat",
                        IsPrivate = false,
                        CreatedAt = DateTime.UtcNow
                    },
                    new ChatRoom
                    {
                        Name = "Random Discussion",
                        IsPrivate = false,
                        CreatedAt = DateTime.UtcNow
                    },
                    new ChatRoom
                    {
                        Name = "Tech Talk",
                        IsPrivate = false,
                        CreatedAt = DateTime.UtcNow
                    },
                    new ChatRoom
                    {
                        Name = "Gaming Lounge",
                        IsPrivate = false,
                        CreatedAt = DateTime.UtcNow
                    },
                    new ChatRoom
                    {
                        Name = "Music & Arts",
                        IsPrivate = false,
                        CreatedAt = DateTime.UtcNow
                    }
                };

                context.ChatRooms.AddRange(publicRooms);
                await context.SaveChangesAsync();
            }
        }
    }
}