using System.Threading.Tasks;

namespace AO.DbSchema.Attributes.Interfaces
{
    public interface ITenant<T>
    {
        Task<T> GetTenantId();
    }
}
