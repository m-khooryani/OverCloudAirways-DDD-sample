using DArch.Application.Configuration.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.UpdateReadModel;

internal class ProjectUserReadModelCommandHandler : CommandHandler<ProjectUserReadModelCommand>
{
    private readonly IAggregateRepository _aggregateRepository;
    private readonly CosmosManager _cosmosManager;

    public ProjectUserReadModelCommandHandler(
        IAggregateRepository aggregateRepository,
        CosmosManager cosmosManager)
    {
        _aggregateRepository = aggregateRepository;
        _cosmosManager = cosmosManager;
    }

    public override async Task HandleAsync(ProjectUserReadModelCommand command, CancellationToken cancellationToken)
    {
        var user = await _aggregateRepository.LoadAsync<User, UserId>(command.UserId);
        var userReadmodel = new UserReadModel(user.Id.Value, user.Name);

        await _cosmosManager.UpsertAsync(ContainersConstants.User, userReadmodel);
    }
}
