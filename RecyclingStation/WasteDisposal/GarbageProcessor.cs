namespace RecyclingStation.WasteDisposal
{
    using System;
    using System.Linq;
    using Attributes;
    using Interfaces;

    public class GarbageProcessor : IGarbageProcessor
    {
        public GarbageProcessor(IStrategyHolder strategyHolder)
        {
            this.StrategyHolder = strategyHolder;
        }

        public IStrategyHolder StrategyHolder { get; private set; }

        public IProcessingData ProcessWaste(IWaste garbage)
        {
            Type type = garbage.GetType();

            DisposableAttribute disposalAttribute = (DisposableAttribute)type.GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType().IsSubclassOf(typeof(DisposableAttribute)));

            IGarbageDisposalStrategy currentStrategy = this.StrategyHolder
                .GetDisposalStrategy(disposalAttribute.GetType());

            return currentStrategy.ProcessGarbage(garbage);
        }
    }
}
