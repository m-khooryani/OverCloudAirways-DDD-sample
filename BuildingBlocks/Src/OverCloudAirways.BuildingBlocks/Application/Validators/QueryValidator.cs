using DArch.Application.Contracts;

namespace FluentValidation;

public abstract class QueryValidator<T> : AbstractValidator<T>
    where T : IQueryBase
{
}
