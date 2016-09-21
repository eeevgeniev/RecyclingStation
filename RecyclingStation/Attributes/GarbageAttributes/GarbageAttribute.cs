namespace RecyclingStation.Attributes.GarbageAttributes
{
    using System;

    public class GarbageAttribute : Attribute
    {
        public GarbageAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
