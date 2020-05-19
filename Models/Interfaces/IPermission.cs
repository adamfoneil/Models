using AO.Models.Enums;
using System.Data;
using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    /// <summary>
    /// use this for permission or role checks that don't involve tenant validation
    /// </summary>
    public interface IPermission
    {
        /// <summary>
        /// return true if the user has permission to get this row
        /// </summary>
        Task<bool> AllowGetAsync(IDbConnection connection, IUserBase user, IDbTransaction txn = null);

        /// <summary>
        /// return true if the user has permission to save this row
        /// </summary>
        Task<bool> AllowSaveAsync(IDbConnection connection, IUserBase user, SaveAction action, IDbTransaction txn = null);

        /// <summary>
        /// return true if the user has permission to delete this row
        /// </summary>
        Task<bool> AllowDeleteAsync(IDbConnection connection, IUserBase user, IDbTransaction txn = null);
    }
}
