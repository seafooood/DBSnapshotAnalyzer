using DBSnapshotAnalyzer.Common.Services;
using DBSnapshotAnalyzer.Compare.Models;
using Moq;
using NLog;
using System.Diagnostics.CodeAnalysis;

namespace DBSnapshotAnalyzer.Compare.UT.Models
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class CompareSnapshotsTests
    {
        #region Compare Tests
        /// <summary>
        /// Test confirms that Compare can find the change in snapshot 2
        /// </summary>
        [Test]
        public void CompareSnapshot_ChangeInS2Test()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var fileSystem = new FileSystemService(mockLogger.Object);
            var zip = new ZipService(mockLogger.Object, fileSystem);
            var s1 = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "s1.zip");
            var s2 = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "s2.zip");
            var systemUnderTest = new CompareSnapshots(mockLogger.Object, fileSystem, zip);

            // Act
            var result = systemUnderTest.Compare(s1, s2);

            // Assert
            Assert.That(result, Is.TypeOf<List<Comparison>>()); // Confirm the result type
            Assert.That(result.Count, Is.EqualTo(2)); // Confirm the number of results 
            Assert.That(result[0].Row, Is.EqualTo("2,ALICES ADVENTURES IN WONDERLAND,LEWIS CARROLL,4,")); // Confirm line from s1 was removed
            Assert.That(result[0].Change, Is.EqualTo(Change.Deleted)); // Confirm line from s1 was removed
            Assert.That(result[0].TableName, Is.EqualTo("BOOKS")); // Confirm the table name
            Assert.That(result[1].Row, Is.EqualTo("2,ALICES ADVENTURES IN WONDERLAND,LEWIS CARROLL,6,")); // Confirm line from s2 was added
            Assert.That(result[1].Change, Is.EqualTo(Change.Inserted)); // Confirm line from s2 was added
            Assert.That(result[1].TableName, Is.EqualTo("BOOKS")); // Confirm the table name
        }
        #endregion
    }
}