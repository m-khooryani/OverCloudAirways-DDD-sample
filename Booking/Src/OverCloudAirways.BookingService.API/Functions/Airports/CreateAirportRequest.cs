using OverCloudAirways.BookingService.Domain.Airports;

namespace OverCloudAirways.BookingService.API.Functions.Airports;

public class CreateAirportRequest
{
    public string Code { get; init; }
    public string Name { get; init; }
    public string Location { get; init; }
    public IReadOnlyList<Terminal> Terminals { get; init; }
}
