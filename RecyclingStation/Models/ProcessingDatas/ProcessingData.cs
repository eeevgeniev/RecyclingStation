namespace RecyclingStation.Models.ProcessingDatas
{
    using WasteDisposal.Interfaces;

    public class ProcessingData : IProcessingData
    {
        public ProcessingData(double capitalBalance, double energyBalance)
        {
            this.CapitalBalance = capitalBalance;
            this.EnergyBalance = energyBalance;
        }

        public double CapitalBalance { get; private set; }

        public double EnergyBalance { get; private set; }
    }
}
