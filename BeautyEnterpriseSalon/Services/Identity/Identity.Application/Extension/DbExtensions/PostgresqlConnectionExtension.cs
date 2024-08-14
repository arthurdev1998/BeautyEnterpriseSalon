using System.Data;
using BuildinBlocks.Core.Data;
using Dapper;

namespace Identity.Application.Extension.DbExtensions;

public static class PostgresqlConnectionExtension
{
    public async static Task<PagedResults<T>> PagedQueryAsync<T>(this IDbConnection connection, string query, int pageIndex, int pageSize, object? parameters = null) where T : class
    {
        var pagedQuery = query + $@" OFFSET {pageSize * (pageIndex - 1)} ROWS
                                     FETCH NEXT {pageSize} ROWS ONLY";

        IEnumerable<T> result = await connection.QueryAsync<T>(pagedQuery, parameters);

        int totalResults = await connection.RowsCounterAsync(query);

        return new PagedResults<T>(result, totalResults, pageIndex, pageSize);
    }

    public async static Task<int> RowsCounterAsync(this IDbConnection connection, string query)
    {
        string queryCount = $"SELECT COUNT(*) FROM ({query})";

        return await connection.QueryFirstOrDefaultAsync<int>(queryCount);
    }
}