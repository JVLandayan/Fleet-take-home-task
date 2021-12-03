# Fleet-take-home-task
Changes:
Made a new project solution and refactored everything including the folder structure.

Context
Stored the context class into a class library, made a new folder for each model that is used for structuring db, segregated the configuration for each entity into separete classes (fluent api)

UOW
Implemented the unit of work pattern so that instead of context being injected on the service, it will just inject the UOW class. this is for saving or disposing changes on the ChangeTracker

DTO
Stored all requests, response models into separate class libraries

Data
This class library contains the generic repository and all repositories of each entity that inherits from IBase and Base repository

Service
This class library contains all services for each entities.

Util
This class library contains the IOC registrations. This was made so that the Program.cs file won't be extremely populated.

/*I didn't use async programming in this project since i didn't have the time to learn parallel programming in C# in time for me to configure the take-home project.
*/

Use dotnet run or open the solution on your Visual Studio editor


