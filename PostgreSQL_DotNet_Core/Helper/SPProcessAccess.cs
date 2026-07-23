using Npgsql;
using System.Data;

namespace PostgreSQL_DotNet_Core.Helper
{
    public class SPProcessAccess
    {
        private readonly SPDataAccess? _dataAccess;

        public SPProcessAccess(string? connectionString)
        {
            if (connectionString != null)
            {
                _dataAccess = new SPDataAccess(connectionString);
            }
        }

        // Shared internal method that formats and runs the query statement against Postgres functions
        public DataSet? ExecutePostgresFunction(string functionName, Dictionary<string, object?> parameters)
        {
            try
            {
                // Dynamic placeholders: $1, $2, $3...
                var placeholders = new List<string>();
                using var cmd = new NpgsqlCommand();
                int index = 1;

                foreach (var param in parameters)
                {
                    placeholders.Add($"${index}");

                    // Convert DBNull or pass values safely
                    var value = param.Value ?? DBNull.Value;
                    cmd.Parameters.AddWithValue(value);

                    index++;
                }

                // Generates format: SELECT * FROM itv_acc.SP_ACCOUNTS_MGT($1, $2, $3...)
                cmd.CommandText = $"SELECT * FROM {functionName}({string.Join(", ", placeholders)});";
                cmd.CommandType = CommandType.Text;

                if (_dataAccess == null)
                {
                   // ErrorTrackingExtension.SetError(new NullReferenceException("_dataAccess is null."));
                    return null;
                }

                return _dataAccess.GetDataSet(cmd);
            }
            catch (Exception exp)
            {
              //  ErrorTrackingExtension.SetError(exp);
                return null;
            }
        }

        public DataSet? GetTransInfo20(string comCode, string SQLprocName, string CallType,
            string mDesc1 = "", string mDesc2 = "", string mDesc3 = "", string mDesc4 = "", string mDesc5 = "",
            string mDesc6 = "", string mDesc7 = "", string mDesc8 = "", string mDesc9 = "", string mDesc10 = "",
            string mDesc11 = "", string mDesc12 = "", string mDesc13 = "", string mDesc14 = "", string mDesc15 = "",
            string mDesc16 = "", string mDesc17 = "", string mDesc18 = "", string mDesc19 = "", string mDesc20 = "",
            string userid = "")
        {
          //  ErrorTrackingExtension.ClearErrors();

            var parameters = new Dictionary<string, object?>
            {
                { "Comcod", comCode },
                { "CallType", CallType },
                { "Desc1", mDesc1 }, { "Desc2", mDesc2 }, { "Desc3", mDesc3 }, { "Desc4", mDesc4 }, { "Desc5", mDesc5 },
                { "Desc6", mDesc6 }, { "Desc7", mDesc7 }, { "Desc8", mDesc8 }, { "Desc9", mDesc9 }, { "Desc10", mDesc10 },
                { "Desc11", mDesc11 }, { "Desc12", mDesc12 }, { "Desc13", mDesc13 }, { "Desc14", mDesc14 }, { "Desc15", mDesc15 },
                { "Desc16", mDesc16 }, { "Desc17", mDesc17 }, { "Desc18", mDesc18 }, { "Desc19", mDesc19 }, { "Desc20", mDesc20 },
                { "UserID", userid }
            };

            return ExecutePostgresFunction(SQLprocName, parameters);
        }

        public DataSet? GetTransInfo50(string comCode, string SQLprocName, string CallType,
            // ... truncated for space, keep your exactly same 50 signatures
            string userid = "")
        {
            //ErrorTrackingExtension.ClearErrors();

            var parameters = new Dictionary<string, object?>
            {
                { "Comcod", comCode },
                { "CallType", CallType }
                // Bind all your 50 params here into the dictionary just like above
            };

            return ExecutePostgresFunction(SQLprocName, parameters);
        }

        public bool UpdateTransInf20(string comCode, string SQLprocName, string CallType,
            string mDesc1 = "", string mDesc2 = "", string mDesc3 = "", string mDesc4 = "", string mDesc5 = "",
            string mDesc6 = "", string mDesc7 = "", string mDesc8 = "", string mDesc9 = "", string mDesc10 = "",
            string mDesc11 = "", string mDesc12 = "", string mDesc13 = "", string mDesc14 = "", string mDesc15 = "",
            string mDesc16 = "", string mDesc17 = "", string mDesc18 = "", string mDesc19 = "", string mDesc20 = "")
        {
            try
            {
               // ErrorTrackingExtension.ClearErrors();
                using var cmd = new NpgsqlCommand();

                var parameters = new List<object> { comCode, CallType, mDesc1, mDesc2, mDesc3, mDesc4, mDesc5, mDesc6, mDesc7, mDesc8, mDesc9, mDesc10, mDesc11, mDesc12, mDesc13, mDesc14, mDesc15, mDesc16, mDesc17, mDesc18, mDesc19, mDesc20 };
                var placeholders = parameters.Select((_, i) => $"${i + 1}").ToList();

                // DML Update operations inside function executed via SELECT
                cmd.CommandText = $"SELECT * FROM {SQLprocName}({string.Join(", ", placeholders)});";
                cmd.CommandType = CommandType.Text;

                foreach (var val in parameters)
                {
                    cmd.Parameters.AddWithValue(val ?? DBNull.Value);
                }

                if (_dataAccess == null) return false;
                return _dataAccess.ExecuteCommand(cmd);
            }
            catch (Exception exp)
            {
               // ErrorTrackingExtension.SetError(exp);
                return false;
            }
        }

    }
}
