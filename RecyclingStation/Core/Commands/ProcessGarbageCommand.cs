namespace RecyclingStation.Core.Commands
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Attributes.CommandAttributes;
    using Attributes.CommandInjectors;
    using Attributes.GarbageAttributes;
    using Bases;
    using IO.Contracts;
    using Models.ChangeManagmentRequirements.Contracts;
    using Repository.Contracts;
    using WasteDisposal.Interfaces;

    [Command]
    public class ProcessGarbageCommand : CommandBase
    {
        [Injector]
        private IRecyclingStationRepository repository;
        [Injector]
        private IGarbageProcessor garbageProcessor;
        [Injector]
        private IStrategyHolder strategyHolder;
        [Injector]
        private IInputOutput inputOutput;

        public ProcessGarbageCommand(string[] parameters) 
            : base(parameters)
        {

        }

        public override void Execute()
        {
            string name = this.Parameters[0];
            double volumePerKg = double.Parse(this.Parameters[2]);
            double weight = double.Parse(this.Parameters[1]);
            string garbageTypeName = this.Parameters[3];

            IWaste waste = null;

            var garbageTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetCustomAttributes().Any(a => a.GetType() == typeof(GarbageAttribute)));

            foreach (var garbageType in garbageTypes)
            {
                var attribute = (GarbageAttribute)garbageType.GetCustomAttribute(typeof(GarbageAttribute));

                if (attribute.Name == garbageTypeName)
                {
                    waste = (IWaste)Activator.CreateInstance(
                        garbageType,
                        new object[] { name, volumePerKg, weight });

                    break;
                }
            }

            if (waste == null)
            {
                return;
            }

            IProcessingData processData = this.garbageProcessor.ProcessWaste(waste);

            IChangeManagementRequirement requirement = 
                this.repository.GetManagmentRequirement(garbageTypeName);

            if (requirement != null)
            {
                if (this.repository.Energy < requirement.EnergyBalance || 
                    this.repository.Capital < requirement.CapitalBalance)
                {
                    this.inputOutput.WriteMessage("Processing Denied!");
                    return;
                }
            }

            this.repository.Add(processData);

            this.inputOutput.WriteMessage($"{waste.Weight:f2} kg of {waste.Name} successfully processed!");
        }
    }
}
