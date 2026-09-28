# 🏋️ Gym Management System

A secure and well-structured **Gym Management System REST API** built with **ASP.NET Core Web API**, **Entity Framework Core**, and **SQL Server**.

The system helps gym staff manage members, coaches, subscription plans, memberships, payments, and system users through a centralized backend API.

## ✨ Features

- 👤 Manage people and system users
- 🏃 Register and manage gym members
- 🏋️ Add and manage coaches
- 📅 Create and track memberships
- 💳 Record and view payments
- 📦 Manage subscription plans
- ⏰ Automatically expire finished memberships
- 🔐 JWT authentication
- 🛡️ Role-based authorization
- 🔄 Access Token and Refresh Token support
- ♻️ Refresh Token rotation
- 🚪 Refresh Token revocation on logout
- 📖 Swagger UI for API testing and documentation

## 🛠️ Technologies

- **.NET 10**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server**
- **JWT Authentication**
- **Swagger / OpenAPI**
- **Layered Architecture**

## 🏗️ Project Structure
-----------------------------------------------------------------------------------
Gym_Management_System
│
├── Gym_Management_System
│   └── API, Controllers, Authentication and Configuration
│
├── Gym_Management_System.Business
│   └── Business Logic, Services and DTOs
│
├── Gym_Management_System.DataAccess
│   └── Database Access and Repositories
│
└── Database
    └── SQL Server Database Script
-------------------------------------------------------------------------------------

🔐 Authentication Flow
Login
The user logs in using a username and password.
After successful authentication, the API returns:
- An Access Token used to access protected endpoints.
- A Refresh Token used to request a new token pair when the Access Token expires.
Refresh Token
When the Access Token expires:
1. The client sends the current Refresh Token.
2. The API validates the Refresh Token.
3. The old Refresh Token is revoked.
4. A new Access Token is generated.
5. A new Refresh Token is generated and returned.
This implements Refresh Token Rotation, which improves authentication security.
Logout
When the user logs out:
1. The client sends the Refresh Token.
2. The API revokes the Refresh Token.
3. The revoked token can no longer be used to generate new Access Tokens.
📌 Main API Areas
Area	Operations
🔐 Authentication	Login, Refresh Token, Logout
👤 Persons	Add, Search, Delete
🏃 Members	Register, List, View Details
🏋️ Coaches	Add, List, Update Status
📅 Memberships	Create, List Active Memberships, View Membership History
📦 Subscriptions	List and Update Subscription Plans
💳 Payments	View All Payments and Member Payments
👥 Users	Create, List, Update Status and Credentials


🗄️ Database
The project uses SQL Server as its relational database.
The database script is available inside the:
Database

folder and can be executed in SQL Server Management Studio (SSMS) to create the required database structure.
🎯 Project Goal
The goal of this project is to demonstrate the implementation of a real-world backend system using ASP.NET Core Web API.
The project demonstrates:
- Building RESTful APIs
- Designing relational databases
- Working with Entity Framework Core
- Applying layered architecture
- Implementing business rules
- Implementing JWT Authentication
- Implementing Role-Based Authorization
- Managing Access Tokens and Refresh Tokens
- Implementing Refresh Token Rotation and Revocation
- Building asynchronous database operations
- Creating background services for automatic membership expiration
👨‍💻 Developed by Ahmad Hani
