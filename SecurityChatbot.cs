using System;
using System.Collections.Generic;
using System.Media;

namespace Chatbot
{
    public class SecurityChatbot : ChatbotBase, IResponder
    {
        private bool _running;
        private Dictionary<string, string> _responses;

        // Constructor with username and greeting audio
        public SecurityChatbot(string username, string audioPath) : base(username, audioPath)
        {
            _running = true;
            InitializeResponses();
        }

        // Constructor with just audio path
        public SecurityChatbot(string audioPath) : base(audioPath)
        {
            _running = true;
            InitializeResponses();
        }

        // Initializes predefined keywords and responses with cybersecurity info
        private void InitializeResponses()
        {
            _responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "how are you", "🤖 I'm fully patched and secured, thanks for asking!" },
                { "purpose", "🔐 I'm here to educate and protect you from cyber threats. Ask me anything cybersecurity!" },
                { "what can i ask you", "🧠 Topics include: phishing, malware, ransomware, VPN, password safety, firewalls, social engineering, antivirus, and general cybersecurity tips." },
                { "cybersecurity", "🛡️ Cybersecurity protects your systems, networks, and data from threats like hackers, viruses, and scams. Stay safe by updating devices, using strong passwords, being cautious online, and learning about threats." },
                { "phishing", "🎣 Phishing is a scam where attackers pose as legitimate sources to steal info. Never click suspicious links or download attachments from unknown senders." },
                { "malware", "🦠 Malware is harmful software that can steal, encrypt, or delete your data. Avoid risky downloads and use antivirus tools." },
                { "password", "🔑 Strong, unique passwords keep accounts safe. Use password managers and turn on two-factor authentication (2FA)." },
                { "browsing", "🌐 Safe browsing means avoiding unsecured sites, clearing cookies, and not clicking on suspicious ads or popups." },
                { "vpn", "🔒 A VPN encrypts your connection, hides your IP, and protects your identity—especially on public Wi-Fi." },
                { "firewall", "🧱 A firewall is like a gatekeeper for your network, blocking unauthorized access. Keep it turned on!" },
                { "social engineering", "🎭 Social engineering is psychological manipulation. Don't trust unsolicited messages—verify before you share anything." },
                { "antivirus", "🛡️ Antivirus software detects and removes malicious threats. Keep it updated and scan your system regularly." },
                { "ransomware", "💰 Ransomware locks your files for a ransom. Prevent it by backing up data and not clicking unknown links." },
                { "two factor", "📲 2FA boosts account security by requiring both a password and a code from your device." },
            };
        }

        // Play greeting sound and show banners
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

        // Main chat interaction loop
        public override void StartChat()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n👋 Hello there {Username}! I'm your Cybersecurity Awareness Bot.");
            Console.ResetColor();

            while (_running)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n💬 Feel free to ask me something about cyberesecurity (type 'exit' to quit):");
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

        // Responds to user input by matching keywords
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

            // If no keyword matches, offer a fallback tip
            Console.WriteLine($"🤔 I'm sorry {Username} I didn't get that try typing 'what can I ask', but here’s something useful:\n");
            Console.WriteLine("💡 **Cybersecurity Tip:** Always keep your software updated, never reuse passwords, and think before you click. Ask me about 'VPNs', 'malware', or 'phishing' to learn more.");
        }
   
    }
}
