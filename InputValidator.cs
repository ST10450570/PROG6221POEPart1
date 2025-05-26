using System;

namespace Chatbot
{
    public static class InputValidator
    {
        public static bool IsValid(string input)
        {
            try
            {
                return !string.IsNullOrWhiteSpace(input);
            }
            catch
            {
                return false;
            }
        }
    }
}