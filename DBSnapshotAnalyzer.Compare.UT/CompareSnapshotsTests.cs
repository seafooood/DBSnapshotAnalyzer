namespace DBSnapshotAnalyzer.Compare.UT
{
    [TestFixture]
    public class Tests
    {
        #region CompareTables Tests

        /// <summary>
        /// Test confirms that CompareTables() will return an empty list when the two tables are the same.
        /// </summary>
        [Test]
        public void CompareTables_SameText()
        {
            // Arrange
            string s1 = "aaa,bbb,ccc\n111,222,333";
            string s2 = "aaa,bbb,ccc\n111,222,333";
            var systemUnderTest = new CompareSnapshots();

            // Act
            var result = systemUnderTest.CompareTables(s1, s2);

            // Assert
            Assert.That(result.Count, Is.EqualTo(0)); // Confirm there are not differences
        }

        /// <summary>
        /// Test confirms that CompareTables() will return one item in the result list when the table 1 contains an extra value
        /// </summary>
        [Test]
        public void CompareTables_Table1NewValue()
        {
            // Arrange
            string s1 = "aaa,bbb,ccc,ddd\n111,222,333";
            string s2 = "aaa,bbb,ccc\n111,222,333";
            var systemUnderTest = new CompareSnapshots();

            // Act
            var result = systemUnderTest.CompareTables(s1, s2);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2)); // Confirm there are two differences
            Assert.That(result[0], Is.EqualTo("-aaa,bbb,ccc,ddd")); // Confirm line from s1 was removed
            Assert.That(result[1], Is.EqualTo("+aaa,bbb,ccc")); // Confirm line from s2 was added
        }

        /// <summary>
        /// Test confirms that CompareTables() will return one item in the result list when the table 1 contains an extra value
        /// </summary>
        [Test]
        public void CompareTables_Table2NewValue()
        {
            // Arrange
            string s1 = "aaa,bbb,ccc\n111,222,333";
            string s2 = "aaa,bbb,ccc,ddd\n111,222,333";
            var systemUnderTest = new CompareSnapshots();

            // Act
            var result = systemUnderTest.CompareTables(s1, s2);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2)); // Confirm there are two differences
            Assert.That(result[0], Is.EqualTo("-aaa,bbb,ccc")); // Confirm line from s1 was removed
            Assert.That(result[1], Is.EqualTo("+aaa,bbb,ccc,ddd")); // Confirm line from s2 was added
        }

        #endregion
    }
}