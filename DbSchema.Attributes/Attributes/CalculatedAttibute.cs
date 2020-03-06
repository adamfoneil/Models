using System;

namespace AO.DbSchema.Attributes.Attributes
{
    /// <summary>
    /// denotes a calculated column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CalculatedAttibute : Attribute
    {
        public CalculatedAttibute(string expression, bool persisted = false)
        {
            Expression = expression;
            IsPersisted = persisted;
        }

        public string Expression { get; }
        public bool IsPersisted { get; }
    }
}
