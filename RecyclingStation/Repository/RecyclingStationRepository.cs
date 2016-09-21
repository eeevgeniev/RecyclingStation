namespace RecyclingStation.Repository
{
    using System.Collections.Generic;
    using Contracts;
    using Models.ChangeManagmentRequirements.Contracts;
    using WasteDisposal.Interfaces;

    public class RecyclingStationRepository : IRecyclingStationRepository
    {
        private readonly Dictionary<string, IChangeManagementRequirement> requirementsByName;

        public RecyclingStationRepository()
        {
            this.requirementsByName = new Dictionary<string, IChangeManagementRequirement>();
        }

        public double Capital { get; private set; }

        public double Energy { get; private set; }

        public void Add(IProcessingData processData)
        {
            this.Capital += processData.CapitalBalance;
            this.Energy += processData.EnergyBalance;
        }

        public void ChangeManagementRequirement(IChangeManagementRequirement requirement)
        {
            string key = requirement.GarbageType;

            if (this.requirementsByName.ContainsKey(key))
            {
                this.requirementsByName[key] = requirement;
            }
            else
            {
                this.requirementsByName.Add(
                    key, 
                    requirement);
            }
        }

        public IChangeManagementRequirement GetManagmentRequirement(string key)
        {
            IChangeManagementRequirement requirement;

            if (this.requirementsByName.TryGetValue(
                key, 
                out requirement))
            {
                return requirement;
            }

            return null;
        }

        public override string ToString()
        {
            return $"Energy: {this.Energy:f2} Capital: {this.Capital:f2}";
        }
    }
}
