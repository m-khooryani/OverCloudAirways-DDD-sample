using System.Text;
using FluentValidation;
using MediatR;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.RequestProcessing.CommandPipelines;

public class CommandValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly IValidator<TRequest>[] _validators;

    public CommandValidationBehavior(IValidator<TRequest>[] validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var errorMessages = GetErrorMessages(request);

        if (errorMessages.Any())
        {
            ThrowException(errorMessages);
        }

        return next();
    }

    private IEnumerable<string> GetErrorMessages(TRequest request)
    {
        return _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .Select(x => x.ErrorMessage);
    }

    private static void ThrowException(IEnumerable<string> errorMessages)
    {
        var builder = new StringBuilder("Reason: " + Environment.NewLine);
        builder.Append(string.Join(Environment.NewLine, errorMessages));

        var message = "InvalidCommand " + builder.ToString();
        throw new ValidationException(message);
    }
}
