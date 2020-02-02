namespace AO.DbSchema.Attributes.Models
{
    public class ValidateResult
    {
        public ValidateResult()
        {
            IsValid = true;
        }

        public ValidateResult(string message)
        {
            IsValid = false;
            Message = message;
        }

        /// <summary>
        /// is the model instance valid to save?
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// if it's not valid, why not?
        /// </summary>
        public string Message { get; set; }
    }
}
