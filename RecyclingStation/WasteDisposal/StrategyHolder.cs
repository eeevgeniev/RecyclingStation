namespace RecyclingStation.WasteDisposal
{
    using System;
    using System.Collections.Generic;
    using Interfaces;

    public class StrategyHolder : IStrategyHolder
    {
        private const string KeyNotFound = "No such strategy found!";

        private readonly IDictionary<Type, IGarbageDisposalStrategy> strategies;

        public StrategyHolder()
        {
            this.strategies = new Dictionary<Type, IGarbageDisposalStrategy>();
        }

        public bool AddStrategy(Type disposableAttribute, IGarbageDisposalStrategy strategy)
        {
            if (this.strategies.ContainsKey(disposableAttribute))
            {
                return false;
            }

            this.strategies.Add(disposableAttribute, strategy);

            return true;
        }

        public IGarbageDisposalStrategy GetDisposalStrategy(Type type)
        {
            IGarbageDisposalStrategy strategy;

            if (this.strategies.TryGetValue(type, out strategy))
            {
                return strategy;
            }

            throw new KeyNotFoundException(KeyNotFound);
        }

        public bool RemoveStrategy(Type disposableAttribute)
        {
            if (!this.strategies.ContainsKey(disposableAttribute))
            {
                return false;
            }

            this.strategies.Remove(disposableAttribute);

            return true;
        }
    }
}
