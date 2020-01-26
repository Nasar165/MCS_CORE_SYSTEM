namespace api.Models.Error
{
    public class CustomError
    {
        public string Error { get; }

        public CustomError(string message)
        {
            Error = message;
        }
    }
}