namespace SearchWebApplication
{
    public interface IApiQueryParser
    {
        void SplitWordsToGroups(string queryString,
            out string[] notWords,
            out string[] orWords,
            out string[] andWords
        );
    }
}