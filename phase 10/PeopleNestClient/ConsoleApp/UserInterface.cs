namespace ConsoleApp
{
    public abstract class UserInterface
    {
        protected readonly IUserCallbacks Callbacks;

        protected UserInterface(IUserCallbacks callbacks)
        {
            Callbacks = callbacks;
        }

        public abstract void Start();
    }
}