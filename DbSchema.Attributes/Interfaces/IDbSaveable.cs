using System.Data;
using System.Threading.Tasks;

namespace AO.DbSchema.Attributes.Interfaces
{
    /// <summary>
    /// use this to make anything able to be saved in a database.    
    /// </summary>
    public interface IDbSaveable
    {
        Task SaveAsync(IDbConnection connection);
    }
}
