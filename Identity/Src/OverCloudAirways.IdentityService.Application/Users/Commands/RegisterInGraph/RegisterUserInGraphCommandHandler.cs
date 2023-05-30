using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.RegisterInGraph;

internal class RegisterUserInGraphCommandHandler : CommandHandler<RegisterUserInGraphCommand>
{
    private readonly IAggregateRepository _repository;
    private readonly IGraphAPIUserRegistrationService _registrationService;

    public RegisterUserInGraphCommandHandler(
        IAggregateRepository repository,
        IGraphAPIUserRegistrationService registrationService)
    {
        _repository = repository;
        _registrationService = registrationService;
    }

    public override async Task HandleAsync(RegisterUserInGraphCommand command, CancellationToken cancellationToken)
    {
        var user = await _repository.LoadAsync<User, UserId>(command.UserId);

        await user.RegisterInGraphAsync(_registrationService, cancellationToken);
    }
}