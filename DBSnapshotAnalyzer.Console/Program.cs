using DBSnapshotAnalyzer.Compare.Models;
using DBSnapshotAnalyzer.Snapshot.Models;
using DBSnapshotAnalyzer.Snapshot.Services;

//TODO: Convert console logs to nlog
Console.WriteLine("=== Starting DBSnapshotAnalyzer ===");

try
{
    if (args.Length > 0)
    {
        //TODO: Improve arg parsing
        switch (args[0])
        {
            case "a":
            case "analyze":
                // a "c:\s1\d1.txt" "c:\s1\d2.txt" "c:\s1\a1.txt"
                AnalyzeComparisions(args[1], args[2], args[3]);
                break;

            case "c":
            case "compare":
                // c "c:\s1\s1.zip" "c:\s1\s2.zip" "c:\s1\d.txt"
                CompareSnapshots(args[1], args[2], args[3]);
                break;

            case "s":
            case "snapshot":
                // s "c:\s1\s1.zip"
                if (args.Length < 1)
                {
                    throw new Exception("Missing argument output file");
                }
                TakeSnapshot(args[1]);
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
        
static void TakeSnapshot(string outputFolder)
{
    var connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=bookshop;Password=mypassword;"; //TODO: Get connection string from settings
    var db = new DatabaseServiceOracle(connectionString);
    var snapshot = new Snapshot(db);
    snapshot.Take(outputFolder);
}

static void CompareSnapshots(string s1, string s2, string outputFile)
{
    var cs = new CompareSnapshots();
    cs.CompareAndSave(s1, s2, outputFile);
}

void AnalyzeComparisions(string v1, string v2, string outputFile)
{
    var ct = new CompareTables();
    ct.CompareAndSave(v1, v2, outputFile);
}
