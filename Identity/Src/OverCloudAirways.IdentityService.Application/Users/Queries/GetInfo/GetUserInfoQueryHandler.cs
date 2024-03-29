﻿using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.IdentityService.Application.Users.Queries.GetInfo;

internal class GetUserInfoQueryHandler : QueryHandler<GetUserInfoQuery, UserDto>
{
    private readonly ICosmosManager _cosmosManager;

    public GetUserInfoQueryHandler(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<UserDto> HandleAsync(GetUserInfoQuery query, CancellationToken cancellationToken)
    {
        var sql = @$"
                    SELECT 
                    user.UserId    AS {nameof(UserDto.Id)}, 
                    user.UserType  AS {nameof(UserDto.UserType)}, 
                    user.Email     AS {nameof(UserDto.Email)}, 
                    user.GivenName AS {nameof(UserDto.GivenName)},
                    user.Surname   AS {nameof(UserDto.Surname)}
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