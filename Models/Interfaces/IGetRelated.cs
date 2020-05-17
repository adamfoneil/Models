using System;
using System.Data;
using System.Threading.Tasks;

namespace AO.DbSchema.Interfaces
{
    public interface IGetRelated
    {
        /// <summary>
        /// perform any additional Gets that are related to this model
        /// </summary>        
        Task GetRelatedAsync(IDbConnection connection, IDbTransaction txn = null);
    }
}
