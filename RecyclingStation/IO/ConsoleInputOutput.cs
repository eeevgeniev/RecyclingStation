namespace RecyclingStation.IO
{
    using System;
    using Contracts;

    public class ConsoleInputOutput : IInputOutput
    {
        public string ReadMessage()
        {
            return Console.ReadLine();
        }

        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
