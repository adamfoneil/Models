using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    public interface ITenant<T>
    {
        Task<T> GetTenantId();
    }
}
