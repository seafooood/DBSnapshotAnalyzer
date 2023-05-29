using DBSnapshotAnalyzer.Snapshot.Models;
using DBSnapshotAnalyzer.Snapshot.Services;

Console.WriteLine("=== Starting DBSnapshotAnalyzer ===");

try
{
    if (args.Length > 0)
    {
        switch (args[0])
        {
            case "s":
            case "snapshot":
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