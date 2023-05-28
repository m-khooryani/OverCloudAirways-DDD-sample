using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.RegisterAirlineStaff;

internal class RegisterAirlineStaffUserCommandHandler : CommandHandler<RegisterAirlineStaffUserCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public RegisterAirlineStaffUserCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override Task HandleAsync(RegisterAirlineStaffUserCommand command, CancellationToken cancellationToken)
    {
        var user = User.RegisterAsync(
            command.UserId,
            UserType.AirlineStaff,
            command.Email,
            command.GivenName,
            command.Surname,
            command.Address);

        _aggregateRepository.Add(user);

        return Task.CompletedTask;
    }
}