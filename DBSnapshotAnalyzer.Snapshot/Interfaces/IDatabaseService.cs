using System.Collections.Generic;
using System.Data;

namespace DBSnapshotAnalyzer.Snapshot.Interfaces
{
    public interface IDatabaseService
    {
        /// <summary>
        /// Get a list of all the tables in the database
        /// </summary>
        /// <returns></returns>
        List<string> GetTableNames();

        /// <summary>
        /// Get all data from a table
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataTable GetTableData(string tableName);
    }
}