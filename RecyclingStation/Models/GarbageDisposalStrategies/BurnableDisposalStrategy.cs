namespace RecyclingStation.Models.GarbageDisposalStrategies
{
    using Attributes.DisposableAttributes;
    using Attributes.GarbageDisposalStrategyAttributes;
    using ProcessingDatas;
    using WasteDisposal.Interfaces;

    [BurnableDisposable]
    [GarbageDisposableStrategy]
    public class BurnableDisposalStrategy : IGarbageDisposalStrategy
    {
        private const double EnergyUsedDivider = 1D - 0.2D;
        private const double DefaultValues = 0D;

        public IProcessingData ProcessGarbage(IWaste garbage)
        {
            double totalGarbageVolume = garbage.Weight * garbage.VolumePerKg;

            double energyEarned = totalGarbageVolume * EnergyUsedDivider;

            double energyBalance = energyEarned;
            double capitalBalance = DefaultValues;

            ProcessingData processData = new ProcessingData(capitalBalance, energyBalance);

            return processData;
        }
    }
}
