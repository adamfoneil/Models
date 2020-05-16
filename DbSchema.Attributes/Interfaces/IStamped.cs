using AO.DbSchema.Enums;

namespace AO.DbSchema.Attributes.Interfaces
{
    public interface IStamped
    {
        /// <summary>
        /// Called right before a row is updated or inserted.
        /// Use this to record any audit tracking or time stamp data on a row
        /// </summary>
        void Stamp<T>(SaveAction saveAction, IUser<T> user);
    }
}
