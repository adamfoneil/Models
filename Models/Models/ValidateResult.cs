using System.Threading.Tasks;

namespace AO.Models
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

        /// <summary>
        /// shorthand method for indicating validation success
        /// </summary>        
        public static ValidateResult Ok() => new ValidateResult() { IsValid = true };

        /// <summary>
        /// shorthand method to indicate failure
        /// </summary>
        public static ValidateResult Failed(string message) => new ValidateResult() { IsValid = false, Message = message };

        /// <summary>
        /// shorthand async method for indicating validation success
        /// </summary>        
        public static async Task<ValidateResult> OkAsync() => await Task.FromResult(Ok());

        /// <summary>
        /// shorthand async method to indicate failure
        /// </summary>
        /// <param name="message"></param>        
        public static async Task<ValidateResult> FailedAsync(string message) => await Task.FromResult(Failed(message));
    }
}
