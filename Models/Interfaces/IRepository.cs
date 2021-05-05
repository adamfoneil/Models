using System;
using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    public interface IRepository<TKey, TUser> where TUser : IUserBase
    {
        Task<object> GetAsync(Type type, TUser user, TKey id);
        Task<object> SaveAsync(Type type, TUser user, object model);
        Task DeleteAsync(Type type, TUser user, TKey id);
    }
}
