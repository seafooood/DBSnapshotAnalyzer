using NUnit.Framework;

namespace Examples.Base
{
    public class BaseTests
    {
        #region Set Up and Tear Down
        protected readonly string _snapshotBefore = "snapshotBefore.sdb";
        protected readonly string _snapshotAfter = "snapshotAfter.sdb";
        protected readonly string _comparisonFile = "_comparisonFile.txt";
        protected readonly string _connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=bookshop;Password=mypassword;";

        [SetUp]
        public void SetUp()
        {
            DeleteFiles();
        }

        [TearDown]
        public void TearDown()
        {
            DeleteFiles();
        }

        protected void DeleteFiles()
        {
            foreach (var file in new List<string>() { _snapshotBefore, _snapshotAfter, _comparisonFile })
            {
                try
                {
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
                catch (Exception) { }
            }
        }
        #endregion
    }
}
