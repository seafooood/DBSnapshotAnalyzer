using DBSnapshotAnalyzer.Compare.Models;
using DBSnapshotAnalyzer.Snapshot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSnapshotAnalyzer.Compare
{
    public class SnapshotAnalyzer
    {
        public void TakeSnapshot(string connectionString, String, string outputFolder)
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

        public List<Comparison> AnalyzeComparisons(string v1, string v2, string outputFile = "")
        {
            var ct = new CompareTables();
            return ct.CompareAndSave(v1, v2, outputFile);
        }
    }
}
