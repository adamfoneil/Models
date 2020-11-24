using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    /// <summary>
    /// Use this on model classes to convert key values to text equivalents.
    /// Used with logged change tracking in Dapper.CX.ChangeTracking
    /// </summary>
    public interface IDbTextLookup
    {
        /// <summary>
        /// what model properties support text value lookup?
        /// </summary>
        IEnumerable<string> GetLookupProperties();

        /// <summary>
        /// gets the text for a given key value on a given property
        /// </summary>
        Task<string> GetTextFromKeyAsync(IDbConnection connection, IDbTransaction transaction, string propertyName, object keyValue);
    }
}
