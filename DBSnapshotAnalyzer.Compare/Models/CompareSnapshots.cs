using DBSnapshotAnalyzer.Common.Services;

namespace DBSnapshotAnalyzer.Compare.Models
{
    public class CompareSnapshots
    {
        /// <summary>
        /// Compare two snapshots
        /// </summary>
        /// <param name="snapshot1"></param>
        /// <param name="snapshot2"></param>
        /// <returns></returns>
        public List<Comparison> Compare(string snapshot1, string snapshot2)
        {
            Console.WriteLine("Opening snapshot files");
            var zs = new ZipService();
            string snapshotFolder1 = zs.OpenSnapshot(snapshot1);
            string snapshotFolder2 = zs.OpenSnapshot(snapshot2);

            Console.WriteLine("Comparing snapshots");
            var result = new List<Comparison> ();
            var ct = new CompareTables();
            var fss = new FileSystemService();
            foreach (var filename in fss.GetFileNamesFromSnapshot(snapshotFolder1, snapshotFolder2)) 
            {
                string tableName = fss.GetTableNameFromFileName(filename);
                Console.WriteLine($"Comparing table {tableName}");

                try
                {                   
                    string filePath1 = Path.Combine(snapshotFolder1, filename);
                    string filePath2 = Path.Combine(snapshotFolder2, filename);

                    if (File.Exists(filePath1) && File.Exists(filePath2))
                    {
                        result.AddRange(ct.CompareFiles(tableName, filePath1, filePath2));
                    }
                    else
                    {
                        result.Add(new Comparison() { TableName = tableName, Change = Change.Deleted });
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Failed to compare {tableName} because {ex.Message}");
                }
            }

            // Remove snapshot folders
            fss.RemoveTemporaryFolder(snapshotFolder1);
            fss.RemoveTemporaryFolder(snapshotFolder2);

            return result;
        }        
    }
}