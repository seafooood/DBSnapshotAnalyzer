using System;
using System.Data;
using System.IO;
using DBSnapshotAnalyzer.Common.Services;
using DBSnapshotAnalyzer.Snapshot.Interfaces;

namespace DBSnapshotAnalyzer.Snapshot.Models
{
    public class Snapshot
    {
        #region Private Members

        private readonly IDatabaseService _db;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db">Database connection</param>
        public Snapshot(IDatabaseService db)
        {
            _db = db;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Take snapshot
        /// </summary>
        /// <param name="outputFolder">Snapshot output file</param>
        public void Take(string snapshotFilename)
        {
            var fss = new FileSystemService();
            string outputFolder = fss.CreateTemporaryFolder();

            foreach (var tableName in _db.GetTableNames())
            {
                ExportTable(tableName, outputFolder);
            }

            var zs = new ZipService();
            zs.SaveSnapshot(snapshotFilename, outputFolder);
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
                Console.WriteLine($"Taking snapshot of table {tableName}");
                var dataTable = _db.GetTableData(tableName);

                Console.WriteLine($"\tExporting snapshot of table {tableName}");
                var outputFile = Path.Combine(outputFolder, tableName + ".csv");
                ExportDataTableToCsv(dataTable, outputFile);
                Console.WriteLine($"\tExported table {tableName} to file {outputFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\tFailed to export {tableName} because {ex.Message}", ex);
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
