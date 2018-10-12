using System;

namespace TechnicalShowcase.Services.Wrappers
{
    public interface IConsoleWrapper
    {
        void WriteLine(object value);
        string ReadLine();
    }

    public class ConsoleWrapper : IConsoleWrapper
    {
        public void WriteLine(object value)
        {
            Console.WriteLine(value.ToString());
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
