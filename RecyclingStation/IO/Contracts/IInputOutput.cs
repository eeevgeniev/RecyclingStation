namespace RecyclingStation.IO.Contracts
{
    public interface IInputOutput
    {
        string ReadMessage();

        void WriteMessage(string message);
    }
}
