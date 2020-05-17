using AO.DbSchema.Enums;
using System.Data;
using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    public interface IPermission
    {
        Task<bool> AllowGetAsync<T>(IDbConnection connection, IUser<T> user);
        Task<bool> AllowSaveAsync<T>(IDbConnection connection, IUser<T> user, SaveAction action);
        Task<bool> AllowDeleteAsync<T>(IDbConnection connection, IUser<T> user);
    }
}
