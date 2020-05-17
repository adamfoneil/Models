using AO.Models.Enums;
using System;

namespace AO.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SaveActionAttribute : Attribute
    {
        public SaveActionAttribute(SaveAction saveAction)
        {
            SaveAction = saveAction;
        }

        public SaveAction SaveAction { get; }
    }
}
