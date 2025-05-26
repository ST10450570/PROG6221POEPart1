using System;
using System.Collections.Generic;
using System.Media;
using System.Linq;

namespace Chatbot
{
    public class SecurityChatbot : ChatbotBase, IResponder
    {
        private bool _running;
        private Dictionary<string, List<string>> _responses;
        private Dictionary<string, string> _userMemory;
        private List<string> _exitCommands = new List<string> { "exit", "quit", "leave", "stop", "end chat", "bye" };
        private string _currentTopic = "";
        private Random _random = new Random();

        public SecurityChatbot(string username, string audioPath) : base(username, audioPath)
        {
            _running = true;
            _userMemory = new Dictionary<string, string>();
            InitializeResponses();
        }

        public SecurityChatbot(string audioPath) : base(audioPath)
        {
            _running = true;
            _userMemory = new Dictionary<string, string>();
            InitializeResponses();
        }

        private void InitializeResponses()
        {
            _responses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "how are you", new List<string> {
                    "🤖 I'm fully patched and secured, thanks for asking! Ready to help with your cybersecurity questions.",
                    "🔒 I'm operating at optimal security levels! How can I assist you today?",
                    "🛡️ My firewalls are up and my antivirus is fresh - ready to protect and serve!"
                }},
                { "purpose", new List<string> {
                    "🔐 I'm here to educate and protect you from cyber threats. Ask me anything about cybersecurity!",
                    "🎓 My mission is cybersecurity awareness. What would you like to learn about today?",
                    "💡 I'm your digital security coach here to help you navigate online dangers."
                }},
                { "what can i ask you", new List<string> {
                    "🧠 Topics include: phishing, malware, ransomware, VPN, password safety, firewalls, social engineering, antivirus, and general cybersecurity tips.",
                    "📚 Ask me about: data breaches, two-factor authentication, secure browsing, email scams, network security, or any cybersecurity concern!",
                    "🌐 I can discuss: cyber threats, privacy protection, secure communications, IoT security, and much more!"
                }},
                { "cybersecurity", new List<string> {
                    "🛡️ Cybersecurity protects your systems, networks, and data from threats like hackers, viruses, and scams. You can stay safe by updating devices, using strong passwords, being cautious online, and learning about threats.",
                    "🔒 Cybersecurity is your digital armor. It includes practices like encryption, access control, and threat detection to keep your information safe from cybercriminals.",
                    "💻 Cybersecurity isn't just for tech experts - everyone needs basic knowledge to protect themselves online in our connected world."
                }},
                { "phishing", new List<string> {
                    "🎣 Phishing is a scam where attackers pose as legitimate sources to steal info. Never click suspicious links or download attachments from unknown senders.",
                    "📧 Phishing emails often create urgency ('Your account will be closed!') to trick you. Always verify by contacting the company directly.",
                    "🕵️‍♂️ Sophisticated phishing (spear phishing) targets specific individuals with personalized messages. Be extra careful with unexpected requests for info."
                }},
                { "malware", new List<string> {
                    "🦠 Malware is harmful software that can steal, encrypt, or delete your data. Avoid risky downloads and use antivirus tools.",
                    "💾 Malware spreads through infected files, malicious ads, or compromised websites. Keep backups and don't disable your security software.",
                    "👾 Types of malware include viruses, worms, trojans, and spyware. Each has different methods of infection and damage."
                }},
                { "password", new List<string> {
                    "🔑 Strong, unique passwords keep accounts safe. Use password managers and turn on two-factor authentication (2FA).",
                    "🔄 Password hygiene: Minimum 12 characters, mix of types, no personal info, and never reuse across sites!",
                    "🔐 Consider passphrases (like 'PurpleTurtleJumped42!') - easier to remember, harder to crack than complex passwords."
                }},
                { "browsing", new List<string> {
                    "🌐 Safe browsing means avoiding unsecured sites, clearing cookies, and not clicking on suspicious ads or popups.",
                    "⚠️ Look for HTTPS and the padlock icon. Browser extensions like uBlock Origin can block malicious content.",
                    "🕵️‍♂️ Private/incognito mode doesn't make you anonymous - it just doesn't save history locally. Use a VPN for real privacy."
                }},
                { "vpn", new List<string> {
                    "🔒 A VPN encrypts your connection, hides your IP, and protects your identity—especially on public Wi-Fi.",
                    "🌍 VPNs can also bypass geographic restrictions, but choose reputable providers that don't log your activity.",
                    "⚡ For best security, enable the VPN kill switch feature which blocks all traffic if the VPN connection drops."
                }},
                { "firewall", new List<string> {
                    "🧱 A firewall is like a gatekeeper for your network, blocking unauthorized access. Keep it turned on!",
                    "🛡️ Firewalls can be hardware (router) or software (Windows Defender). Both provide important layers of protection.",
                    "🔍 Configure firewall rules carefully - too restrictive may break apps, too loose creates vulnerabilities."
                }},
                { "social engineering", new List<string> {
                    "🎭 Social engineering is psychological manipulation. Don't trust unsolicited messages—verify before you share anything.",
                    "📞 Common tactics include: impersonating IT support, fake emergencies, or offering too-good-to-be-true deals.",
                    "🤔 Always verify identities independently (call back using official numbers) before providing sensitive information."
                }},
                { "antivirus", new List<string> {
                    "🛡️ Antivirus software detects and removes malicious threats. Keep it updated and scan your system regularly.",
                    "⏰ Schedule weekly scans and enable real-time protection. Free options like Windows Defender provide basic coverage.",
                    "🔄 Even the best antivirus can't catch everything - combine it with safe browsing habits for best protection."
                }},
                { "ransomware", new List<string> {
                    "💰 Ransomware locks your files for a ransom. Prevent it by backing up data and not clicking unknown links.",
                    "💾 The 3-2-1 backup rule protects against ransomware: 3 copies, 2 different media, 1 offsite/cloud.",
                    "🚫 If infected, don't pay - there's no guarantee you'll get files back, and it funds criminal operations."
                }},
                { "two factor", new List<string> {
                    "📲 2FA boosts account security by requiring both a password and a code from your device.",
                    "🔢 Prefer app-based 2FA (Google/Microsoft Authenticator) over SMS which can be intercepted via SIM swapping.",
                    "🛡️ Backup 2FA codes! Store them securely in case you lose access to your primary authentication method."
                }},
                { "data breach", new List<string> {
                    "💥 A data breach exposes sensitive information. Check haveibeenpwned.com to see if your accounts are compromised.",
                    "🔄 After a breach: Change that password everywhere it was used, enable 2FA, and watch for suspicious activity.",
                    "📧 Breached email? Be extra vigilant for phishing attempts using your personal info to appear legitimate."
                }},
                { "iot security", new List<string> {
                    "🏠 IoT devices (smart home gadgets) often have weak security. Change default passwords and keep firmware updated.",
                    "📶 Create a separate WiFi network for IoT devices to limit their access to your main devices and data.",
                    "🔌 Disable unnecessary features like remote access if you don't need them - every enabled feature is a potential vulnerability."
                }},
                { "public wifi", new List<string> {
                    "☕ Public WiFi is risky - hackers can intercept traffic. Avoid accessing sensitive accounts or use a VPN.",
                    "📱 If you must use public WiFi, stick to HTTPS sites and consider using your mobile hotspot instead.",
                    "🏦 Never do online banking or shopping on public WiFi without VPN protection - your data could be intercepted."
                }},
                { "encryption", new List<string> {
                    "🔐 Encryption scrambles data so only authorized parties can read it. Look for end-to-end encrypted messaging apps.",
                    "💾 Full-disk encryption (BitLocker/FileVault) protects your data if your device is lost or stolen.",
                    "📁 For sensitive files, use encrypted containers (VeraCrypt) or password-protected ZIPs with strong passwords."
                }},
                { "dark web", new List<string> {
                    "🌑 The dark web requires special browsers (Tor) and hosts both legitimate privacy tools and illegal markets.",
                    "⚠️ Your personal info might be for sale on the dark web after data breaches. Consider dark web monitoring services.",
                    "🔍 Law enforcement monitors dark web markets - accessing illegal content carries serious legal risks."
                }},
                { "zero trust", new List<string> {
                    "🔄 Zero Trust means 'never trust, always verify' - requiring authentication for every access attempt, even inside networks.",
                    "🏢 Enterprises adopt Zero Trust architectures to prevent lateral movement by hackers who breach perimeter defenses.",
                    "🏠 You can apply Zero Trust principles at home by segmenting networks and requiring authentication for all devices."
                }}
            };

            // Initialize tips for each topic
            foreach (var topic in _responses.Keys.ToList())
            {
                var tips = new List<string>();
                switch (topic.ToLower())
                {
                    case "phishing":
                        tips.AddRange(new[] {
                            "💡 Tip: Hover over links to see the real URL before clicking.",
                            "💡 Tip: Legitimate companies won't ask for sensitive info via email.",
                            "💡 Tip: Check for poor grammar/spelling - common in phishing attempts.",
                            "💡 Tip: If an email creates urgency, it's likely a scam.",
                            "💡 Tip: Bookmark important sites rather than clicking links in emails."
                        });
                        break;
                    case "password":
                        tips.AddRange(new[] {
                            "💡 Tip: Use a passphrase like 'CorrectHorseBatteryStaple' instead of complex passwords.",
                            "💡 Tip: Change passwords every 3-6 months, especially for critical accounts.",
                            "💡 Tip: Never share passwords via email/text - use a secure sharing tool if needed.",
                            "💡 Tip: Enable biometric authentication (fingerprint/face) where available.",
                            "💡 Tip: Use your password manager's password generator for truly random passwords."
                        });
                        break;
                    case "vpn":
                        tips.AddRange(new[] {
                            "💡 Tip: Choose VPNs with a no-logs policy and independent audits.",
                            "💡 Tip: Connect to VPN servers geographically close for better speeds.",
                            "💡 Tip: Some countries restrict VPNs - check local laws before traveling.",
                            "💡 Tip: VPNs can slow connections - disable when not needed for privacy.",
                            "💡 Tip: Free VPNs often monetize your data - paid options are more trustworthy."
                        });
                        break;
                    case "malware":
                        tips.AddRange(new[] {
                            "💡 Tip: Don't disable User Account Control (UAC) - it prevents silent installs.",
                            "💡 Tip: Regularly check browser extensions - remove unused or suspicious ones.",
                            "💡 Tip: Windows Defender + occasional Malwarebytes scans provide solid free protection.",
                            "💡 Tip: Be wary of 'tech support' cold calls claiming your computer is infected.",
                            "💡 Tip: Pirated software often contains malware - it's not worth the risk."
                        });
                        break;
                    case "social engineering":
                        tips.AddRange(new[] {
                            "💡 Tip: Verify unexpected requests by contacting the person through another channel.",
                            "💡 Tip: Scammers often pose as authority figures (police, IRS, IT) to create fear.",
                            "💡 Tip: Be skeptical of anyone asking for gift cards as payment - it's a scam hallmark.",
                            "💡 Tip: Don't confirm personal info to callers - they could be data mining.",
                            "💡 Tip: Social engineers exploit kindness - it's okay to say no to suspicious requests."
                        });
                        break;
                    default:
                        tips.Add("💡 Tip: Stay curious about cybersecurity - the more you learn, the safer you'll be!");
                        break;
                }
                _responses[topic].AddRange(tips);
            }
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
            Console.WriteLine($"\n👋 Welcome {Username}! I'm your Cybersecurity Awareness Bot.");
            
           Console.ResetColor();

            while (_running)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n💬 Ask me something (type 'exit', 'quit', or 'bye' to end):");
                Console.ResetColor();
                Console.Write("> ");
                string input = Console.ReadLine();

                if (!InputValidator.IsValid(input))
                {
                    Console.WriteLine("🤖 Please type something I can understand.");
                    continue;
                }

                if (_exitCommands.Contains(input.ToLower()))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n👋 Stay safe, {Username}! Remember: \"Security is not a product, but a process.\"");
                    Console.ResetColor();
                    _running = false;
                    break;
                }

                Respond(input.ToLower());
            }
        }

        public void Respond(string input)
        {
            // Check for sentiment
            string sentiment = DetectSentiment(input);

            // Check if user is asking for tips
            if (input.Contains("tip") || input.Contains("advice") || input.Contains("suggestion"))
            {
                if (!string.IsNullOrEmpty(_currentTopic))
                {
                    var tips = _responses[_currentTopic].Where(r => r.StartsWith("💡")).ToList();
                    if (tips.Any())
                    {
                        Console.WriteLine(AdjustForSentiment(tips[_random.Next(tips.Count)], sentiment));
                        return;
                    }
                }
                Console.WriteLine(AdjustForSentiment("💡 General cybersecurity tip: Make regular backups and test restoring them. It's the only sure way to recover from ransomware or hardware failure.", sentiment));
                return;
            }

            // Check if user wants to change topic
            if (input.Contains("change topic") || input.Contains("new topic") || input.Contains("something else"))
            {
                _currentTopic = "";
                Console.WriteLine(AdjustForSentiment("🔄 Sure! What would you like to discuss instead? You can ask about any cybersecurity topic.", sentiment));
                return;
            }

            // Check for keyword matches
            foreach (var keyword in _responses.Keys)
            {
                if (input.Contains(keyword))
                {
                    _currentTopic = keyword;
                    _userMemory["last_topic"] = keyword;

                    if (!_userMemory.ContainsKey("favorite_topic"))
                    {
                        _userMemory["favorite_topic"] = keyword;
                        Console.WriteLine(AdjustForSentiment($"🌟 I'll remember that {keyword} interests you! Feel free to ask more or say 'change topic'.", sentiment));
                    }

                    var responses = _responses[keyword].Where(r => !r.StartsWith("💡")).ToList();
                    Console.WriteLine(AdjustForSentiment(responses[_random.Next(responses.Count)], sentiment));

                    // Offer tips if the user seems interested
                    if (sentiment == "positive" || sentiment == "curious")
                    {
                        Console.WriteLine(AdjustForSentiment($"\nWould you like some specific tips about {keyword}? Just ask!", sentiment));
                    }
                    return;
                }
            }

            // Check memory for follow-up
            if (_userMemory.ContainsKey("last_topic") && (input.Contains("more") || input.Contains("explain") || input.Contains("detail")))
            {
                var topic = _userMemory["last_topic"];
                var responses = _responses[topic].Where(r => !r.StartsWith("💡")).ToList();
                if (responses.Count > 1)
                {
                    var unusedResponses = responses.Where(r => !r.Contains(_currentTopic)).ToList();
                    if (unusedResponses.Any())
                    {
                        Console.WriteLine(AdjustForSentiment(unusedResponses[_random.Next(unusedResponses.Count)], sentiment));
                        return;
                    }
                }
            }

            // Personalized response if we know user's favorite topic
            if (_userMemory.ContainsKey("favorite_topic") && input.Contains("favorite"))
            {
                Console.WriteLine(AdjustForSentiment($"🌟 Your favorite topic seems to be {_userMemory["favorite_topic"]}. Would you like to discuss that more?", sentiment));
                return;
            }

            // Fallback response
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(AdjustForSentiment("🤔 I'm not sure I understand. Try being more specific or ask about:\n" +
                "- Phishing scams\n- Password security\n- VPNs\n- Malware protection\nOr say 'tips' for random security advice.", sentiment));
            Console.ResetColor();
        }

        private string DetectSentiment(string input)
        {
            if (input.Contains("?") || input.Contains("how") || input.Contains("what") || input.Contains("why"))
                return "curious";
            if (input.Contains("!") || input.Contains("great") || input.Contains("awesome") || input.Contains("thank"))
                return "positive";
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("nervous") || input.Contains("help"))
                return "anxious";
            if (input.Contains("mad") || input.Contains("angry") || input.Contains("frustrated") || input.Contains("hate"))
                return "negative";
            return "neutral";
        }

        private string AdjustForSentiment(string message, string sentiment)
        {
            switch (sentiment)
            {
                case "positive":
                    return message + " 😊";
                case "anxious":
                    return "🤗 " + message + "\nRemember, good security practices can greatly reduce risks!";
                case "negative":
                    return "😌 " + message.Replace("!", ".") + " I'm here to help make security easier for you.";
                case "curious":
                    return "🧠 " + message + "\nThat's a great question!";
                default:
                    return message;
            }
        }
    }
}