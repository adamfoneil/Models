using AO.Models.Enums;
using System.Data;
using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    /// <summary>
    /// use much as you would SQL Server triggers
    /// </summary>
    public interface IDbTrigger
    {
        Task RowSavingAsync(IDbConnection connection, SaveAction saveAction, IDbTransaction txn = null, IUserBase user = null);
        Task RowSavedAsync(IDbConnection connection, SaveAction saveAction, IDbTransaction txn = null, IUserBase user = null);
        Task RowDeletedAsync(IDbConnection connection, IDbTransaction txn = null, IUserBase user = null);
    }
}
