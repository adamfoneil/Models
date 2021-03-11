using System;

namespace AO.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DefaultAttribute : Attribute
    {
        public DefaultAttribute(string expression)
        {
            Expression = expression;
        }

        public string Expression { get; }
    }
}
