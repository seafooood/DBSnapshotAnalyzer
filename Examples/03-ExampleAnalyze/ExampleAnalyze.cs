using DBSnapshotAnalyzer.Compare;
using Examples.Base;
using NUnit.Framework;

namespace Examples._03_ExampleAnalyze
{
    [TestFixture]
    public class ExampleAnalyze : BaseTests
    {
        [Test]
        public void ExampleAnalyzeTest()
        {
            // Arrange            
            var analyzer = new SnapshotAnalyzer();
            analyzer.TakeSnapshot(_connectionString, _snapshotBefore); // Take the before change snapshot
            var systemUnderTest = new Bookshop.Bookshop();

            // Act
            systemUnderTest.InsertRow(_connectionString);

            // Assert
            analyzer.TakeSnapshot(_connectionString, _snapshotAfter); // Take the after change snapshot
            analyzer.CompareSnapshots(_snapshotBefore, _snapshotAfter, _comparisonFile); // Compare the before and after snapshots then save the comparison to file _comparisonFile
            var result = analyzer.AnalyzeComparisons(_comparisonFile, Path.Combine(TestContext.CurrentContext.TestDirectory, "03-ExampleAnalyze", "c.txt")); // Analyze the comparison files
            Assert.That(result.Count, Is.EqualTo(0)); // Confirm there are no differences between the comparison files
        }
    }
}
