# Cafe Employee Management System

This project consists of a .NET 8 Web API backend and a React frontend built with Vite.js. The guide below provides detailed steps to set up both the backend and frontend, along with deployment instructions using Docker.

---

## Backend (.NET 8)

### Development Environment:
- You can use either Visual Studio 2022 or Visual Studio Code for backend development.
- Ensure that you have one of these tools installed on your machine.

### Internet Connection:
- Before building the application, ensure that your machine is connected to the internet.

### Build the Project:
1. Build the .NET 8 Web API project.
2. If you face any NuGet package issues, make sure your machine is connected to the internet.
3. Update the `appsettings.json` file with your database connection details. Below is a sample connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=ServerNamehere;Database=CafeEmployee;User Id=idhere;Password=passwordhere; TrustServerCertificate=True;"
}
```

```In Visual Studio Code, you can run the project using the following command:
dotnet run
