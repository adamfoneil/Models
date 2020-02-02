﻿using System;

namespace AO.DbSchema.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class IdentityAttribute : Attribute
    {        
        public IdentityAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; }
    }
}