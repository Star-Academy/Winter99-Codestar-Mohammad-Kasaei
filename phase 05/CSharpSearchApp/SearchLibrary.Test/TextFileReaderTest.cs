using System.Collections.Generic;
using Xunit;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using System;

namespace SearchLibrary.Test
{
    [ExcludeFromCodeCoverage]
    public class TextFileReaderTest : IDisposable
    {

        private readonly string fileContent1 = "This is the content of first file";
        private readonly string fileContent2 = "This is the content of second file";
        private readonly string directoryName = "testFolderDirectory";
        private readonly string doc1Name = "doc1";
        private readonly string doc2Name = "doc2";

        public TextFileReaderTest()
        {
            Directory.CreateDirectory(directoryName);
            File.WriteAllText(@$"{directoryName}\{doc1Name}", fileContent1);
            File.WriteAllText(@$"{directoryName}\{doc2Name}", fileContent2);
        }

        [Fact]
        public void ReadAllFilesInFolderTest()
        {
            var expected = new Dictionary<Document, string>()
            {
                { new Document(doc1Name) , fileContent1},
                { new Document(doc2Name) , fileContent2},
            };
            var actual = TextFileReader.ReadAllFilesInDirectory(directoryName);
            Assert.Equal(expected, actual);
        }

        public void Dispose()
        {
            File.Delete(@$"{directoryName}\{doc1Name}");
            File.Delete(@$"{directoryName}\{doc2Name}");
            Directory.Delete(directoryName);
        }
    }
}
