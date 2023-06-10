# OverCloudAirways: A Collaborative Learning Platform for a DDD application

Welcome to **OverCloudAirways**! This project serves as a collaborative learning platform for developers interested in exploring cutting-edge technologies, architectural patterns, and best practices in software development. Inspired by flight booking systems, the primary focus is on providing a comprehensive sample for developers to study, contribute, and learn from.

OverCloudAirways showcases _serverless_ technologies and core architectural patterns, including **Domain-Driven Design (DDD)**, **CQRS**, **Microservices**, and **Event-Driven Architecture (EDA)**. We emphasize that these technologies and patterns are not a silver bullet, and the decisions made in this project depend on multiple factors. Our goal is to create a resource that encourages collaboration and helps developers improve their skills, explore new ideas, and gain insights into building modern, scalable, and maintainable enterprise applications.

**Your contributions are welcome!** Whether you're a seasoned developer or a newcomer, we invite you to join us in our quest to learn and share knowledge about building better software.

## Table of Contents

1. [Architecture and Design](#architecture-and-design)
   - [Domain-Driven Design (DDD)](#domain-driven-design-ddd)
      - [Ubiquitous Language](#ubiquitous-language) 
      - [Bounded Contexts](#bounded-contexts)
      - [Event Storming](#event-storming)
      - [Aggregates](#aggregates)
      - [Domain Events](#domain-events)
      - [Business Rules](#business-rules)
      - [Domain Services](#domain-services)
      - [Domain Policies](#domain-policies)
   - [CQRS](#cqrs)
      - [Commands](#commands)
      - [Command Validators](#command-validators)
      - [Command Handlers](#command-handlers)
      - [Queries](#queries)
      - [Query Handlers](#query-handlers)
   - [Event Sourcing](#event-sourcing)
      - [Event Store](#event-store)
      - [Event Streams](#event-streams)
      - [Event Versioning](#event-versioning)
      - [Snapshots](#snapshots)
   - [Microservices and Event-Driven Architecture](#microservices-and-event-driven-architecture)
      - [Microservices](#microservices)
      - [Event-Driven Architecture](#event-driven-architecture)
      - [Event-Driven Distributed Transactions Example](#event-driven-distributed-transactions-example)
   - [Authentication and Authorization](#authentication-and-authorization)
      - [Choosing Azure AD B2C](#choosing-azure-ad-b2c)
      - [Role Based Access Control](#role-based-access-control)
      - [Secure Communication](#secure-communication)
      - [Handling Authentication in Microservices Architecture](#handling-authentication-in-microservices-architecture)
   - [Clean Architecture](#clean-architecture)
   - [Composition Root](#composition-root)
   - [Outbox Pattern](#outbox-pattern)
   - [Unit of Work Pattern](#unit-of-work-pattern)
   - [Enumeration](#enumeration)
2. [Key Features and Components](#key-features-and-components)
3. [Technologies and Libraries](#technologies-and-libraries)
   - [C#](#c)
   - [MediatR](#mediatr)
   - [Polly](#polly)
   - [Serilog](#serilog)
   - [xUnit](#xunit)
   - [Autofac](#autofac)
   - [Newtonsoft.Json](#newtonsoftjson)
4. [Azure Services and Technologies](#azure-services-and-technologies)
   - [Azure Functions](#azure-functions)
   - [Azure Service Bus](#azure-service-bus)
   - [Azure Cosmos DB](#azure-cosmos-db)
   - [Azure Key Vault](#azure-key-vault)
   - [Azure Application Insights](#azure-application-insights)
   - [Azure API Management](#azure-api-management)
   - [Azure Active Directory B2C](#azure-active-directory-b2c)
5. [Testing](#testing)
   - [Unit Testing](#unit-testing)
   - [Integration Testing](#integration-testing)
   - [Testing Tools and Patterns](#testing-tools-and-patterns)
      - [xUnit](#xunit)
      - [Builder Pattern](#builder-pattern)
      - [DB Sandboxing](#db-sandboxing)
      - [Mocking Library](#mocking-library)
      - [Shared Context](#shared-context)
6. [CI/CD Pipeline and Deployment](#cicd-pipeline-and-deployment)
7. [Contributing and Collaboration](#contributing-and-collaboration)
8. [Changelog and Versioning](#changelog-and-versioning)
9. [License](#license)
10. [Acknowledgments](#acknowledgments)
11. [Further Learning and Useful Resources](#further-learning-and-useful-resources)

## Architecture and Design

### Domain-Driven Design (DDD)

> "Domain-driven design is an approach to software development that centers the development on programming a domain model that has a rich understanding of the processes and rules of a domain." - Eric Evans, author of the book "Domain-Driven Design: Tackling Complexity in the Heart of Software"

  <p align="center" width="100%">
  <img width="449" alt="image" src="https://user-images.githubusercontent.com/7968282/235445663-12fc56e5-b4bc-47e0-9619-7682bf2550f4.png">
  </p>

  OverCloudAirways employs Domain-Driven Design (DDD) principles to build a solid foundation for the system's architecture. We chose DDD for this project because it provides a proven set of practices and patterns for tackling complexity in software systems, allowing us to create a maintainable and scalable solution that serves as a valuable learning resource for developers.
  By adhering to DDD principles, OverCloudAirways aims to provide a maintainable and scalable solution that serves as a valuable learning resource for developers.

In our implementation of DDD, we focus on the following key aspects:

- #### **Ubiquitous Language**
  Establishing a shared language among developers that promotes effective communication and a common understanding of the domain. Although we don't have real domain experts for this project, we strive to use consistent terminology and naming conventions that accurately represent domain concepts.

- #### **Bounded Contexts**
  Defining logical boundaries that separate different areas of the domain, promoting modularity and limiting the scope of domain models. In OverCloudAirways, we have identified the following Bounded Contexts:

   - **Identity**: Responsible for registering users, managing authentication, and handling user profiles.
   - **Payment**: Handles payment processing, invoicing, and payment-related notifications.
   - **Booking**: Takes care of flight booking processes, including seat reservation, ticket issuance, and booking confirmation.
   - **CRM**: Manages customer relationship management features, such as customer feedback, support requests, and customer communication.

![image](https://user-images.githubusercontent.com/7968282/231226243-a22d84ae-c77f-4e80-90a3-cbcddef419da.png)

These Bounded Contexts work together to provide a comprehensive flight booking system that covers essential aspects of the business domain.

- #### **Event Storming**
  Event Storming is a collaborative modeling technique that brings together developers, domain experts, and other stakeholders to explore and model a domain using events, aggregates, commands, and other domain elements. This technique helps to create a shared understanding of the domain, identify potential inconsistencies, and uncover hidden complexities.

  In the development of the OverCloudAirways project, we used Event Storming to map out the domain, its processes, and its interactions. The image below, taken from our Miro board, demonstrates how Event Storming helped shape the project's design:

  Overview:
  <img width="1462" alt="image" src="https://github.com/m-khooryani/OverCloudAirways/assets/7968282/60ab5963-a66f-4e18-8807-7b0116ac4e9c">
  
  [Download High Quality Image](https://github.com/m-khooryani/OverCloudAirways/assets/7968282/092f831c-a2d7-4fee-9eb7-90cc87ca39ce)
  
  Identity:
  
  <img width="881" alt="image" src="https://github.com/m-khooryani/OverCloudAirways/assets/7968282/a3315e2b-7c1e-418d-afe3-d63fa3a5318b">

  Booking:
  
  <img width="1641" alt="image" src="https://github.com/m-khooryani/OverCloudAirways/assets/7968282/35040d25-c1aa-4558-8479-52b101eedd70">
  
  Payment:
  
  <img width="1721" alt="image" src="https://github.com/m-khooryani/OverCloudAirways/assets/7968282/8390da09-f97a-4824-975f-a687a98a9016">

  CRM:
  
  <img width="936" alt="image" src="https://github.com/m-khooryani/OverCloudAirways/assets/7968282/2897e382-50f2-42cf-b06d-d175b4455051">


  Through Event Storming, we were able to visualize the relationships between different parts of the domain and ensure that our design decisions aligned with the needs of the project.


- #### **Aggregates**

  In Domain-Driven Design, an Aggregate is a cluster of domain objects (entities and value objects) that are treated as a single unit. The Aggregate Root is an entity within the aggregate that serves as the entry point for interactions with the aggregate.

  In our project, `FlightBooking` is an example of an Aggregate Root:

  ``` csharp
  public class FlightBooking : AggregateRoot<FlightBookingId>
  {
      public CustomerId CustomerId { get; private set; }
      public FlightId FlightId { get; private set; }
      private List<Passenger> _passengers;
      public IReadOnlyCollection<Passenger> Passengers => _passengers.AsReadOnly();
      public FlightBookingStatus Status { get; private set; }
  
      private FlightBooking()
      {
          _passengers = new List<Passenger>();
      }
  
      public static async Task<FlightBooking> ReserveAsync(
          FlightBookingId flightBookingId,
          CustomerId customerId,
          Flight flight,
          IReadOnlyList<Passenger> passengers)
      {
          await CheckRuleAsync(new FlightBookingCanOnlyBeReservedForFlightsHasNotYetDepartedRule(flight));
  
          var @event = new FlightBookingReservedDomainEvent(flightBookingId, customerId, flight.Id, passengers);
  
          var flightBooking = new FlightBooking();
          flightBooking.Apply(@event);
  
          return flightBooking;
      }
  
      public async Task ConfirmAsync(IAggregateRepository repository)
      {
          await CheckRuleAsync(new OnlyReservedFlightBookingsCanBeConfirmedRule(Status));
          await CheckRuleAsync(new FlightBookingCanOnlyBeConfirmedForFlightsHasNotYetDepartedRule(repository, FlightId));
  
          var @event = new FlightBookingConfirmedDomainEvent(Id);
          Apply(@event);
      }
  
      public async Task CancelAsync(IAggregateRepository repository)
      {
          await CheckRuleAsync(new FlightBookingCanOnlyBeCancelledForFlightsHasNotYetDepartedRule(repository, FlightId));
  
          var @event = new FlightBookingCancelledDomainEvent(Id, FlightId, Passengers.Count);
          Apply(@event);
      }
  
      protected void When(FlightBookingReservedDomainEvent @event)
      {
          Id = @event.FlightBookingId;
          CustomerId = @event.CustomerId;
          FlightId = @event.FlightId;
          _passengers = new List<Passenger>(@event.Passengers);
          Status = FlightBookingStatus.Reserved;
      }
  
      protected void When(FlightBookingConfirmedDomainEvent _)
      {
          Status = FlightBookingStatus.Confirmed;
      }
  
      protected void When(FlightBookingCancelledDomainEvent _)
      {
          Status = FlightBookingStatus.Cancelled;
      }
  }
  ```

  The `FlightBooking` aggregate contains the following elements:

  - **Properties**: It holds information about the flight booking, such as `CustomerId`, `FlightId`, a collection of `Passengers`, and the booking `Status`.
  - **Private constructor**: The private constructor ensures that instances of `FlightBooking` can only be created through factory methods.
  - **Factory methods and behavior methods**: `ReserveAsync`, `ConfirmAsync`, and `CancelAsync` are methods that represent domain actions and enforce business rules. They either create new instances of the aggregate or modify its internal state.
  - **Internal event handlers**: The `When` methods are responsible for applying changes to the aggregate's state based on domain events. These methods are called in response to domain events being applied to the aggregate. They are a key part of the event-sourcing process, as they ensure that the aggregate's state is consistent with its event history.

  This example demonstrates how the Aggregate Root pattern can be used to encapsulate domain logic and maintain consistency within an aggregate.


- #### **Domain Events**

  Domain events capture and communicate significant state changes in the domain, allowing for better decoupling between components. They represent something that has happened within the domain and can be used to trigger side effects, enforce consistency, or update read models.

  There are a few conventions and best practices surrounding domain events:
  - They are named in the past tense, reflecting that the event has already occurred.
  - They are immutable, meaning that once created, their properties cannot be changed.
  
  In our project, we use domain events extensively to implement event-sourcing patterns. One example is the `FlightBookingReservedDomainEvent`:

  ``` csharp
  public record FlightBookingReservedDomainEvent(
      FlightBookingId FlightBookingId,
      CustomerId CustomerId,
      FlightId FlightId,
      IReadOnlyList<Passenger> Passengers) : DomainEvent(FlightBookingId);
  ```

  This event is triggered when a flight booking has been reserved. It contains essential information about the booking, such as `FlightBookingId`, `CustomerId`, `FlightId`, and a list of `Passengers`. The event can then be used to trigger side effects, update read models, or enforce consistency within the system.

  We used the `record` keyword to define the domain event in C#. Records provide value-based equality and immutability, making them a suitable choice for modeling domain events.

  Domain events help to model the domain more accurately and make the system more flexible, extensible, and maintainable.
  
- #### **Business Rules**
  Domain-Driven Design emphasizes the importance of encapsulating complex business logic within the domain layer. A useful technique to accomplish this is by implementing Business Rules as explicit, testable classes. This approach helps to decouple business rules from the rest of the domain model, making the code more modular, maintainable, and expressive.
  
  ``` csharp
  internal class FlightBookingCanOnlyBeCancelledForFlightsHasNotYetDepartedRule : IBusinessRule
  {
      private readonly IAggregateRepository _repository;
      private readonly FlightId _flightId;
  
      public FlightBookingCanOnlyBeCancelledForFlightsHasNotYetDepartedRule(
          IAggregateRepository repository,
          FlightId flightId)
      {
          _repository = repository;
          _flightId = flightId;
      }
  
      public string TranslationKey => "FlightBooking_Can_Only_Be_Cancelled_For_Flights_Has_Not_Yet_Departed";
  
      public async Task<bool> IsFollowedAsync()
      {
          var flight = await _repository.LoadAsync<Flight, FlightId>(_flightId);
          return flight.Status.HasNotYetDeparted();
      }
  }
  ```
  
  In the example above, the business rule `FlightBookingCanOnlyBeCancelledForFlightsHasNotYetDepartedRule` is encapsulated in a separate class, rather than being embedded within an if condition.(Inspired by [Modular Monolith with DDD](https://github.com/kgrzybek/modular-monolith-with-ddd)) This approach has several advantages:

  - Readability: Expressing the rule as a separate class with a descriptive name makes it easier for developers to understand the intent and the purpose of the rule.
  - Testability: Encapsulating the business rule in a separate class makes it simpler to test the rule in isolation, ensuring that it behaves as expected.
  - Reusability: By implementing the business rule as a standalone class, it can be reused across multiple aggregates or services without duplication.
  - Domain-Specific Language (DSL): Defining the business rules as separate classes contributes to building a domain-specific language that more closely mirrors the ubiquitous language of the domain. This helps to promote a shared understanding of the domain among developers and domain experts.

- #### **Domain Services**

  Domain services encapsulate domain-specific logic that doesn't naturally fit within aggregates or value objects. They represent behaviors or operations that require coordination between multiple domain objects or that depend on external resources, like databases.

  In our project, an example of a domain service is `IConnectedOrders`, which is responsible for finding orders that are connected to a specific product:

  ``` csharp
  public interface IConnectedOrders
  {
      Task<ReadOnlyCollection<OrderId>> GetConnectedOrderIds(ProductId productId);
  }
  ```

  The implementation of this domain service, `ConnectedOrders`, interacts with a CosmosDB instance through an `ICosmosManager`:

  ``` csharp
  internal class ConnectedOrders : IConnectedOrders
  {
      private readonly ICosmosManager _cosmosManager;
  
      public ConnectedOrders(ICosmosManager cosmosManager)
      {
          _cosmosManager = cosmosManager;
      }
  
      public async Task<ReadOnlyCollection<OrderId>> GetConnectedOrderIds(ProductId productId)
      {
          var sql = @$"
                      SELECT 
                      orders.OrderId AS {nameof(OrderModel.Id)}
                      FROM orders
                      WHERE 
                      orders.partitionKey = @productId 
          ";
          var queryDefinition = new QueryDefinition(sql)
              .WithParameter("@productId", productId);
          var orders = await _cosmosManager.AsListAsync<OrderModel>("readmodels", queryDefinition);
  
          return orders
              .Where(x => x.Id is not null)
              .Select(x => x.Id)
              .ToList()
              .AsReadOnly();
      }
  
      private class OrderModel
      {
          public OrderId Id { get; set; }
      }
  }
  ```

  The `GetConnectedOrderIds` method in `ConnectedOrders` uses a SQL query to fetch connected orders from the read model in CosmosDB. It then filters and converts the result into a list of `OrderId` objects.

  Domain services help to separate domain logic from infrastructure concerns and ensure that domain concepts are modeled accurately. They also promote the Single Responsibility Principle (SRP) by allowing aggregates and value objects to focus on their core responsibilities.

- #### **Domain Policies**:

  Domain Policies are a way to represent cross-cutting concerns and reactions to domain events that occur in the system. They define the relationship between a domain event and a corresponding action, usually in the form of a command.

  In the OverCloudAirways project, we have implemented Domain Policies to manage specific business rules. One example is the `LoyaltyProgramQualifiedForCustomerPolicy`, which reacts to the `LoyaltyProgramQualifiedForCustomerDomainEvent`:

  ``` csharp
  public class LoyaltyProgramQualifiedForCustomerPolicy : DomainEventPolicy<LoyaltyProgramQualifiedForCustomerDomainEvent>
  {
      public LoyaltyProgramQualifiedForCustomerPolicy(LoyaltyProgramQualifiedForCustomerDomainEvent domainEvent) 
         : base(domainEvent)
      {
      }
  }
  ```

  The policy is handled by the `ResetCustomerLoyaltyPointsPolicyHandler`, which enqueues a `ResetCustomerLoyaltyPointsCommand` when the policy is triggered:

  ``` csharp
  internal class ResetCustomerLoyaltyPointsPolicyHandler :
      IDomainPolicyHandler<LoyaltyProgramQualifiedForCustomerPolicy, LoyaltyProgramQualifiedForCustomerDomainEvent>
  {
      private readonly ICommandsScheduler _commandsScheduler;

      public ResetCustomerLoyaltyPointsPolicyHandler(ICommandsScheduler commandsScheduler)
      {
          _commandsScheduler = commandsScheduler;
      }

      public async Task Handle(LoyaltyProgramQualifiedForCustomerPolicy notification, CancellationToken cancellationToken)
      {
          var resetPointsCommand = new ResetCustomerLoyaltyPointsCommand(notification.DomainEvent.CustomerId);

          await _commandsScheduler.EnqueueAsync(resetPointsCommand);
      }
  }
  ```

  This example demonstrates how Domain Policies can be used to handle cross-cutting concerns in the domain and enforce business rules consistently.

  In our project, we have made a conscious decision to keep domain event handlers focused on reacting to events by queuing commands and not containing any state-changing logic. State changes are only performed by commands, which helps maintain a clear separation of concerns and ensures consistency within our domain model. This approach also improves the extensibility of the system, as adding new reactions to domain events only requires implementing new command handlers, rather than modifying existing domain event handlers.



### CQRS

  CQRS is an architectural pattern that promotes the separation of concerns by dividing a system's operations into two distinct categories: Commands and Queries. Commands are responsible for making changes to the system's state, while Queries are responsible for reading data from the system without altering its state.

The CQRS pattern brings several benefits to software systems, including improved scalability, maintainability, and flexibility. By segregating command and query responsibilities, developers can optimize each side independently to cater to the system's specific needs. This separation also makes it easier to reason about the system, as the roles and responsibilities of each component are clearly defined.

In the OverCloudAirways project, we have implemented the CQRS pattern to effectively manage the complexities of our domain while ensuring high performance and a robust architecture.

<img width="945" alt="image" src="https://user-images.githubusercontent.com/7968282/236449324-d278ea57-dc52-4b73-bdfd-24d86ea12c64.png">

- #### Commands 
  Commands are the part of the CQRS pattern responsible for changing the system's state. They represent the intent to perform an action, and typically contain the necessary data to carry out that action. Commands should be named in an imperative form, reflecting the desired outcome of the operation.
  In the OverCloudAirways project, we have implemented several commands to handle different operations. One example is the IssueTicketCommand, which is used to issue a ticket for a specific flight and customer:

  ``` csharp
  public record IssueTicketCommand(
      TicketId TicketId,
      FlightId FlightId,
      CustomerId CustomerId) : Command;
  ```
  This command includes the `TicketId`, `FlightId`, and `CustomerId`, which provide the necessary information to issue a ticket. When the command is handled, the appropriate action is taken to update the system's state, ensuring that a new ticket is issued and associated with the correct Flight and Customer.
  
- #### Command Validators
  Command Validators are responsible for ensuring that the data provided in a command is valid and adheres to the business rules before the command is executed. This helps maintain data integrity and prevents invalid operations from being performed in the system.
  
  ![image](https://user-images.githubusercontent.com/7968282/236455019-a8fe5a6c-9981-47e3-94c0-45cc1d599cea.png)

  In the OverCloudAirways project, we use the [FluentValidation](https://docs.fluentvalidation.net/en/latest/) library to create validators for our commands. One example is the `RegisterBuyerCommandValidator`, which validates the data in the `RegisterBuyerCommand`:

  ``` csharp
  internal class RegisterBuyerCommandValidator : AbstractValidator<RegisterBuyerCommand>
  {
      public RegisterBuyerCommandValidator()
      {
          RuleFor(x => x.BuyerId)
              .NotEmpty();
  
          RuleFor(x => x.FirstName)
              .NotEmpty()
              .MaximumLength(100);
  
          RuleFor(x => x.LastName)
              .NotEmpty()
              .MaximumLength(100);
  
          RuleFor(x => x.Email)
              .NotEmpty()
              .EmailAddress();
  
          RuleFor(x => x.PhoneNumber)
              .NotEmpty()
              .Must(BeAValidPhoneNumber)
              .WithMessage("Invalid phone number");
      }
  
      private bool BeAValidPhoneNumber(string phoneNumber)
      {
          return Regex.Match(phoneNumber, @"^\d{10}$").Success;
      }
  }
  ```
  This validator checks for various conditions, such as ensuring that the `BuyerId`, `FirstName`, `LastName`, `Email`, and `PhoneNumber` are not empty, and that they meet specific requirements like maximum length and format. If any of these conditions are not met, the validator will generate an error message, preventing the command from being executed until the issues are resolved. (TODO: link to pipeline)

- #### Command Handlers
  Command Handlers are responsible for processing commands and orchestrating the necessary steps to carry out the requested operation. They act as a bridge between the command and the aggregate, coordinating interactions with the domain model to execute the operation.

  In the OverCloudAirways project, we have designed Command Handlers to contain minimal code, focusing on delegating the work to the appropriate domain model. One example is the `PlaceOrderCommandHandler`, which handles the `PlaceOrderCommand`:

  ``` csharp
  class PlaceOrderCommandHandler : CommandHandler<PlaceOrderCommand>
  {
      private readonly IAggregateRepository _repository;
  
      public PlaceOrderCommandHandler(IAggregateRepository repository)
      {
          _repository = repository;
      }
  
      public override async Task HandleAsync(PlaceOrderCommand command, CancellationToken cancellationToken)
      {
          var order = await Order.PlaceAsync(
              _repository,
              command.OrderId,
              command.BuyerId,
              command.OrderItems);
  
          _repository.Add(order);
      }
  }
  ```
  This command handler interacts with the `IAggregateRepository` to load and save the Order aggregate. It calls the `Order.PlaceAsync` method, which encapsulates the domain logic for placing an order. The resulting aggregate is then tracked by the repository, but not yet saved to a persistent storage.

  We have chosen to create a custom `CommandHandler` base class instead of directly implementing [MediatR](https://github.com/jbogard/MediatR) interfaces for a few reasons:

  - It allows us to encapsulate any common behavior or logic required by all command handlers in a single place.
  - It helps maintain consistency in the structure of command handlers across the project, making it easier for developers to understand and navigate the codebase.

- #### Queries 
  Queries are used to retrieve information from the system without modifying the state of the domain. In the OverCloudAirways project, we follow the CQRS pattern, where queries are designed to be simple and optimized for read operations. Queries can return different types of information, such as DTOs (Data Transfer Objects), which are tailored to the specific needs of a particular use case.

  Here's an example of a query in the OverCloudAirways project:

  ``` csharp
  public record GetPurchaseInfoQuery(
      PurchaseId PurchaseId,
      CustomerId CustomerId) : Query<PurchaseDto>;
  ```
  
  This query is designed to retrieve purchase information for a given PurchaseId and CustomerId. The result of the query is a PurchaseDto, which is a Data Transfer Object containing the necessary information about the purchase.
  
- #### Query Handlers
  Query Handlers are responsible for executing queries and retrieving the requested data. In the OverCloudAirways project, we implement query handlers to interact with the underlying data storage and retrieve the necessary information to fulfill a query.

  Here's an example of a query handler in the OverCloudAirways project:
  
  ``` csharp
  internal class GetPurchaseInfoQueryHandler : QueryHandler<GetPurchaseInfoQuery, PurchaseDto>
  {
      private readonly ICosmosManager _cosmosManager;
  
      public GetPurchaseInfoQueryHandler(ICosmosManager cosmosManager)
      {
          _cosmosManager = cosmosManager;
      }
  
      public override async Task<PurchaseDto> HandleAsync(GetPurchaseInfoQuery query, CancellationToken cancellationToken)
      {
          var sql = @$"
                      SELECT 
                      purchase.PurchaseId        AS {nameof(PurchaseDto.PurchaseId)}, 
                      purchase.CustomerId        AS {nameof(PurchaseDto.CustomerId)}, 
                      purchase.CustomerFirstName AS {nameof(PurchaseDto.CustomerFirstName)}, 
                      purchase.CustomerLastName  AS {nameof(PurchaseDto.CustomerLastName)}, 
                      purchase.Date              AS {nameof(PurchaseDto.Date)}, 
                      purchase.Amount            AS {nameof(PurchaseDto.Amount)}
                      FROM purchase 
                      WHERE 
                      purchase.id = @purchaseId AND
                      purchase.partitionKey = @customerId
          ";
          var queryDefinition = new QueryDefinition(sql)
              .WithParameter("@purchaseId", query.PurchaseId)
              .WithParameter("@customerId", query.CustomerId);
          var purchase = await _cosmosManager.QuerySingleAsync<PurchaseDto>(ContainersConstants.ReadModels, queryDefinition);
  
          return purchase;
      }
  }
  ```
  
  In this example, the GetPurchaseInfoQueryHandler retrieves purchase information from the data store using a SQL query and the ICosmosManager to interact with Cosmos DB. The handler constructs a QueryDefinition with the necessary parameters and retrieves a single PurchaseDto that matches the given PurchaseId and CustomerId.
  
### Event Sourcing
Event Sourcing is an architectural pattern that focuses on storing the changes in the application state as a sequence of events, rather than the actual state. This allows us to reconstruct the state at any point in time, enabling features like auditing, debugging, and replaying scenarios.

In OverCloudAirways, we implement Event Sourcing to enable better scalability, traceability, and ease of debugging. Here are the key components and concepts in our Event Sourcing implementation:
  
  - #### Event Store
    The Event Store is the data storage system that holds the event streams. It is responsible for persisting events, as well as querying and retrieving event data. In OverCloudAirways, we use Azure Cosmos DB as our Event Store.
    
  - #### Event Streams
    An Event Stream is a sequence of events associated with a specific aggregate. It represents the history of state changes for a given aggregate, allowing us to recreate its state at any point in time by replaying the events. 
    
    We utilize the `AggregateId` as the PartitionKey for our event streams. This approach enables efficient querying and management of events related to a specific aggregate. By using the AggregateId as the PartitionKey, we can ensure that all events related to an aggregate are stored together, providing better performance when querying or replaying the event stream.

    Furthermore, to guarantee the uniqueness of events in our event store, we create a combination index on the `AggregateId` and the event `Version`. This combination index enforces that each event has a unique combination of AggregateId and Version, preventing the possibility of duplicate events being stored in the event stream. It also prevents conflicts that could arise when multiple instances of the application are trying to save events concurrently for the same aggregate, ensuring consistency and data integrity.
    
  - #### Event Versioning
    In the OverCloudAirways project, we handle event versioning by creating a new version of an event when the structure or content of the event changes. This helps maintain backward compatibility and allows us to evolve our system without breaking existing functionality. We use the IUpCastable interface to implement the necessary transformation logic between different versions of an event.
    
    TODO: add code
    
    The UpCast() method contains the transformation logic, which can be customized as needed.
By implementing event versioning, we can ensure that our Event Sourcing implementation remains robust and maintainable as our system evolves over time.

  - #### Snapshots
    (NOT IMPLEMENTED YET) Snapshots are used to optimize the performance of event-sourced systems. Instead of replaying the entire event stream to reconstruct an aggregate's state, snapshots store the aggregate's state at specific points in time, allowing us to reduce the number of events that need to be replayed.

### Microservices and Event-Driven Architecture
Microservices and Event-Driven Architecture (EDA) play a crucial role in the OverCloudAirways project, enabling a highly scalable and decoupled system. By combining these two concepts, we can take advantage of the best aspects of each approach to create a more resilient and adaptable application.

  - #### Microservices
    Microservices are an architectural pattern that breaks down a system into small, independently deployable, and loosely coupled services. Each microservice focuses on a specific business capability and can be developed, deployed, and scaled independently. This approach enhances the overall maintainability, flexibility, and scalability of the system.

    In the OverCloudAirways project, we have implemented microservices to handle various aspects of the application, such as flight booking, customer management, and payment processing. This separation of concerns allows each microservice to evolve independently, making it easier to adapt to changing business requirements and technologies.
    
  - #### Event-Driven Architecture
    Event-Driven Architecture (EDA) is a pattern that promotes asynchronous communication between components through events. Components can publish events to notify others about changes in their state or business processes, and other components can subscribe to these events to react accordingly.

    In our project, we use EDA to decouple microservices and ensure reliable communication between them. When a microservice needs to notify others about a change in its state or a business event, it publishes a domain event. Other interested microservices can subscribe to these events and take appropriate actions.

    This approach has several benefits:

      - Decoupling: Components are less dependent on each other, making it easier to evolve them independently and reducing the impact of changes.
      - Scalability: Since communication is asynchronous, components can scale independently, and the system can handle increased loads more efficiently.
      - Resilience: The system becomes more resilient to failures, as components can continue to function even when others are temporarily unavailable.

  - #### Event-Driven Distributed Transactions Example
    To illustrate how Microservices and EDA work together in our project, consider the following example of an event-driven distributed transaction:

      - `ConfirmOrder` event is published by the `Payment` microservice.
      - The `Payment` microservice reacts to `ConfirmOrder` event and publishes `AcceptInvoice` event.
      - The `CRM` microservice reacts to the `AcceptInvoice` event and publishes `MakePurchase` event.
      - The `CRM` microservice reacts to the `MakePurchase` event and publishes `CollectCustomerLoyaltyPoints` event.
      - The `CRM` microservice reacts to the `CollectCustomerLoyaltyPoints` event and publishes `EvaluateLoyaltyProgramForCustomer` event.
      - The `CRM` microservice reacts to the `EvaluateLoyaltyProgramForCustomer` event and publishes `LaunchPromotion` event.
      
      <p align="center" width="100%">
      <img width="860" alt="image" src="https://user-images.githubusercontent.com/7968282/232315552-90d2579b-2792-46e0-bda9-700c437f24e6.png">
      </p>
      
     This example demonstrates how EDA allows for asynchronous communication between microservices, enabling distributed transactions across multiple business capabilities. The event-driven nature of the transaction ensures that the system remains decoupled and can scale efficiently.

### Authentication and Authorization

Authentication and authorization are two crucial aspects of security in any application. Authentication is the process of verifying the identity of a user, while authorization involves checking the permissions of an authenticated user, determining what they are allowed to do within the application.

  - #### Choosing Azure AD B2C
    For OverCloudAirways, Azure AD B2C was chosen as the identity management solution. This choice was driven by the robust features of Azure AD B2C, including its capability to handle customer identities at scale, integration with various identity providers, and robust security features such as multi-factor authentication. Moreover, Azure AD B2C's flexibility to customize the user experience, and its comprehensive developer APIs, made it a suitable choice for this project.
    <p align="center" width="100%">
      <img width="210" alt="image" src="https://github.com/m-khooryani/OverCloudAirways/assets/7968282/5441fcbb-2b65-4ae8-993f-8e7f55abe142">
    </p>
  - #### Role Based Access Control
    Role-based access control (RBAC) was implemented using Azure AD B2C to manage user permissions based on their roles. Roles such as 'Administrator', 'Airline Staff', and 'Customer' were defined, each with a distinct set of permissions. These roles are assigned to users during the sign-up process or by an administrator, and are used in the application to decide what operations a user can perform.
    <p align="center" width="100%">
      <img width="605" alt="image" src="https://user-images.githubusercontent.com/7968282/244481866-3853c9d8-3905-4b78-8fec-939620fd427b.png">
    </p>
  - #### Secure Communication
    Azure AD B2C uses OpenID Connect and OAuth 2.0 protocols to provide tokens for secure communication. After a successful authentication, a user receives an ID token and an access token. These tokens contain claims about the user, which can include their identity, role, and other information. The application uses these tokens to authenticate the user and authorize their actions.
    <p align="center" width="100%">
      <img width="431" alt="image" src="https://user-images.githubusercontent.com/7968282/244483055-3b9fef48-6df0-4975-8510-285b2b0ad3dc.png">
    </p>
    
    <img width="953" alt="image" src="https://github.com/m-khooryani/OverCloudAirways/assets/7968282/78e02287-311e-431e-966e-a8685ede5853">

  - #### Handling Authentication in Microservices Architecture
    In a microservices architecture, like the one used in OverCloudAirways, ensuring secure and consistent authentication can be a challenge. With Azure AD B2C, each microservice can independently verify a user's token, ensuring that the user is who they claim to be. Additionally, user permissions defined through roles in Azure AD B2C can be used to ensure that the user is authorized to access specific services or operations.

### Clean Architecture

In the OverCloudAirways project, we have adopted the Clean Architecture principles to ensure a clear separation of concerns and maintainability. This architecture focuses on structuring the application into distinct layers, such as Domain, Application, and Infrastructure, which allows for greater flexibility and testability.

Some key aspects of Clean Architecture in our project include:

Decoupling the core business logic from the framework, infrastructure, and user interface concerns.
Following the Dependency Inversion Principle to reduce coupling between components.
Organizing the codebase into separate projects or folders for each layer, improving overall code organization and readability.
<p align="center" width="100%">
      <img alt="image" src="https://user-images.githubusercontent.com/7968282/232602570-fbc3d8c6-24e7-410b-b475-48ad5ab4f59c.png">
</p>

### Composition Root

![image](https://user-images.githubusercontent.com/7968282/235443476-5912683f-4e0e-4815-8b55-201c00c39c7e.png)

The Composition Root is a design pattern that refers to the place in an application where all of the dependencies between components are wired together. It is typically implemented in the infrastructure layer of the application and is responsible for setting up and configuring the various components that make up the application.

In the sample code, the CompositionRoot class serves as the Composition Root for the application. It has a Initialize method that takes a list of Module objects as input and registers them with an inversion of control (IoC) container using the ContainerBuilder class from the Autofac library. The Module objects define how the components in the application should be wired together and how they should be configured.

This initializes the Composition Root with the specified Module objects. The Module objects can be used to define the dependencies between components in the application and how they should be configured.

One benefit of using the Composition Root design pattern is that it allows different implementations of services to be easily swapped in and out depending on the environment in which the application is running. This can be especially useful for integration tests and API environments, where different implementations of services may be needed.

For example, consider an application that has a IUserRepository interface that defines the contract for a repository that stores user data. The application may have different implementations of this interface for different environments, such as a UserRepository class that uses a database to store user data, and a MockUserRepository class that stores user data in memory for use in integration tests.

By using the Composition Root pattern, these different implementations can be registered with the IoC container and swapped in and out as needed. For example, the UserRepository class could be registered in the production environment, while the MockUserRepository class could be registered in the integration test environment.

To do this, the different implementations of the IUserRepository interface could be defined as Module classes, and then passed to the CompositionRoot.Initialize method as needed. For example:

```csharp
// Production environment
CompositionRoot.Initialize(new ProductionModule());

// Integration test environment
CompositionRoot.Initialize(new TestModule());

```

This allows the application to easily switch between different implementations of services without changing the code that depends on those services.

```csharp

public static class CompositionRoot
{
    private static IContainer _container;

    public static void Initialize(params Module[] modules)
    {
        var containerBuilder = new ContainerBuilder();

        foreach (var module in modules)
        {
            containerBuilder.RegisterModule(module);
        }

        _container = containerBuilder.Build();
    }

    public static ILifetimeScope BeginLifetimeScope()
    {
        return _container.BeginLifetimeScope();
    }
}
```

Usage:

```csharp
CompositionRoot.Initialize(
    assemblyLayersModule,
    processingModule,
    mediatorModule,
    unitOfWorkModule,
    loggingModule,
    contextAccessorModule,
    domainServiceModule,
    retryPolicyModule,
    azureServiceBusModule,
    cosmosModule);

```

### Outbox Pattern

The Outbox Pattern is a reliable approach to manage distributed data, particularly for systems employing event-driven architecture. It is a solution that guarantees the atomicity of database transactions and corresponding event publishing. Essentially, it ensures that data changes within a transaction are saved to a database and related events are published in an atomic operation, thereby reducing the risk of data inconsistencies and lost messages.

In its most basic form, the Outbox Pattern involves storing outbound messages or events into an intermediate 'Outbox' database table as part of the same local transaction that affects the business entities. After the transaction has been committed, the events in the Outbox table are then published to the message broker.

<p align="center" width="100%">
      <img alt="image" src="https://github.com/m-khooryani/OverCloudAirways/assets/7968282/753b8eee-ebd7-49c8-ae64-a9d13bdfeecf">
</p>

In distributed data systems, maintaining data consistency across multiple microservices is a challenging task. The issues become more prominent when we attempt to directly publish events to the message broker either before or after committing changes to the database. Let's understand this with an example of registering a user and sending a welcome email.

  - Sending Email Before User Registration Commit:

    In this scenario, we try to send the welcome email before committing the user registration details to the database.
    ```csharp
    try 
    {
        EmailService.SendWelcomeEmail(user);  // Sending welcome email
        DatabaseService.RegisterUser(user);  // Registering user in the database
    }
    catch (Exception e)
    {
        // Handle exception
    }

    ```
    Here, if an error occurs after sending the email but before the user data is saved (e.g., the database is temporarily down), the email service will have sent a welcome email to a user who isn't registered in our system. This leads to inconsistency and confusion.
    
  - Sending Email After User Registration Commit:

    In contrast, let's see what happens when we commit the user registration before sending the welcome email.
    ```csharp
    try
    {
        DatabaseService.RegisterUser(user);  // Registering user in the database
        EmailService.SendWelcomeEmail(user);  // Sending welcome email
    }
    catch (Exception e)
    {
        // Handle exception
    }

    ```
    If an error occurs after registering the user but before sending the welcome email (maybe the email service is temporarily unavailable), the user will be registered, but they won't receive a welcome email. This might lead to a poor user experience and potential loss of user engagement.

Both scenarios highlight the possible issues in distributed systems without using the Outbox Pattern. 

The Outbox Pattern comes as a solution to the problems we discussed earlier. It combines the operations of committing data and publishing events into a single atomic operation to ensure data consistency across all services in the distributed system.

Here's how it works, following our user registration and welcome email example:
```csharp
try 
{
    using (var transaction = new TransactionScope())
    {
        DatabaseService.RegisterUser(user);  // Registering user in the database

        OutboxMessage outboxMessage = new OutboxMessage
        {
            // Details about the message, such as type, destination, and data
            Type = "WelcomeEmail",
            Destination = user.Email,
            Data = $"Welcome, {user.Name}!"
        };

        DatabaseService.AddToOutbox(outboxMessage);  // Adding outbox message to the database

        transaction.Complete();  // Committing the transaction
    }
}
catch (Exception e)
{
    // Handle exception
}

```

In this code, we begin a transaction. Within this transaction, we register the user and also add a message to the 'Outbox' table in the database. This message contains the details of the welcome email that needs to be sent. Then we commit the transaction, making sure that both the user registration and the outbox message addition are done atomically.

Once this transaction is committed, a separate process (typically a background job or a separate microservice) takes over(Polling Mechanism):
```csharp
public void ProcessOutboxMessages()
{
    var messagesToProcess = DatabaseService.GetUnprocessedOutboxMessages();

    foreach (var message in messagesToProcess)
    {
        // Publish the message to the message broker
        MessageBroker.Publish(message);

        // Mark the message as processed
        DatabaseService.MarkAsProcessed(message);
    }
}

```
This process fetches unprocessed messages from the 'Outbox' table and publishes them to the message broker. Once published, the message is marked as processed in the 'Outbox' table.

The Outbox Pattern ensures that the system maintains consistency by making sure that if a user registration is committed, the corresponding welcome email message will also be committed. The actual publishing of the message is taken care of by a separate process, which makes sure all committed messages are eventually published.

The process can be made more robust with retries, dead-letter queues, and error monitoring to handle any issues with message publishing. This pattern significantly reduces the chances of message loss and data inconsistency.

### Unit of Work Pattern
In software development, especially when working with databases, it's often crucial to ensure that a group of operations is executed as a single unit. This necessity led to the creation of the Unit of Work (UoW) design pattern. The UoW pattern is a key component of Domain-Driven Design (DDD).

The UoW pattern organizes business logic into atomic, consistent, isolated, and durable (ACID) transactions, ensuring all operations either complete successfully or fail together, leaving the system in a consistent state.

<p align="center" width="100%">
      <img  width="600" alt="image" src="https://user-images.githubusercontent.com/7968282/243746303-0db6a33b-8f68-4839-87ae-80740d85fe39.png">
</p>

Decorators play a significant role in augmenting the functionality of the Unit of Work (UoW) without changing its definition. They wrap the UoW with additional behavior, thus adhering to the Open-Closed Principle - open for extension, closed for modification.

In the context of a UoW, decorators can be used to add logging, validation, performance tracking, or any other cross-cutting concern that needs to be applied. Let's consider how decorators can be registered and used in C# with Autofac as the Dependency Injection (DI) container:

```csharp
builder.RegisterType<UnitOfWork>()
    .As<IUnitOfWork>()
    .InstancePerLifetimeScope();
builder.RegisterDecorator(
    typeof(AppendingAggregateHistoryUnitOfWorkDecorator),
    typeof(IUnitOfWork));
builder.RegisterDecorator(
    typeof(PublishOutboxMessagesUnitOfWorkDecorator),
    typeof(IUnitOfWork));
builder.RegisterDecorator(
    typeof(CreateOutboxMessagesUnitOfWorkDecorator),
    typeof(IUnitOfWork));
builder.RegisterDecorator(
    typeof(LoggingUnitOfWorkDecorator),
    typeof(IUnitOfWork));

```

In this code, the UnitOfWork class implements the IUnitOfWork interface. We use Autofac's RegisterDecorator method to add additional behavior to the UnitOfWork:

  - AppendingAggregateHistoryUnitOfWorkDecorator: This could be used to append the aggregate history of entities involved in the unit of work.
  - PublishOutboxMessagesUnitOfWorkDecorator: This could be responsible for publishing messages stored in the outbox once the unit of work is successfully completed.
  - CreateOutboxMessagesUnitOfWorkDecorator: This might create outbox messages during the processing of the unit of work.
  - LoggingUnitOfWorkDecorator: This decorator can add logging functionality to track the progress and result of the unit of work.

Each decorator is a layer that wraps the original UnitOfWork, adding its own specific functionality while maintaining the fundamental operations defined by IUnitOfWork.

<p align="center" width="100%">
      <img  width="550" alt="image" src="https://user-images.githubusercontent.com/7968282/243790060-4560ca93-52b4-4989-8412-6399f9c3c0a0.png">
</p>

The UoW pattern is typically tightly coupled with the database transactions. For instance, we've implemented the UoW pattern in our applications with Cosmos DB. As of the time of this writing, the provider for EF Core does not support transactions across multiple documents in Cosmos DB, a feature we hope will be introduced in the future. Consequently, when using the UoW pattern, it's essential to ensure your database technology supports the necessary transaction capabilities.

### Enumeration
> "Enumeration or Enum types are an integral part of C# language. They have been around since the inception of language. Unfortunately, the Enum types also have some limitations. Enum types are not object-oriented. When we have to use Enums for control flow statements, behavior around Enums gets scattered across the application. We cannot inherit or extend the Enum types. These issues can especially be a deal-breaker in Domain-Driven Design (DDD)."

For a more detailed understanding, I recommend referring to [this insightful article](https://ankitvijay.net/2020/06/12/series-enumeration-classes-ddd-and-beyond/).

```csharp
public abstract class FlightStatus : Enumeration
{
    public static readonly FlightStatus None = new NoneFlightStatus();
    public static readonly FlightStatus Scheduled = new ScheduledFlightStatus();
    public static readonly FlightStatus Cancelled = new CancelledFlightStatus();
    public static readonly FlightStatus Departed = new DepartedFlightStatus();
    public static readonly FlightStatus Arrived = new ArrivedFlightStatus();

    private FlightStatus(int value, string name) : base(value, name)
    {
    }

    internal bool HasNotYetDeparted()
    {
        return this != None && this == Scheduled;
    }

    private class NoneFlightStatus : FlightStatus
    {
        public NoneFlightStatus() : base(0, "None")
        {
        }
    }

    private class ScheduledFlightStatus : FlightStatus
    {
        public ScheduledFlightStatus() : base(1, "Scheduled")
        {
        }
    }

    private class CancelledFlightStatus : FlightStatus
    {
        public CancelledFlightStatus() : base(2, "Cancelled")
        {
        }
    }

    private class DepartedFlightStatus : FlightStatus
    {
        public DepartedFlightStatus() : base(3, "Departed")
        {
        }
    }

    private class ArrivedFlightStatus : FlightStatus
    {
        public ArrivedFlightStatus() : base(4, "Arrived")
        {
        }
    }
}
```

## Key Features and Components

This section highlights some of the key features and components of our project that differentiate it from other solutions and showcase our architectural decisions.
Scalability and Performance: Our solution is designed with scalability and performance in mind, using best practices and patterns to ensure that it can easily handle increasing loads and user demands.

  - Serverless Architecture: We have leveraged serverless technologies to build a highly scalable and cost-efficient solution, eliminating the need to manage underlying infrastructure and allowing the system to automatically scale with demand.

  - Resilience and Fault Tolerance: We have implemented strategies such as retries, and timeouts to ensure that our system can gracefully handle failures and continue operating even in the face of unexpected issues.

  - Extensibility and Modularity: The modular design of our project allows for easy extensibility and customization, enabling developers to quickly add new features and functionality without impacting existing components.

  - Security and Compliance: We have prioritized security and compliance throughout the development process, implementing features such as encryption, authentication, and authorization to safeguard user data and ensure regulatory adherence.

  - Developer Experience: We have focused on providing a seamless and enjoyable developer experience, with clear documentation, consistent coding standards, and efficient tooling and automation.

## Technologies and Libraries
This section provides an overview of the technologies, frameworks, and libraries we used to build our solution. 

  - #### [C#](https://learn.microsoft.com/en-us/dotnet/csharp/)
    We used C# as our primary programming language due to its modern features, strong typing, and excellent support for object-oriented programming principles.
  - #### [MediatR](https://github.com/jbogard/MediatR)
    A simple, unambitious mediator implementation in .NET that helps in implementing the Mediator pattern for in-process messaging.
  - #### [FluentValidation](https://docs.fluentvalidation.net/en/latest/)
    A library for building strongly-typed validation rules in a fluent and testable manner.
  - #### [Polly](https://github.com/App-vNext/Polly)
    A fault-handling library for .NET that allows developers to express resilience and transient fault handling policies, such as retries, circuit breakers, and timeouts.
  - #### [Serilog](https://serilog.net/)
    A powerful and extensible logging library for .NET applications.
  - #### [xUnit](https://xunit.net/)
    A popular and easy-to-use testing framework for writing unit tests in .NET applications.
  - #### [Autofac](https://autofac.org/)
    An open-source IoC (Inversion of Control) container for .NET, enabling dependency injection and improving maintainability and testability of the code.
    The decision to use Autofac over [MSDI](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection) was driven by its performance, advanced features like Module registration, flexibility, and strong community support. While MSDI is a suitable choice for many applications, Autofac's capabilities make it a more attractive option for our project, which has more complex requirements and dependencies.
  - #### [Newtonsoft.Json](https://www.newtonsoft.com/json)
    A popular high-performance JSON framework for .NET, chosen for its flexibility and feature set that provides better control over serialization and deserialization compared to [System.Text.Json](https://learn.microsoft.com/en-us/dotnet/api/system.text.json).
    
    Newtonsoft.Json was chosen over STJ due to its compatibility with the broader .NET ecosystem, rich feature set, ease of use, and proven maturity and stability. While STJ has its advantages, particularly in terms of performance, Newtonsoft.Json's capabilities and flexibility make it a more suitable choice for our project's requirements. 
    
    However, we remain open to replacing Newtonsoft.Json with STJ in the future if STJ evolves to better support our project's needs and provides comparable features and flexibility. By carefully decoupling our project from specific technologies, we have designed the system to be adaptable and capable of accommodating such changes with minimal disruption.

## Azure Services and Technologies
Our project leverages several Azure services and technologies. Below is a list of the key Azure services and technologies we used in the project:

  - #### [Azure Functions](https://learn.microsoft.com/en-us/azure/azure-functions/functions-get-started)
    A serverless compute service that enables you to run code without managing infrastructure, allowing us to build event-driven, scalable, and cost-effective applications for the presentation layer.
  - #### [Azure Service Bus](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview)
  
    ![image](https://user-images.githubusercontent.com/7968282/235447105-b3824062-dedd-461a-a86b-881585c9fdc6.png)

    A fully-managed enterprise integration message broker, used for decoupling applications and services and improving the overall resilience and reliability of the system.
  - #### [Azure Cosmos DB](https://learn.microsoft.com/en-us/azure/cosmos-db/introduction)
    Azure Cosmos DB is a globally distributed, multi-model database service that provides seamless horizontal scaling and low-latency access to data. We have used Cosmos DB as our primary data store, leveraging its support for event sourcing and strong consistency.
  - #### [Azure Key Vault](https://learn.microsoft.com/en-us/azure/key-vault/general/overview)
    <p align="center" width="100%">
    <img width="500" alt="image" src="https://user-images.githubusercontent.com/7968282/235447448-c66dab75-c165-44e7-beb5-f914a7acc718.png">
    </p>

    Azure Key Vault is a cloud service for securely storing and accessing secrets, such as encryption keys and certificates. In our project, we use Azure Key Vault to store and manage sensitive information, such as connection strings and API keys, enhancing the security of our application.
  - #### [Azure Application Insights](https://learn.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview)
    Azure Application Insights is an application performance management service that provides insights into the performance and usage of our application. We use Application Insights to monitor and diagnose issues in real-time, ensuring a reliable and high-performing experience for our users.
  - #### [Azure API Management](https://learn.microsoft.com/en-us/azure/api-management/api-management-key-concepts)
    Azure API Management is a fully managed service that enables us to create, publish, and manage APIs for our microservices. With API Management, we can define and enforce policies, monitor usage, and gain insights into API performance, ensuring a secure and efficient APIs.
  - #### [Azure Active Directory B2C](https://learn.microsoft.com/en-us/azure/active-directory-b2c/)
    Azure AD B2C is a comprehensive identity management service that provides secure access to our application. It enables users to sign in with their preferred social, enterprise, or local account identities, while providing advanced features like multi-factor authentication and single sign-on.

## Testing
This section provides an overview of the testing methodologies and tools used in our solution.

  <p align="center" width="100%">
  <img width="449" alt="image" src="https://user-images.githubusercontent.com/7968282/235441386-0490d735-8238-4d3d-b2b6-2849ab4668ab.png">
  </p>

  - #### Unit Testing
    Unit testing is a crucial aspect of software development, allowing developers to ensure that individual units of code behave correctly and meet their requirements. In this project, we have implemented unit tests for the domain and application layers, which are the primary layers concerning the business logic. The infrastructure and presentation layers are covered by higher-level tests, such as integration and end-to-end tests.

    We have focused on testing the following aspects in our domain and application layers:
    
      - Domain events being published as expected
      - Proper enforcement of business rules
      - Correct handling of commands and policies
      
    Here are some examples of our unit tests:
    
      - Domain Layer:
      
        - Testing Domain Events
        
          In this example, we test the proper behavior of the `AcceptInvoice` method in our `Invoice` aggregate by first ensuring that an `InvoiceAcceptedDomainEvent` is published when the invoice is successfully accepted.
      
          ``` csharp
          [Fact]
          public async void AcceptInvoice_Given_Valid_Input_Should_Successfully_Accept_Invoice_And_Publish_Event()
          {
              // Arrange
              var product = new ProductBuilder().Build();
              var repository = Substitute.For<IAggregateRepository>();
              repository.LoadAsync<Product, ProductId>(product.Id).Returns(product);
          
              var invoice = await new InvoiceBuilder()
                  .ClearItems()
                  .SetAggregateRepository(repository)
                  .BuildAsync();
              await invoice.PayAsync();
          
              // Act
              await invoice.AcceptAsync();
          
              // Assert
              Assert.Equal(InvoiceStatus.Accepted, invoice.Status);
              AssertPublishedDomainEvent<InvoiceAcceptedDomainEvent>(invoice);
          }
          ```
          
        - Testing Business Rules
        
          In this example, we verify that a business error is thrown when trying to accept an `Invoice` that has not been paid.
          
          ``` csharp
          [Fact]
          public async Task AcceptInvoice_Given_NotPaidInvoice_Should_Throw_Business_Error()
          {
              // Arrange
              var product = new ProductBuilder().Build();
              var repository = Substitute.For<IAggregateRepository>();
              repository.LoadAsync<Product, ProductId>(product.Id).Returns(product);
          
              var invoice = await new InvoiceBuilder()
                  .ClearItems()
                  .SetAggregateRepository(repository)
                  .BuildAsync();
          
              // Act, Assert
              await AssertViolatedRuleAsync<OnlyPaidInvoiceCanBeAcceptedRule>(async () =>
              {
                  await invoice.AcceptAsync();
              });
          }
          ```
          
      - Application Layer:
      
        In this example, we test that the `EnqueueProjectingReadModelInvoiceAcceptedPolicyHandler` enqueues a `ProjectInvoiceReadModelCommand` with the correct InvoiceId when handling the `InvoiceAcceptedPolicy`.
        
        ``` csharp
        [Fact]
        public async Task EnqueueProjectingReadModelInvoiceAcceptedPolicyHandler_Should_Enqueue_ProjectInvoiceReadModelCommand()
        {
            // Arrange
            var commandsScheduler = Substitute.For<ICommandsScheduler>();
            var handler = new EnqueueProjectingReadModelInvoiceAcceptedPolicyHandler(commandsScheduler);
            var policy = new InvoiceAcceptedPolicyBuilder().Build();
        
            // Act
            await handler.Handle(policy, CancellationToken.None);
        
            // Assert
            await commandsScheduler
                .Received(1)
                .EnqueueAsync(Arg.Is<ProjectInvoiceReadModelCommand>(c => c.InvoiceId == policy.DomainEvent.InvoiceId));
        }
        ```     
        
  - #### Integration Testing
    Unit tests are crucial in ensuring that individual components or parts of a system function correctly in isolation. However, they don't provide insights into how these components interact with each other. To use an analogy, think of a car made up of numerous parts. While it is important to test each part separately, it is equally important to ensure that all parts work well together when assembled.
    
    <p align="center" width="100%">
    <img width="449" alt="image" src="https://user-images.githubusercontent.com/7968282/235440221-49d34dd4-7123-4957-9a2c-3e1b6c26a635.png">
    </p>
    <p align="center" width="100%">
    <img width="449" alt="image" src="https://user-images.githubusercontent.com/7968282/235440234-25b03c14-ae96-40b2-9b24-b3a69732e008.png">
    </p>
  
    Integration testing is an important part of the testing process, allowing developers to test the interactions between different components or subsystems to ensure they work correctly together. In this project, we followed [![Twitter URL](https://img.shields.io/twitter/url/https/twitter.com/bukotsunikki.svg?style=social&label=Kamil+Grzybek%27s+approach)](https://twitter.com/kamgrzybek/status/1280770569475182592) to integration testing, focusing on the application layer as the entry point for our tests, rather than testing the presentation layer with HTTP requests.
    
    
    <p align="center" width="100%">
    <img width="449" alt="image" src="https://user-images.githubusercontent.com/7968282/233802841-b3d6b1f1-555e-4015-8325-5e9e89a2a340.png">
    </p>

    According to the approach, testing at the application layer level provides a higher level of abstraction and allows for a more business-driven approach, as it enables writing tests for each use case in the system. This method can also accommodate various adapters, such as integration event handling, background job processing, and CLI commands handling.
    
    Here's an example of an integration test based on Kamil's approach, which ensures that making a purchase behaves as expected and all properties match the expected values:
    
      ``` csharp
      [Fact]
      public async Task MakePurchase_PurchaseShouldBeMade_AndAllPropertiesShouldMatch()
      {
          await _testFixture.ResetAsync();

          var date = DateTimeOffset.UtcNow;
          Clock.SetCustomDate(date);
          var customerId = CustomerId.New();
          var purchaseId = PurchaseId.New();

          // Create Customer 
          var createCustomerCommand = new CreateCustomerCommandBuilder()
              .SetCustomerId(customerId)
              .Build();
          await _invoker.CommandAsync(createCustomerCommand);

          // Make Purchase
          var makePurchaseCommand = new MakePurchaseCommandBuilder()
              .SetPurchaseId(purchaseId)
              .SetCustomerId(customerId)
              .Build();
          await _invoker.CommandAsync(makePurchaseCommand);

          // Project ReadModels
          await _testFixture.ProcessOutboxMessagesAsync();

          // Purchase Query
          var query = new GetPurchaseInfoQuery(purchaseId, customerId);
          var purchase = await _invoker.QueryAsync(query);

          // Assert
          Assert.NotNull(purchase);
          Assert.Equal(purchaseId.Value, purchase.PurchaseId);
          Assert.Equal(createCustomerCommand.CustomerId, purchase.CustomerId);
          Assert.Equal(createCustomerCommand.FirstName, purchase.CustomerFirstName);
          Assert.Equal(createCustomerCommand.LastName, purchase.CustomerLastName);
          Assert.Equal(makePurchaseCommand.Amount, purchase.Amount);
          Assert.Equal(date, purchase.Date);
      }
      ```  
      
      This test demonstrates how our system components interact at the application layer, providing confidence that they are working together correctly. Integration tests play a critical role in identifying issues that may not be evident through unit testing alone.

    **Logging**
    
    In the project, we've implemented configurable logging within the integration testing environment to provide better insight into the test execution and help with diagnosing issues during the testing process. To achieve this, we have utilized a custom `LoggerFactory` that allows filtering log messages based on their source and log level.

    Here's a snippet of the `GetLoggerFactory` method that demonstrates the configuration:
    ``` csharp
    private static LoggerFactory GetLoggerFactory()
    {
        return new LoggerFactory(new[]
        {
            new LogToActionLoggerProvider(
                new Dictionary<string, LogLevel>()
                {
                    { "Default", LogLevel.Information },
                    { "Microsoft", LogLevel.Warning },
                },
                log =>
                {
                    try
                    {
                        Output?.WriteLine(log);
                    }
                    catch
                    { }
                }
            )
        });
    }
    ```  
    
    In this method, we create a new `LoggerFactory` instance and configure it with a custom `LogToActionLoggerProvider`. This logger provider takes a dictionary that maps the source of the log messages to the minimum log level that should be output. For instance, in this configuration, we have set the "Default" source to output logs with a minimum log level of `Information`, while the "Microsoft" source is set to output logs with a minimum log level of `Warning`.
    
    By using this configurable logging setup, we can control the verbosity of the logs generated during the integration tests and selectively filter log messages based on their source and log level, making it easier to identify and diagnose issues that may arise during the test execution.
    
    <p align="center" width="100%">
    <img width="526" alt="image" src="https://user-images.githubusercontent.com/7968282/233826976-dbd8e7ac-0673-4396-a166-b19c363bdab4.png">
    </p>

  - #### Testing Tools and Patterns

    - #### [xUnit](https://www.amazon.com/gp/product/0131495054/)
      The project utilizes xUnit as its testing framework. xUnit is a popular and widely used open-source testing framework for .NET, designed to enable a range of testing scenarios with a clean and easy-to-understand syntax. It provides features like Fact and Theory attributes, Assert methods, and other testing utilities that enable developers to create comprehensive and robust test suites.

    - #### **Builder Pattern**

      In the project, the Builder pattern was employed to facilitate the creation of complex objects, making it easy to set up test data with default or customized values. This pattern proved helpful in maintaining the readability and simplicity of test code while allowing the fine-tuning of object properties as needed.
      
      [This article](https://ardalis.com/improve-tests-with-the-builder-pattern-for-test-data/) is a valuable resource to learn more about this pattern and its application in testing.
      ``` csharp
      public class CustomerLoyaltyPointsCollectedDomainEventBuilder
      {
          private CustomerId _customerId = CustomerId.New();
          private decimal _loyaltyPoints = 100M;
      
          public CustomerLoyaltyPointsCollectedDomainEvent Build()
          {
              return new CustomerLoyaltyPointsCollectedDomainEvent(_customerId, _loyaltyPoints);
          }
      
          public CustomerLoyaltyPointsCollectedDomainEventBuilder SetCustomerId(CustomerId customerId)
          {
              _customerId = customerId;
              return this;
          }
      
          public CustomerLoyaltyPointsCollectedDomainEventBuilder SetLoyaltyPoints(decimal loyaltyPoints)
          {
              _loyaltyPoints = loyaltyPoints;
              return this;
          }
      }
      ```
      
    - #### DB Sandboxing
      A custom method `ResetAsync` was utilized to ensure database isolation for each test case. By clearing the database and resetting any custom settings, a clean and controlled environment was provided for each test run, preventing tests from interfering with one another.      
      
    - #### Mocking Library
      [`NSubstitute`](https://github.com/nsubstitute/NSubstitute) is employed as the mocking library for the project. `NSubstitute` is a friendly and lightweight .NET mocking library designed for ease of use and readability. It enables developers to create mock objects for interfaces or classes, configure their behavior, and verify that expected interactions have occurred during the tests.
      
    - #### Shared Context
      A useful resource for understanding how to share setup and cleanup code across test classes is [the xUnit article](https://xunit.net/docs/shared-context) titled "Shared Context between Tests". This article explains various methods for sharing test context, including Constructor and Dispose, Class Fixtures, and Collection Fixtures, depending on the desired scope and the costs associated with the setup and cleanup code.
      
      Collection Fixtures, as used in the project, enable shared object instances across multiple test classes. By leveraging Collection Fixtures, test classes can share setup and cleanup code, leading to more efficient and maintainable test suites. The article provides valuable insights into the different methods for sharing test context and offers guidance on how to apply these approaches in practice.
      ``` csharp
      [Collection("CRM")]
      public class PurchaseTests
      {
         .
         .
         .
      }
      ```
      
    - #### Test Doubles
      Test doubles, such as the `Clock` and `FakeAccessor` classes, were implemented to replace real implementations with controlled versions during testing. This enabled the project to isolate specific behaviors and ensure the system under test behaved as expected.
      
        - Clock
        
          The `Clock` class provided methods to set a custom date and reset it, giving control over the current time during tests.
          
          ``` csharp
          public static class Clock
          {
              private static DateTimeOffset? _customDate;
          
              public static DateTimeOffset Now => _customDate ?? DateTime.UtcNow;
              public static DateOnly Today => DateOnly.FromDateTime(_customDate?.DateTime ?? DateTime.UtcNow);
          
              public static void SetCustomDate(DateTimeOffset customDate)
              {
                  _customDate = customDate;
              }
          
              public static void Reset()
              {
                  _customDate = null;
              }
          }
          ```
          
        - FakeAccessor
        
          The `FakeAccessor` was designed to simulate the behavior of an actual user who originated the request. In the application, a real user accessor would retrieve the user's ID and other related information from the current request context. In the test environment, the `FakeAccessor` provided a controlled way to generate and manage fake user information. It offered methods for managing its state, allowing the test environment to have predictable and isolated user-related data.

          ``` csharp
          class FakeAccessor : IUserAccessor
          {
              private FakeAccessor()
              {
              }
          
              public static FakeAccessor Instance = new();
          
              private static Guid _userId;
              private static void ResetUserId()
              {
                  _userId = Guid.NewGuid();
              }
              public Guid UserId => _userId;
          
              private static string _fullName;
              private static void ResetFullName()
              {
                  _fullName = Guid.NewGuid().ToString();
              }
              public string FullName => _fullName;
          
              public string TcpConnectionId => Guid.NewGuid().ToString();
          
              private Guid _storedUserId;
              private string _storedFullName;
          
              internal void SaveState()
              {
                  _storedUserId = _userId;
                  _storedFullName = _fullName;
          
                  Reset();
              }
          
              internal void RestoreState()
              {
                  _userId = _storedUserId;
                  _fullName = _storedFullName;
              }
          
              internal static void Reset()
              {
                  ResetUserId();
                  ResetFullName();
              }
          }
          ```
          
## CI/CD Pipeline and Deployment
The project is set up with a continuous integration (CI) and continuous deployment (CD) pipeline using GitHub Actions. This ensures the code is built, tested, and deployed to the Azure environment whenever there's a new commit to the master branch.

<p align="center" width="100%">
  <img width="958" alt="image" src="https://github.com/m-khooryani/OverCloudAirways/assets/7968282/2eda4dac-64f7-42c7-a3c1-9436c6608bff">
</p>

  - #### Continuous Integration
    The CI pipeline is defined in the build.yml file. This pipeline is triggered on every pull request. Here are the main steps of the CI pipeline:
    
      - Environment Setup: We determine the environment (either DEV or PROD) based on the branch that triggered the workflow.
      - Build: The code is checked out and the required .NET SDK is set up. The project is then built using dotnet build.
      - Test: We run unit tests using dotnet test.

      ``` yml
      jobs:
        setup:
            name: Choose Secrets Environment Job
            runs-on: windows-latest
            # Other setup steps...
        build:
            name: Build
            needs: setup
            runs-on: windows-latest
            steps:
              # Other setup steps...
              - name: Build
                run: dotnet build --no-restore -c Release
              - name: Test
                run: dotnet test --no-restore --verbosity normal
      ```

  - #### Continuous Deployment
    The CD pipeline is defined in the publish.yml file. This pipeline is triggered on every push to the master branch. Here are the main steps of the CD pipeline:

      - Build and Publish: We build and publish each service in the solution individually using dotnet publish.
      - Deploy: We deploy each service to its respective Azure Function App using the Azure/functions-action@v1 action.

      ``` yml
      jobs:
        build-and-deploy:
          runs-on: ubuntu-latest
          steps:
          # Other setup steps...
          - name: Build and publish Identity Service
            run: |
              dotnet build --configuration Release ./Identity/Src/OverCloudAirways.IdentityService.API/OverCloudAirways.IdentityService.API.csproj
              dotnet publish --configuration Release --output ${{ env.IDENTITY_OUTPUT_DIR }} ./Identity/Src/OverCloudAirways.IdentityService.API/OverCloudAirways.IdentityService.API.csproj
          - name: Deploy Identity to Azure Function App
            uses: Azure/functions-action@v1
            with:
              app-name: overcloudairways-identity
              package: ${{ env.IDENTITY_OUTPUT_DIR }}
              publish-profile: ${{ secrets.OVERCLOUDAIRWAYSIDENTITYFUNCTION_PUBLISHSETTINGS }}
      ```

  - #### Azure Environment
    The services are hosted on Azure Function Apps. Each service has its own Function App. The deployment uses the publish profile of the Function App, which can be obtained from the Azure portal.
    <img width="746" alt="image" src="https://github.com/m-khooryani/OverCloudAirways/assets/7968282/caa32800-d0a9-492f-a038-3bab380f1863">


## Contributing and Collaboration

  - Contributing to OverCloudAirways
  
    The contribution to OverCloudAirways is highly encouraged and appreciated. Whether you are an experienced developer or a newcomer, your input can greatly enhance the project. Contributions can be made in various ways: by providing suggestions, reporting bugs, improving documentation, submitting pull requests, or even just by using the software and providing feedback.

  - Code of Conduct
  
    All contributors are expected to adhere to the project's Code of Conduct. This ensures a welcoming environment for everyone involved. 

  - Collaboration and Communication
  
    Open communication is key to the success of OverCloudAirways. Contributors are encouraged to communicate freely and openly about their work, ideas, and any challenges they face. This project utilizes various communication channels, such as GitHub issues and pull requests, to facilitate effective collaboration.

  - Quality Standards
  
    To maintain a high standard of quality, all contributions are expected to meet certain criteria. This includes adherence to coding standards, proper documentation of code, and thorough testing. Code reviews are conducted to ensure these standards are met before any changes are merged into the project.

  - Getting Started with Contributing
  
    If you are new to contributing, don't worry, we've got you covered. The project provides comprehensive guides and resources to help you get started. This includes setting up the development environment, understanding the codebase, and submitting your first pull request. So dive in and contribute to the development of OverCloudAirways!
    
<p align="center" width="100%">
  <img width="526" alt="image" src="https://github.com/m-khooryani/OverCloudAirways/assets/7968282/b78fa696-635a-42c3-b932-6f2b3264b270">
</p>

Remember, every contribution counts and helps in shaping and enhancing OverCloudAirways. Your effort, no matter how small, can make a significant impact. Let's collaborate and learn together!

## Changelog and Versioning
Software evolves over time, and keeping a record of all these changes is crucial to maintain a transparent development environment and facilitate tracking, reverting, or understanding the impact of certain modifications. This record, typically referred to as a 'changelog', includes detailed logs of all alterations in the system. Each new release of the software should be accompanied by a detailed changelog.

Each new version is thoroughly documented in the project's changelog, providing developers and users with a clear understanding of what has changed, why, and how it may impact them. This includes comprehensive details about new features, bug fixes, performance improvements, and possible breaking changes.

Note: Any substantial pull request should include an update to the changelog, detailing the changes introduced. This helps maintain an accurate and up-to-date log of the system's evolution.

``` shell
## [1.1.0] - YYYY-MM-DD
### Initialized
- New feature enabling ...

### Added
- Improvement of ... 

### Fixed
- Bug fix of ...

### Removed
- Deprecation of ...

## [1.0.0] - YYYY-MM-DD
### Initialized
- Initial release of ...

``` 

## License
The OverCloudAirways project is licensed under the terms of the MIT License. This permissive license is short and to the point. It lets people do anything they want with the code as long as they provide attribution back to you and don’t hold you liable.

MIT License is a popular choice among open-source projects due to its simplicity and flexibility. It allows contributors to freely use, copy, modify, merge, publish, distribute, sublicense, and even sell the software provided they include the original copyright notice and disclaimer. This encourages wide dissemination and use of the code, which aligns with the primary goal of OverCloudAirways - to provide a collaborative learning platform for developers to explore, study, and improve their skills.

The full details of the MIT License for this project can be found in the LICENSE file in the project's root directory. It typically includes the following sections:

  - Permission Notice: A brief note granting permissions to use, copy, modify, merge, publish, distribute, sublicense, and/or sell the Software.

  - Conditions: The only condition is the inclusion of the copyright notice and this permission notice in all copies or substantial portions of the software.

  - Limitations: The software is provided "as is", without warranty of any kind, express or implied, including but not limited to the warranties of merchantability, fitness for a particular purpose, and noninfringement. In no event shall the authors or copyright holders be liable for any claim, damages, or other liability, whether in an action of contract, tort, or otherwise, arising from, out of, or in connection with the software or the use or other dealings in the software.

Please be sure to review the license terms before contributing to the project or using any part of the software in your own projects.

## Acknowledgments
This section is dedicated to acknowledging all those who have made a significant contribution to the OverCloudAirways project, whether by sharing knowledge, contributing code, providing feedback, or supporting the project in any other meaningful way.

  - The Open Source Community: The OverCloudAirways project wouldn't have been possible without the collective knowledge, tools, and resources shared by the open source community. A huge thank you goes to all the developers, authors, and contributors of the various open source projects and libraries that this project depends upon.

  - Contributors: A sincere thank you to all the contributors who have spent their valuable time and effort to make this project better. Your contributions, ranging from code enhancements, bug fixes, documentation improvements, to new feature suggestions, have played an invaluable role in shaping and improving OverCloudAirways.
  
  - Sponsors: Special appreciation goes to the sponsors who have generously supported the project. A big thank you to [2MNordic](2mnordic.com), for their substantial contribution in terms of cloud resources and Azure costs. Their sponsorship has significantly helped the project in its cloud operations.
  
<p align="center" width="100%">
  <img width="130" alt="image" src="https://github.com/m-khooryani/OverCloudAirways/assets/7968282/02dd9af7-0733-4aca-b5f2-efaf779e0051">
</p>

  - Supporters: Lastly, the project team extends its gratitude to all those who have shown their support in various ways - from giving a star on the project's GitHub repository, sharing it on social media, blogging about it, or simply spreading the word about OverCloudAirways within their networks. Your support is highly appreciated and motivates the team to continue improving the project.

This project stands on the shoulders of giants and the team acknowledges the effort and dedication of all those who have played a part in its journey. The project team looks forward to a future of continuous learning, improvement, and collaboration.

## Further Learning and Useful Resources
In this section, we provide a list of learning resources and references that are related to the concepts, technologies, and methodologies used in this project. Whether you're a beginner or an experienced developer, these resources can help you deepen your understanding and expand your knowledge base.

### Domain-Driven Design
  - [Domain-Driven Design: Tackling Complexity in the Heart of Software](https://www.amazon.com/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215) book, Eric Evans
  - [Implementing Domain-Driven Design](https://www.amazon.com/Implementing-Domain-Driven-Design-Vaughn-Vernon/dp/0321834577) book, Vaughn Vernon

### Azure
  - [Microsoft Learn - Azure](https://learn.microsoft.com/en-us/training/azure/)
  - [Azure for Architects](https://www.amazon.com/Azure-Architects-scalable-high-availability-applications/dp/1839215860)
