# Backend (.NET 8)

Development Environment:
      •	You can use Visual Studio 2022 or Visual Studio Code for the backend development.
      •	Make sure you have either of these tools installed on your machine.
Internet Connection:
      •	Before building the application, ensure that your machine is connected to the internet.
Build the Project:
      •	Before running the application, build the .NET 8 Web API project.
      •	If you face any Nuget packages issue make sure fore building connect to internet.
      •	Go to the appsetting.json change the connectionstring below sample
    "ConnectionStrings": {
      "DefaultConnection": "Server=ServerNamehere;Database=CafeEmployee;User Id=idhere;Password=passwordhere; TrustServerCertificate=True;"
    }, 
      •	In Visual Studio Code, you can run the project using the following command:
        o	dotnet run
      •	In Visual Studio 2022, click the “Run” button in the navigation.

# Frontend (React/Vite.js)
Development Environment:
      •	For the frontend, you’ll be using React and Vite.js.
      •	Make sure you have Visual Studio Code installed.
      •	Change the below link according to you machine
      •	const API_BASE_URL = 'http://localhost:26655/api'; // Set your base URL here
Node.js and npm:
      •	Ensure that your machine has the following versions installed:
          o	Node.js v20.17.0
          o	npm v10.8.2
Run the Project:
      •	To run the frontend project, execute the following command:
          o	npm run dev

Remember to follow these steps in order, and you’ll be on your way to setting up your Café Employee Management system! If you encounter any issues or need further assistance, feel free to ask.
