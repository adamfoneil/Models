using AO.Models.Enums;
using System.Data;
using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    /// <summary>
    /// use this for permission checks that don't involve tenant validation
    /// </summary>
    public interface IPermission
    {
        /// <summary>
        /// return true if the user has permission to get this row
        /// </summary>
        Task<bool> AllowGetAsync<T>(IDbConnection connection, IUser<T> user, IDbTransaction txn = null);

        /// <summary>
        /// return true if the user has permission to save this row
        /// </summary>
        Task<bool> AllowSaveAsync<T>(IDbConnection connection, IUser<T> user, SaveAction action, IDbTransaction txn = null);

        /// <summary>
        /// return true if the user has permission to delete this row
        /// </summary>
        Task<bool> AllowDeleteAsync<T>(IDbConnection connection, IUser<T> user, IDbTransaction txn = null);
    }
}
