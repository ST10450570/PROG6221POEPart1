using System;

namespace Chatbot
{
    // This class checks if user input is valid (not empty or whitespace)
    public static class InputValidator
    {
        public static bool IsValid(string input)
        {
            try
            {
                // Returns true if the input is not null, empty, or whitespace
                return !string.IsNullOrWhiteSpace(input);
            }
            catch
            {
                // In case of unexpected error, treat input as invalid
                return false;
            }
        }
    }
}
