# 🏋️ GemManagment - Gym Management System

GemManagment is an ASP.NET MVC web application designed to manage a gym system.  
The application allows administrators to manage trainers, members, subscription plans, and memberships through a secure authentication system built with ASP.NET Identity.

The project follows the MVC architecture pattern and is structured into separate layers for better maintainability and scalability.

---

## 🧠 Project Overview

GemManagment provides a complete solution for gym administration including:

- Trainer management
- Member management
- Subscription plans
- Membership tracking
- Membership scheduling
- Secure admin authentication

Only authenticated administrators can manage system data.

---

## 🚀 Main Features

### 🏠 Home Page
- Landing page of the system
- Navigation to all system modules
- General gym information display

---

### 🏋️ Trainers Management
- Add new trainers
- Edit trainer information
- Delete trainers
- View trainers list
- Store trainer details (name, specialization, contact info)

---

### 👥 Members Management
- Register new members
- Update member information
- Delete members
- View members list
- Track member details and status

---

### 💳 Plans Management
- Create subscription plans
- Define plan duration and price
- Edit plans
- Delete plans

Example plans:
- Monthly Plan
- 3-Month Plan
- Annual Plan

---

### 📋 Membership Management
- Assign subscription plan to members
- Track membership start and end dates
- Monitor active and expired memberships
- Manage subscription status

---

### 🔄 Membership Scheduling
- Organize membership periods
- Track renewals
- Update expiration dates
- Manage subscription cycles

---

### 🔐 Authentication & Authorization

The system uses **ASP.NET Identity** for:

- Admin login & logout
- Role-based authorization
- Secure password handling
- Restricting access to management pages

Only Admin users can:
- Manage trainers
- Manage members
- Manage plans
- Manage memberships

---

## 🏗 Project Architecture

The solution follows a layered architecture:
GemManagment/
│
├── GemManagment.PL → Presentation Layer (MVC Controllers & Views)
├── GemManagment.BLL → Business Logic Layer
├── GemManagment.DAL → Data Access Layer (Entity Framework)
└── GemManagmentSoultion.sln

### Layers Description

- **Presentation Layer (PL)**  
  Contains MVC Controllers, Views, and UI logic.

- **Business Logic Layer (BLL)**  
  Handles business rules and application logic.

- **Data Access Layer (DAL)**  
  Manages database operations using Entity Framework.

---

## 🛠 Technologies Used

- ASP.NET MVC
- C#
- Entity Framework
- SQL Server
- ASP.NET Identity
- HTML5 / CSS3 / Bootstrap
- JavaScript

---

## ⚙️ Installation & Setup

### Prerequisites

- Visual Studio 2019 or later
- SQL Server (Express or LocalDB)
- .NET Framework installed

### Steps

1. Clone the repository:
-git clone https://github.com/Gemy33/GemManagment.git

2. Open the solution file in Visual Studio.

3. Restore NuGet packages.

4. Update the connection string in Web.config.

5. Run database migrations (if using Code First).

Press F5 to run the application.
