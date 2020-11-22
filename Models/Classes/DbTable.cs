namespace AO.Models.Classes
{
    /// <summary>
    /// extension point for table-level behaviors (stamping, validation, triggers) that apply to a model class.
    /// This would be passed to Dapper.CX methods as optional arg. Implement existing interfaces here as you want:
    /// IAudit, ITrigger, IGetRelated, IValidate, etc. This moves table-level, business layer stuff out of model layer
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
