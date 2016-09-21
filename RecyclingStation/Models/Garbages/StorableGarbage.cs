namespace RecyclingStation.Models.Garbages
{
    using Attributes.DisposableAttributes;
    using Attributes.GarbageAttributes;
    using Base;

    [StorableDisposable]
    [Garbage("Storable")]
    public class StorableGarbage : GarbageBase
    {
        public StorableGarbage(string name, double volumnePerKg, double weight) 
            : base(name, volumnePerKg, weight)
        {
        }
    }
}
