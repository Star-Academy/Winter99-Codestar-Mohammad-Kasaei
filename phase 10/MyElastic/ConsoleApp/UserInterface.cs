using System.Threading.Tasks;

namespace ConsoleApp
{
    public abstract class UserInterface
    {
        protected readonly ICallback Callback;

        protected UserInterface(ICallback callback)
        {
            Callback = callback;
        }

        public interface ICallback
        {
            bool LoadAppSettings(string path);
            bool InitMyElastic();
            string GetDefaultAppSettingsFile();
            Task<bool> CheckConnectionAsync();
            Task CreateIndex(string indexName);
            Task AddObjectToIndex(string indexName, string jsonString);
            bool OnTerminate();
        }

        public abstract Task Start();
    }
}