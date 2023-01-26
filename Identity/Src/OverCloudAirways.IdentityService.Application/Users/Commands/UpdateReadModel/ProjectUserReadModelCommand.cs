using DArch.Application.Contracts;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.UpdateReadModel;

public record ProjectUserReadModelCommand(UserId UserId) : Command;
