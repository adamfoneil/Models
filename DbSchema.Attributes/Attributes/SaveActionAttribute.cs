﻿using AO.DbSchema.Enums;
using System;

namespace AO.DbSchema.Attributes
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