namespace RecyclingStation.Models.GarbageDisposalStrategies
{
    using Attributes.DisposableAttributes;
    using Attributes.GarbageDisposalStrategyAttributes;
    using ProcessingDatas;
    using WasteDisposal.Interfaces;

    [StorableDisposable]
    [GarbageDisposableStrategy]
    public class StorableDisposalStrategy : IGarbageDisposalStrategy
    {
        private const double DefaultValues = 0D;
        private const double EnergyUsedDivider = 0.13;
        private const double CapitalUsedDivider = 0.65;

        public IProcessingData ProcessGarbage(IWaste garbage)
        {
            double totalGarbageVolume = garbage.Weight * garbage.VolumePerKg;

            double capitaUsed = totalGarbageVolume * CapitalUsedDivider;
            double energyUsed = totalGarbageVolume * EnergyUsedDivider;

            double energyBalance = DefaultValues - energyUsed;
            double capitalBalance = DefaultValues - capitaUsed;

            ProcessingData processData = new ProcessingData(capitalBalance, energyBalance);

            return processData;
        }
    }
}
