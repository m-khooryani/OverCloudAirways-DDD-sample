using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.ProjectReadModel;

internal class ProjectUserReadModelCommandHandler : CommandHandler<ProjectUserReadModelCommand>
{
    private readonly IAggregateRepository _aggregateRepository;
    private readonly ICosmosManager _cosmosManager;

    public ProjectUserReadModelCommandHandler(
        IAggregateRepository aggregateRepository,
        ICosmosManager cosmosManager)
    {
        _aggregateRepository = aggregateRepository;
        _cosmosManager = cosmosManager;
    }

    public override async Task HandleAsync(ProjectUserReadModelCommand command, CancellationToken cancellationToken)
    {
        var aggregate = await _aggregateRepository.LoadAsync<User, UserId>(command.UserId);
        var readModel = new UserReadModel(
            aggregate.Id.Value, 
            aggregate.UserType,
            aggregate.Email,
            aggregate.GivenName,
            aggregate.Surname);

        await _cosmosManager.UpsertAsync(ContainersConstants.User, readModel);
    }
}
