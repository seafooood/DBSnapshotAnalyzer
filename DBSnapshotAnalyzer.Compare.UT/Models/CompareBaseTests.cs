using DBSnapshotAnalyzer.Compare.Models;

namespace DBSnapshotAnalyzer.Compare.UT.Models
{
    [TestFixture]
    public class CompareBaseTests
    {
        #region Save Tests

        [Test]
        public void saveTest()
        {
            // Arrange
            string tableName = "table1";
            string row1 = "111,222,333";
            string row2 = "aaa,bbb,ccc";
            Change change1 = Change.Inserted;
            Change change2 = Change.Deleted;
            var c1 = new Comparison() { TableName = tableName, Change = change1, Row = row1 };
            var c2 = new Comparison() { TableName = tableName, Change = change2, Row = row2 };
            var systemUnderTest = new CompareBase();

            // Act
            systemUnderTest.Save(_fileName, new List<Comparison> { c1, c2 });

            // Assert
            Assert.That(File.Exists(_fileName)); // Confirm the file has been created
            Assert.That(File.ReadAllText(_fileName), Is.EqualTo($"{tableName}{change1.ToFriendlyString()}{row1}" + Environment.NewLine + $"{tableName}{change2.ToFriendlyString()}{row2}")); // Confirm the file content
        }

        #endregion

        #region Setup Up and Tear Down

        string _fileName = @"c:\temp\unittest.txt";

        [SetUp]
        public void SetUp()
        {
            DeleteUnitTestFile();
        }

        [TearDown]
        public void TearDown()
        {
            DeleteUnitTestFile();
        }

        private void DeleteUnitTestFile()
        {
            try
            {
                if (File.Exists(_fileName))
                {
                    File.Delete(_fileName);
                }
            }
            catch (Exception)
            { }
        }

        #endregion
    }
}
