namespace Components.Mail.Interface
{
    public interface ISmtpClient
    {
        void SendMail(MailDetails mail, params string[] to);
        void SendMailAsync(MailDetails mail, params string[] to);
    }
}