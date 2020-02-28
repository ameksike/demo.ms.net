# Simple Demo App with ASP.NET Core, Angular, Entity Framework and MVC pattern design

# REST API
- GET /api/person


# create new project
- dotnet new --install Microsoft.DotNet.Web.Spa.ProjectTemplates:2.1.1
- dotnet new sln
- dotnet new angular -o ASP.NET.Angular
- dotnet sln add ASP.NET.Angular/ASP.NET.Angular.csproj
- -------------------------------------------------------
- dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
- dotnet build 
- dotnet aspnet-codegenerator -h
- dotnet aspnet-codegenerator controller -name PersonController -api -m Person -dc ApplicationDbContext -outDir Controllers
- ------------------------------------------------------ 
- Add-Migration Persons
- update-database
