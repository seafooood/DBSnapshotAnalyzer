using DBSnapshotAnalyzer.Compare.Models;

namespace DBSnapshotAnalyzer.Compare.UT.Models
{
    [TestFixture]
    public class CompareTablesTests
    {
        #region Compare Tests

        /// <summary>
        /// Test confirms that Compare() will return an empty list when the two tables are the same.
        /// </summary>
        [Test]
        public void Compare_SameText()
        {
            // Arrange
            string tableName = "table1";
            string s1 = "aaa,bbb,ccc\n111,222,333";
            string s2 = "aaa,bbb,ccc\n111,222,333";
            var systemUnderTest = new CompareTables();

            // Act
            var result = systemUnderTest.Compare(tableName, s1, s2);

            // Assert
            Assert.That(result, Is.TypeOf<List<Comparison>>()); // Confirm the object type
            Assert.That(result.Count, Is.EqualTo(0)); // Confirm there are not differences
        }

        /// <summary>
        /// Test confirms that Compare() will return one item in the result list when the table 1 contains an extra value
        /// </summary>
        [Test]
        public void Compare_Table1NewValue()
        {
            // Arrange
            string tableName = "table1";
            string s1 = "aaa,bbb,ccc,ddd\n111,222,333";
            string s2 = "aaa,bbb,ccc\n111,222,333";
            var systemUnderTest = new CompareTables();

            // Act
            var result = systemUnderTest.Compare(tableName, s1, s2);

            // Assert
            Assert.That(result, Is.TypeOf<List<Comparison>>()); // Confirm the object type
            Assert.That(result.Count, Is.EqualTo(2)); // Confirm there are two differences
            Assert.That(result[0].Row, Is.EqualTo("aaa,bbb,ccc,ddd")); // Confirm line from s1 was removed
            Assert.That(result[0].Change, Is.EqualTo(Change.Deleted)); // Confirm line from s1 was removed
            Assert.That(result[0].TableName, Is.EqualTo(tableName)); // Confirm the table name
            Assert.That(result[1].Row, Is.EqualTo("aaa,bbb,ccc")); // Confirm line from s2 was added
            Assert.That(result[1].Change, Is.EqualTo(Change.Inserted)); // Confirm line from s2 was added
            Assert.That(result[1].TableName, Is.EqualTo(tableName)); // Confirm the table name
        }

        /// <summary>
        /// Test confirms that Compare() will return one item in the result list when the table 1 contains an extra value
        /// </summary>
        [Test]
        public void Compare_Table2NewValue()
        {
            // Arrange
            string tableName = "table1";
            string s1 = "aaa,bbb,ccc\n111,222,333";
            string s2 = "aaa,bbb,ccc,ddd\n111,222,333";
            var systemUnderTest = new CompareTables();

            // Act
            var result = systemUnderTest.Compare(tableName, s1, s2);

            // Assert
            Assert.That(result, Is.TypeOf<List<Comparison>>()); // Confirm the object type
            Assert.That(result.Count, Is.EqualTo(2)); // Confirm there are two differences
            Assert.That(result[0].Row, Is.EqualTo("aaa,bbb,ccc")); // Confirm line from s1 was removed
            Assert.That(result[0].Change, Is.EqualTo(Change.Deleted)); // Confirm line from s1 was removed
            Assert.That(result[0].TableName, Is.EqualTo(tableName)); // Confirm the table name
            Assert.That(result[1].Row, Is.EqualTo("aaa,bbb,ccc,ddd")); // Confirm line from s2 was added
            Assert.That(result[1].Change, Is.EqualTo(Change.Inserted)); // Confirm line from s2 was added
            Assert.That(result[0].TableName, Is.EqualTo(tableName)); // Confirm the table name
        }

        #endregion

        #region ConvertChangeType Tests

        /// <summary>
        /// Test confirms that ConvertChangeType can convert Inserted
        /// </summary>
        [Test]
        public void ConvertChangeType_InsertTest()
        {
            // Arrange 
            var systemUnderTest = new CompareTables();

            // Act
            var result = systemUnderTest.ConvertChangeType(DiffPlex.DiffBuilder.Model.ChangeType.Inserted);

            // Assert
            Assert.That(result, Is.EqualTo(Change.Inserted));
        }

        // <summary>
        /// Test confirms that ConvertChangeType can convert Deleted
        /// </summary>
        [Test]
        public void ConvertChangeType_DeletedTest()
        {
            // Arrange 
            var systemUnderTest = new CompareTables();

            // Act
            var result = systemUnderTest.ConvertChangeType(DiffPlex.DiffBuilder.Model.ChangeType.Deleted);

            // Assert
            Assert.That(result, Is.EqualTo(Change.Deleted));
        }

        #endregion

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
            var systemUnderTest = new CompareTables();

            // Act
            systemUnderTest.Save(_fileName, new List<Comparison> { c1, c2});

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
