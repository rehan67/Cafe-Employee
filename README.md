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

In Visual Studio Code, you can run the project using the following command:

```runcmd
dotnet run
```
In Visual Studio 2022, click the “Run” button in the navigation to start the project.

### Frontend (React/Vite.js)
Development Environment:
1. For the frontend, you’ll be using React and Vite.js.
2). Make sure you have Visual Studio Code installed.
Node.js and npm:
1). Ensure that your machine has the following versions installed:
  1).Node.js v20.17.0
  2).npm v10.8.2
Update API Base URL:
In the frontend, change the base API URL according to your backend setup. You can find this in the configuration file or directly in the code:

        const API_BASE_URL = 'http://localhost:26655/api'; // Set your base URL here

Run the Project:
Open a terminal in the frontend folder and run the following command to start the development server:

```runcmd
npm run dev
```
