namespace RecyclingStation.Repository.Contracts
{
    using WasteDisposal.Interfaces;
    using Models.ChangeManagmentRequirements.Contracts;

    public interface IRecyclingStationRepository
    {
        double Capital { get; }

        double Energy { get; }

        void Add(IProcessingData processData);

        void ChangeManagementRequirement(IChangeManagementRequirement requirement);

        IChangeManagementRequirement GetManagmentRequirement(string key);
    }
}
