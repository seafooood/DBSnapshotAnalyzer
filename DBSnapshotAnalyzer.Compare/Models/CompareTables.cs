using DiffPlex.DiffBuilder.Model;
using DiffPlex.DiffBuilder;
using DiffPlex;
using DBSnapshotAnalyzer.Common.Services;

namespace DBSnapshotAnalyzer.Compare.Models
{
    public class CompareTables
    {
        /// <summary>
        /// COmpare the contents of two files
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        /// <returns></returns>
        public List<Comparison> CompareFiles(string tableName, string file1, string file2)
        {
            return Compare(tableName, File.ReadAllText(file1), File.ReadAllText(file2));    
        }

        /// <summary>
        /// Compare the contents of two tables
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="tableContent1">Contents of table 1</param>
        /// <param name="tableContent2">Contents of table 2</param>
        /// <returns>Comparison List</returns>
        public List<Comparison> Compare(string tableName, string tableContent1, string tableContent2)
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

        /// <summary>
        /// Save the results of a comparison
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="comparison"></param>
        /// <exception cref="Exception"></exception>
        public void Save(string filename, List<Comparison> comparison)
        {
            try
            {
                Console.WriteLine($"Saving file {filename}");
                new FileSystemService().CreateFolderForOutputFile(filename);                
                File.WriteAllText(filename, string.Join(Environment.NewLine, comparison));
                Console.WriteLine($"Saved file {filename}");
            }
            catch(Exception ex) 
            { 
                throw new Exception($"Failed to save results of comparison to file {filename} because {ex.Message}");
            }
        }
    }
}
