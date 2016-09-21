namespace RecyclingStation.Models.ChangeManagmentRequirements.Contracts
{
    public interface IChangeManagementRequirement
    {
        double EnergyBalance { get; }

        double CapitalBalance { get; }

        string GarbageType { get; }
    }
}
