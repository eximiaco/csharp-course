using CSharpFunctionalExtensions;
using Dapper;
using Eximia.CsharpCourse.SeedWork;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Eximia.CsharpCourse.Orders.Queries;

public class OrdersDataAccess : IService<OrdersDataAccess>
{
    private readonly string _connectionString;

    public OrdersDataAccess(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<Maybe<string>> GetStatusByIdAsync(int id, CancellationToken cancellationToken)
    {
        var sql = @"SELECT Status
                    FROM Orders
                    WHERE Id = @Id";

        DynamicParameters param = new();
        param.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        await using SqlConnection connection = new(_connectionString);
        var status = await connection.QueryFirstOrDefaultAsync<string>(sql, param).WaitAsync(cancellationToken).ConfigureAwait(false);
        if (string.IsNullOrWhiteSpace(status))
            return Maybe.None;
        return status;
    }
}
