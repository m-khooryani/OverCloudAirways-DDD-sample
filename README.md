# OverCloudAirways: A Collaborative Learning Platform for a DDD application

Welcome to **OverCloudAirways**! This project serves as a collaborative learning platform for developers interested in exploring cutting-edge technologies, architectural patterns, and best practices in software development. Inspired by flight booking systems, the primary focus is on providing a comprehensive sample for developers to study, contribute, and learn from.

OverCloudAirways showcases _serverless_ technologies and core architectural patterns, including **Domain-Driven Design (DDD)**, **CQRS**, **Microservices**, and **Event-Driven Architecture (EDA)**. We emphasize that these technologies and patterns are not a silver bullet, and the decisions made in this project depend on multiple factors. Our goal is to create a resource that encourages collaboration and helps developers improve their skills, explore new ideas, and gain insights into building modern, scalable, and maintainable enterprise applications.

**Your contributions are welcome!** Whether you're a seasoned developer or a newcomer, we invite you to join us in our quest to learn and share knowledge about building better software.

## Table of Contents

1. [Architecture and Design](#architecture-and-design)
   - [Domain-Driven Design (DDD)](#domain-driven-design-ddd)
   - [CQRS and Event Sourcing](#cqrs-and-event-sourcing)
   - [Microservices and Event-Driven Architecture](#microservices-and-event-driven-architecture)
   - [Clean Architecture and Other Patterns](#clean-architecture-and-other-patterns)
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

In our implementation of DDD, we focus on the following key aspects:

- **Ubiquitous Language**: Establishing a shared language among developers that promotes effective communication and a common understanding of the domain. Although we don't have real domain experts for this project, we strive to use consistent terminology and naming conventions that accurately represent domain concepts.
- **Bounded Contexts**: Defining logical boundaries that separate different areas of the domain, promoting modularity and limiting the scope of domain models. In OverCloudAirways, we have identified the following Bounded Contexts:

  - **Identity**: Responsible for registering users, managing authentication, and handling user profiles.
  - **Payment**: Handles payment processing, invoicing, and payment-related notifications.
  - **Booking**: Takes care of flight booking processes, including seat reservation, ticket issuance, and booking confirmation.
  - **CRM**: Manages customer relationship management features, such as customer feedback, support requests, and customer communication.

![image](https://user-images.githubusercontent.com/7968282/231226243-a22d84ae-c77f-4e80-90a3-cbcddef419da.png)

These Bounded Contexts work together to provide a comprehensive flight booking system that covers essential aspects of the business domain.

- **Event Storming**:

  Event Storming is a collaborative modeling technique that brings together developers, domain experts, and other stakeholders to explore and model a domain using events, aggregates, commands, and other domain elements. This technique helps to create a shared understanding of the domain, identify potential inconsistencies, and uncover hidden complexities.

  In the development of the OverCloudAirways project, we used Event Storming to map out the domain, its processes, and its interactions. The image below, taken from our Miro board, demonstrates how Event Storming helped shape the project's design:

  ![Event Storming Miro board](path-to-your-image)

  Through Event Storming, we were able to visualize the relationships between different parts of the domain and ensure that our design decisions aligned with the needs of the project.


- **Aggregates**: 

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


- **Domain Events**:

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

- **Domain Services**:

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

- **Domain Policies**:

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



Since we use event-sourcing in OverCloudAirways, we don't have traditional repositories. Instead, we store and retrieve the state of aggregates by replaying domain events.

By adhering to DDD principles, OverCloudAirways aims to provide a maintainable and scalable solution that serves as a valuable learning resource for developers.


