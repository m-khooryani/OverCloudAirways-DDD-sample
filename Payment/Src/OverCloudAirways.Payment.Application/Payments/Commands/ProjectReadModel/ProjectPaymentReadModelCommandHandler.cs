using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Payments;

namespace OverCloudAirways.PaymentService.Application.Payments.Commands.ProjectReadModel;

internal class ProjectPaymentReadModelCommandHandler : CommandHandler<ProjectPaymentReadModelCommand>
{
    private readonly ICosmosManager _cosmosManager;
    private readonly IAggregateRepository _aggregateRepository;

    public ProjectPaymentReadModelCommandHandler(
        ICosmosManager cosmosManager,
        IAggregateRepository aggregateRepository)
    {
        _cosmosManager = cosmosManager;
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(ProjectPaymentReadModelCommand command, CancellationToken cancellationToken)
    {
        var payment = await _aggregateRepository.LoadAsync<Payment, PaymentId>(command.PaymentId);
        var invoice = await _aggregateRepository.LoadAsync<Invoice, InvoiceId>(payment.InvoiceId);

        var readmodel = new PaymentReadModel(
            payment.Id.Value,
            payment.Amount,
            invoice.TotalAmount,
            payment.Method,
            payment.ReferenceNumber);

        await _cosmosManager.UpsertAsync(ContainersConstants.ReadModels, readmodel);
    }
}
