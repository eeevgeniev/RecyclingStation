namespace RecyclingStation.Models.Garbages
{
    using Base;
    using Attributes.DisposableAttributes;
    using Attributes.GarbageAttributes;

    [BurnableDisposable]
    [Garbage("Burnable")]
    public class BurnableGarbage : GarbageBase
    {
        public BurnableGarbage(string name, double volumnePerKg, double weight) 
            : base(name, volumnePerKg, weight)
        {
        }
    }
}
