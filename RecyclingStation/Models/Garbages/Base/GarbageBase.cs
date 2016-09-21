namespace RecyclingStation.Models.Garbages.Base
{
    using System;
    using WasteDisposal.Interfaces;

    public abstract class GarbageBase : IWaste
    {
        protected GarbageBase(string name, double volumePerKg, double weight)
        {
            this.Name = name;
            this.VolumePerKg = volumePerKg;
            this.Weight = weight;
        }

        public string Name { get; private set; }

        public double VolumePerKg { get; private set; }

        public double Weight { get; private set; }
    }
}
