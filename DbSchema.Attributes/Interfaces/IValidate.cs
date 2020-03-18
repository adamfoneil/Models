using AO.DbSchema.Attributes.Models;
using System.Data;
using System.Threading.Tasks;

namespace AO.DbSchema.Attributes.Interfaces
{
    public interface IValidate
    {
        ValidateResult Validate();
        Task<ValidateResult> ValidateAsync(IDbConnection connection, IDbTransaction txn = null);        
    }
}
