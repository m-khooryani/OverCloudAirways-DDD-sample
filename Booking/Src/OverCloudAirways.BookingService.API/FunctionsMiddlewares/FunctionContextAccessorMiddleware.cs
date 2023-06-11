using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.API.FunctionsMiddlewares;
public class FunctionContextAccessorMiddleware : IFunctionsWorkerMiddleware
{
    private UserAccessor FunctionContextAccessor { get; }

    public FunctionContextAccessorMiddleware(IUserAccessor accessor)
    {
        FunctionContextAccessor = accessor as UserAccessor;
    }

    public Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        if (FunctionContextAccessor.FunctionContext != null)
        {
            // This should never happen because the context should be localized to the current Task chain.
            // But if it does happen (perhaps the implementation is bugged), then we need to know immediately so it can be fixed.
            throw new InvalidOperationException($"Unable to initalize {nameof(IUserAccessor)}: context has already been initialized.");
        }

        FunctionContextAccessor.FunctionContext = context;

        return next(context);
    }
}
