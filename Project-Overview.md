## **SafeChat — Project Overview**

### 1. **Project Name**

**SafeChat** — A privacy-focused, real-time chat platform.

---

### 2. **What is the Project?**

SafeChat is a minimalistic, real-time chat application designed to offer `private` and `global` messaging. Users can join public or private chat rooms, generate QR codes for quick sharing, and enjoy instant communication without storing data for long periods.

---

### 3. **Aim of the Project**

To build a **secure, fast, and privacy-respecting** chat platform that enables seamless real-time conversations, while automatically deleting old messages to ensure confidentiality and minimal data retention.

---

### 4. **Why We Are Building This**

* Many chat apps store conversations indefinitely, posing privacy risks.
* Our solution is designed for situations where **privacy and anonymity are priorities** — such as quick team discussions, or temporary group chats.
* It demonstrates real-time communication using modern web technologies and SignalR for instant updates.

---

### 5. **Full Flow of the Web App (User Experience)**

1. **Login / Signup**

   * Users create an account or log in using a simple interface.

2. **Dashboard / Room Selection**
   * User will see 3 pages at the navigation bar `Home | Chats | Rules`
   * On going to the Chat tab you will find 2 options `Public rooms | Private rooms`
   * On clicking on Private rooms generate a **QR code** and **invite link** for sharing.
      * Everytime you enter any chat room either public or private a pop-up will come and the user as to name a in-chat name if the user doesn't enter any name then the system will assign the a unique name by itself 

3. **Chat Interface**
   * Real-time messaging powered by **SignalR**.
   * Messages appear instantly without refreshing the page.
   * Typing indicators show when someone is composing a message.

4. **Message Auto-Deletion**
   * Every 1 minutes, the system checks for messages older than 5 minutes and **automatically removes them**.

5. **User Exit / Logout**
   * Users can log out anytime.
   * All their messages will naturally disappear when they reach the 5-minute limit.
   * If it's a private room and the room gets empty the room will get automaticaly deleted.

---

### 6. **Tech Stack**

#### **Backend**

* **ASP.NET MVC** — Handles routes, authentication, and API endpoints.
* **SignalR** — Enables real-time chat communication.

#### **Frontend**

* **JavaScript** — For chat functionality and UI interactions.
* **SignalR JavaScript Client** — To connect to the SignalR hub.
* **HTML + CSS** — For UI structure and styling.  (.apsx file typically)

#### **Database**

* **SQL Server** — Stores temporary messages and user data.

#### **Other**

* **QR Code Generator Library** — To create QR codes for private room invites.
* **Entity Framework** — For database operations.
* **GitHub** — For version control.
* **Deployment** — Render.

---

### 7. **Features**

* User authentication (Login & Signup)
* Auto-generated usernames if not set manually
* Public and private chat rooms
* QR code invite system for private rooms
* Real-time messaging with SignalR
* Auto-deletion of messages older than 5 minutes
* Responsive, minimal UI

---

### 8. **Future Scope**

* End-to-end encryption for private messages
* Support for file sharing
* Mobile app integration
* Dark mode for better UI/UX

---

### 9. [**System Architecture and Schema design**](./Architecture.md)
### 10. [**Git Versioning Instructions**](./Git-Versioning-information.md)
