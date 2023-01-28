using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BuildingBlocks.Application.Validators;

public abstract class CommandValidator<T> : AbstractValidator<T>
    where T : ICommandBase
{
}
