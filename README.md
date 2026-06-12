# Bit2Byte

Bit2Byte is a **web-based club management and community platform** developed using **ASP.NET Web Forms (.NET Framework)**. The project was initially designed as a **static website using HTML, CSS, and JavaScript**, featuring the Home, About, Events, Members, Login and Registration pages. It was later converted into a dynamic ASP.NET application inside the **Bit2Byte** project folder, where database integration, authentication, profile management, and administrative features were implemented.

The application follows a layered architecture using C#, ADO.NET, and the Repository Pattern to provide a structured and maintainable codebase.

---

## Features

### Website Pages

- Home page with club overview and highlights
- About page describing the club and its activities
- Events page displaying upcoming events
- Registration and Login pages
- Shared layout using ASP.NET **Site.Master**

### User System

- User registration with validation
- Secure login system
- Session-based authentication
- Role-based access (Admin, Member)

### Security and Session Management

- Password hashing using PBKDF2
- Secure password verification
- Protected pages using session validation
- Email validation with KUET domain restriction

### Profile Management

- Update username, bio, and interests
- Upload and manage avatar images
- Change password securely
- Email change request and confirmation system

### Event Management (Admin)

- Create, update, and delete events
- View all events in the admin dashboard

### User Management (Admin)

- View all registered users
- Edit user information and roles
- Delete users from the system

### Remember Me Feature (Cookies)

- Stores user email using HTTP cookies
- Automatically fills the email field on future visits (7-day expiry)

### Responsive Design & Client-Side Features

- Responsive layouts using CSS Grid, Flexbox, and media queries
- Dark-themed user interface
- JavaScript email and password validation
- Dynamic footer year update using JavaScript
- Browser back/forward cache handling for improved navigation

---

## Technologies Used

- ASP.NET Web Forms (.NET Framework)
- C#
- ADO.NET / Repository Pattern
- HTML5
- CSS3
- JavaScript
- SQL Server
- Bootstrap

---

## Authentication Flow

1. User logs in using email and password.
2. Password is verified using hashed credentials.
3. Session variables are created for authenticated users.
4. Optional cookie stores the user's email for convenience.

---

## Core Modules

- Authentication (Login / Register)
- Profile Management
- Event Management
- User Management
- Admin Dashboard

---

## Author

**Bit2Byte Project**  
Web Programming Laboratory
Kazi Sakibul Hasan (Roll : 2207008)  
Khulna University of Engineering & Technology (KUET)

---

## Notes

- Developed for academic and learning purposes.
- Started as a static HTML/CSS/JavaScript website before being migrated to ASP.NET Web Forms.