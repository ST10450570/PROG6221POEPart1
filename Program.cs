using System;

namespace Chatbot
{
    internal class Program
    {
       public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string audioPath = @"C:\Users\lab_services_student\Downloads\greeting.wav";
            SecurityChatbot botAudio = new SecurityChatbot(audioPath);
            botAudio.Greet();
            Console.Write("Please enter your name: ");
            string username = Console.ReadLine();
            Console.Title = "Cybersecurity Awareness Chatbot";

            SecurityChatbot bot = new SecurityChatbot(username, audioPath);

            bot.StartChat();
            
        }
    }
}
