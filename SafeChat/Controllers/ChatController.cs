using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeChat.Data;
using SafeChat.Models;
using System.Security.Claims;

namespace SafeChat.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Chat/Index (main chat page)
        public IActionResult Index()
        {
            return View();
        }

        // GET: Chat/PublicRooms
        public async Task<IActionResult> PublicRooms()
        {
            var rooms = await _context.ChatRooms
                .Where(r => !r.IsPrivate)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return View(rooms);
        }

        // GET: Chat/PrivateRooms
        public IActionResult PrivateRooms()
        {
            return View();
        }

        // POST: Create a new private room
        [HttpPost]
        public async Task<IActionResult> CreatePrivateRoom()
        {
            var room = new ChatRoom
            {
                Name = $"Private-{Guid.NewGuid().ToString().Substring(0, 8)}",
                IsPrivate = true,
                InviteCode = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow
            };

            _context.ChatRooms.Add(room);
            await _context.SaveChangesAsync();

            return RedirectToAction("Room", new { id = room.Id });
        }

        // GET: Chat/Room/5
        public async Task<IActionResult> Room(int id)
        {
            var room = await _context.ChatRooms.FindAsync(id);

            if (room == null)
                return NotFound();

            // Check if user already has a chat name for this room
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingName = await _context.UserChatNames
                .FirstOrDefaultAsync(u => u.UserId == userId && u.ChatRoomId == id);

            ViewBag.ChatName = existingName?.ChatName;
            ViewBag.RoomId = id;
            ViewBag.RoomName = room.Name;

            // Get recent messages (last 5 minutes)
            var recentMessages = await _context.Messages
                .Where(m => m.ChatRoomId == id && m.Timestamp > DateTime.UtcNow.AddMinutes(-5))
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            ViewBag.Messages = recentMessages;

            return View(room);
        }

        // POST: Save user's chat name
        [HttpPost]
        public async Task<IActionResult> SetChatName(int roomId, string chatName)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check if name already exists
            var existingName = await _context.UserChatNames
                .FirstOrDefaultAsync(u => u.UserId == userId && u.ChatRoomId == roomId);

            if (existingName == null)
            {
                // Generate random name if empty
                if (string.IsNullOrWhiteSpace(chatName))
                {
                    chatName = $"User{new Random().Next(1000, 9999)}";
                }

                var userChatName = new UserChatName
                {
                    UserId = userId,
                    ChatRoomId = roomId,
                    ChatName = chatName
                };

                _context.UserChatNames.Add(userChatName);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Room", new { id = roomId });
        }

        // GET: Generate QR Code for private room
        public async Task<IActionResult> GetQRCode(int roomId)
        {
            var room = await _context.ChatRooms.FindAsync(roomId);

            if (room == null || !room.IsPrivate)
                return NotFound();

            var inviteUrl = Url.Action("JoinPrivate", "Chat", new { code = room.InviteCode }, Request.Scheme);

            // Generate QR Code
            using var qrGenerator = new QRCoder.QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(inviteUrl, QRCoder.QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCoder.PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(20);

            return File(qrCodeImage, "image/png");
        }

        // GET: Join private room via invite code
        public async Task<IActionResult> JoinPrivate(string code)
        {
            var room = await _context.ChatRooms
                .FirstOrDefaultAsync(r => r.InviteCode == code);

            if (room == null)
            {
                TempData["Error"] = "Invalid invite code";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Room", new { id = room.Id });
        }
    }
}