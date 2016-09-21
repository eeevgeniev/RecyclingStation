namespace RecyclingStation.Models.ChangeManagmentRequirements
{
    using Contracts;

    public class ChangeManagmentRequirement : IChangeManagementRequirement
    {
        public ChangeManagmentRequirement(
            double energyBalance,
            double capitalBalance,
            string garbageType
            )
        {
            this.GarbageType = garbageType;
            this.CapitalBalance = capitalBalance;
            this.EnergyBalance = energyBalance;
        }

        public double CapitalBalance { get; private set; }

        public double EnergyBalance { get; private set; }

        public string GarbageType { get; private set; }
    }
}
