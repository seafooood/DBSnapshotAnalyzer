using DBSnapshotAnalyzer.Common.Services;
using Moq;
using NLog;
using System.Diagnostics.CodeAnalysis;

namespace DBSnapshotAnalyzer.Common.UT.Services
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class FileSystemServiceTests
    {
        #region GetTableNameFromFileName Tests
        /// <summary>
        /// Test confirms that GetTableNameFromFileName can convert a file name to table name
        /// </summary>
        /// <param name="name"></param>
        [TestCase("bob")]
        [TestCase("name")]
        public void GetTableNameFromFileNameTest(string name)
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var systemUnderTest = new FileSystemService(mockLogger.Object);

            // Act
            var result = systemUnderTest.GetTableNameFromFileName(name+".csv");

            // Assert
            Assert.That(result, Is.EqualTo(name)); // Confirm the table name 
        }
        #endregion
    }
}
