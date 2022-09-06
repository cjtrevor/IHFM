using MFiles.VAF.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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
        private const string connectionString = @"Server=WIN-L5VS4AH5TL5\MSSQLSERVER01;Database=MFILESVAF;User Id=MFilesVAF;Password=MFilesVAF;";

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
            catch(Exception ex) {
                SysUtils.ReportErrorToEventLog($"Export: {storedProc.procedureName} Error: {ex.Message}");
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string ExecuteStoredProcScalar(StoredProc storedProc)
        {
            string returnValue = "";

            sqlCommand = new SqlCommand(storedProc.procedureName, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (KeyValuePair<string, object> kvp in storedProc.storedProcParams)
            {
                sqlCommand.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
            }

            try
            {
                sqlConnection.Open();
                returnValue = sqlCommand.ExecuteScalar().ToString();
            }
            catch (Exception ex) { }
            finally
            {
                sqlConnection.Close();
            }

            return returnValue;
        }

        public Dictionary<string, object> GetStoredProcParams<T>(T export)
        {
            Dictionary<string, object> props = new Dictionary<string, object>();

            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            foreach (PropertyInfo info in propertyInfos)
            {
                props.Add($"@{info.Name}", info.GetValue(export));
            }

            return props;
        }
    }
}
