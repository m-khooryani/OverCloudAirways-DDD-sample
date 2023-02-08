﻿using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.BookingService.Application.Customers.Commands.ProjectReadModel;

internal class ProjectCustomerReadModelCommandHandler : CommandHandler<ProjectCustomerReadModelCommand>
{
    private readonly ICosmosManager _cosmosManager;
    private readonly IAggregateRepository _aggregateRepository;

    public ProjectCustomerReadModelCommandHandler(
        ICosmosManager cosmosManager,
        IAggregateRepository aggregateRepository)
    {
        _cosmosManager = cosmosManager;
        _aggregateRepository = aggregateRepository;
    }


    public override async Task HandleAsync(ProjectCustomerReadModelCommand command, CancellationToken cancellationToken)
    {
        var customer = await _aggregateRepository.LoadAsync<Customer, CustomerId>(command.CustomerId);

        var readmodel = new CustomerReadModel(
            customer.Id.Value,
            customer.FirstName,
            customer.LastName,
            customer.Email,
            customer.DateOfBirth,
            customer.PhoneNumber,
            customer.Address);

        await _cosmosManager.UpsertAsync(ContainersConstants.ReadModels, readmodel);
    }
}
