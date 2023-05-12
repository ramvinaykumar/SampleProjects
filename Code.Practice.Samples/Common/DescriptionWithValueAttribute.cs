using System;
using System.ComponentModel;

namespace Code.Practice.Samples.Common
{
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionWithValueAttribute : DescriptionAttribute
    {
        public int Value { get; private set; }

        public DescriptionWithValueAttribute(string description, int value)
            : base(description)
        {
            Value = value;
        }
    }
}
