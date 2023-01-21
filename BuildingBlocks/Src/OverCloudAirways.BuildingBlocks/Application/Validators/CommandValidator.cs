using DArch.Application.Contracts;

namespace FluentValidation;

public abstract class CommandValidator<T> : AbstractValidator<T>
    where T : ICommandBase
{
}
