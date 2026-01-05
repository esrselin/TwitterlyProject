# TwitterlyProject 🐦

TwitterlyProject is an ASP.NET Core web application that implements a miniature Twitter‑like social network. Users can sign up, post tweets, follow or unfollow other users and view a personalized feed containing their own tweets and those from people they follow. The project is split into multiple layers to separate concerns Domain for entity definitions, Common for data transfer objects (DTOs), Core for business services and Presentation for the web UI providing a maintainable, testable architecture.

This project focuses on **clean architecture, separation of concerns, and scalable backend design**.

---

## 🚀 Features

- User registration & authentication (ASP.NET Core Identity)
- Post tweets
- Follow & unfollow users
- Personalized timeline (own tweets + followed users’ tweets)
- Tweet count statistics per user
- Redis caching for frequently accessed data (for example: timeline and user stats)
- Clean layered architecture
- Entity Framework Core with SQL Server
- Razor Pages & MVC hybrid structure

---

## 🧱 Architecture Overview

The solution is divided into **four main layers**, each with a clear responsibility:

- **Domain** → Database entities, DbContext, migrations  
- **Common** → DTOs used for data transfer  
- **Core** → Business logic & services  
- **Presentation** → Web UI (MVC, Razor Pages, Identity)

This separation makes the project easier to maintain, test, and extend.

---

## 📂 Project Structure

```
TwitterlyProject
│
├── Common
│   └── DTO
│       ├── TweetDTO.cs
│       ├── TwitterUserDTO.cs
│       ├── UserFollowDTO.cs
│       └── UserTweetCountDTO.cs
│
├── Core
│   ├── Interfaces
│   │   ├── ITweetService.cs
│   │   ├── ITwitterUserService.cs
│   │   └── IUserFollowService.cs
│   │
│   └── Services
│       ├── TweetService.cs
│       ├── TwitterUserService.cs
│       ├── UserFollowService.cs
│       └── UnitOfWork.cs
│
├── Domain
│   ├── ContextFactory
│   │   └── TwitterDbContextFactory.cs
│   │
│   ├── DbContext
│   │   └── TwitterDbContext.cs
│   │
│   ├── Entities
│   │   ├── Tweet.cs
│   │   ├── TwitterUser.cs
│   │   ├── UserFollow.cs
│   │   └── UserTweetCount.cs
│   │
│   └── Migrations
│       └── (EF Core migration files)
│
├── Presentation
│   ├── Areas
│   │   └── Identity
│   │       └── Pages (Login, Register, etc.)
│   │
│   ├── Controllers
│   │   └── HomeController.cs
│   │
│   ├── Models
│   │   └── ErrorViewModel.cs
│   │
│   ├── Views
│   │   ├── Home
│   │   └── Shared
│   │
│   ├── wwwroot
│   │   ├── css
│   │   ├── js
│   │   └── lib
│   │
│   ├── Program.cs
│   ├── appsettings.json
│   └── appsettings.Development.json
│
├── TwitterlyProject.sln
└── README.md
```

---

## 🛠️ Technologies Used

- **.NET 8**
- **ASP.NET Core MVC & Razor Pages**
- **ASP.NET Core Identity**
- **Entity Framework Core**
- **SQL Server**
- **Redis**
- **C#**
- **Razor**

---

## ⚙️ Getting Started

### Prerequisites

- .NET SDK 8.0
- SQL Server (LocalDB or full instance)
- Redis (local)

---

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/esrselin/TwitterlyProject.git
   cd TwitterlyProject
   ```

2. **Configure database**
   - Open `Presentation/appsettings.json`
   - Update the `DefaultConnection` string if needed

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Apply database migrations**
   ```bash
   cd Presentation
   dotnet ef database update
   cd ..
   ```

5. **Run the project**
   ```bash
   dotnet run --project Presentation
   ```

6. Open your browser:
   ```
   https://localhost:5001
   ```

---

## 👤 Usage

- Register or log in
- Post tweets from the dashboard
- Follow or unfollow other users
- View your personalized timeline
- Check tweet count statistics

---

## 🤝 Contributing

Contributions are welcome!

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Open a Pull Request

---

## ✨ Author

**Esra Selin Akgül**  
Computer Engineer | Backend & .NET Developer
