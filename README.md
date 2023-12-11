# Solution Organization
I prefer to follow a clean code architecture for organizing my solutions. Primarily, I structure my projects into three folders:
1. docs: This folder stores useful notes, documents, and content supporting the easy setup of the project for local usage.
2. src: This folder houses the source code for the entire application.
3. tests: Here, I place tests covering all essential parts of the application.
<img width="282" alt="soloution" src="https://github.com/lukadlm97/DTI-Puzzle/assets/36825550/b1b10727-cd00-4a94-a0e9-d0d5ea26cf8a">

## Src Folder Structure
Within the Src folder, there is a Core folder where the heart of the entire application resides—domain logic and business logic at an appropriate level of abstraction. I aim for this part to be independent of all other components, allowing you to achieve the main application goals.

### Core
In the project named DTI.Puzzle.Domain, declarations of domain objects and commonly used abstractions for communication with business logic are located.
In the project DTI.Puzzle.Application, I implement all the features the application allows. This includes specifications for DTO objects, AutoMapper profiles, Data Access specifications, etc. The libraries used in this part include:
- FluentValidation: A useful library for validating value types of business entities.
- MediatR: Used to achieve loose coupling of the business logic layer, facilitating easy testing.
- AutoMapper: Utilized for easy transformation of domain objects to data transfer objects.
- Throughout this project, I adhere to the CQRS principle to achieve benefits such as Separation of Concerns, Scalability, and Flexibility

### Infrastructure
This layer is responsible for implementing infrastructure concerns, such as data access, external services, and other technical details. The library used here is:
- EntityFrameworkCore: This library provides an abstraction over the relational database, allowing me to focus on business-related aspects while handling connections and query building.

### App
This section includes an ASP.NET WebApplication implemented using MVC orientation, primarily focused on developing a backend API. Although this technology isn't my primary expertise, I find it essential to visually showcase all aspects of my work.

### API
I've implemented basic CRUD operations using two different approaches: a classical RESTful API with controllers and a gRPC service, offering a powerful solution for communication when using HTTP2+.

### Tests
In this section, I've created a unit test wrapper around the application project (business logic part) covering the complete logic with a xUnit test suite. The libraries used include:
- xUnit: A powerful tool for mocking all assets.
- Moq: Used for mocking objects.

## Demo
### Notable configuration
In case that you want test it at your machine, and don’t like to use some persitane storage, just in memory data access variant, just set at appsettings.Development EnablePersistence to false as it below:

"Storage": {
 "EnablePersistence": false,
 "ConnectionStrings": {
 "DefaultConnection": ""
 }
}

In case that you setup my db schema and content at your db system, you can set EnablePersistence to true, and add default connection string at previous json. Then you will be able to use both of APIs and App on right way.

Web
When you open the app in your browser, you should see the home page as shown below. 
<img width="960" alt="home-screen" src="https://github.com/lukadlm97/DTI-Puzzle/assets/36825550/fa72e8c7-9013-491a-8f33-576a9baeeb54">
After this, you can select the index page for glossary items from the menu option called 'Glossary,' and you should see the next page. 
<img width="958" alt="all-item" src="https://github.com/lukadlm97/DTI-Puzzle/assets/36825550/be39c3ab-7cc7-46ae-b975-48726d98e2bc">
You can observe that I have implemented paging options, as illustrated below.
<img width="958" alt="paginator-visualization" src="https://github.com/lukadlm97/DTI-Puzzle/assets/36825550/edaf2220-e8b7-49b3-8e51-c22bc48d38c2">
If you try to move to the next page, you can see the items and the appropriate URL. 
<img width="960" alt="paginator-url" src="https://github.com/lukadlm97/DTI-Puzzle/assets/36825550/b805faef-decb-4c32-a532-883eac5ba67c">
You can attempt to perform a search by term, also with paging options. 
<img width="959" alt="search-result" src="https://github.com/lukadlm97/DTI-Puzzle/assets/36825550/0562c389-30f6-48e5-a67f-c8e7365a5e4d">
If you want to create a new glossary item, you can use the button and form. 
<img width="959" alt="add-new-glossary-item" src="https://github.com/lukadlm97/DTI-Puzzle/assets/36825550/c174c5cd-8e90-4ca5-8add-6e88859adf82">
Additionally, we have validation for new items.
<img width="960" alt="add-new-validation" src="https://github.com/lukadlm97/DTI-Puzzle/assets/36825550/94f8516a-253f-49c1-b368-d8af52040e59">
If you successfully complete that, you will see the next page. 
<img width="960" alt="add-new-success" src="https://github.com/lukadlm97/DTI-Puzzle/assets/36825550/a3b910de-e073-46a3-ac38-bfcd75b88c8d">
Also, if an item already exists in the glossary, you will be unable to add a new item. 
<img width="958" alt="add-new-item-that-exist" src="https://github.com/lukadlm97/DTI-Puzzle/assets/36825550/8586fc53-dc30-4d01-a272-92fcd15a26bc">
In case you want to update some item, you will see the same form with exactly the same validation. 
<img width="960" alt="edit-item-form" src="https://github.com/lukadlm97/DTI-Puzzle/assets/36825550/87525b24-7de1-4762-8733-e7ef2deb4dac">
If you want to delete some item, you can use the next button.
<img width="958" alt="delete-visual" src="https://github.com/lukadlm97/DTI-Puzzle/assets/36825550/08381a39-2af4-4bf0-a285-8a8692ff693c">
After it is performed, you will see the next page
<img width="960" alt="delete-success" src="https://github.com/lukadlm97/DTI-Puzzle/assets/36825550/55147374-9eaf-4dc1-b893-a2a2bd09841f">


## References:
- Data transfer objects - https://medium.com/codex/start-using-c-records-for-dtos-instead-of-regularclasses-1f84bd5997ca
- Milan Jovanovic clean code projects - https://github.com/m-jovanovic/event-reminder and https://github.com/m-jovanovic/rally-simulator
- Leave Management System - SOLID and Clean Architecture -https://github.com/trevoirwilliams/HR.LeaveManagement.NET6
- CQRS - https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs
- MediatR - https://github.com/jbogard/MediatR and https://medium.com/dotnet-hub/use-mediatrin-asp-net-or-asp-net-core-cqrs-and-mediator-in-dotnet-how-to-use-mediatr-cqrs-aspnetcore5076e2f2880c
- Automapper - https://automapper.org/
- Fluent validation - https://docs.fluentvalidation.net/en/latest/aspnet.html
- xUnit - https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
- Entityframework - https://learn.microsoft.com/en-us/ef/core/providers/sql-server/?tabs=dotnetcore-cli and https://learn.microsoft.com/en-us/ef/core/get-started/overview/firstapp?tabs=netcore-cli
- Grpc - https://github.com/grpc/grpc-dotnet and https://learn.microsoft.com/enus/aspnet/core/grpc/?view=aspnetcore-8.0
- Asp.net MVC - https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/startmvc?view=aspnetcore-8.0&tabs=visual-studi
