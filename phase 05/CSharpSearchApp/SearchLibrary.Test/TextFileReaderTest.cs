using System.Collections.Generic;
using Xunit;
using System.IO;
using System.Diagnostics.CodeAnalysis;

namespace SearchLibrary.Test
{
    [ExcludeFromCodeCoverage]
    public class TextFileReaderTest
    {

        private readonly string fileContent1 = "This is the content of first file";
        private readonly string fileContent2 = "This is the content of second file";
        private readonly string directoryName = "testFolderDirectory";
        private readonly string doc1Name = "doc1";
        private readonly string doc2Name = "doc2";


        private void Setup()
        {
            Directory.CreateDirectory(directoryName);
            File.WriteAllText(@$"{directoryName}\{doc1Name}", fileContent1);
            File.WriteAllText(@$"{directoryName}\{doc2Name}", fileContent2);
        }

        private void TearDown()
        {
            File.Delete(@$"{directoryName}\{doc1Name}");
            File.Delete(@$"{directoryName}\{doc2Name}");
            Directory.Delete(directoryName);
        }


        [Fact]
        public void ReadAllFilesInFolderTest()
        {
            Setup();
            var expected = new Dictionary<Document, string>()
            {
                { new Document(doc1Name) , fileContent1},
                { new Document(doc2Name) , fileContent2},
            };
            var actual = TextFileReader.ReadAllFilesInDirectory(directoryName);
            Assert.Equal(expected, actual);
            TearDown();
        }
    }
}
