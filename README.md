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
   - [Clean Architecture](#clean-architecture)
2. [Key Features and Components](#key-features-and-components)
3. [Technologies and Libraries](#technologies-and-libraries)
4. [Azure Services and Technologies](#azure-services-and-technologies)
5. [Testing](#testing)
   - [Unit Testing](#unit-testing)
   - [Integration Testing](#integration-testing)
   - [Testing Tools and Patterns](#testing-tools-and-patterns)
6. [CI/CD Pipeline and Deployment](#cicd-pipeline-and-deployment)
   - [GitHub Actions](#github-actions)
   - [Deployment Process](#deployment-process)
   - [Configuration and Environment Variables](#configuration-and-environment-variables)
7. [Event-Storming and DDD](#event-storming-and-ddd)
8. [Contributing and Collaboration](#contributing-and-collaboration)
9. [Changelog and Versioning](#changelog-and-versioning)
10. [License](#license)
11. [Acknowledgments](#acknowledgments)
12. [Contact Information](#contact-information)

## Architecture and Design

### Domain-Driven Design (DDD)

> "Domain-driven design is an approach to software development that centers the development on programming a domain model that has a rich understanding of the processes and rules of a domain." - Eric Evans, author of the book "Domain-Driven Design: Tackling Complexity in the Heart of Software"

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

  ![Event Storming Miro board](path-to-your-image)
  <img width="1181" alt="image" src="https://user-images.githubusercontent.com/7968282/233003514-c847cefa-30f1-4744-99fb-363a99c37114.png">


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

### Clean Architecture

In the OverCloudAirways project, we have adopted the Clean Architecture principles to ensure a clear separation of concerns and maintainability. This architecture focuses on structuring the application into distinct layers, such as Domain, Application, and Infrastructure, which allows for greater flexibility and testability.

Some key aspects of Clean Architecture in our project include:

Decoupling the core business logic from the framework, infrastructure, and user interface concerns.
Following the Dependency Inversion Principle to reduce coupling between components.
Organizing the codebase into separate projects or folders for each layer, improving overall code organization and readability.
<p align="center" width="100%">
      <img alt="image" src="https://user-images.githubusercontent.com/7968282/232602570-fbc3d8c6-24e7-410b-b475-48ad5ab4f59c.png">
</p>

## Key Features and Components

This section highlights some of the key features and components of our project that differentiate it from other solutions and showcase our architectural decisions.
Scalability and Performance: Our solution is designed with scalability and performance in mind, using best practices and patterns to ensure that it can easily handle increasing loads and user demands.

  - Serverless Architecture: We have leveraged serverless technologies to build a highly scalable and cost-efficient solution, eliminating the need to manage underlying infrastructure and allowing the system to automatically scale with demand.

  - Resilience and Fault Tolerance: We have implemented strategies such as retries, and timeouts to ensure that our system can gracefully handle failures and continue operating even in the face of unexpected issues.

  - Extensibility and Modularity: The modular design of our project allows for easy extensibility and customization, enabling developers to quickly add new features and functionality without impacting existing components.

  - Security and Compliance: We have prioritized security and compliance throughout the development process, implementing features such as encryption, authentication, and authorization to safeguard user data and ensure regulatory adherence.

  - Developer Experience: We have focused on providing a seamless and enjoyable developer experience, with clear documentation, consistent coding standards, and efficient tooling and automation.

These features and components come together to create a robust, scalable, and maintainable system
