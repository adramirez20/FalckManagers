Employee Management API – Overview

This API was developed in .NET 7 as a minimal implementation for a technical employee management test. The project follows Clean Architecture and SOLID principles, clearly separating the domain, application, infrastructure, and API layers to keep the code scalable and maintainable.

The solution uses MediatR to implement the CQRS pattern, allowing for the separation of read and write operations. It also integrates AutoMapper for mapping between entities and DTOs, FluentValidation for data validation, and Entity Framework Core with an in-memory database to simplify execution during testing.

For security, the API uses JWT Authentication along with ASP.NET Identity for user and role management. Additionally, several design patterns such as Repository, Strategy, and Factory are implemented to help structure the system logic. The project also includes Serilog for log logging and correlation ID management for request tracking.

To run the project, simply execute the command:

dotnet run --project src/EmployeeManagement.API

The application will start using an in-memory database. An initial administrator user is also included:

Username: admin

Password: Admin123!

Role: Admin

Main Endpoints

Authentication

POST /api/auth/register – Allows new users to register.

POST /api/auth/login – Allows users to log in and generate a JWT token for authentication.

Employee Management

GET /api/employees – Retrieves a list of all employees.

GET /api/employees/{id} – Retrieves a specific employee by their ID.

GET /api/employees/department/{departmentId}/with-projects – Retrieves the employees in a department who are involved in at least one project.

POST /api/employees – Creates a new employee (Admin role only).

PUT /api/employees/{id} – Updates an employee's information (Admin role only).

DELETE /api/employees/{id} – Deletes an employee (Admin role only).


Login et token token
{
  "username": "admin",
  "password": "Admin123!"
}

Add employye
{
  "name": "Ana",
  "currentPosition": 1,
  "salary": 4000,
  "departmentId": 1
}
Position
•	0 → nivel inicial / junior
•	1 → nivel intermedio
•	2 → seniorLogin et token token
{
  "username": "admin",
  "password": "Admin123!"
}

Add employye
{
  "name": "Ana",
  "currentPosition": 1,
  "salary": 4000,
  "departmentId": 1
}
Position
•	0 → nivel inicial / junior
•	1 → nivel intermedio
•	2 → senior

