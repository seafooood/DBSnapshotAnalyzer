using DBSnapshotAnalyzer.Common.Interfaces;
using NLog;

namespace DBSnapshotAnalyzer.Compare.Models
{
    public class CompareSnapshots : CompareBase
    {
        #region Private Members
        private readonly IFileSystemService _fileSystem;
        private readonly IZipService _zip;
        #endregion

        #region Constructor
        public CompareSnapshots(ILogger log, IFileSystemService fileSystem, IZipService zip) : base(log)
        {
            _fileSystem = fileSystem;
            _zip = zip;
        }
        #endregion

        /// <summary>
        /// Compare two snapshots
        /// </summary>
        /// <param name="snapshot1"></param>
        /// <param name="snapshot2"></param>
        /// <returns></returns>
        public override List<Comparison> Compare(string snapshot1, string snapshot2)
        {
            _log.Trace("Opening snapshot files");
            string snapshotFolder1 = _zip.OpenSnapshot(snapshot1);
            string snapshotFolder2 = _zip.OpenSnapshot(snapshot2);

            _log.Trace("Comparing snapshots");
            var result = new List<Comparison>();
            var ct = new CompareTables(_log);
            foreach (var filename in _fileSystem.GetFileNamesFromSnapshot(snapshotFolder1, snapshotFolder2))
            {
                string tableName = _fileSystem.GetTableNameFromFileName(filename);
                _log.Trace($"Comparing table {tableName}");

                try
                {
                    string filePath1 = Path.Combine(snapshotFolder1, filename);
                    string filePath2 = Path.Combine(snapshotFolder2, filename);

                    if (File.Exists(filePath1) && File.Exists(filePath2))
                    {
                        result.AddRange(ct.CompareTableFiles(tableName, filePath1, filePath2));
                    }
                    else
                    {
                        result.Add(new Comparison() { TableName = tableName, Change = Change.Deleted });
                    }
                }
                catch (Exception ex)
                {
                    _log.Error(ex, $"Failed to compare {tableName} because {ex.Message}");
                }
            }

            // Remove snapshot folders
            _fileSystem.RemoveTemporaryFolder(snapshotFolder1);
            _fileSystem.RemoveTemporaryFolder(snapshotFolder2);

            return result;
        }
    }
}