using System;

namespace Chatbot
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string audioPath = @"C:\Users\chuma\OneDrive\Documents\PROG6221POEPart1\greeting.wav";

            // Play intro + banner
            SecurityChatbot botAudio = new SecurityChatbot(audioPath);
            botAudio.Greet();

            // Ask user for name
            Console.Write("Please enter your name: ");
            string username = Console.ReadLine();
            Console.Title = "Cybersecurity Awareness Chatbot";

            // Start main chat session
            SecurityChatbot bot = new SecurityChatbot(username, audioPath);
            bot.StartChat();
        }
    }
}