using Commander.NET.Attributes;

namespace DBSnapshotAnalyzer.Console
{
    public class Commands
    {
        [Command("s", "snapshot", Description = "Take Snapshot")]
        public TakeSnapshotCommand? TakeSnapshotCommand;

        [Command("c", "compare", Description = "Compare snapshots")]
        public CompareSnapshotsCommand? CompareSnapshotsCommand;

        [Command("a", "analyze", Description = "Analyze comparison files")]
        public AnalyzeComparisonsCommand? AnalyzeComparisonsCommand;
    }

    public class TakeSnapshotCommand
    {
        //"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=bookshop;Password=mypassword;"
        [PositionalParameter(0, "ConnectionString")]
        public string ConnectionString = string.Empty;

        [PositionalParameter(1, "SnapshotFile")]
        public string SnapshotFile = string.Empty;
    }

    public class CompareSnapshotsCommand
    {
        [PositionalParameter(0, "SnapshotFile", Required = Required.Yes)]
        public string SnapshotFile1 = string.Empty;

        [PositionalParameter(1, "SnapshotFile", Required = Required.Yes)]
        public string SnapshotFile2 = string.Empty;

        [PositionalParameter(2, "ComparisonFile", Required = Required.Yes)]
        public string ComparisonFile = string.Empty;
    }

    public class AnalyzeComparisonsCommand
    {
        [PositionalParameter(0, "ComparisonFile")]
        public string ComparisonFile1 = string.Empty;

        [PositionalParameter(1, "ComparisonFile")]
        public string ComparisonFile2 = string.Empty;

        [PositionalParameter(2, "AnalyzeFile")]
        public string AnalyzeFile1 = string.Empty;
    }
}
