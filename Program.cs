using System;

namespace Chatbot
{
    internal class Program
    {
       public static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Chatbot";

            Console.Write("Please enter your name: ");
            string username = Console.ReadLine();

            string audioPath = @"C:\Users\lab_services_student\Downloads\greeting.wav";

            SecurityChatbot bot = new SecurityChatbot(username, audioPath);
            bot.Greet();
            bot.StartChat();
            
        }
    }
}
