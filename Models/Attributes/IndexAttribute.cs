using System;

namespace AO.Models.Attributes
{
    /// <summary>
    /// defines a non-unique index
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class IndexAttribute : Attribute
    {
        public IndexAttribute(params string[] propertyNames)
        {
            PropertyNames = propertyNames;
        }

        public string[] PropertyNames { get; }
    }
}
