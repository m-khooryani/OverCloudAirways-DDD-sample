using DArch.Application.Configuration.Queries;
using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.IdentityService.Application.Users.Queries.GetInfo;

internal class GetUserInfoQueryHandler : QueryHandler<GetUserInfoQuery, UserDto>
{
    private readonly CosmosManager _cosmosManager;

    public GetUserInfoQueryHandler(CosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<UserDto> HandleAsync(GetUserInfoQuery query, CancellationToken cancellationToken)
    {
        var sql = @$"
                    SELECT 
                    user.id   AS {nameof(UserDto.Id)}, 
                    user.Name AS {nameof(UserDto.Name)} 
                    FROM user 
                    WHERE 
                    user.id = @userId AND 
                    user.partitionKey = @userId ";

        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@userId", query.UserId);
        var user = await _cosmosManager.QuerySingleAsync<UserDto>(ContainersConstants.User, queryDefinition);

        return user;
    }
}