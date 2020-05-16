using System.Data;
using System.Threading.Tasks;

namespace AO.DbSchema.Attributes.Interfaces
{
    public interface IPermission
    {
        Task<bool> AllowGetAsync<T>(IDbConnection connection, IUser<T> user);
        Task<bool> AllowSaveAsync<T>(IDbConnection connection, IUser<T> user);
        Task<bool> AllowDeleteAsync<T>(IDbConnection connection, IUser<T> user);
    }
}
