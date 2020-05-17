using System;

namespace AO.Models
{
    /// <summary>
    /// denotes a calculated column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CalculatedAttribute : Attribute
    {
        public CalculatedAttribute(string expression, bool persisted = false)
        {
            Expression = expression;
            IsPersisted = persisted;
        }

        public string Expression { get; }
        public bool IsPersisted { get; }
    }
}
