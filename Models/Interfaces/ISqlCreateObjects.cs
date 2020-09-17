using System;
using System.Collections.Generic;

namespace AO.Models.Interfaces
{
    /// <summary>
    /// describes an object that provides SQL statements
    /// </summary>
    public interface ISqlCreateObjects
    {
        IEnumerable<string> GetStatements(IEnumerable<Type> types);
    }
}
