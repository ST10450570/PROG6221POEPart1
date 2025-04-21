namespace Chatbot
{
    // Interface to enforce consistent response behavior in bots
    public interface IResponder
    {
        void Respond(string input);
    }
}
