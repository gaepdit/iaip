﻿using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;

namespace Iaip.CxApi.DbHelper;

public class DbHelper : IDbHelper
{
    private string _connectionString = "";

    public void SetConnectionString(string value) => _connectionString = value;

    public Task<string> SpGetStringAsync(string spName) => SpGetStringAsync(spName, []);

    public async Task<string> SpGetStringAsync(string spName, SqlParameter[] parameterArray) =>
        GetNullable<string>(await SpExecuteScalarAsync(spName, parameterArray)) ?? string.Empty;

    public async Task<bool> SpGetBooleanAsync(string spName) =>
        (bool)(await SpExecuteScalarAsync(spName, []) ?? throw new InvalidOperationException());

    // Database connection procedures

    /// <summary>
    /// Retrieves a single value from the database by calling a stored procedure.
    /// </summary>
    /// <param name="spName">The name of the stored procedure to execute.</param>
    /// <param name="parameterArray">An array of SqlParameter values. The array should not include
    /// output parameters.</param>
    /// <returns>The first column of the first row in the result set, or a null reference if
    /// the result set is empty.</returns>
    private async Task<object?> SpExecuteScalarAsync(string spName, SqlParameter[] parameterArray)
    {
        Guard.NotNullOrWhiteSpace(spName);
        foreach (var parameter in parameterArray) parameter.Value ??= DBNull.Value;

        await using var connection = new SqlConnection(_connectionString);
        await using var sqlCommand = new SqlCommand(spName, connection);

        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.AddRange(parameterArray);

        await sqlCommand.Connection.OpenAsync().ConfigureAwait(false);
        var result = await sqlCommand.ExecuteScalarAsync().ConfigureAwait(false);
        await sqlCommand.Connection.CloseAsync().ConfigureAwait(false);

        sqlCommand.Parameters.Clear();

        return result;
    }

    private static T? GetNullable<T>(object? obj) =>
        obj is null || Convert.IsDBNull(obj) ? default : Conversions.ToGenericParameter<T>(obj);
}
