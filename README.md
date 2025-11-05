<div align="center">
  <h1>💬 SafeChat</h1>
  <p><strong>Privacy-Focused, Real-Time Messaging Platform</strong></p>
  
  <p>
    <a href="#features">Features</a> •
    <a href="#demo">Demo</a> •
    <a href="#tech-stack">Tech Stack</a> •
    <a href="#installation">Installation</a> •
    <a href="#usage">Usage</a> •
    <a href="#screenshots">Screenshots</a> •
  </p>

  ![SafeChat Banner](https://via.placeholder.com/800x200/667eea/ffffff?text=SafeChat+-+Privacy+First+Messaging)
  
</div>

---

## 🚀 About SafeChat

SafeChat is a minimalistic, real-time chat application designed with **privacy as the core principle**. Unlike traditional messaging platforms that store conversations indefinitely, SafeChat automatically deletes messages after **5 minutes**, ensuring minimal data retention and maximum confidentiality.

Perfect for:
- 🔐 Sensitive team discussions
- 👥 Temporary group coordination
- 💬 Anonymous conversations
- 🎯 Quick Q&A sessions

---

## ✨ Features

### 🔒 Privacy & Security
- **Auto-Delete Messages**: All messages automatically vanish after 5 minutes
- **No Permanent Storage**: Zero long-term chat history retention
- **Anonymous Chat Names**: Use pseudonyms without revealing identity
- **Self-Destructing Rooms**: Empty private rooms auto-delete after 10 minutes

### ⚡ Real-Time Communication
- **Instant Messaging**: Powered by SignalR WebSockets
- **Live Typing Indicators**: See when others are typing
- **User Presence**: Real-time join/leave notifications
- **Zero Latency**: Messages appear instantly without page refresh

### 🌐 Room Types
- **Public Rooms**: 5 themed rooms (General, Tech, Gaming, Music, Random)
- **Private Rooms**: Create invite-only rooms with unique codes
- **QR Code Sharing**: Generate QR codes for easy mobile joining
- **Shareable Links**: One-click invite URLs

### 🎨 User Experience
- **Modern UI**: Glassmorphism design with smooth animations
- **Responsive Design**: Works seamlessly on desktop, tablet, and mobile
- **Dark Mode Ready**: Easy-to-read interface
- **Custom Scrollbars**: Polished visual details

---

## 🛠️ Tech Stack

### Backend
- **ASP.NET Core 8.0 MVC** - Web framework
- **SignalR** - Real-time WebSocket communication
- **Entity Framework Core** - ORM for database operations
- **ASP.NET Core Identity** - Authentication & authorization
- **SQL Server** - Database (LocalDB for development)

### Frontend
- **Razor Views** (.cshtml) - Server-side rendering
- **Bootstrap 5** - UI framework
- **Bootstrap Icons** - Icon library
- **JavaScript (Vanilla)** - SignalR client integration
- **Custom CSS** - Modern styling with gradients and animations

### Libraries & Tools
- **QRCoder** - QR code generation
- **C# BackgroundService** - Auto-cleanup scheduler

---

## 📋 Prerequisites

Before running SafeChat, ensure you have:

- **.NET 8.0 SDK** or higher ([Download](https://dotnet.microsoft.com/download))
- **Visual Studio 2022** or **Visual Studio Code**
- **SQL Server** (LocalDB comes with Visual Studio)
- **Git** (optional, for cloning)

---

## ⚙️ Installation

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/safechat.git
cd safechat
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Update Database Connection String

Open `appsettings.json` and verify the connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=aspnet-SafeChat;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### 4. Apply Database Migrations
```bash
dotnet ef database update
```

This creates:
- `ChatRooms` table
- `Messages` table
- `UserChatNames` table
- `AspNetUsers` and related Identity tables

### 5. Run the Application
```bash
dotnet run
```

Or press **F5** in Visual Studio.

## 📖 Usage

### Getting Started

1. **Register** a new account (email format: `user@example.com`, password: min 4 chars)
2. **Login** with your credentials
3. Click **"Chat"** in the navigation bar

### Joining Public Rooms

1. Click **"Browse Public Rooms"**
2. Select any of the 5 themed rooms:
   - 💬 General Chat
   - 🎲 Random Discussion
   - 💻 Tech Talk
   - 🎮 Gaming Lounge
   - 🎵 Music & Arts
3. Enter your chat name (or leave blank for auto-generated name)
4. Start chatting!

### Creating Private Rooms

1. Click **"Create Private Room"**
2. A unique room with invite code is generated
3. Click **"Invite"** button to see:
   - 📱 **QR Code** (scan with mobile)
   - 🔗 **Invite Link** (copy and share)
4. Others can join via link or QR code

### Message Features

- Type and press **Enter** or click **Send**
- See **typing indicators** when others are typing
- Messages show **timestamp in IST** (Indian Standard Time)
- Messages **auto-delete after 5 minutes**
- **Leave notification** when you exit

---

## 📸 Screenshots

### Home Page
![Home Page](https://via.placeholder.com/800x400/667eea/ffffff?text=Home+Page)

### Chat Interface
![Chat Interface](https://via.placeholder.com/800x400/764ba2/ffffff?text=Chat+Interface)

### Private Room with QR Code
![QR Code](https://via.placeholder.com/800x400/10b981/ffffff?text=QR+Code+Invite)

---

## 🏗️ Project Structure
```
SafeChat/
├── Controllers/
│   ├── HomeController.cs          # Home and Rules pages
│   └── ChatController.cs          # Chat room logic
├── Models/
│   ├── ChatRoom.cs               # Room entity
│   ├── Message.cs                # Message entity
│   └── UserChatName.cs           # User chat names
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml          # Landing page
│   │   └── Rules.cshtml          # Rules page
│   ├── Chat/
│   │   ├── Index.cshtml          # Chat home
│   │   ├── PublicRooms.cshtml    # Public room list
│   │   ├── PrivateRooms.cshtml   # Create private room
│   │   └── Room.cshtml           # Chat interface
│   └── Shared/
│       ├── _Layout.cshtml        # Main layout
│       └── _LoginPartial.cshtml  # User menu
├── Hubs/
│   └── ChatHub.cs                # SignalR hub
├── Services/
│   └── MessageCleanupService.cs  # Background cleanup
├── Data/
│   ├── ApplicationDbContext.cs   # EF Core context
│   └── DbSeeder.cs              # Seed public rooms
├── wwwroot/
│   ├── css/
│   │   └── custom.css           # Custom styles
│   └── lib/                     # Client libraries
└── appsettings.json             # Configuration
```

---

## 🐛 Known Issues & Limitations

- **No End-to-End Encryption**: Messages are stored in plain text (for 5 minutes)
- **No Message History**: Once deleted, messages cannot be recovered
- **Single Server**: Requires Redis backplane for multi-server SignalR scaling
- **No File Sharing**: Text-only messaging currently supported
- **No Notifications**: No browser/email notifications for new messages

---

## 🗺️ Roadmap

- [ ] End-to-end encryption
- [ ] File/image sharing
- [ ] Voice messages
- [ ] Browser notifications
- [ ] Dark mode toggle
- [ ] Emoji picker
- [ ] User profiles with avatars
- [ ] Message reactions
- [ ] Admin/moderator tools
- [ ] Mobile apps (iOS/Android)

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/AmazingFeature`)
3. **Commit** your changes (`git commit -m 'Add some AmazingFeature'`)
4. **Push** to the branch (`git push origin feature/AmazingFeature`)
5. **Open** a Pull Request

---

## 🙏 Acknowledgments

- Built with [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet)
- Real-time powered by [SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr)
- UI inspired by modern web design trends
- Icons by [Bootstrap Icons](https://icons.getbootstrap.com/)

---

<div align="center">
  <p>Made with ❤️ and ☕</p>
  <p>⭐ Star this repo if you found it useful!</p>
</div>
```

---
