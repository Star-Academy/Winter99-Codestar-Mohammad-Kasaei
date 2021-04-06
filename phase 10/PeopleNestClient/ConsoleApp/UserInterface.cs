namespace ConsoleApp
{
    public abstract class UserInterface
    {
        protected readonly IUserCallbacks _callbacks;

        protected UserInterface(IUserCallbacks callbacks)
        {
            _callbacks = callbacks;
        }

        public abstract void Start();
    }
}