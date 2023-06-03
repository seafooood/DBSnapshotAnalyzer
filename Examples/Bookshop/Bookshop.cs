using Oracle.ManagedDataAccess.Client;

namespace Examples.Bookshop
{
    public class Bookshop
    {
        #region Public Methods
        public void InsertRow(string connectionString)
        {
            ExecuteCommand(connectionString, "Insert into BOOKS (id,title,author) values (8,'myTitle','myAuthor')");
        }
        #endregion

        #region Private Methods
        private void ExecuteCommand(string connectionString, string query)
        {
            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Connection.Open();
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to execute database query '{query}' because {ex.Message}", ex);
            }
        }
        #endregion
    }
}