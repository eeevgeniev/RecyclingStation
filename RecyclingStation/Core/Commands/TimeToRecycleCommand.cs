namespace RecyclingStation.Core.Commands
{
    using System;
    using Attributes.CommandAttributes;
    using Bases;

    [Command]
    public class TimeToRecycleCommand : CommandBase
    {
        public TimeToRecycleCommand(string[] parameters) 
            : base(parameters)
        {
        }

        public override void Execute()
        {
            Environment.Exit(0);
        }
    }
}
