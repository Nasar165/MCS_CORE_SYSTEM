namespace api.Middleware.Constants
{
    public static class XSSProtectionOptions
    {
        public static readonly string Header = "X-XSS-Protection";
        public static readonly string Mode1Block = "1; mode=block";
    }
}