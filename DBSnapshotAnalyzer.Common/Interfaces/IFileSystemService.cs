namespace DBSnapshotAnalyzer.Common.Interfaces
{
    public interface IFileSystemService
    {
        void CreateFolderForOutputFile(string fileName);
        void CreateOutputFolder(string outputFolder);
        string CreateTemporaryFolder();
        List<string> GetFileNamesFromSnapshot(string snapshotFolder1, string snapshotFolder2);
        string GetTableNameFromFileName(string filename);
        void RemoveTemporaryFolder(string outputFolder);
    }
}