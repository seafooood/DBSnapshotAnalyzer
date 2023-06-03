using Commander.NET;
using Commander.NET.Exceptions;
using DBSnapshotAnalyzer.Compare;
using DBSnapshotAnalyzer.Console;
using NLog;

var _log = LogManager.GetCurrentClassLogger();
_log.Info("=== Starting DBSnapshotAnalyzer ===");

try
{
    var sa = new SnapshotAnalyzer(_log);
    var cp = new CommanderParser<Commands>().Add(args).Parse();

    if (cp.AnalyzeComparisonsCommand != null)
    {
        sa.AnalyzeComparisons(cp.AnalyzeComparisonsCommand.ComparisonFile1, cp.AnalyzeComparisonsCommand.ComparisonFile2, cp.AnalyzeComparisonsCommand.AnalyzeFile1);
    }
    else if (cp.CompareSnapshotsCommand != null) 
    {
        sa.CompareSnapshots(cp.CompareSnapshotsCommand.SnapshotFile1, cp.CompareSnapshotsCommand.SnapshotFile2, cp.CompareSnapshotsCommand.ComparisonFile);
    }
    else if (cp.TakeSnapshotCommand != null) 
    {
        sa.TakeSnapshot(cp.TakeSnapshotCommand.ConnectionString, cp.TakeSnapshotCommand.SnapshotFile);
    }
}
catch (ParameterMissingException ex)
{
    // A required parameter was missing
    _log.Fatal("Missing parameter: " + ex.ParameterName, ex);
}
catch (ParameterFormatException ex)
{
    /*
	*	A string-parsing method raised a FormatException
	*	ex.ParameterName
	*	ex.Value
	*	ex.RequiredType
	*/
    _log.Fatal(ex.Message, ex);
}
catch (Exception ex)
{
    _log.Fatal($"Fatal error {ex.Message} {ex?.InnerException?.Message}", ex);
}

_log.Info("=== Finished DBSnapshotAnalyzer ===");
