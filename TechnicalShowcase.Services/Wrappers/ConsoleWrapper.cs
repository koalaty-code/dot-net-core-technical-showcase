using System;

namespace TechnicalShowcase.Services.Wrappers
{
    public interface IConsoleWrapper
    {
        void Write(object value);
        void WriteLine(object value);
        string ReadLine();
    }

    public class ConsoleWrapper : IConsoleWrapper
    {
        public void Write(object value)
        {
            Console.Write(value.ToString());
        }

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
