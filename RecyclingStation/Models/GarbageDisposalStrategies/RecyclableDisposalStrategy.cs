namespace RecyclingStation.Models.GarbageDisposalStrategies
{
    using Attributes.DisposableAttributes;
    using Attributes.GarbageDisposalStrategyAttributes;
    using ProcessingDatas;
    using WasteDisposal.Interfaces;

    [RecyclableDisposable]
    [GarbageDisposableStrategy]
    public class RecyclableDisposalStrategy : IGarbageDisposalStrategy
    {
        private const double CapitalDefaulEarned = 400D;
        private const double EnergyUsedDivider = 0.5;
        private const double DefaultValues = 0D;

        public IProcessingData ProcessGarbage(IWaste garbage)
        {
            double totalGarbageVolume = garbage.Weight * garbage.VolumePerKg;

            double capitalEarned = CapitalDefaulEarned * garbage.Weight;
            double energyUsed = totalGarbageVolume * EnergyUsedDivider;
            
            double energyBalance = DefaultValues - energyUsed;
            double capitalBalance = capitalEarned;

            ProcessingData processData = new ProcessingData(capitalBalance, energyBalance);

            return processData;
        }
    }
}
