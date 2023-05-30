using DBSnapshotAnalyzer.Common.Interfaces;
using Ionic.Zip;
using NLog;

namespace DBSnapshotAnalyzer.Common.Services
{
    public class ZipService : IZipService
    {
        #region Private Members
        private readonly ILogger _log;
        IFileSystemService _fileSystemService;
        #endregion

        #region Constructor
        public ZipService(ILogger log, IFileSystemService fileSystemService)
        {
            _log = log;
            _fileSystemService = fileSystemService;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Open a snapshot file
        /// </summary>
        /// <param name="snapshotFile"></param>
        /// <returns>Folder containing the snapshot files</returns>
        public string OpenSnapshot(string snapshotFile)
        {
            string folder = _fileSystemService.CreateTemporaryFolder();
            UnZipFile(snapshotFile, folder);
            return folder;
        }

        /// <summary>
        /// Save a snapshot file
        /// </summary>
        /// <param name="snapshotFile"></param>
        /// <param name="directoryToZip"></param>
        public void SaveSnapshot(string snapshotFile, string directoryToZip)
        {
            _log.Trace("Saving snapshot");
            ZipFolder(snapshotFile, directoryToZip);
            _fileSystemService.RemoveTemporaryFolder(directoryToZip);
            _log.Trace($"Saved snapshot to {snapshotFile}");
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Create a zip file
        /// </summary>
        /// <param name="zipFileToCreate"></param>
        /// <param name="directoryToZip"></param>
        /// <exception cref="Exception"></exception>
        private void ZipFolder(string zipFileToCreate, string directoryToZip)
        {
            try
            {
                _fileSystemService.CreateFolderForOutputFile(zipFileToCreate);
                using (ZipFile zip = new ZipFile())
                {
                    foreach (var filename in Directory.GetFiles(directoryToZip))
                    {
                        zip.AddFile(filename, "");
                    }
                    zip.Save(zipFileToCreate);

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create zip {zipFileToCreate} because {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Unzip a file
        /// </summary>
        /// <param name="zipFile">File to unzip</param>
        /// <param name="directoryToUnzip">Output folder</param>
        /// <exception cref="Exception"></exception>
        private void UnZipFile(string zipFile, string directoryToUnzip)
        {
            try
            {
                _fileSystemService.CreateOutputFolder(directoryToUnzip);

                using (ZipFile zip = ZipFile.Read(zipFile))
                {
                    zip.ExtractAll(directoryToUnzip);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to unzip file {zipFile} because {ex.Message}", ex);
            }
        }
        #endregion
    }
}