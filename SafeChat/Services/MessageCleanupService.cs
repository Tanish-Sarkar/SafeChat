using Microsoft.EntityFrameworkCore;
using SafeChat.Data;

namespace SafeChat.Services
{
    public class MessageCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MessageCleanupService> _logger;

        public MessageCleanupService(IServiceProvider serviceProvider, ILogger<MessageCleanupService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Message Cleanup Service started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CleanupOldMessages();
                    await CleanupEmptyPrivateRooms();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in Message Cleanup Service");
                }

                // Wait 1 minute before next cleanup
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task CleanupOldMessages()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Delete messages older than 5 minutes
            var cutoffTime = DateTime.UtcNow.AddMinutes(-5);
            var oldMessages = await context.Messages
                .Where(m => m.Timestamp < cutoffTime)
                .ToListAsync();

            if (oldMessages.Any())
            {
                context.Messages.RemoveRange(oldMessages);
                await context.SaveChangesAsync();
                _logger.LogInformation($"Deleted {oldMessages.Count} old messages");
            }
        }

        private async Task CleanupEmptyPrivateRooms()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Find private rooms with no messages in the last 10 minutes
            var cutoffTime = DateTime.UtcNow.AddMinutes(-10);
            var emptyPrivateRooms = await context.ChatRooms
                .Where(r => r.IsPrivate && !r.Messages.Any(m => m.Timestamp > cutoffTime))
                .ToListAsync();

            if (emptyPrivateRooms.Any())
            {
                context.ChatRooms.RemoveRange(emptyPrivateRooms);
                await context.SaveChangesAsync();
                _logger.LogInformation($"Deleted {emptyPrivateRooms.Count} empty private rooms");
            }
        }
    }
}