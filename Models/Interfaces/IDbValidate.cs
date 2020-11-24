using System.Data;
using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    public interface IDbValidate
    {
        Task<ValidateResult> ValidateAsync(IDbConnection connection, IDbTransaction txn = null);
    }
}
