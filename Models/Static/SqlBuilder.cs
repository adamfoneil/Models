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

        public static string Get(Type modelType, char startDelimiter = '[', char endDelimiter = ']', string identityColumn = null)
        {
            string identityCol = identityColumn ?? modelType.GetIdentityName();
            return $"SELECT * FROM {TableName(modelType, startDelimiter, endDelimiter)} WHERE {ApplyDelimiter(identityCol, startDelimiter, endDelimiter)}=@{identityCol}";
        }            

        public static string Get<T>(char startDelimiter = '[', char endDelimiter = ']', string identityColumn = null) =>
            Get(typeof(T), startDelimiter, endDelimiter, identityColumn);

        public static string GetWhere(Type modelType, object criteria, char startDelimiter = '[', char endDelimiter = ']')
        {
            var columns = criteria.GetType().GetProperties().Where(p => SupportedTypes.Contains(p.PropertyType)).Select(p => p.Name);
            return GetWhere(modelType, columns, startDelimiter, endDelimiter);
        }

        public static string GetWhere<T>(object criteria, char startDelimiter = '[', char endDelimiter = ']') =>
            GetWhere(typeof(T), criteria, startDelimiter, endDelimiter);

        public static string GetWhere(Type modelType, IEnumerable<string> whereColumns, char startDelimiter = '[', char endDelimiter = ']') =>
            $"SELECT * FROM {TableName(modelType, startDelimiter, endDelimiter)} WHERE {string.Join(" AND ", whereColumns.Select(col => $"{ApplyDelimiter(col, startDelimiter, endDelimiter)}=@{col}"))}";

        public static string Insert(Type modelType, IEnumerable<string> columnNames = null, char startDelimiter = '[', char endDelimiter = ']', string identityColumn = null, string tableName = null, Func<PropertyInfo, bool> propertiesWhere = null)
        {
            var columns = GetColumns(modelType, SaveAction.Insert, columnNames, identityColumn, propertiesWhere);

            return
                $@"INSERT INTO {ApplyDelimiter(tableName, startDelimiter, endDelimiter) ?? TableName(modelType, startDelimiter, endDelimiter)} (
                    {string.Join(", ", columns.Select(col => ApplyDelimiter(col.columnName, startDelimiter, endDelimiter)))}
                ) VALUES (
                    {string.Join(", ", columns.Select(col => "@" + col.parameterName))}
                );";
        }

        public static string Insert<T>(IEnumerable<string> columnNames = null, char startDelimiter = '[', char endDelimiter = ']', string identityColumn = null, string tableName = null, Func<PropertyInfo, bool> propertiesWhere = null) =>
            Insert(typeof(T), columnNames, startDelimiter, endDelimiter, identityColumn, tableName, propertiesWhere);

        public static string Update(Type modelType, IEnumerable<string> columnNames = null, char startDelimiter = '[', char endDelimiter = ']', string identityColumn = null, string identityParam = null, string tableName = null, Func<PropertyInfo, bool> propertiesWhere = null)
        {
            var columns = GetColumns(modelType, SaveAction.Update, columnNames, propertiesWhere: propertiesWhere);
            
            string identityCol = identityColumn ?? modelType.GetIdentityName();

            columns = columns.Except(new (string, string)[]
            {
                (identityCol, identityParam ?? identityCol)
            });

            return
                $@"UPDATE {ApplyDelimiter(tableName, startDelimiter, endDelimiter) ?? TableName(modelType, startDelimiter, endDelimiter)} SET 
                    {string.Join(", ", columns.Select(col => $"{ApplyDelimiter(col.columnName, startDelimiter, endDelimiter)}=@{col.parameterName}"))} 
                WHERE 
                    {ApplyDelimiter(identityCol, startDelimiter, endDelimiter)}=@{identityParam ?? identityCol}";
        }

        public static string Update<T>(IEnumerable<string> columnNames = null, char startDelimiter = '[', char endDelimiter = ']', 
            string identityColumn = null, string identityParam = null, string tableName = null, Func<PropertyInfo, bool> propertiesWhere = null) => 
            Update(typeof(T), columnNames, startDelimiter, endDelimiter, identityColumn, identityParam, tableName, propertiesWhere);

        public static string Delete(Type modelType, char startDelimiter = '[', char endDelimiter = ']', string identityColumn = null, string tableName = null) =>
            $@"DELETE {ApplyDelimiter(tableName, startDelimiter, endDelimiter) ?? TableName(modelType, startDelimiter, endDelimiter)} WHERE {ApplyDelimiter(identityColumn, startDelimiter, endDelimiter) ?? ApplyDelimiter(modelType.GetIdentityName(), startDelimiter, endDelimiter)}=@id";

        public static string Delete<T>(char startDelimiter = '[', char endDelimiter = ']', string identityColumn = null, string tableName = null) => Delete(typeof(T), startDelimiter, endDelimiter, identityColumn, tableName);

        private static IEnumerable<(string columnName, string parameterName)> GetColumns(
            Type modelType, SaveAction saveAction, IEnumerable<string> explicitColumns, 
            string identityColumn = null, Func<PropertyInfo, bool> propertiesWhere = null)
        {
            var result =
                explicitColumns?.Select(col => (col, col)) ??
                GetMappedProperties(modelType, saveAction, identityColumn)
                .Where(pi => propertiesWhere?.Invoke(pi) ?? true)
                .Select(pi => (pi.GetColumnName(), pi.Name));

            if (!result.Any()) throw new InvalidOperationException($"Model type {modelType.Name} must have at least one column to build SQL {saveAction} statement.");

            return result;
        }

        public static IEnumerable<PropertyInfo> GetMappedProperties(Type modelType, SaveAction saveAction, string identityColumn = null)
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
                if (!string.IsNullOrEmpty(identityColumn) && pi.Name.Equals(identityColumn)) return false;
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

        private static string TableName(Type modelType, char startDelimiter, char endDelimiter) =>
            ApplyDelimiter(modelType.GetTableName(), startDelimiter, endDelimiter);

        public static string ApplyDelimiter(string name, char startDelimiter, char endDelimiter) => 
            (!string.IsNullOrEmpty(name)) ? string.Join(".", name
                .Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(namePart => $"{startDelimiter}{namePart}{endDelimiter}")) : null;        
    }
}
