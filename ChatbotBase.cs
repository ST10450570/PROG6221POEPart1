using System;

namespace Chatbot
{
    public abstract class ChatbotBase
    {
        protected string Username { get; set; }
        protected string AudioPath { get; set; }

        public ChatbotBase(string username, string audioPath)
        {
            Username = string.IsNullOrEmpty(username) ? "Friend" : username;
            AudioPath = audioPath;
        }

        public ChatbotBase(string audioPath)
        {
            AudioPath = audioPath;
        }

        public abstract void Greet();
        public abstract void StartChat();
    }
}