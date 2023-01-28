using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.BuildingBlocks.Application.Validators;

public abstract class QueryValidator<T> : AbstractValidator<T>
    where T : IQueryBase
{
}
