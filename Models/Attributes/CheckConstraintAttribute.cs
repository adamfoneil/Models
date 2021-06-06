using System;

namespace AO.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CheckConstraintAttribute : Attribute
    {
        public CheckConstraintAttribute(string constraintName, string expression)
        {
            ConstraintName = constraintName;
            Expression = expression;
        }

        public string ConstraintName { get; }
        public string Expression { get; }
    }
}
