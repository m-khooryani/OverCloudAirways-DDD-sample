﻿using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.Register;

internal class RegisterCustomerUserCommandHandler : CommandHandler<RegisterCustomerUserCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public RegisterCustomerUserCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override Task HandleAsync(RegisterCustomerUserCommand command, CancellationToken cancellationToken)
    {
        var user = User.RegisterCustomer(
            command.UserId, 
            command.Email,
            command.GivenName,
            command.Surname,
            command.Address);

        _aggregateRepository.Add(user);

        return Task.CompletedTask;
    }
}
