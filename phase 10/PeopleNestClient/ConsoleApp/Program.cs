namespace ConsoleApp
{
    class Program
    {
        private static readonly IUserCallbacks Callbacks = new UserCallbacks();
        private static readonly UserInterface UserInterface = new CommandLineUserInterface(Callbacks);

        static void Main()
        {
            UserInterface.Start();
        }
    }
}