using System;
using System.Threading;

namespace Chatbot
{
    public static class ArtDisplay
    {
        public static void ShowAsciiTitle()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            // WELCOME ASCII
            string[] welcome = new string[]
            {
                " __        __   _                            _          ",
                " \\ \\      / /__| | ___ ___  _ __ ___   ___  | |_ ___    ",
                "  \\ \\ /\\ / / _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\ | __/ _ \\   ",
                "   \\ V  V /  __/ | (_| (_) | | | | | |  __/ | || (_) |  ",
                "    \\_/\\_/ \\___|_|\\___\\___/|_| |_| |_|\\___|  \\__\\___/   ",
            };

            // CYBERSECURITY ASCII (provided)
            string[] cybersecurity = new string[]
            {
                "",
                "   _____      _                                        _ _         ",
                "  / ____|    | |                                      (_) |        ",
                " | |    _   _| |__   ___ _ __ ___  ___  ___ _   _ _ __ _| |_ _   _ ",
                " | |   | | | | '_ \\ / _ \\ '__/ __|/ _ \\/ __| | | | '__| | __| | | |",
                " | |___| |_| | |_) |  __/ |  \\__ \\  __/ (__| |_| | |  | | |_| |_| |",
                "  \\_____|\\__, |_.__/ \\___|_|  |___/\\___|\\___|\\__,_|_|  |_|\\__|\\__, |",
                "         __/ |                                                __/ |",
                "        |___/                                                |___/ "
                ,
                "                            A W A R E N E S S   B O T"
            };

            // Draw welcome
            foreach (var line in welcome)
            {
                Console.WriteLine(line);
                Thread.Sleep(200);
            }

            Thread.Sleep(400); // Pause between banners

            // Draw cybersecurity banner
            foreach (var line in cybersecurity)
            {
                Console.WriteLine(line);
                Thread.Sleep(150);
            }

            Console.ResetColor();
        }
    }
}
