using DBSnapshotAnalyzer.Common.Services;
using DBSnapshotAnalyzer.Compare.Models;
using DBSnapshotAnalyzer.Snapshot.Services;
using NLog;

namespace DBSnapshotAnalyzer.Compare
{
    public class SnapshotAnalyzer
    {
        #region Private Members
        private readonly ILogger _log;
        #endregion

        #region Constructor
        public SnapshotAnalyzer() : this(LogManager.GetCurrentClassLogger())
        {
        }
        public SnapshotAnalyzer(ILogger log)
        {
            _log = log;
        }
        #endregion

        #region Public Methods
        public void TakeSnapshot(string connectionString, string outputFolder)
        {
            var fileSystem = new FileSystemService(_log);
            var zip = new ZipService(_log, fileSystem);
            var db = new DatabaseServiceOracle(connectionString);
            var snapshot = new Snapshot.Models.Snapshot(_log, db, fileSystem, zip);
            snapshot.Take(outputFolder);
        }

        public List<Comparison> CompareSnapshots(string s1, string s2, string outputFile = "")
        {
            var fileSystem = new FileSystemService(_log);
            var zip = new ZipService(_log, fileSystem);
            var cs = new CompareSnapshots(_log, fileSystem, zip);
            return cs.CompareAndSave(s1, s2, outputFile);
        }

        public List<Comparison> AnalyzeComparisons(string c1, string c2, string outputFile = "")
        {
            var ct = new CompareTables(_log);
            return ct.CompareAndSave(c1, c2, outputFile);
        }
        #endregion
    }
}
