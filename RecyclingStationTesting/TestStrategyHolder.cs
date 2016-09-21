namespace RecyclingStationTesting
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RecyclingStation.Attributes.DisposableAttributes;
    using RecyclingStation.Models.Garbages;
    using RecyclingStation.Models.GarbageDisposalStrategies;
    using RecyclingStation.WasteDisposal;
    using RecyclingStation.WasteDisposal.Interfaces;

    [TestClass]
    public class TestStrategyHolder
    {
        [TestMethod]
        public void TestAddingGettingStrategyToStrategyHolder()
        {
            StrategyHolder holder = new StrategyHolder();

            BurnableDisposalStrategy burnStrategy = new BurnableDisposalStrategy();
            RecyclableDisposalStrategy recStrategy = new RecyclableDisposalStrategy();
            StorableDisposalStrategy storeStrategy = new StorableDisposalStrategy();

            holder.AddStrategy(
                typeof(BurnableDisposableAttribute),
                burnStrategy);

            holder.AddStrategy(
                typeof(RecyclableDisposableAttribute),
                recStrategy);

            holder.AddStrategy(
                typeof(StorableDisposableAttribute),
                storeStrategy);

            Assert.AreEqual(burnStrategy,
                holder.GetDisposalStrategy(typeof(BurnableDisposableAttribute)));

            Assert.AreEqual(recStrategy,
            holder.GetDisposalStrategy(typeof(RecyclableDisposableAttribute)));

            Assert.AreEqual(storeStrategy,
                holder.GetDisposalStrategy(typeof(StorableDisposableAttribute)));

        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestStrategyNotFoundThrowsKeyNotFoundException()
        {
            StrategyHolder holder = new StrategyHolder();

            holder.GetDisposalStrategy(typeof(BurnableDisposableAttribute));
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestRemoveStrategy()
        {
            StrategyHolder holder = new StrategyHolder();

            BurnableDisposalStrategy burnStrategy = new BurnableDisposalStrategy();
            RecyclableDisposalStrategy recStrategy = new RecyclableDisposalStrategy();
            StorableDisposalStrategy storeStrategy = new StorableDisposalStrategy();

            holder.AddStrategy(
                typeof(BurnableDisposableAttribute),
                burnStrategy);

            holder.AddStrategy(
                typeof(RecyclableDisposableAttribute),
                recStrategy);

            holder.AddStrategy(
                typeof(StorableDisposableAttribute),
                storeStrategy);

            holder.RemoveStrategy(typeof(BurnableDisposableAttribute));

            holder.GetDisposalStrategy(typeof(BurnableDisposableAttribute));
        }
    }
}
