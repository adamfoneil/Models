using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AO.Models.Interfaces
{
    /// <summary>
    /// describes an object that provides SQL statements that create objects
    /// </summary>
    public interface ISqlObjectCreator
    {
        Task<IEnumerable<string>> GetStatementsAsync(IDbConnection connection, IEnumerable<Type> types);
    }
}
