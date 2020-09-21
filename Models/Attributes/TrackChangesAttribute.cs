using System;
using System.Collections.Generic;
using System.Linq;

namespace AO.Models.Attributes
{
    /// <summary>
    /// use this to turn on logged changed tracking on a model class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TrackChangesAttribute : Attribute
    {
        public TrackChangesAttribute(string ignoreProperties = null)
        {
            IgnoreProperties = ignoreProperties;
        }

        public string IgnoreProperties { get; }

        public IEnumerable<string> GetIgnoreProperties() => 
            IgnoreProperties?.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries) ??
            Enumerable.Empty<string>();
    }
}
