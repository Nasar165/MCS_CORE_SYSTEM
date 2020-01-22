namespace Components.Mail.Interface
{
    public interface ISmtpClient
    {
        void SendMail(string subject, string from, string message, bool IsHtml, params string[] to);
        void SendMailAsync(string subject, string from, string message, bool IsHtml, params string[] to);
    }
}