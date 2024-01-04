using Microsoft.Data.SqlClient;

namespace Iaip.CxApi.DbHelper;

public interface IDbHelper
{
    void SetConnectionString(string value);

    string SpGetString(string spName);
    string SpGetString(string spName, SqlParameter[] parameterArray);
    bool SpGetBoolean(string spName);
    bool SpGetBoolean(string spName, SqlParameter[] parameterArray);
    int SpRunCommand(string spName, SqlParameter[] parameterArray);
}
