using System;

namespace AO.Models.Attributes
{
    /// <summary>
    /// causes a property to be ignored by change tracking
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NoChangeTrackingAttribute : Attribute
    {
    }
}
