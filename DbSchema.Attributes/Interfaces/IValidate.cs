using AO.Models.Models;
using System.Data;
using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    public interface IValidate
    {
        ValidateResult Validate();
        Task<ValidateResult> ValidateAsync(IDbConnection connection, IDbTransaction txn = null);        
    }
}
