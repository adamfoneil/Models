using AO.Models.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace AO.Models.Extensions
{
    public static class ITenantIsolatedExtensions
    {
        /// <summary>
        /// provides a standard implementation of <see cref="ITenantIsolated{T}.IsValidTenantAsync(IDbConnection, IUser{T}, IDbTransaction)"/>
        /// </summary>
        public static async Task<bool> IsValidTenantAsync<T>(this ITenantIsolated<T> model, IDbConnection connection, IUser<T> user, IDbTransaction txn = null)
        {
            var tenantId = await model.GetTenantIdAsync(connection, txn);
            return user.TenantId.Equals(tenantId);
        }
    }
}
