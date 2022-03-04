 //create A Solution File and add all projects to the solution,
 
 dotnet new sln --name AccountOpeningSolution
 dotnet sln add ./StanbicIBTC.AccountOpening.Domain/StanbicIBTC.AccountOpening.Domain.csproj
 dotnet sln add ./StanbicIBTC.AccountOpening.Data/StanbicIBTC.AccountOpening.Data.csproj
 dotnet sln add ./StanbicIBTC.AccountOpening.Service/StanbicIBTC.AccountOpening.Service.csproj
 dotnet sln add ./StanbicIBTC.AccountOpening.API/StanbicIBTC.AccountOpening.API.csproj

//Restore all dependencies in the solution
dotnet restore 


 //Build The Solution
 dotnet build AccountOpeningSolution.sln


//Build a particular project : Change into Project directory and run:
dotnet build

//Run your app and do instant hot reloading (dotnet watch runs Production Environment by default. run the ENVIRONMENT export 
//Before runing dotnet watch for hot reload.
)
run export ASPNETCORE_ENVIRONMENT=Development
dotnet watch

//Run The API : Change into the API directory (./StanbicIBTC.AccountOpening.API) and run 
dotnet run


//To Add A Package 
dotnet add package [package full description]
e.g dotnet add package Microsoft.EntityFrameworkCore.Design

//CORS Enabled in Project. Consider Removing it from Program.cs if you have a specific reasons not to use it 



 //To Create Different Project Types

dotnet new classlib -n [ProjectName].[Data] — class library 
dotnet new webapi -n [ProjectName].[API] - Web API.
dotnet new console --name Foo --output Foo  --Console App

