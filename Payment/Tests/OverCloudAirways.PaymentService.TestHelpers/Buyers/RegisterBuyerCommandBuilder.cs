using OverCloudAirways.PaymentService.Application.Buyers.Commands.Register;
using OverCloudAirways.PaymentService.Domain.Buyers;

namespace OverCloudAirways.PaymentService.TestHelpers.Buyers;

public class RegisterBuyerCommandBuilder
{
    private BuyerId _customerId = BuyerId.New();
    private string _firstName = "John";
    private string _lastName = "Doe";
    private string _email = "john.doe@example.com";
    private string _phoneNumber = "1234567890";

    public RegisterBuyerCommand Build()
    {
        return new RegisterBuyerCommand(_customerId, _firstName, _lastName, _email, _phoneNumber);
    }

    public RegisterBuyerCommandBuilder SetBuyerId(BuyerId buyerId)
    {
        _customerId = buyerId;
        return this;
    }

    public RegisterBuyerCommandBuilder SetFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public RegisterBuyerCommandBuilder SetLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public RegisterBuyerCommandBuilder SetEmail(string email)
    {
        _email = email;
        return this;
    }

    public RegisterBuyerCommandBuilder SetPhoneNumber(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }
}
