using System;
using System.Data;
using System.Threading.Tasks;

namespace AO.DbSchema.Interfaces
{
    public interface IGetRelated<TModel>
    {
        /// <summary>
        /// perform any additional Gets that are related to this model
        /// </summary>        
        Func<IDbConnection, TModel, Task> OnGetAsync { get; }
    }
}
