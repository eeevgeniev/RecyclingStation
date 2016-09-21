namespace RecyclingStation.Core.Commands
{
    using Attributes.CommandAttributes;
    using Attributes.CommandInjectors;
    using Bases;
    using IO.Contracts;
    using Models.ChangeManagmentRequirements;
    using Models.ChangeManagmentRequirements.Contracts;
    using Repository.Contracts;

    [Command]
    public class ChangeManagementRequirementCommand : CommandBase
    {
        [Injector]
        private IRecyclingStationRepository repository;
        [Injector]
        private IInputOutput inputOutput;

        public ChangeManagementRequirementCommand(string[] parameters) 
            : base(parameters)
        {
        }

        public override void Execute()
        {
            if (this.Parameters.Length != 3)
            {
                return;
            }

            IChangeManagementRequirement requirement = new ChangeManagmentRequirement(
                double.Parse(this.Parameters[0]),
                double.Parse(this.Parameters[1]),
                this.Parameters[2]);

            this.repository.ChangeManagementRequirement(requirement);

            this.inputOutput.WriteMessage("Management requirement changed!");
        }
    }
}
