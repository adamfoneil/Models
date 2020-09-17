using System;
using System.Collections.Generic;

namespace AO.Models.Attributes
{
    /// <summary>
    /// use this to turn on logged changed tracking on a model class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TrackChangesAttribute : Attribute
    {
        public TrackChangesAttribute(params string[] ignoreProperties)
        {
            IgnoreProperties = ignoreProperties;
        }

        public IEnumerable<string> IgnoreProperties { get; }
    }
}
