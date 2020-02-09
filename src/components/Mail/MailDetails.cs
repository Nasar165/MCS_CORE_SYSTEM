namespace Components.Mail
{
    public class MailDetails
    {
        public string Subject { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
        public bool IsHtml { get; set; } = false;
    }
}