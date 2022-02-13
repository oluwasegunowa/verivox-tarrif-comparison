# Verivox task
Tarrif Comparison


.NET Core 5.0

Microservices
-----------------------------------------------

Find below frameworks and tools used in the development;
 
1. Domain Driven Design & Reposiotry Pattern
2. Documentation - Swagger
3. Logging - Serilog
4. Database -  Postgress
5. Unit Testing - xUnit, Moq
6. Global exception handling


Layers
---------------------------------------------
For the task, I tried to Keep It Simple by using folders to separate the layers.
1. Presentation -> Controllers, Views (if any)
2. Application -> Business Logic
3. Domain -> Global Error Handling, Extensions
4. Infrastructure -> Database


Assumptions
-------------------------------------------
1. Supplied Consumption must be greater than 0,
2. More products can be created in future
To do that,  just implement ITarrifModel, provide the name and calculation. 


- BasicConsumptionTarrif
- PackagedConsumptionTarrif






Running Migrations on the database
--------------------------------------
1. Provide valid postgress database connection string and run the appliaction for the database to be automatically created.


Host={Server_Here};user id={Database_User};password={Database_Password};database={Database_Name};Port=5432


Provide a valid database connection string, when you run the application
the database is created automatically.

2. You can also decide to run migrations using the following;
dotnet ef migrations add Initial 
dotnet ef database update




Running the application
--------------------------------------
dotnet run


Running Test
--------------------------------------
dotnet test 





