README 

ST10274003 Kuzivakwashe C Kanyemba

CMCSApp — Claim Management and Coordination System

Overview
CMCSApp is an ASP.NET Core MVC web application designed to simplify and manage lecturer claim submissions, verification, and approvals within an academic institution.

The system supports multiple user roles:
-	Lecturer – submits claims and uploads supporting documents  
-	Coordinator – reviews, verifies, or sends back claims  
-	Academic Manager – reviews and approves verified claims  

Built using:
-	ASP.NET Core MVC (C#)
-	Entity Framework Core (SQL Server)
-	Razor Views
-	Dependency Injection & Authentication (Cookies)


Features

Lecturer
-	Login securely
-	Submit new claims (month, hours, hourly rate, notes)
-	Upload supporting documents
-	View submitted and pending claims

Programme Coordinator
-	Review all submitted claims
-	View claim details and uploaded documents
-	Verify or send back claims for changes

Academic Manager
-	View verified claims
-	Approve or reject claims


















Project Setup
1.	Prerequisites
-	Visual Studio 2022 or later  
-	.NET 8.0 SDK or later  
-	Microsoft SQL Server  
-	EF Core Tools

2.	Clone or Open the Project
-	Open the project folder in **Visual Studio**.  
-	The main files you’ll interact with are: 
-	Controllers/
Models/
Views/
Data/

3.	Configure the Database
-	Edit your connection string in `appsettings.json`:
```json "ConnectionStrings": 
{
"DefaultConnection":"Server=YOUR_SERVER_NAME;Database=CMCS;Trusted_Connection=True;TrustServerCertificate=True;"
}
Replace YOUR_SERVER_NAME with your SQL Server instance name (e.g. DESKTOP-XXXXXXX).

4.	Apply Migrations and Create the Database
-	Open Package Manager Console and run:
-	Add-Migration InitialCreate
-	Update-Database
-	This creates the CMCS database with all tables defined in ApplicationDbContext.

5.	Run the Application
-	In Visual Studio:
-	Press Ctrl + F5 (or click “Run without Debugging”)
-	Then open your browser at:
-	https://localhost:xxxx

6.	Default Users
-	The system seeds default demo users in the in-memory repository.
Role	Username	Password	Description
Lecturer	lect1	password	Lecturer can submit claims
Coordinator	coord1	password	Coordinator can verify or send back claims
Manager	mgr1	password	Academic Manager can approve or reject claims
-	You can add new user registration and database persistence later if needed.





7.	Project Structure
CMCSApp/
Controllers/
   		─ AccountController.cs
  		─ LecturerController.cs
  		─ CoordinatorController.cs
   		─ ManagerController.cs

Data/
- ApplicationDbContext.cs
─ InMemoryRepository.cs

Models/
─ Claim.cs
─ User.cs

Views/
─ Account/
─ Lecturer/
─ Coordinator/
─ Manager/
─ Shared/

 wwwroot/
   		─ css/
   		─ js/
    		─ uploads/

Key Components
Entity Models
•	User – represents lecturers, coordinators, and managers
•	Claim – represents a submitted lecturer claim
Data Context
•	ApplicationDbContext – manages the database via EF Core
Repository
•	InMemoryRepository – stores seeded demo users and claims (for testing)

Running Tests (Manual)
1.	Login as lect1
o	Submit a claim under Lecturer → Fill Claim
o	Upload a file
2.	Login as coord1
o	Go to Coordinator → Review
o	Click View, Verify, or Send Back
3.	Login as mgr1
o	Go to Manager → Verified Claims
o	Approve or reject claims





Technologies Used
Technology	Purpose
ASP.NET Core MVC	Web framework
Entity Framework Core	ORM for SQL Server
Razor Pages	Dynamic view rendering
Microsoft SQL Server	Database
Bootstrap	UI styling
Cookie Authentication	User login/session handling

License
This project is for educational and institutional use.
Each NuGet package and dependency retains its original license.

Author
Developed by: [Kuzivakwashe C Kanyemba]
Institution: [IIE Monash South Africa]
Year: 2025

Support
If you encounter issues:
•	Recheck your connection string in appsettings.json
•	Make sure migrations were applied (Update-Database)
•	Ensure the InMemoryRepository is registered in Program.cs:
•	builder.Services.AddSingleton<InMemoryRepository>();

You’re now ready to use and extend CMCSApp — your full Claim Management System!

