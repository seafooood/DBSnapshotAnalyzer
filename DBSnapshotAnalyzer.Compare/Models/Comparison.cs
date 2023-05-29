
namespace DBSnapshotAnalyzer.Compare.Models
{
    public class Comparison
    {
        public string TableName { get; set; } = string.Empty;
        public string Row { get; set; } = string.Empty;
        public Change Change { get; set; }

        public override string ToString()
        {
            return $"{TableName}{Change.ToFriendlyString()}{Row}";
        }
    }
}
