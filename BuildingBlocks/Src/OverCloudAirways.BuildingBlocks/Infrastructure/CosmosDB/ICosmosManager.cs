using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Application.Models;
using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

public interface ICosmosManager
{
    Task<T[]> AsListAsync<T>(string containerName, string sql);
    Task<T[]> AsListAsync<T>(string containerName, QueryDefinition query);
    Task<T> QuerySingleAsync<T>(string containerName, string sql);
    Task<T> QuerySingleAsync<T>(string containerName, QueryDefinition queryDefinition);
    Task UpsertAsync(string containerName, ReadModel item);
    Task<PagedDto<T>> AsPagedAsync<T>(string containerName, PageableQuery<T> request, string sql, QueryDefinition fromWhere);
    Task<PagedDto<T>> AsPagedAsync<T>(string containerName, PageableQuery<T> request, string sql, string fromWhere);
    Task<PagedDto<T>> AsPagedAsync<T>(string containerName, PageableQuery<T> request, QueryDefinition queryDefinition, string fromWhere);
}