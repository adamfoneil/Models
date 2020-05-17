using System.Data;
using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    public interface IGetRelated
    {
        /// <summary>
        /// perform any additional Gets that are related to this model
        /// </summary>        
        Task GetRelatedAsync(IDbConnection connection, IDbTransaction txn = null);
    }
}
