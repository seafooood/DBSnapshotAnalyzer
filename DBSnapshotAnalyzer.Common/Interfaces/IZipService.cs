namespace DBSnapshotAnalyzer.Common.Interfaces
{
    public interface IZipService
    {
        string OpenSnapshot(string snapshotFile);
        void SaveSnapshot(string snapshotFile, string directoryToZip);
    }
}