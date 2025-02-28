using System.Data.SqlClient;

namespace CrdWindowsFormsAdoNet.DataAccess
{
    public class DbFactory
    {
        private readonly string connectionString = AppSettings.Default.DbConnection;

        public SqlConnection Create() => new SqlConnection(connectionString);
    }
}
