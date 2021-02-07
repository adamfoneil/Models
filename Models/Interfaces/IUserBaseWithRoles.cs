using System.Collections.Generic;

namespace AO.Models.Interfaces
{
    public interface IUserBaseWithRoles : IUserBase
    {
        HashSet<string> Roles { get; }
    }
}
