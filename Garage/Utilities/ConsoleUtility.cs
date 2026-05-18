using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Utilities
{
    internal static class ConsoleUtility
    {
        public static void Message(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
