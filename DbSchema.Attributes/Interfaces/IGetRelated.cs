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
        Func<IDbConnection, IDbTransaction, Task> OnGetAsync { get; }
    }
}
