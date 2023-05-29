
namespace DBSnapshotAnalyzer.Compare.Models
{
    public enum Change
    {
        Inserted,
        Deleted
    }
    public static class ChangeExtensions
    {
        public static string ToFriendlyString(this Change c)
        {
            switch (c)
            {
                case Change.Inserted:
                    return "+";
                case Change.Deleted:
                    return "-";
            }

            throw new Exception($"Failed to convert {c} ToFriendlyString");
        }
    }
}
