using System.Data;
using System.Data.SqlClient;

namespace MvcSolution
{
    public class SqlHelper
    {
        public static void BulkCopy(DataTable table, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;
                connection.Open();
                try
                {
                    transaction = connection.BeginTransaction();
                    using (var sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        sqlBulkCopy.BatchSize = table.Rows.Count;
                        sqlBulkCopy.DestinationTableName = table.TableName;
                        sqlBulkCopy.MapColumns(table);
                        sqlBulkCopy.WriteToServer(table);
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction?.Rollback();
                    throw;
                }
            }
        }
    }
}
