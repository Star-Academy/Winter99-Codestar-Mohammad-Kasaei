using System;

namespace ConsoleApp
{
    public class TextInputOutput : ITextInputOutput
    {
        public void Write(string text)
        {
            Console.Write(text);
        }

        public string Read()
        {
            return Console.ReadLine();
        }
    }
}