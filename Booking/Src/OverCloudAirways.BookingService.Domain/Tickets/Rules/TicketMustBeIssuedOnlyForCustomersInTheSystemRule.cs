using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Domain.Tickets.Rules;

internal class TicketMustBeIssuedOnlyForCustomersInTheSystemRule : IBusinessRule
{
    private readonly CustomerId _customerId;
    private readonly IAggregateRepository _aggregateRepository;

    public TicketMustBeIssuedOnlyForCustomersInTheSystemRule(
        CustomerId customerId,
        IAggregateRepository aggregateRepository)
    {
        _customerId = customerId;
        _aggregateRepository = aggregateRepository;
    }

    public string TranslationKey => "Ticket_Must_Be_Issued_Only_For_Customers_In_The_System";

    public async Task<bool> IsFollowedAsync()
    {
        var customer = await _aggregateRepository.LoadAsync<Customer, CustomerId>(_customerId);

        return customer is not null;
    }
}
