﻿using System;
using Ionic.Zip;

namespace DBSnapshotAnalyzer.Common.Services
{
    public class ZipService
    {
        /// <summary>
        /// Open a snapshot file
        /// </summary>
        /// <param name="snapshotFile"></param>
        /// <returns>Folder containing the snapshot files</returns>
        public string OpenSnapshot(string snapshotFile)
        {
            string folder = new FileSystemService().CreateTemporaryFolder();
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
            Console.WriteLine("Saving snapshot");
            ZipFolder(snapshotFile, directoryToZip);
            new FileSystemService().RemoveTemporaryFolder(directoryToZip);
            Console.WriteLine($"Saved snapshot to {snapshotFile}");
        }

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
                new FileSystemService().CreateFolderForOutputFile(zipFileToCreate);
                using (ZipFile zip = new ZipFile())
                {
                    foreach (var filename in Directory.GetFiles(directoryToZip))
                    {
                        zip.AddFile(filename,"");
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
                new FileSystemService().CreateOutputFolder(directoryToUnzip);

                using (ZipFile zip = ZipFile.Read(zipFile))
                {
                    zip.ExtractAll(directoryToUnzip);
                }
            }
            catch(Exception ex) 
            {
                throw new Exception($"Failed to unzip file {zipFile} because {ex.Message}", ex);
            }
        }
    }
}