using System.Data;
using System.Threading.Tasks;

namespace AO.DbSchema.Attributes.Interfaces
{
    public interface IValidate<TModel>
    {
        bool IsValid(TModel model);
        Task<bool> IsValidAsync(IDbConnection connection, TModel model);
    }
}
