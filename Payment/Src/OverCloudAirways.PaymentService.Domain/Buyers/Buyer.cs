using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.PaymentService.Domain.Buyers.Events;

namespace OverCloudAirways.PaymentService.Domain.Buyers;

public class Buyer : AggregateRoot<BuyerId>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public decimal Balance { get; private set; }

    private Buyer()
    {
    }

    public static Buyer Register(
        BuyerId id,
        string firstName,
        string lastName,
        string email,
        string phoneNumber)
    {
        var @event = new BuyerRegisteredDomainEvent(
            id,
            firstName,
            lastName,
            email,
            phoneNumber);

        var buyer = new Buyer();
        buyer.Apply(@event);

        return buyer;
    }

    public void Refund(decimal amount)
    {
        var @event = new BuyerBalanceRefundedDomainEvent(Id, amount);
        Apply(@event);
    }

    protected void When(BuyerRegisteredDomainEvent @event)
    {
        Id = @event.BuyerId; 
        FirstName = @event.FirstName; 
        LastName = @event.LastName; 
        Email = @event.Email; 
        PhoneNumber = @event.PhoneNumber;
        Balance = 0M;
    }

    protected void When(BuyerBalanceRefundedDomainEvent @event)
    {
        Balance += @event.Amount;
    }
}
