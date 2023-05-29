using DBSnapshotAnalyzer.Compare.Models;
using DBSnapshotAnalyzer.Snapshot.Services;

namespace DBSnapshotAnalyzer.Compare
{
    public class SnapshotAnalyzer
    {
        public void TakeSnapshot(string connectionString, string outputFolder)
        {
            var db = new DatabaseServiceOracle(connectionString);
            var snapshot = new Snapshot.Models.Snapshot(db);
            snapshot.Take(outputFolder);
        }

        public List<Comparison> CompareSnapshots(string s1, string s2, string outputFile = "")
        {
            var cs = new CompareSnapshots();
            return cs.CompareAndSave(s1, s2, outputFile);
        }

        public List<Comparison> AnalyzeComparisons(string c1, string c2, string outputFile = "")
        {
            var ct = new CompareTables();
            return ct.CompareAndSave(c1, c2, outputFile);
        }
    }
}
