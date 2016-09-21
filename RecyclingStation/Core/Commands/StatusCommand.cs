namespace RecyclingStation.Core.Commands
{
    using Attributes.CommandAttributes;
    using Attributes.CommandInjectors;
    using Bases;
    using IO.Contracts;
    using Repository.Contracts;

    [Command]
    public class StatusCommand : CommandBase
    {
        [Injector]
        private IRecyclingStationRepository repository;
        [Injector]
        private IInputOutput inputOutput;

        public StatusCommand(string[] parameters) 
            : base(parameters)
        {
        }

        public override void Execute()
        {
            this.inputOutput.WriteMessage(this.repository.ToString());
        }
    }
}
