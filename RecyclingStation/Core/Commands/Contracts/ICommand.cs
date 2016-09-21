namespace RecyclingStation.Core.Commands.Contracts
{
    public interface ICommand
    {
        string[] Parameters { get; }

        void Execute();
    }
}
