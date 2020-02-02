using AO.DbSchema.Attributes.Models;
using System.Data;
using System.Threading.Tasks;

namespace AO.DbSchema.Attributes.Interfaces
{
    public interface IValidate<TModel>
    {
        ValidationResult Validate();
        Task<ValidationResult> ValidateAsync(IDbConnection connection);        
    }
}
