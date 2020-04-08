﻿using AO.DbSchema.Attributes.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AO.DbSchema.Attributes.Models
{
    public class ObjectName
    {
        public string Schema { get; set; }
        public string Name { get; set; }

        public override string ToString() => (!string.IsNullOrEmpty(Schema)) ? $"{Schema}.{Name}" : Name;

        public override bool Equals(object obj)
        {
            var test = obj as ObjectName;
            return (test != null) ? test.ToString().ToLower().Equals(this.ToString().ToLower()) : false;
        }

        public override int GetHashCode()
        {
            return ToString().ToLower().GetHashCode();
        }

        public static ObjectName FromType(Type type, string defaultSchema = "dbo")
        {
            string name = (type.HasAttribute(out TableAttribute tableAttr)) ? tableAttr.Name : CleanedGenericName(type);

            string schema =
                (type.HasAttribute(out SchemaAttribute schemaAttr)) ? schemaAttr.Name :
                (tableAttr != null && !string.IsNullOrEmpty(tableAttr.Schema)) ? tableAttr.Schema :
                defaultSchema;

            return new ObjectName() { Schema = schema, Name = name };
        }

        private static string CleanedGenericName(Type type)
        {
            return (type.IsGenericType) ? 
                $"{UpTo(type.GetGenericTypeDefinition().Name, "`")}_{string.Join("_", type.GetGenericArguments().Select(t => AliasFromTypeName(t)))}" : 
                type.Name;
        }

        private static string UpTo(string input, string lookFor)
        {
            int indexOf = input.IndexOf(lookFor);
            return (indexOf > -1) ? input.Substring(0, indexOf) : input;
        }

        private static string AliasFromTypeName(Type t)
        {
            Dictionary<Type, string> types = new Dictionary<Type, string>()
            {
                { typeof(int), "int" },
                { typeof(long), "long" }
            };

            return (types.ContainsKey(t)) ? types[t] : t.Name;
        }
    }
}
