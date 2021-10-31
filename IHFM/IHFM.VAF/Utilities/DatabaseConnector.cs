using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public struct StoredProc
    {
        public string procedureName;
        public Dictionary<string, object> storedProcParams;
    }

    public class DatabaseConnector
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private const string connectionString = @"Server=localhost\mssqlserver01;Database=MFILESVAF;User Id=MFilesVAF;Password=MFilesVAF;";

        public DatabaseConnector()
        {
            Setup();
        }

        private void Setup()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public void ExecuteStoredProc(StoredProc storedProc)
        {
            sqlCommand = new SqlCommand(storedProc.procedureName,sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (KeyValuePair<string,object> kvp in storedProc.storedProcParams)
            {
                sqlCommand.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
            }
         
            try
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch {}
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
