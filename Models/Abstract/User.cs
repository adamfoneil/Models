using AO.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace AO.Models.Abstract
{
    /// <summary>
    /// implenment in your application to provide a way to take advantage of IPermission and IAudit
    /// </summary>
    /// <typeparam name="T">user Id and Tenant Id type</typeparam>
    public abstract class User<T> : IUser<T>
    {
        public User(IPrincipal principal, IEnumerable<Claim> claims)
        {
            Principal = principal;
            Claims = claims;
            Initialize();
        }

        protected IPrincipal Principal { get;  }
        protected IEnumerable<Claim> Claims { get; }

        /// <summary>
        /// use this to inspect the Principal and Claims to determine IUser properties
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// implement this to get the user's current local time.
        /// For example, this assumes that you capture the user's time zone during the Initialize method
        /// </summary>        
        protected abstract DateTime LocalTimeImplementation();

        public DateTime LocalTime => LocalTimeImplementation();
        public T TenantId { get; private set; }
        public string Name { get; private set; }        
        public T Id { get; private set; }
    }
}
