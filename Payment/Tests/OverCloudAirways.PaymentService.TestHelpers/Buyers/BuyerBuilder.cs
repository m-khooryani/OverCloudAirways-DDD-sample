using OverCloudAirways.PaymentService.Domain.Buyers;

namespace OverCloudAirways.PaymentService.TestHelpers.Buyers;

public class BuyerBuilder
{
    private BuyerId _buyerId = BuyerId.New();
    private string _firstName = "John";
    private string _lastName = "Doe";
    private string _email = "john.doe@example.com";
    private string _phoneNumber = "1234567890";

    public Buyer Build()
    {
        return Buyer.Register(_buyerId, _firstName, _lastName, _email, _phoneNumber);
    }

    public BuyerBuilder SetBuyerId(BuyerId buyerId)
    {
        _buyerId = buyerId;
        return this;
    }

    public BuyerBuilder SetFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public BuyerBuilder SetLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public BuyerBuilder SetEmail(string email)
    {
        _email = email;
        return this;
    }

    public BuyerBuilder SetPhoneNumber(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }
}
