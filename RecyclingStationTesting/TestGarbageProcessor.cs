namespace RecyclingStationTesting
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RecyclingStation.Attributes.DisposableAttributes;
    using RecyclingStation.Models.Garbages;
    using RecyclingStation.Models.GarbageDisposalStrategies;
    using RecyclingStation.WasteDisposal;
    using RecyclingStation.WasteDisposal.Interfaces;

    [TestClass]
    public class TestGarbageProcessor
    {
        [TestMethod]
        public void TestAddingStrategyHolderToConstructor()
        {
            IStrategyHolder strategyHolder = new StrategyHolder();

            IGarbageProcessor processor = new GarbageProcessor(strategyHolder);

            Assert.AreEqual(strategyHolder, processor.StrategyHolder);
        }

        [TestMethod]
        public void ProcessWaste()
        {
            double expectedRecCapital = 4000;
            double expectedRecBurnEnergy = -5.7;
            double expectedBurnEnergy = 97.0012;
            double expectedCapital = 0;
            double expectedStorCapital = -2.2961;
            double expectedStorEnergy = -0.4592;

            BurnableGarbage burnGarbage = new BurnableGarbage("Burnable", 3.657, 33.156);
            RecyclableGarbage recGarbage = new RecyclableGarbage("rec", 1.14, 10);
            StorableGarbage storGarbage = new StorableGarbage("stor", 1.57, 2.25);

            IStrategyHolder strategyHolder = new StrategyHolder();

            strategyHolder.AddStrategy(
                typeof(BurnableDisposableAttribute),
                new BurnableDisposalStrategy());

            strategyHolder.AddStrategy(
                typeof(RecyclableDisposableAttribute),
                new RecyclableDisposalStrategy());

            strategyHolder.AddStrategy(
                typeof(StorableDisposableAttribute),
                new StorableDisposalStrategy());

            IGarbageProcessor processor = new GarbageProcessor(strategyHolder);
            IProcessingData processData = processor.ProcessWaste(burnGarbage);

            Assert.AreEqual(expectedBurnEnergy, Math.Round(processData.EnergyBalance, 4));
            Assert.AreEqual(expectedCapital, processData.CapitalBalance);

            processData = processor.ProcessWaste(recGarbage);

            Assert.AreEqual(expectedRecBurnEnergy, Math.Round(processData.EnergyBalance, 1));
            Assert.AreEqual(expectedRecCapital, processData.CapitalBalance);

            processData = processor.ProcessWaste(storGarbage);

            Assert.AreEqual(expectedStorEnergy, Math.Round(processData.EnergyBalance, 4));
            Assert.AreEqual(expectedStorCapital, Math.Round(processData.CapitalBalance, 4));
        }
    }
}
