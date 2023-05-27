using DiffPlex.DiffBuilder;
using DiffPlex;
using DiffPlex.DiffBuilder.Model;

namespace DBSnapshotAnalyzer.Compare
{
    public class CompareSnapshots
    {
        public List<string> CompareTables(string tableContent1, string tableContent2)
        {
            var diffBuilder = new InlineDiffBuilder(new Differ());
            var diff = diffBuilder.BuildDiffModel(tableContent1, tableContent2);

            var result = new List<string>();
            foreach (var line in diff.Lines)
            {
                switch (line.Type)
                {
                    case ChangeType.Inserted:
                        result.Add($"+{line.Text}");
                        break;
                    case ChangeType.Deleted:
                        result.Add($"-{line.Text}");
                        break;
                }
            }

            return result;
        }
    }
}