using System.Data;
using System.Threading.Tasks;

namespace AO.DbSchema.Attributes.Interfaces
{
    /// <summary>
    /// Use this on model classes to convert key values to text equivalents.
    /// Used with logged change tracking in Dapper.CX.ChangeTracking
    /// </summary>
    public interface ITextLookup
    {
        Task<string> GetTextFromKeyAsync(IDbConnection connection, string propertyName, object keyValue);
    }
}
