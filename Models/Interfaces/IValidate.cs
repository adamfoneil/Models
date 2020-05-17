using System.Data;
using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    /// <summary>
    /// use this to validate properties on a model, not intended for permission checks    
    /// </summary>
    public interface IValidate
    {
        ValidateResult Validate();
        Task<ValidateResult> ValidateAsync(IDbConnection connection, IDbTransaction txn = null);
    }
}
