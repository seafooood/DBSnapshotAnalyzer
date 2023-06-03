using DBSnapshotAnalyzer.Compare.Models;
using DBSnapshotAnalyzer.Compare;
using NUnit.Framework;
using Examples.Base;

namespace Examples._02_ExampleCompare
{
    internal class ExampleCompare : BaseTests
    {
        [Test]
        public void ExampleCompareTest()
        {
            // Arrange            
            var analyzer = new SnapshotAnalyzer();
            analyzer.TakeSnapshot(_connectionString, _snapshotBefore); // Take the before change snapshot
            var systemUnderTest = new Bookshop.Bookshop();

            // Act
            systemUnderTest.InsertRow(_connectionString);

            // Assert
            analyzer.TakeSnapshot(_connectionString, _snapshotAfter); // Take the after change snapshot
            var comparision = analyzer.CompareSnapshots(_snapshotBefore, _snapshotAfter); // Compare the before and after snapshots
            Assert.That(comparision.Count, Is.EqualTo(1)); // Confirm there is one difference
            Assert.That(comparision[0].Change, Is.EqualTo(Change.Inserted)); // Confirm the difference is an insert
            Assert.That(comparision[0].TableName, Is.EqualTo("BOOKS")); // Confirm the difference is in the table Books
            Assert.That(comparision[0].Row, Is.EqualTo("8,myTitle,myAuthor,,")); // Confirm the difference is an insert
        }
    }
}
