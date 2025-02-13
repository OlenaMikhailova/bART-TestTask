# bART Test Task

## Description

This project is a test task that involves creating an API using ASP.NET Core and Entity Framework that allows working with incidents, accounts, and contacts.

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Swagger

## How to Run

1. Clone the repository:
   ```git clone https://github.com/OlenaMikhailova/bART-TestTask.git

2. Restore the required dependencies:
   ```dotnet restore

3. Update your appsettings.json file with the correct connection string:
   ```"ConnectionStrings": {"DefaultConnection": "Host=localhost;Port=5432;Database=bARTDB;Username=postgres;Password=yourpassword"}

4. Apply the migrations to set up the database schema:
   ```dotnet ef database update

5. Run the backend:
   ```dotnet run
