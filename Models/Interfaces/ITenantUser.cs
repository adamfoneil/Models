namespace AO.Models.Interfaces
{
    /// <summary>
    /// general-purpose way to describe an authenticated user at the model level
    /// </summary>
    /// <typeparam name="T">Type of TenantId (in a multi-tenant system) and the User Id</typeparam>
    public interface ITenantUser<T> : IUserBase
    {
        T TenantId { get; }
    }
}
