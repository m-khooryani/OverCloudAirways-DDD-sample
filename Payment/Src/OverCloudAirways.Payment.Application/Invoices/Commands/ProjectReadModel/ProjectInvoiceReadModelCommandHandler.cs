using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Invoices;

namespace OverCloudAirways.PaymentService.Application.Invoices.Commands.ProjectReadModel;

internal class ProjectInvoiceReadModelCommandHandler : CommandHandler<ProjectInvoiceReadModelCommand>
{
    private readonly ICosmosManager _cosmosManager;
    private readonly IAggregateRepository _aggregateRepository;

    public ProjectInvoiceReadModelCommandHandler(
        ICosmosManager cosmosManager,
        IAggregateRepository aggregateRepository)
    {
        _cosmosManager = cosmosManager;
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(ProjectInvoiceReadModelCommand command, CancellationToken cancellationToken)
    {
        var invoice = await _aggregateRepository.LoadAsync<Invoice, InvoiceId>(command.InvoiceId);
        var buyer = await _aggregateRepository.LoadAsync<Buyer, BuyerId>(invoice.BuyerId);

        var readmodel = new InvoiceReadModel(
            invoice.Id.Value,
            buyer.FirstName,
            buyer.LastName,
            invoice.DueDate,
            invoice.TotalAmount,
            invoice.Items.ToList());

        await _cosmosManager.UpsertAsync(ContainersConstants.ReadModels, readmodel);
    }
}
