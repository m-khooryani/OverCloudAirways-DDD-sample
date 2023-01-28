using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Aircrafts.Commands.ProjectReadModel;

internal record ProjectAircraftReadModelCommand(AircraftId AircraftId) : Command;
