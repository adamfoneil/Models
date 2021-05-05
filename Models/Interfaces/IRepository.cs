using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    public interface IRepository<TKey, TUser> where TUser : IUserBase
    {
        Task<TModel> GetAsync<TModel>(TUser user, TKey id) where TModel : IModel<TKey>;
        Task<TModel> SaveAsync<TModel>(TUser user, TModel model) where TModel : IModel<TKey>;
        Task DeleteAsync<TModel>(TUser user, TKey id) where TModel : IModel<TKey>;
    }
}
