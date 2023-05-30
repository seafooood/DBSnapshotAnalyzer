using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using NLog;

namespace DBSnapshotAnalyzer.Compare.Models
{
    public class CompareTables : CompareBase
    {
        #region Constructor
        public CompareTables(ILogger log) : base(log)
        {
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Compare the contents of two files
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        /// <returns></returns>
        public override List<Comparison> Compare(string file1, string file2)
        {
            return CompareContent("", File.ReadAllText(file1), File.ReadAllText(file2));
        }

        /// <summary>
        /// Compare the contents of two files containing table data
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        /// <returns></returns>
        public List<Comparison> CompareTableFiles(string tableName, string file1, string file2)
        {
            return CompareContent(tableName, File.ReadAllText(file1), File.ReadAllText(file2));
        }

        /// <summary>
        /// Compare the contents of two tables
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="tableContent1">Contents of table 1</param>
        /// <param name="tableContent2">Contents of table 2</param>
        /// <returns>Comparison List</returns>
        public List<Comparison> CompareContent(string tableName, string tableContent1, string tableContent2)
        {
            var diffBuilder = new InlineDiffBuilder(new Differ());
            var diff = diffBuilder.BuildDiffModel(tableContent1, tableContent2);

            var result = new List<Comparison>();
            foreach (var line in diff.Lines)
            {
                if (line.Type == ChangeType.Inserted || line.Type == ChangeType.Deleted)
                {
                    result.Add(new Comparison()
                    {
                        TableName = tableName,
                        Row = line.Text,
                        Change = ConvertChangeType(line.Type)
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// Convert ChangeType to Change
        /// </summary>
        /// <param name="type">ChangeType</param>
        /// <returns>Change</returns>
        /// <exception cref="Exception"></exception>
        public Change ConvertChangeType(ChangeType type)
        {
            if (type == ChangeType.Inserted)
            {
                return Change.Inserted;
            }

            if (type == ChangeType.Deleted)
            {
                return Change.Deleted;
            }

            throw new Exception($"Unable to convert ChangeType {type}");
        }
        #endregion
    }
}
