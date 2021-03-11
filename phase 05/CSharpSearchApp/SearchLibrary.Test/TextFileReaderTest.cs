using System.Collections.Generic;
using Xunit;
using System.IO;
using System.Diagnostics.CodeAnalysis;

namespace SearchLibrary.Test
{
    [ExcludeFromCodeCoverage]
    public class TextFileReaderTest
    {
        [Fact]
        public void ReadAllFilesInFolderTest()
        {
            // setup 
            var fileContent1 = "This is the content of first file";
            var fileContent2 = "This is the content of second file";
            var directoryName = "testFolderDirectory";
            var doc1Name = "doc1";
            var doc2Name = "doc2";
            var expected = new Dictionary<Document, string>()
            {
                { new Document(doc1Name) , fileContent1},
                { new Document(doc2Name) , fileContent2},
            };
            Directory.CreateDirectory(directoryName);
            File.WriteAllText(@$"{directoryName}\{doc1Name}", fileContent1);
            File.WriteAllText(@$"{directoryName}\{doc2Name}", fileContent2);

            // test
            var actual = TextFileReader.ReadAllFilesInDirectory(directoryName);
            Assert.Equal(expected, actual);

            // tear down
            File.Delete(@$"{directoryName}\{doc1Name}");
            File.Delete(@$"{directoryName}\{doc2Name}");
            Directory.Delete(directoryName);
        }
    }
}
