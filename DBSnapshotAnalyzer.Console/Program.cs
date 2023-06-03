using DBSnapshotAnalyzer.Compare;
using NLog;

var _log = LogManager.GetCurrentClassLogger();
_log.Info("=== Starting DBSnapshotAnalyzer ===");

try
{
    var sa = new SnapshotAnalyzer(_log);
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
                // s "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=bookshop;Password=mypassword;" "c:\s1\s1.zip"                
                sa.TakeSnapshot(args[1], args[2]);
                break;
        }
    }
}
catch (Exception ex)
{
    _log.Fatal($"Fatal error {ex.Message}", ex);
}

_log.Info("=== Finished DBSnapshotAnalyzer ===");
