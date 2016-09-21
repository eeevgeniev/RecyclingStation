namespace RecyclingStation
{
    using System.Globalization;
    using System.Threading;
    using Core.Engines;
    using Core.Engines.Contracts;
    using IO;
    using IO.Contracts;
    using Repository;
    using Repository.Contracts;
    using WasteDisposal.Interfaces;
    using WasteDisposal;

    public class Startup
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            IInputOutput consoleInputOutput = new ConsoleInputOutput();
            IRecyclingStationRepository repository = new RecyclingStationRepository();
            IStrategyHolder strategyHolder = new StrategyHolder();
            IGarbageProcessor garbageProcessor = new GarbageProcessor(strategyHolder);

            IEngine engine = new Engine(
                repository, 
                garbageProcessor, 
                strategyHolder, 
                consoleInputOutput);

            engine.Run();
        }
    }
}
