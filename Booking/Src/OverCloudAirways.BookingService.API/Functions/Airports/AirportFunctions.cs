using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using OverCloudAirways.BookingService.API.FunctionsMiddlewares;
using OverCloudAirways.BookingService.Application.Airports.Commands.Create;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure;

namespace OverCloudAirways.BookingService.API.Functions.Airports;

public class AirportFunctions
{
    private readonly CqrsInvoker _cqrsInvoker;
    private readonly IJsonSerializer _jsonSerializer;

    public AirportFunctions(
        CqrsInvoker cqrsInvoker,
        IJsonSerializer jsonSerializer)
    {
        _cqrsInvoker = cqrsInvoker;
        _jsonSerializer = jsonSerializer;
    }

    [Function("create-airport")]
    [Authorized("AirlineStaff")]
    public async Task CreateUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "airports")] HttpRequestData req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var request = _jsonSerializer.Deserialize<CreateAirportRequest>(requestBody);

        var command = new CreateAirportCommand(
            AirportId.New(),
            request.Code,
            request.Name,
            request.Location,
            request.Terminals);
        await _cqrsInvoker.CommandAsync(command);
    }
}
