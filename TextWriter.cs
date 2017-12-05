using System;
using System.Media;
using System.Threading;
using System.Threading.Tasks;

namespace RPG_Final
{
    public static class TextWriter
    {
        /*
            Codes:
            %0 - Gray (default)
            %1 - DarkGray
            %2 - Red
            %3 - Green
            %4 - Cyan
            %5 - Yellow

            ` - Delay as if there was a period.

            use '\' to escape a character, so 
                %2hi 
            will draw 'hi' in red, but 
                \%2hi 
            will draw '%2hi' in the normal color.)
        */
        private static Random random = new Random();

        public static void Write(string str, int delay = 20)
        {
            Console.Write("\n");
            for (int i = 0; i < str.Length; ++i)
            {
                if (str[i] == '%')
                {
                    int index = Convert.ToInt32(char.GetNumericValue(str[++i]));
                    switch (index)
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case 1:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case 3:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case 4:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        case 5:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                    }
                }
                else if(str[i] == '\\')
                {
                    Console.Write(str[++i]);
                }
                else if(str[i] == ' ' && Console.CursorLeft % Console.BufferWidth > 70)
                {
                    Console.Write("\n");
                }
                else if(str[i] == '`')
                {
                    Thread.Sleep(10 * delay);
                }
                else if(str[i] == ',' || str[i] == '.' || str[i] == '!' || str[i] == '?')
                {
                    Console.Write(str[i]);
                    Thread.Sleep(10 * delay);
                }
                else
                {
                    Console.Write(str[i]);
                    Thread.Sleep(delay);
                }
            }
        }
    }
}
