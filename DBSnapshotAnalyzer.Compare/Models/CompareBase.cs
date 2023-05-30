using DBSnapshotAnalyzer.Common.Services;
using NLog;

namespace DBSnapshotAnalyzer.Compare.Models
{
    public class CompareBase
    {
        #region Private Members
        protected readonly ILogger _log;
        #endregion

        #region Constructor
        public CompareBase(ILogger log)
        {
            _log = log;
        }
        #endregion

        #region Public Methods
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
                _log.Trace($"Saving file {filename}");
                new FileSystemService(_log).CreateFolderForOutputFile(filename);
                File.WriteAllText(filename, string.Join(Environment.NewLine, comparison));
                _log.Trace($"Saved file {filename}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save results of comparison to file {filename} because {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Compare two files and save the results to a file
        /// </summary>
        /// <param name="snapshot1"></param>
        /// <param name="snapshot2"></param>
        /// <param name="outputFile"></param>
        public List<Comparison> CompareAndSave(string snapshot1, string snapshot2, string outputFile)
        {
            var result = Compare(snapshot1, snapshot2);

            if (String.IsNullOrEmpty(outputFile) == false)
            {
                Save(outputFile, result);
            }

            return result;
        }

        /// <summary>
        /// Placeholder for the compare functions
        /// </summary>
        /// <param name="snapshot1"></param>
        /// <param name="snapshot2"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual List<Comparison> Compare(string snapshot1, string snapshot2)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
