using System.Data;
using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    /// <summary>
    /// describes a model that is partitioned into different tenants that must maintain isolation
    /// </summary>  
    public interface ITenantIsolated<T>
    {
        Task<T> GetTenantIdAsync(IDbConnection connection, IDbTransaction txn = null);

        Task<bool> IsValidTenantAsync(IDbConnection connection, IUser<T> user, IDbTransaction txn = null);
    }
}
