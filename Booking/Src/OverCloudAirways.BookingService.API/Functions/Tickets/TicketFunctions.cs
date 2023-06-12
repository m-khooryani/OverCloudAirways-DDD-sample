using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using OverCloudAirways.BookingService.API.FunctionsMiddlewares;
using OverCloudAirways.BookingService.Application.Tickets.Commands.Issue;
using OverCloudAirways.BookingService.Domain.Tickets;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure;

namespace OverCloudAirways.BookingService.API.Functions.Tickets;

public class TicketFunctions
{
    private readonly CqrsInvoker _cqrsInvoker;
    private readonly IJsonSerializer _jsonSerializer;

    public TicketFunctions(
        CqrsInvoker cqrsInvoker,
        IJsonSerializer jsonSerializer)
    {
        _cqrsInvoker = cqrsInvoker;
        _jsonSerializer = jsonSerializer;
    }

    [Function("issue-ticket")]
    [Authorized("AirlineStaff")]
    public async Task IssueTicket(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users")] HttpRequestData req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var request = _jsonSerializer.Deserialize<IssueTicketRequest>(requestBody);

        var registerUserCommand = new IssueTicketCommand(
            TicketId.New(),
            request.FlightId,
            request.CustomerId);
        await _cqrsInvoker.CommandAsync(registerUserCommand);
    }
}
