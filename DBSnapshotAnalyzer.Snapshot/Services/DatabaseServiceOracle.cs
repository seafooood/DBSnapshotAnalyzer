using System;
using System.Collections.Generic;
using System.Data;
using DBSnapshotAnalyzer.Snapshot.Interfaces;
using Oracle.ManagedDataAccess.Client;

namespace DBSnapshotAnalyzer.Snapshot.Services
{
    public class DatabaseServiceOracle : IDatabaseService
    {
        #region Private Members
        private readonly string _connectionString;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public DatabaseServiceOracle(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Get a list of all the tables in the database
        /// </summary>
        /// <returns></returns>
        public List<string> GetTableNames()
        {
            return ConvertDataTableToList(ExecuteQuery("SELECT table_name FROM user_tables"));
        }

        /// <summary>
        /// Get all data from a table
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns>DataTable</returns>
        public DataTable GetTableData(string tableName)
        {
            return ExecuteQuery($"SELECT * FROM \"{tableName}\"");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Execute a database query
        /// </summary>
        /// <param name="query">SQL statement</param>
        /// <returns>DataTable</returns>
        /// <exception cref="Exception"></exception>
        private DataTable ExecuteQuery(string query)
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    using (var selectCommand = new OracleCommand(query, connection))
                    {
                        selectCommand.Connection.Open();
                        using (var adapter = new OracleDataAdapter(selectCommand))
                        {
                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to execute database query '{query}' because {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Convert the dataTable to a list
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private List<string> ConvertDataTableToList(DataTable dataTable)
        {
            List<string> resultList = new List<string>();

            // Iterate through each row in the DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Iterate through each column in the row
                foreach (DataColumn column in dataTable.Columns)
                {
                    // Add the value of the column to the result list
                    resultList.Add(row[column].ToString());
                }
            }

            return resultList;
        }

        #endregion
    }
}
