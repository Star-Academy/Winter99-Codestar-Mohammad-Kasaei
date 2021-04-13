namespace ConsoleApp
{
    public class Document
    {
        public string Id { get; set; }
        public string Content { get; set; }

        public Document(string id, string content)
        {
            Id = id;
            Content = content;
        }
    }
}