using System;

namespace MobileCRM.Shared.Attributes
{
    public class DisplayAttribute : Attribute
    {
        public string Name { get; private set; }

        public DisplayAttribute (string name)
        {
            Name = name;
        }
    }
}

