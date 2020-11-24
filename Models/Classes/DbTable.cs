namespace AO.Models.Classes
{
    /// <summary>
    /// extension point for IDb* interface implementations that apply to a model class.
    /// This would be passed to Dapper.CX methods as optional arg. 
    /// </summary>    
    public class DbTable<TModel>
    {
        public DbTable(TModel model)
        {
            Model = model;
        }

        public TModel Model { get; }
    }
}
