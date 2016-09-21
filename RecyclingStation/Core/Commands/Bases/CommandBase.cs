namespace RecyclingStation.Core.Commands.Bases
{
    using Contracts;

    public abstract class CommandBase : ICommand
    {
        protected CommandBase(string[] parameters)
        {
            this.Parameters = parameters;
        }

        public string[] Parameters { get; private set; }

        public abstract void Execute();
    }
}
