using System;

namespace AO.Models.Interfaces
{
    public interface IUserBase
    {
        string Name { get; }
        DateTime LocalTime { get; }        
    }
}
