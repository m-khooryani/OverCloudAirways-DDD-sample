using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Domain.Airports.Rules;

internal class AirportCodeShouldBeUniqueRule : IBusinessRule
{
    private readonly string _code;
    private readonly IAirportCodeUniqueChecker _airportCodeUniqueChecker;

    public AirportCodeShouldBeUniqueRule(string code, IAirportCodeUniqueChecker airportCodeUniqueChecker)
    {
        _code = code;
        _airportCodeUniqueChecker = airportCodeUniqueChecker;
    }

    public string TranslationKey => "Airport_Code_Should_Be_Unique";

    public async Task<bool> IsFollowedAsync()
    {
        return await _airportCodeUniqueChecker.IsUniqueAsync(_code);
    }
}
