namespace ConsoleApp
{
    public interface IUserCallbacks
    {
        string DefaultAppSettingsPath();
        bool Init(string appSettingsPath);
        bool IndexCreation(string indexName, bool forceCreate);
        bool BulkInsertFromFile(string filePath);
        bool Terminate();
    }
}