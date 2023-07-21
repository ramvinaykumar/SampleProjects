using System;
using System.ComponentModel;

namespace Code.Practice.Samples.Common
{
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionWithStringAttribute : DescriptionAttribute
    {
        public string Value { get; private set; }

        public DescriptionWithStringAttribute(string description, string value) : base(description)
        {
            Value = value;
        }
    }
}
