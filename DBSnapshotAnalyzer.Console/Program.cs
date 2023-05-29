using DBSnapshotAnalyzer.Compare;
using DBSnapshotAnalyzer.Compare.Models;
using DBSnapshotAnalyzer.Snapshot.Models;
using DBSnapshotAnalyzer.Snapshot.Services;

//TODO: Convert console logs to nlog
Console.WriteLine("=== Starting DBSnapshotAnalyzer ===");

try
{
    var sa = new SnapshotAnalyzer();
    if (args.Length > 0)
    {
        //TODO: Improve arg parsing
        switch (args[0])
        {
            case "a":
            case "analyze":
                // a "c:\s1\d1.txt" "c:\s1\d2.txt" "c:\s1\a1.txt"
                sa.AnalyzeComparisons(args[1], args[2], args[3]);
                break;

            case "c":
            case "compare":
                // c "c:\s1\s1.zip" "c:\s1\s2.zip" "c:\s1\d.txt"
                sa.CompareSnapshots(args[1], args[2], args[3]);
                break;

            case "s":
            case "snapshot":
                // s "c:\s1\s1.zip"
                if (args.Length < 1)
                {
                    throw new Exception("Missing argument output file");
                }
                sa.TakeSnapshot(args[1]);
                break;
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Fatal error {ex.Message}");
    throw;
}

Console.WriteLine("=== Finished DBSnapshotAnalyzer ===");
