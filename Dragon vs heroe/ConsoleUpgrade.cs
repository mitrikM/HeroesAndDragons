using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_vs_hero
{
    public static class ConsoleUpgrade
    {
        private static readonly ConsoleColor defaultColor;
        public static ConsoleColor DefaultColor
        { get { return defaultColor; } }
        static ConsoleUpgrade()
        {
            ConsoleUpgrade.defaultColor = ConsoleColor.Gray;
        }
        public static void WriteLine(string text, ConsoleColor color)
        {
            ConsoleColor ConsoleColorPrevious = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColorPrevious;
        }
    }
}
