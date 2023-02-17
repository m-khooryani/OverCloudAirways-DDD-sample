using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.BookingService.Application.Tickets.Commands.Issue;

internal class IssueTicketCommandValidator : CommandValidator<IssueTicketCommand>
{
    public IssueTicketCommandValidator()
    {
        RuleFor(x => x.TicketId)
            .NotEmpty();
        RuleFor(x => x.FlightId)
            .NotEmpty();
        RuleFor(x => x.CustomerId)
            .NotEmpty();
    }
}
