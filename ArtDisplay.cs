using System;

namespace Chatbot
{
    public static class ArtDisplay
    {
        public static void ShowAsciiTitle()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
   _____ ______  _____ _    _ _____ _______ _   _ 
  / ____|  ____|/ ____| |  | |_   _|__   __| \ | |
 | (___ | |__  | (___ | |  | | | |    | |  |  \| |
  \___ \|  __|  \___ \| |  | | | |    | |  | . ` |
  ____) | |____ ____) | |__| |_| |_   | |  | |\  |
 |_____/|______|_____/ \____/|_____|  |_|  |_| \_|

                   S E C U R I T Y
            ");
            Console.ResetColor();
        }
    }
}
