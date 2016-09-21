namespace RecyclingStation.Models.Garbages
{
    using Base;
    using Attributes.DisposableAttributes;
    using Attributes.GarbageAttributes;

    [RecyclableDisposable]
    [Garbage("Recyclable")]
    public class RecyclableGarbage : GarbageBase
    {
        public RecyclableGarbage(string name, double volumnePerKg, double weight) 
            : base(name, volumnePerKg, weight)
        {
        }
    }
}
