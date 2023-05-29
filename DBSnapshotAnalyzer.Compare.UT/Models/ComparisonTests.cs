using DBSnapshotAnalyzer.Compare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSnapshotAnalyzer.Compare.UT.Models
{
    [TestFixture]
    public class ComparisonTests
    {
        /// <summary>
        /// Test confirms that a Insert Comparison can be converted to a string
        /// </summary>
        [Test]
        public void ToString_InsertTest()
        {
            // Arrange
            string tableName = "table1";
            string row = "row1";
            Change change = Change.Inserted;
            var systemUnderTest = new Comparison()
            {
                TableName = tableName,
                Row = row,
                Change = change
            };

            // Act
            var result = systemUnderTest.ToString();

            // Assert
            Assert.That(result, Is.EqualTo($"{tableName}+{row}"));
        }

        /// <summary>
        /// Test confirms that a Delete Comparison can be converted to a string
        /// </summary>
        [Test]
        public void ToString_DeleteTest()
        {
            // Arrange
            string tableName = "table1";
            string row = "row1";
            Change change = Change.Deleted;
            var systemUnderTest = new Comparison()
            {
                TableName = tableName,
                Row = row,
                Change = change
            };

            // Act
            var result = systemUnderTest.ToString();

            // Assert
            Assert.That(result, Is.EqualTo($"{tableName}-{row}"));
        }
    }
}
