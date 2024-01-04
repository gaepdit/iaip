using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;

namespace Iaip.CxApi.DbHelper;

public class DbHelper : IDbHelper
{
    private string _connectionString = "";

    public void SetConnectionString(string value) => _connectionString = value;

    public string SpGetString(string spName) => SpGetString(spName, Array.Empty<SqlParameter>());

    public string SpGetString(string spName, SqlParameter[] parameterArray) =>
        GetNullable<string>(SpExecuteScalar(spName, parameterArray)) ?? string.Empty;

    public bool SpGetBoolean(string spName) =>
        (bool)(SpExecuteScalar(spName, Array.Empty<SqlParameter>()) ?? throw new InvalidOperationException());

    public bool SpGetBoolean(string spName, SqlParameter[] parameterArray) =>
        (bool)(SpExecuteScalar(spName, parameterArray) ?? throw new InvalidOperationException());

    public int SpRunCommand(string spName, SqlParameter[] parameterArray) =>
        SpExecuteNonQuery(spName, parameterArray);
    
    // Database connection procedures

    /// <summary>
    /// Retrieves a single value from the database by calling a stored procedure.
    /// </summary>
    /// <param name="spName">The name of the stored procedure to execute.</param>
    /// <param name="parameterArray">An array of SqlParameter values. The array should not include
    /// output parameters.</param>
    /// <returns>The first column of the first row in the result set, or a null reference if
    /// the result set is empty.</returns>
    private object? SpExecuteScalar(string spName, SqlParameter[] parameterArray)
    {
        Guard.NotNullOrWhiteSpace(spName);
        foreach (var parameter in parameterArray) parameter.Value ??= DBNull.Value;

        using var connection = new SqlConnection(_connectionString);
        using var sqlCommand = new SqlCommand(spName, connection);

        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.AddRange(parameterArray);

        sqlCommand.Connection.Open();
        var result = sqlCommand.ExecuteScalar();
        sqlCommand.Connection.Close();

        sqlCommand.Parameters.Clear();

        return result;
    }

    /// <summary>
    /// Executes a non-query stored procedure on the database.
    /// </summary>
    /// <param name="spName">The name of the stored procedure to execute.</param>
    /// <param name="parameterArray">An array of SqlParameter values. The array should not include
    /// output parameters.</param>
    /// <returns>The RETURN value of the stored procedure.</returns>
    private int SpExecuteNonQuery(string spName, SqlParameter[] parameterArray)
    {
        Guard.NotNullOrWhiteSpace(spName);
        foreach (var parameter in parameterArray) parameter.Value ??= DBNull.Value;

        using var connection = new SqlConnection(_connectionString);
        using var sqlCommand = new SqlCommand(spName, connection);

        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.AddRange(parameterArray);

        var returnParameter = ReturnValueParameter();
        sqlCommand.Parameters.Add(returnParameter);

        sqlCommand.Connection.Open();
        sqlCommand.ExecuteNonQuery();
        sqlCommand.Connection.Close();

        var returnValue = (int)returnParameter.Value;
        sqlCommand.Parameters.Clear();

        return returnValue;
    }

    private static T? GetNullable<T>(object? obj) =>
        obj is null || Convert.IsDBNull(obj) ? default : Conversions.ToGenericParameter<T>(obj);

    private static SqlParameter ReturnValueParameter() =>
        new("@DbHelperReturnValue", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
}
