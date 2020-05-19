using System;

namespace AO.Models.Interfaces
{
    public interface IUserBase
    {
        string Name { get; }
        DateTime LocalTime { get; }        
    }

    /// <summary>
    /// general-purpose way to describe an authenticated user at the model level
    /// </summary>
    /// <typeparam name="T">Type of TenantId (in a multi-tenant system) and the User Id</typeparam>
    public interface IUser<T> : IUserBase
    {
        T TenantId { get; }
        T Id { get; }
    }
}
