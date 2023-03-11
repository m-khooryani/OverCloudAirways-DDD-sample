using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Domain.Customers;

namespace OverCloudAirways.CrmService.Application.Customers.Commands.Create;

internal class CreateCustomerCommandHandler : CommandHandler<CreateCustomerCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public CreateCustomerCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override Task HandleAsync(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(
            command.CustomerId,
            command.FirstName,
            command.LastName,
            command.Email,
            command.DateOfBirth,
            command.PhoneNumber,
            command.Address);

        _aggregateRepository.Add(customer);

        return Task.CompletedTask;
    }
}