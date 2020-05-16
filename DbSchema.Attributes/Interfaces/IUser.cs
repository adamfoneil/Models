using System;

namespace AO.DbSchema.Attributes.Interfaces
{
    /// <summary>
    /// general-purpose way to describe an authenticated user
    /// </summary>
    /// <typeparam name="T">Type of TenantId in a multi-tenant system</typeparam>
    public interface IUser<T>
    {
        T TenantId { get; }
        string Name { get; }
        DateTime LocalTime { get; }
        T Id { get; }
    }
}
