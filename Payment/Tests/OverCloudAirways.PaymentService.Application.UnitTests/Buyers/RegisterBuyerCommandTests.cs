using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Buyers.Commands.Refund;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.TestHelpers.Buyers;
using Xunit;

namespace OverCloudAirways.PaymentService.Application.UnitTests.Buyers;

public class RegisterBuyerCommandTests
{
    [Fact]
    public async Task RefundBuyerBalanceCommandHandler_RefundsBuyerBalance()
    {
        // Arrange
        var refundAmount = 100;
        var buyer = new BuyerBuilder().Build();

        var aggregateRepository = Substitute.For<IAggregateRepository>();
        aggregateRepository.LoadAsync<Buyer, BuyerId>(Arg.Any<BuyerId>()).Returns(buyer);

        var handler = new RefundBuyerBalanceCommandHandler(aggregateRepository);
        var command = new RefundBuyerBalanceCommandBuilder()
            .SetAmount(refundAmount)
            .Build();

        // Act
        await handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.Equal(refundAmount, buyer.Balance);
    }
}
