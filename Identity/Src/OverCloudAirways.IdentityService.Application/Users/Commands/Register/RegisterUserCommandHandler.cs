using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DArch.Application.Configuration.Commands;
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
        var user = User.Register(command.UserId, command.Name);

        _aggregateRepository.Add<User, UserId>(user);

        return Task.CompletedTask;
    }
}
