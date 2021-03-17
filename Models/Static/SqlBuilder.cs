using AO.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace AO.Models.Static
{
    public static class SqlBuilder
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

        public static string Insert(Type modelType, IEnumerable<string> columnNames = null, char startDelimiter = '[', char endDelimiter = ']')
        {
            var columns = GetColumns(modelType, SaveAction.Insert, columnNames);

            return
                $@"INSERT INTO {ApplyDelimiter(modelType.GetTableName(), startDelimiter, endDelimiter)} (
                    {string.Join(", ", columns.Select(col => ApplyDelimiter(col.columnName, startDelimiter, endDelimiter)))}
                ) VALUES (
                    {string.Join(", ", columns.Select(col => "@" + col.parameterName))}
                );";
        }

        public static string Insert<T>(IEnumerable<string> columnNames = null, char startDelimiter = '[', char endDelimiter = ']')
        {
            return Insert(typeof(T), columnNames, startDelimiter, endDelimiter);
        }

        public static string Update(Type modelType, IEnumerable<string> columnNames = null, char startDelimiter = '[', char endDelimiter = ']')
        {
            var columns = GetColumns(modelType, SaveAction.Update, columnNames);
            
            string identityCol = modelType.GetIdentityName();

            return
                $@"UPDATE {ApplyDelimiter(modelType.GetTableName(), startDelimiter, endDelimiter)} SET 
                    {string.Join(", ", columns.Select(col => $"{ApplyDelimiter(col.columnName, startDelimiter, endDelimiter)}=@{col.parameterName}"))} 
                WHERE 
                    {ApplyDelimiter(identityCol, startDelimiter, endDelimiter)}=@{identityCol}";
        }

        public static string Update<T>(IEnumerable<string> columnNames = null, char startDelimiter = '[', char endDelimiter = ']')
        {
            return Update(typeof(T), columnNames, startDelimiter, endDelimiter);
        }

        public static string Delete(Type modelType, char startDelimiter = '[', char endDelimiter = ']') =>
            $@"DELETE {ApplyDelimiter(modelType.GetTableName(), startDelimiter, endDelimiter)} WHERE {ApplyDelimiter(modelType.GetIdentityName(), startDelimiter, endDelimiter)}=@id";

        public static string Delete<T>(char startDelimiter = '[', char endDelimiter = ']') => Delete(typeof(T), startDelimiter, endDelimiter);

        private static IEnumerable<(string columnName, string parameterName)> GetColumns(Type modelType, SaveAction saveAction, IEnumerable<string> explicitColumns)
        {
            var result =
                explicitColumns?.Select(col => (col, col)) ??
                GetMappedProperties(modelType, saveAction).Select(pi => (pi.GetColumnName(), pi.Name));

            if (!result.Any()) throw new InvalidOperationException($"Model type {modelType.Name} must have at least one column to build SQL {saveAction} statement.");

            return result;
        }

        public static IEnumerable<PropertyInfo> GetMappedProperties(Type modelType, SaveAction saveAction)
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

                var calc = pi.GetCustomAttribute<CalculatedAttribute>();
                if (calc != null) return false;

                return true;
            };

            return modelType.GetProperties().Where(pi => isMapped(pi)).ToArray();
        }

        public static string ApplyDelimiter(string name, char startDelimiter, char endDelimiter) => 
            string.Join(".", name
                .Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(namePart => $"{startDelimiter}{namePart}{endDelimiter}"));        
    }
}
