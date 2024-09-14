Café Employee Management System
Backend (.NET 8)
Development Environment:
You can use Visual Studio 2022 or Visual Studio Code for backend development.
Ensure that either Visual Studio 2022 or Visual Studio Code is installed on your machine.
Internet Connection:
Make sure your machine is connected to the internet before building the application.
Build the Project:
Before running the application, build the .NET 8 Web API project.

If you face any issues with NuGet packages, ensure you are connected to the internet while building.

Update the connection string in the appsettings.json file. Here's a sample configuration:

json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "Server=ServerNameHere;Database=CafeEmployee;User Id=YourUserId;Password=YourPassword;TrustServerCertificate=True;"
}
To run the project:

In Visual Studio Code, use the following command:
bash
Copy code
dotnet run
In Visual Studio 2022, click the Run button in the navigation.
Frontend (React / Vite.js)
Development Environment:
The frontend is developed using React with Vite.js.

Ensure that Visual Studio Code is installed.

Update the API base URL in your code to match your machine's configuration:

javascript
Copy code
const API_BASE_URL = 'http://localhost:26655/api'; // Set your base URL here
Node.js and npm:
Ensure the following versions are installed on your machine:
Node.js: v20.17.0
npm: v10.8.2
Run the Project:
To run the frontend project, use the following command:
bash
Copy code
npm run dev
Follow these steps to set up the Café Employee Management System. If you encounter any issues or need further assistance, feel free to ask!

