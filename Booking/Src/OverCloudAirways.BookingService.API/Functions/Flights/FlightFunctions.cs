﻿using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using OverCloudAirways.BookingService.API.Functions.Flights.Requests;
using OverCloudAirways.BookingService.API.FunctionsMiddlewares;
using OverCloudAirways.BookingService.Application.Flights.Commands.Cancel;
using OverCloudAirways.BookingService.Application.Flights.Commands.ChangeCapacity;
using OverCloudAirways.BookingService.Application.Flights.Commands.ChangeStatus;
using OverCloudAirways.BookingService.Application.Flights.Commands.ReplaceAircraft;
using OverCloudAirways.BookingService.Application.Flights.Commands.ReserveSeats;
using OverCloudAirways.BookingService.Application.Flights.Commands.Schedule;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure;

namespace OverCloudAirways.BookingService.API.Functions.Flights;

public class FlightFunctions
{
    private readonly CqrsInvoker _cqrsInvoker;
    private readonly IJsonSerializer _jsonSerializer;

    public FlightFunctions(
        CqrsInvoker cqrsInvoker,
        IJsonSerializer jsonSerializer)
    {
        _cqrsInvoker = cqrsInvoker;
        _jsonSerializer = jsonSerializer;
    }

    [Function("schedule-flight")]
    [Authorized("AirlineStaff")]
    public async Task ScheduleFlightAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "flights")] HttpRequestData req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var request = _jsonSerializer.Deserialize<ScheduleFlightRequest>(requestBody);

        var command = new ScheduleFlightCommand(
            FlightId.New(),
            request.Number,
            request.DepartureAirportId,
            request.DepartureAirportId,
            request.DepartureTime,
            request.ArrivalTime,
            request.Route,
            request.Distance,
            request.AircraftId,
            request.AvailableSeats,
            request.MaximumLuggageWeight);
        await _cqrsInvoker.CommandAsync(command);
    }

    [Function("cancel-flight")]
    [Authorized("AirlineStaff")]
    public async Task CancelFlightAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "flights/cancel")] HttpRequestData req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var request = _jsonSerializer.Deserialize<CancelFlightRequest>(requestBody);

        var command = new CancelFlightCommand(request.FlightId);
        await _cqrsInvoker.CommandAsync(command);
    }

    [Function("change-flight-capacity")]
    [Authorized("AirlineStaff")]
    public async Task ChangeFlightCapacityAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "flights/change-capacity")] HttpRequestData req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var request = _jsonSerializer.Deserialize<ChangeFlightCapacityRequest>(requestBody);

        var command = new ChangeFlightCapacityCommand(
            request.FlightId,
            request.Capacity);
        await _cqrsInvoker.CommandAsync(command);
    }

    [Function("change-flight-status")]
    [Authorized("AirlineStaff")]
    public async Task ChangeFlightStatusAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "flights/change-status")] HttpRequestData req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var request = _jsonSerializer.Deserialize<ChangeFlightStatusRequest>(requestBody);

        var command = new ChangeFlightStatusCommand(
            request.FlightId,
            request.Status);
        await _cqrsInvoker.CommandAsync(command);
    }

    [Function("reserve-flight-seats")]
    [Authorized("Customer")]
    public async Task ReserveFlightSeatsAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "flights/reserve-seats")] HttpRequestData req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var request = _jsonSerializer.Deserialize<ReserveFlightSeatsRequest>(requestBody);

        var command = new ReserveFlightSeatsCommand(
            request.FlightId,
            request.Passengers);
        await _cqrsInvoker.CommandAsync(command);
    }

    [Function("replace-flight-aircraft")]
    [Authorized("AirlineStaff")]
    public async Task ReplaceFlightAircraftAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "flights/replace-aircraft")] HttpRequestData req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var request = _jsonSerializer.Deserialize<ReplaceFlightAircraftRequest>(requestBody);

        var command = new ReplaceFlightAircraftCommand(
            request.FlightId,
            request.AircraftId);
        await _cqrsInvoker.CommandAsync(command);
    }
}
