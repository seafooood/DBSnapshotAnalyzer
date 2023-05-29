using DBSnapshotAnalyzer.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSnapshotAnalyzer.Compare.Models
{
    public class CompareBase
    {
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
            catch (Exception ex)
            {
                throw new Exception($"Failed to save results of comparison to file {filename} because {ex.Message}");
            }
        }

        /// <summary>
        /// Compare two files and save the results to a file
        /// </summary>
        /// <param name="snapshot1"></param>
        /// <param name="snapshot2"></param>
        /// <param name="outputFile"></param>
        public void CompareAndSave(string snapshot1, string snapshot2, string outputFile)
        {
            var result = Compare(snapshot1, snapshot2);
            Save(outputFile, result);
        }

        public virtual List<Comparison> Compare(string snapshot1, string snapshot2) 
        { 
            throw new NotImplementedException();
        }
        
    }
}
