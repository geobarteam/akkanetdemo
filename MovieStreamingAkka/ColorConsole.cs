using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStreamingAkka
{
    public static class ColorConsole
    {
        public static void WriteLine(ConsoleColor color, string line)
        {
            var beforeColor = Console.ForegroundColor;

            Console.ForegroundColor = color;

            Console.WriteLine(line);

            Console.ForegroundColor = beforeColor;

        }

        public static void WriteLineYellow(string line)
            => WriteLine(ConsoleColor.Yellow, line);

        public static void WriteLineGreen(string line)
            => WriteLine(ConsoleColor.Green, line);

        public static void WriteLineBlue(string line)
            => WriteLine(ConsoleColor.Blue, line);

        public static void WriteLineRed(string line)
            => WriteLine(ConsoleColor.Red, line);
    }
}
