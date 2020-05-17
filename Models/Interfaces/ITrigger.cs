using AO.Models.Enums;
using System.Data;
using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    /// <summary>
    /// use much as you would SQL Server triggers, but recommend limit actions to be database-specific.
    /// For example, don't send email nor call external services
    /// </summary>
    public interface ITrigger
    {
        Task RowSavedAsync(IDbConnection connection, SaveAction saveAction, IDbTransaction txn = null);
        Task RowDeletedAsync(IDbConnection connection, IDbTransaction txn = null);
    }
}
