using DBSnapshotAnalyzer.Common.Interfaces;
using DBSnapshotAnalyzer.Snapshot.Interfaces;
using NLog;
using System.Data;

namespace DBSnapshotAnalyzer.Snapshot.Models
{
    public class Snapshot
    {
        #region Private Members
        private readonly ILogger _log;
        private readonly IDatabaseService _db;
        private readonly IFileSystemService _fileSystem;
        private readonly IZipService _zip;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db">Database connection</param>
        public Snapshot(ILogger log, IDatabaseService db, IFileSystemService fileSystemService, IZipService zipService)
        {
            _log = log;
            _db = db;
            _fileSystem = fileSystemService;
            _zip = zipService;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Take snapshot
        /// </summary>
        /// <param name="outputFolder">Snapshot output file</param>
        public void Take(string snapshotFilename)
        {
            string outputFolder = _fileSystem.CreateTemporaryFolder();

            foreach (var tableName in _db.GetTableNames())
            {
                ExportTable(tableName, outputFolder);
            }

            _zip.SaveSnapshot(snapshotFilename, outputFolder);
        }

        /// <summary>
        /// Export database table
        /// </summary>
        /// <param name="tableName">Database table name</param>
        /// <param name="outputFolder">Snapshot output folder</param>
        public void ExportTable(string tableName, string outputFolder)
        {
            try
            {
                _log.Trace($"Taking snapshot of table {tableName}");
                var dataTable = _db.GetTableData(tableName);

                _log.Trace($"\tExporting snapshot of table {tableName}");
                var outputFile = Path.Combine(outputFolder, tableName + ".csv");
                ExportDataTableToCsv(dataTable, outputFile);
                _log.Trace($"\tExported table {tableName} to file {outputFile}");
            }
            catch (Exception ex)
            {
                _log.Error(ex, $"\tFailed to export {tableName} because {ex.Message}");
            }
        }

        /// <summary>
        /// Export data table to CSV file
        /// </summary>
        /// <param name="dataTable">Data table</param>
        /// <param name="filePath">Snapshot output file</param>
        public void ExportDataTableToCsv(DataTable dataTable, string filePath)
        {
            // Write the DataTable content to the CSV file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the column headers
                foreach (DataColumn column in dataTable.Columns)
                {
                    writer.Write(column.ColumnName);
                    writer.Write(",");
                }

                writer.WriteLine();

                // Write the data rows
                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        writer.Write(item);
                        writer.Write(",");
                    }

                    writer.WriteLine();
                }
            }
        }
        #endregion
    }
}
