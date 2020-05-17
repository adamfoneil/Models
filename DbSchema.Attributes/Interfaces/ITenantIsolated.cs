using System.Data;
using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    /// <summary>
    /// describes a model that is partitioned into different tenants that must maintain isolation
    /// </summary>  
    public interface ITenantIsolated<T>
    {
        /// <summary>
        /// this can be an ordinary property value, but can also be a database query through a join
        /// </summary>
        Task<T> GetTenantIdAsync(IDbConnection connection, IDbTransaction txn = null);

        /// <summary>
        /// called whenever Crud actions are performed on a model to verify that the user has permission
        /// </summary>
        Task<bool> IsValidTenantAsync(IDbConnection connection, IUser<T> user, IDbTransaction txn = null);
    }
}
