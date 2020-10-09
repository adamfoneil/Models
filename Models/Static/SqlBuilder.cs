using AO.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace AO.Models.Static
{
    public class SqlBuilder
    {
        public static IEnumerable<Type> SupportedTypes => new Type[]
        {
            typeof(string),
            typeof(int), typeof(int?),
            typeof(DateTime), typeof(DateTime?),
            typeof(bool), typeof(bool?),
            typeof(long), typeof(long?),
            typeof(decimal), typeof(decimal?),
            typeof(double), typeof(double?),
            typeof(float), typeof(float?),
            typeof(TimeSpan), typeof(TimeSpan?),
            typeof(Guid), typeof(Guid?)
        };

        public static string Insert<T>(IEnumerable<string> columnNames = null, char startDelimiter = '[', char endDelimiter = ']')
        {
            var columns = 
                columnNames ?? 
                GetMappedProperties(typeof(T), SaveAction.Insert).Select(pi => pi.GetColumnName());

            if (!columns.Any()) throw new InvalidOperationException($"Model type {typeof(T).Name} must have at least one column to build INSERT statement.");

            return
                $@"INSERT INTO {ApplyDelimiter(typeof(T).GetTableName(), startDelimiter, endDelimiter)} (
                    {string.Join(", ", columns.Select(col => ApplyDelimiter(col, startDelimiter, endDelimiter)))}
                ) VALUES (
                    {string.Join(", ", columns.Select(col => "@" + col))}
                );";
        }

        public static string Update<T>(IEnumerable<string> columnNames = null, char startDelimiter = '[', char endDelimiter = ']')
        {
            var columns =
               columnNames ??               
               GetMappedProperties(typeof(T), SaveAction.Update).Select(pi => pi.GetColumnName());

            if (!columns.Any()) throw new InvalidOperationException($"Model type {typeof(T).Name} must have at least one column to build UPDATE statement.");

            var type = typeof(T);
            string identityCol = type.GetIdentityName();

            return
                $@"UPDATE {ApplyDelimiter(type.GetTableName(), startDelimiter, endDelimiter)} SET 
                    {string.Join(", ", columns.Select(col => $"{ApplyDelimiter(col, startDelimiter, endDelimiter)}=@{col}"))} 
                WHERE 
                    {ApplyDelimiter(identityCol, startDelimiter, endDelimiter)}=@{identityCol}";
        }

        public static string Delete<T>(char startDelimiter = '[', char endDelimiter = ']') =>
            $@"DELETE {ApplyDelimiter(typeof(T).GetTableName(), startDelimiter, endDelimiter)} WHERE {ApplyDelimiter(typeof(T).GetIdentityName(), startDelimiter, endDelimiter)}=@id";

        private static IEnumerable<PropertyInfo> GetMappedProperties(Type modelType, SaveAction saveAction)
        {
            bool isNullableEnum(Type type)
            {
                return
                    type.IsGenericType &&
                    type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) &&
                    type.GetGenericArguments()[0].IsEnum;
            }

            bool isMapped(PropertyInfo pi)
            {
                if (!pi.CanWrite) return false;
                if (pi.IsIdentity()) return false;
                if (!SupportedTypes.Contains(pi.PropertyType) && !pi.PropertyType.IsEnum && !isNullableEnum(pi.PropertyType)) return false;
                if (!pi.AllowSaveAction(saveAction)) return false;

                var attr = pi.GetCustomAttribute<NotMappedAttribute>();
                if (attr != null) return false;

                return true;
            };

            return modelType.GetProperties().Where(pi => isMapped(pi)).ToArray();
        }

        protected static string ApplyDelimiter(string name, char startDelimiter, char endDelimiter) => 
            string.Join(".", name
                .Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(namePart => $"{startDelimiter}{namePart}{endDelimiter}"));        
    }
}
