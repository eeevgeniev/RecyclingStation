namespace RecyclingStation.Core.Engines
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Attributes.CommandAttributes;
    using Attributes.CommandInjectors;
    using Attributes.GarbageAttributes;
    using Attributes.GarbageDisposalStrategyAttributes;
    using Contracts;
    using Commands.Bases;
    using IO.Contracts;
    using Repository.Contracts;
    using WasteDisposal.Interfaces;
    using WasteDisposal.Attributes;

    public class Engine : IEngine
    {
        private const string CommandExtension = "Command";
        private const char CommandSeparator = '|';

        private readonly IRecyclingStationRepository repository;
        private readonly IGarbageProcessor garbageProcessor;
        private readonly IStrategyHolder strategyHolder;
        private readonly IInputOutput inputOutput;

        public Engine(
            IRecyclingStationRepository repository,
            IGarbageProcessor garbageProcessor,
            IStrategyHolder strategyHolder,
            IInputOutput inputOutput)
        {
            this.repository = repository;
            this.garbageProcessor = garbageProcessor;
            this.strategyHolder = strategyHolder;
            this.inputOutput = inputOutput;

            this.LoadStrategies();
        }

        public virtual void Run()
        {
            while (true)
            {
                string userCommandInput = this.inputOutput.ReadMessage();

                string[] commandParameters = userCommandInput.Split(new char[] { });

                string[] parameters = null;

                if (commandParameters.Length > 1)
                {
                    parameters = commandParameters[1].Split(
                        new char[] { CommandSeparator }, 
                        StringSplitOptions.RemoveEmptyEntries);
                }

                string commandName = commandParameters[0] + CommandExtension;

                var engineFields = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

                var commandTypes = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => t.GetCustomAttributes().Any(a => a.GetType() == typeof(CommandAttribute)));

                foreach (var commandType in commandTypes)
                {
                    if (commandType.Name == commandName)
                    {
                        var command = (CommandBase)Activator.CreateInstance(commandType, new object[] { parameters });

                        var fields = commandType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                            .Where(f => f.GetCustomAttributes().Any(a => a.GetType() == typeof(InjectorAttribute)));

                        foreach (var commandField in fields)
                        {
                            foreach (var engineField in engineFields)
                            {
                                if (commandField.FieldType == engineField.FieldType)
                                {
                                    commandField.SetValue(command, engineField.GetValue(this));

                                    break;
                                }
                            }
                        }

                        command.Execute();
                    }
                }
            }
        }

        private void LoadStrategies()
        {
            var garbageTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetCustomAttributes().Any(a => a.GetType() == typeof(GarbageAttribute)));

            var garbagesTypeStrategy = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetCustomAttributes().Any(a => a.GetType() == typeof(GarbageDisposableStrategyAttribute)));

            if (garbageTypes != null && garbagesTypeStrategy != null)
            {
                foreach (var garbageType in garbageTypes)
                {
                    var garbageDisposalAttribute = (DisposableAttribute)garbageType
                        .GetCustomAttributes(true)
                        .FirstOrDefault(a => a.GetType().IsSubclassOf(typeof(DisposableAttribute)));

                    foreach (var garbageTypeStrategy in garbagesTypeStrategy)
                    {
                        var garbageStrategyDisposalAttribute = (DisposableAttribute)garbageTypeStrategy
                            .GetCustomAttributes(true)
                            .FirstOrDefault(a => a.GetType().IsSubclassOf(typeof(DisposableAttribute)));

                        if (garbageDisposalAttribute.GetType() == garbageStrategyDisposalAttribute.GetType())
                        {
                            IGarbageDisposalStrategy holder = (IGarbageDisposalStrategy)Activator.
                                CreateInstance(garbageTypeStrategy);

                            this.strategyHolder.AddStrategy(
                                garbageStrategyDisposalAttribute.GetType(),
                                holder);

                            break;
                        }
                    }
                }
            }
        }
    }
}
