using System.Threading.Tasks;

namespace ConsoleApp
{
    static class Program
    {
        private static readonly ITextInputOutput TextInputOutputObject = new TextInputOutput();
        private static readonly UserInterface.ICallback CallBackHandlerObject = new CallBackHandler();

        private static readonly UserInterface UserInterface =
            new TextUserInterface(CallBackHandlerObject, TextInputOutputObject);

        private static async Task Main()
        {
            await UserInterface.Start();
        }
    }
}