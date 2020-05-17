using AO.DbSchema.Enums;
using System.Data;
using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    public interface ITrigger
    {
        Task RowSavedAsync(IDbConnection connection, SaveAction saveAction);
        Task RowDeletedAsync(IDbConnection connection);
    }
}
