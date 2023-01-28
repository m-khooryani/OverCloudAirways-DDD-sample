using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using OverCloudAirways.BuildingBlocks.Application.Models;
using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

internal class CosmosManager : ICosmosManager
{
    private readonly ILogger _logger;
    private readonly Database _database;

    public CosmosManager(ILogger logger, Database database)
    {
        _logger = logger;
        _database = database;
    }

    private Container GetContainer(string containerName)
    {
        return _database.GetContainer(containerName);
    }

    public async Task<T[]> AsListAsync<T>(string containerName, string sql)
    {
        var queryDefinition = new QueryDefinition(sql);
        return await AsListAsync<T>(containerName, queryDefinition);
    }

    public async Task<T[]> AsListAsync<T>(string containerName, QueryDefinition query)
    {
        var container = GetContainer(containerName);
        _logger.LogInformation("Executing Query on DB:");
        _logger.LogInformation(query.QueryText);

        var queryResult = container.GetItemQueryIterator<T>(query);

        var items = new List<T>();
        while (queryResult.HasMoreResults)
        {
            var currentResultSet = await queryResult.ReadNextAsync();
            items.AddRange(currentResultSet);
        }

        return items.ToArray();
    }

    public async Task<PagedDto<T>> AsPagedAsync<T>(
        string containerName,
        PageableQuery<T> request,
        string sql,
        string fromWhere)
    {
        return await AsPagedAsync(
            containerName,
            request,
            sql,
            new QueryDefinition(fromWhere));
    }

    public async Task<PagedDto<T>> AsPagedAsync<T>(
        string containerName,
        PageableQuery<T> request,
        QueryDefinition queryDefinition,
        string fromWhere)
    {
        var container = GetContainer(containerName);
        var pagableQueryDefinition = new QueryDefinition(
            queryDefinition.QueryText + $" OFFSET {request.PageSize * (request.PageNumber - 1)} LIMIT {request.PageSize}");
        var @params = queryDefinition.GetQueryParameters();
        foreach (var queryParameter in @params)
        {
            pagableQueryDefinition = pagableQueryDefinition.WithParameter(queryParameter.Name, queryParameter.Value);
        }

        _logger.LogInformation("Executing Query on DB:");
        _logger.LogInformation(queryDefinition.QueryText);
        var queryResultSetIterator = container.GetItemQueryIterator<T>(pagableQueryDefinition);

        var items = new List<T>();
        while (queryResultSetIterator.HasMoreResults)
        {
            var currentResultSet = await queryResultSetIterator.ReadNextAsync();
            items.AddRange(currentResultSet);
        }

        var totalItemsSql = $"SELECT VALUE COUNT(1) {fromWhere}";
        var totalItemsQuery = new QueryDefinition(totalItemsSql);
        foreach (var queryParameter in @params)
        {
            totalItemsQuery = totalItemsQuery.WithParameter(queryParameter.Name, queryParameter.Value);
        }
        var pagedDto = new PagedDto<T>
        {
            Items = items.ToArray(),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalItems = await QuerySingleAsync<int>(containerName, totalItemsQuery),
        };
        pagedDto.TotalPages = (int)Math.Ceiling(pagedDto.TotalItems * 1.0 / pagedDto.PageSize);

        return pagedDto;
    }

    public async Task<PagedDto<T>> AsPagedAsync<T>(
        string containerName,
        PageableQuery<T> request,
        string sql,
        QueryDefinition fromWhere)
    {
        var container = GetContainer(containerName);
        var queryDefinition = new QueryDefinition(sql + $" OFFSET {request.PageSize * (request.PageNumber - 1)} LIMIT {request.PageSize}");
        var queryResultSetIterator = container.GetItemQueryIterator<T>(queryDefinition);

        var items = new List<T>();
        while (queryResultSetIterator.HasMoreResults)
        {
            var currentResultSet = await queryResultSetIterator.ReadNextAsync();
            items.AddRange(currentResultSet);
        }

        _logger.LogInformation("Executing Query on DB:");
        _logger.LogInformation(queryDefinition.QueryText);

        var totalItemsSql = $"SELECT VALUE COUNT(1) {fromWhere.QueryText}";
        var queryParameters = fromWhere.GetQueryParameters();
        var totalItemsQuery = new QueryDefinition(totalItemsSql);
        foreach (var queryParameter in queryParameters)
        {
            totalItemsQuery = totalItemsQuery.WithParameter(queryParameter.Name, queryParameter.Value);
        }
        var pagedDto = new PagedDto<T>
        {
            Items = items.ToArray(),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalItems = await QuerySingleAsync<int>(containerName, totalItemsQuery),
        };
        pagedDto.TotalPages = (int)Math.Ceiling(pagedDto.TotalItems * 1.0 / pagedDto.PageSize);

        return pagedDto;
    }

    public async Task<T> QuerySingleAsync<T>(string containerName, string sql)
    {
        return await QuerySingleAsync<T>(containerName, new QueryDefinition(sql));
    }

    public async Task<T> QuerySingleAsync<T>(string containerName, QueryDefinition queryDefinition)
    {
        var container = GetContainer(containerName);
        _logger.LogInformation("Executing Query on DB:");
        _logger.LogInformation(queryDefinition.QueryText);
        var results = container.GetItemQueryIterator<T>(queryDefinition);

        var item = await results.ReadNextAsync();

        return item.Resource.FirstOrDefault();
    }

    public async Task UpsertAsync(string containerName, ReadModel item)
    {
        var container = GetContainer(containerName);
        _logger.LogInformation("Upserting item...");

        var response = await container.UpsertItemAsync(item, new PartitionKey(item.PartitionKey));
        _logger.LogInformation($"Response: {response}");

        if (!(200 <= (int)response.StatusCode && (int)response.StatusCode < 300))
        {
            throw new Exception();
        }
    }
}
