using DBSnapshotAnalyzer.Compare;
using Examples.Base;
using NUnit.Framework;

namespace Examples._01_ExampleSnapshot
{
    public class ExampleSnapshot : BaseTests
    {
        public void ExampleSnapshotTest()
        {
            // Create instance of SnapshotAnalyzer
            var analyzer = new SnapshotAnalyzer();

            // Take a snapshot of the database 
            analyzer.TakeSnapshot(_connectionString, _snapshotBefore);

            Console.WriteLine($"Snapshot saved to {_snapshotBefore}");
        }
    }
}
