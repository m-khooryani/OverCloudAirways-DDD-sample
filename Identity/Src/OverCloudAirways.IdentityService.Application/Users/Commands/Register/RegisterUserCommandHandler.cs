using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.Register;

internal class RegisterUserCommandHandler : CommandHandler<RegisterUserCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public RegisterUserCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override Task HandleAsync(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var user = User.RegisterAsync(
            command.UserId,
            command.UserType,
            command.Email,
            command.GivenName,
            command.Surname,
            command.Address);

        _aggregateRepository.Add(user);

        return Task.CompletedTask;
    }
}