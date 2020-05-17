using System;

namespace AO.Models
{
    /// <summary>
    /// defines a schema for use with generated tables
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SchemaAttribute : Attribute
    {
        public SchemaAttribute(string schemaName)
        {
            Name = schemaName;
        }

        public string Name { get; }
    }
}
