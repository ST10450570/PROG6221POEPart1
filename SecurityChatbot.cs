using System;
using System.Media;

namespace Chatbot
{
    public class SecurityChatbot : ChatbotBase, IResponder
    {
        private bool _running;

        public SecurityChatbot(string username, string audioPath)
            : base(username, audioPath)
        {
            _running = true;
        }

        public override void Greet()
        {
            try
            {
                SoundPlayer player = new SoundPlayer(AudioPath);
                player.PlaySync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error playing greeting: " + ex.Message);
            }

            ArtDisplay.ShowAsciiTitle();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"👋 Welcome {Username}! I’m your Cybersecurity Awareness Bot.\n");
            Console.ResetColor();
        }

        public override void StartChat()
        {
            while (_running)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n💬 Ask me something (type 'exit' to quit):");
                Console.ResetColor();
                Console.Write("> ");
                string input = Console.ReadLine();

                if (!InputValidator.IsValid(input))
                {
                    Console.WriteLine("🤖 Please type something I can understand.");
                    continue;
                }

                if (input.ToLower() == "exit")
                {
                    Console.WriteLine($"\n👋 Stay safe, {Username}!");
                    _running = false;
                    break;
                }

                Respond(input.ToLower());
            }
        }

        public void Respond(string input)
        {
            if (input.Contains("how are you"))
            {
                Console.WriteLine("🤖 I'm fully patched and secured, thanks for asking!");
            }
            else if (input.Contains("purpose"))
            {
                Console.WriteLine("🔐 I’m here to teach you cybersecurity basics and help you stay safe online.");
            }
            else if (input.Contains("phishing"))
            {
                Console.WriteLine("🎣 Phishing is a scam tricking you into giving private info. Watch for shady links!");
            }
            else if (input.Contains("password"))
            {
                Console.WriteLine("🔑 Use strong, unique passwords and enable two-factor authentication.");
            }
            else if (input.Contains("browsing"))
            {
                Console.WriteLine("🌐 Stick to HTTPS websites, avoid pop-ups, and update your browser.");
            }
            else
            {
                Console.WriteLine("🤔 I didn’t get that. Try asking about 'phishing', 'passwords', or 'browsing'.");
            }
        }
    }
}
