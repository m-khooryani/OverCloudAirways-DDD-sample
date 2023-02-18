using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Buyers.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Buyers;

public class BuyerRegisteredDomainEventBuilder
{
    private BuyerId _customerId = BuyerId.New();
    private string _firstName = "John";
    private string _lastName = "Doe";
    private string _email = "john.doe@example.com";
    private string _phoneNumber = "1234567890";

    public BuyerRegisteredDomainEvent Build()
    {
        return new BuyerRegisteredDomainEvent(_customerId, _firstName, _lastName, _email, _phoneNumber);
    }

    public BuyerRegisteredDomainEventBuilder SetBuyerId(BuyerId buyerId)
    {
        _customerId = buyerId;
        return this;
    }

    public BuyerRegisteredDomainEventBuilder SetFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public BuyerRegisteredDomainEventBuilder SetLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public BuyerRegisteredDomainEventBuilder SetEmail(string email)
    {
        _email = email;
        return this;
    }

    public BuyerRegisteredDomainEventBuilder SetPhoneNumber(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }
}
