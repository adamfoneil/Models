using System;

namespace AO.DbSchema.Attributes
{
    /// <summary>
    /// defines a foreign key on a property by referring to the primary type identity.
    /// CascadeUpdate not supported because identity values are read-only by definition
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ReferencesAttribute : Attribute
    {
        public ReferencesAttribute(Type primaryType)
        {
            PrimaryType = primaryType;
        }

        public Type PrimaryType { get; }

        public bool CascadeDelete { get; set; }
    }
}
