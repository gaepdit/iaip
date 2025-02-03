using Microsoft.Data.SqlClient;

namespace Iaip.CxApi.DbHelper;

public interface IDbHelper
{
    void SetConnectionString(string value);
    Task<string> SpGetStringAsync(string spName);
    Task<string> SpGetStringAsync(string spName, SqlParameter[] parameterArray);
    Task<bool> SpGetBooleanAsync(string spName);
}
