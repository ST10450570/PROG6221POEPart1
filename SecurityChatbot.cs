using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;

namespace Chatbot
{
    public class SecurityChatbot : ChatbotBase, IResponder
    {
        private bool _running;
        private Dictionary<string, string> _responses;

        public SecurityChatbot(string username, string audioPath) : base(username, audioPath)
        {
            _running = true;
            InitializeResponses();
        }

        public SecurityChatbot(string audioPath) : base(audioPath)
        {
            _running = true;
            InitializeResponses();
        }

        private void InitializeResponses()
        {
            _responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "how are you", "🤖 I'm fully patched and secured, thanks for asking!" },
                { "purpose", "🔐 I'm your Cybersecurity Awareness Bot. I help you stay safe online by answering questions about cybersecurity." },
                { "what can i ask you", "🧠 You can ask me about phishing, malware, passwords, browsing safely, firewalls, antivirus, social engineering, VPNs, and more!" },
                { "phishing", "🎣 Phishing is when attackers trick you into revealing sensitive information. Always check URLs and never click unknown links." },
                { "malware", "🦠 Malware is malicious software that can damage your system or steal data. Use trusted antivirus programs!" },
                { "password", "🔑 Use long, unique passwords for each account. Consider a password manager and enable two-factor authentication." },
                { "browsing", "🌐 Only visit HTTPS sites, block pop-ups, and clear your cache regularly." },
                { "vpn", "🔒 A VPN hides your IP address and encrypts your data, especially useful on public Wi-Fi." },
                { "firewall", "🧱 A firewall acts as a barrier between your computer and threats from the internet." },
                { "social engineering", "🎭 It's when attackers manipulate people into giving up confidential information. Always verify before sharing info!" },
                { "antivirus", "🛡️ Keep your antivirus software up-to-date to catch the latest threats." },
                { "ransomware", "💰 Ransomware locks your files and demands payment. Backup your data regularly!" },
                { "two factor", "📲 2FA adds a second layer of protection by requiring something you know and something you have." },
            };
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
        }

        public override void StartChat()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n👋 Welcome {Username}! I’m your Cybersecurity Awareness Bot.");
            Console.ResetColor();

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
            foreach (var keyword in _responses.Keys)
            {
                if (input.Contains(keyword))
                {
                    Console.WriteLine(_responses[keyword]);
                    return;
                }
            }

            Console.WriteLine("🤔 I didn’t get that. Try asking about topics like 'phishing', 'VPN', or 'social engineering'. Type 'what can I ask you' for help.");
        }
    }
}
