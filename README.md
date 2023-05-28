# Microservices-eCommerce
> Implementing an eCommerce application with Domain-Driven Design and CQRS in .Net Core.

## 1. The Purposes of This Project
- The **Microservices** with **DDD** implementation.
- Correct separation of bounded contexts.
- Communications between bounded contexts through asynchronous **RabbitMQ** and **gRPC**
- Example of simple **CQRS** implementation and **Event Driven Architecture**.
- Managing distributed transactions with **Sage Pattern*
- **Event Sourcing** for Auditability and Temporal queries
- Using **Best Practice** and **Design Patterns**.

## 2. Technologies and Libraries
- ✔️ **[`.NET Core 6`](https://dotnet.microsoft.com/download)** - .NET aspnet-api-versioning)** - Set of libraries which add service API versioning to ASP.NET Web API, OData with ASP.NET Web API, and ASP.NET Core
- ✔️ **[`EF Core`](https://github.com/dotnet/efcore)** - Modern object-database mapper for .NET. It supports LINQ queries, change tracking, updates, and schema migrations
- ✔️ **[`CAP`](https://github.com/dotnetcore/CAP)** - An EventBus with local persistent message functionality for system integration in SOA or Microservice architecture
- ✔️ **[`MassTransit`](https://masstransit.io/)** 
- ✔️ **[`RabbitMQ`](https://masstransit.io/)** 
- ✔️ **`Azure Devops`** Using Azure App Service to deploy services into cloud environment
- ✔️ **[`FluentValidation`](https://github.com/FluentValidation/FluentValidation)** - Popular .NET validation library for building strongly-typed validation rules
- ✔️ **[`Swagger & Swagger UI`](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)** - Swagger tools for documenting API's built on ASP.NET Core
- ✔️ **[`Serilog`](https://github.com/serilog/serilog)** - Simple .NET logging with fully-structured events
- ✔️ **[`Polly`](https://github.com/App-vNext/Polly)** - Polly is a .NET resilience and transient-fault-handling library that allows developers to express policies such as Retry, Circuit Breaker, Timeout, Bulkhead Isolation, and Fallback in a fluent and thread-safe manner
- ✔️ **[`Ocelot`](https://github.com/ThreeMammals/Ocelot)** - API Gateway created using .NET Core
- ✔️ **[`Identity Server 6`](https://duendesoftware.com/products/identityserver)** - Implement identity server for identity service to authorize, authenticate and SSO


## 3. Structure of services
Our clean architecture in each service consists of 4 main parts:
- **API** - This layer responsible for hosting microservice on .net core webapi and using swagger for documentation.
- **Application** - Here you should find the implementation of use cases related to the module. the application is responsible for requests processing. Application contains use cases, domain events, integration events and its contracts, internal commands.
- **Domain** - Domain Model in Domain-Driven Design terms implements the applicable Bounded Context
- **Infrastructure** - This is where the implementation of secondary adapters should be. Secondary adapters are responsible for communication with the external dependencies.
infrastructural code responsible for module initialization, background processing, data access, communication with Events Bus and other external components or systems
