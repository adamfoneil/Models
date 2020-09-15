using AO.Models.Enums;
using System.Data;
using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    /// <summary>
    /// use much as you would SQL Server triggers, but recommend limit actions to be database-internal.
    /// For example, don't send email nor call external services because of the depdendencies you'd have to add
    /// </summary>
    public interface ITrigger
    {
        Task RowSavingAsync(IDbConnection connection, SaveAction saveAction, IDbTransaction txn = null, IUserBase user = null);
        Task RowSavedAsync(IDbConnection connection, SaveAction saveAction, IDbTransaction txn = null, IUserBase user = null);
        Task RowDeletedAsync(IDbConnection connection, IDbTransaction txn = null, IUserBase user = null);
    }
}
