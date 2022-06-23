using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Projekt_architektura
{
    public class Utils: Window
    {
        public static bool HexValidator(string input)
        {
            for (var i = 0; i < input.Length; i++)
                if (!(input[i] >= '0' && input[i] <= '9' || input[i] >= 'A' && input[i] <= 'F'))
                    return false;
            return true;
        }

        public static string RandomHexGenerator8Bit()
        {
            const string chars = "0123456789ABCDEF";
            var rand = new Random();
            return new string(Enumerable.Repeat(chars, 2).Select(s => s[rand.Next(s.Length)]).ToArray());
        }
    }
}
