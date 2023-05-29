
namespace DBSnapshotAnalyzer.Common.Services
{
    public class FileSystemService
    {
        /// <summary>
        /// Create output folder
        /// </summary>
        /// <param name="outputFolder">Snapshot output folder</param>
        /// <exception cref="Exception"></exception>
        public void CreateOutputFolder(string outputFolder)
        {
            try
            {
                Console.WriteLine($"Checking if output folder exists {outputFolder}");
                if (Directory.Exists(outputFolder) == false)
                {
                    Console.WriteLine($"Creating output folder {outputFolder}");
                    Directory.CreateDirectory(outputFolder);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create output folder {outputFolder} because {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Create a temporary folder
        /// </summary>
        /// <returns></returns>
        public string CreateTemporaryFolder()
        {
            try
            {
                string baseFolder = @"c:\snapshot"; //TODO: Remove hard coded folder and store value in settings
                string folderName = Guid.NewGuid().ToString();
                string path = Path.Combine(baseFolder, folderName);

                CreateOutputFolder(path);

                return path;
            }
            catch (Exception ex)
            { 
                throw new Exception($"Failed to create temporary folder because {ex.Message}", ex); 
            }
        }

        /// <summary>
        /// Create the folder structure for an output file
        /// </summary>
        /// <param name="fileName"></param>
        /// <exception cref="Exception"></exception>
        public void CreateFolderForOutputFile(string fileName)
        {
            try
            {
                string? path = Path.GetDirectoryName(fileName);
                Console.WriteLine($"Checking if directory {path} exists");
                if (path != null && Path.Exists(path) == false)
                {
                    
                    CreateOutputFolder(path);
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to create folder for file {fileName} because {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Delete a folder
        /// </summary>
        /// <param name="outputFolder"></param>
        public void RemoveTemporaryFolder(string outputFolder)
        {
            try
            {
                Console.WriteLine($"Removing folder {outputFolder}");
                Directory.Delete(outputFolder, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to remove temporary folder {outputFolder} because {ex.Message}");
            }            
        }

        /// <summary>
        /// Get a Distinct list of file names from snapshot 1 and 2
        /// </summary>
        /// <param name="snapshotFolder1">Snapshot folder</param>
        /// <param name="snapshotFolder2">Snapshot folder</param>
        /// <returns>List of file names</returns>
        public List<string> GetFileNamesFromSnapshot(string snapshotFolder1, string snapshotFolder2)
        {
            List<string> result = new List<string>();

            foreach (var dir in new List<string>() { snapshotFolder1, snapshotFolder2 })
            {
                foreach (var file in Directory.GetFiles(dir))
                {
                    result.Add(Path.GetFileName(file));
                }
            }

            return result.Distinct().ToList();
        }

        /// <summary>
        /// Get the table name from the file name
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns>table name</returns>
        public string GetTableNameFromFileName(string filename)
        {
            return Path.GetFileNameWithoutExtension(filename);
        }
    }
}
