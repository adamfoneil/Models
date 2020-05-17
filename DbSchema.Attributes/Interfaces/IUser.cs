using System;

namespace AO.Models.Interfaces
{
    /// <summary>
    /// general-purpose way to describe an authenticated user
    /// </summary>
    /// <typeparam name="T">Type of TenantId (in a multi-tenant system) and the User Id</typeparam>
    public interface IUser<T>
    {
        T TenantId { get; }
        string Name { get; }
        DateTime LocalTime { get; }
        T Id { get; }
    }
}
