using System.Collections.Generic;
using System.Linq;

namespace AO.Models.Static
{
    public static partial class SqlBuilder
    {
        public static string Insert(string tableName, IEnumerable<string> columnNames, char startDelimiter = '[', char endDelimiter = ']') =>
            $@"INSERT INTO {ApplyDelimiter(tableName, startDelimiter, endDelimiter)} (
                {string.Join(", ", columnNames.Select(col => ApplyDelimiter(col, startDelimiter, endDelimiter)))}
            ) VALUES (
                {string.Join(", ", columnNames.Select(col => "@" + col))}
            );";

        public static string Update(string tableName, IEnumerable<string> columnNames, char startDelimiter = '[', char endDelimiter = ']', string identityColumn = "Id") =>
            $@"UPDATE {ApplyDelimiter(tableName, startDelimiter, endDelimiter)} SET
                {string.Join(", ", columnNames.Select(col => $"{ApplyDelimiter(col, startDelimiter, endDelimiter)}=@{col}"))}
            WHERE
                {ApplyDelimiter(identityColumn, startDelimiter, endDelimiter)}=@{identityColumn};";
        
    }
}
