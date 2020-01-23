using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Components.Errorhandler;
using Components.Mail.Interface;

namespace Components.Mail
{
    public class Smtp : ISmtpClient
    {
        private SmtpClient SmtpSender { get; }
        public Smtp(IServerConfig mailServer)
        {
            SmtpSender = CreateSmtpClient(mailServer);
        }

        private SmtpClient CreateSmtpClient(IServerConfig mailServer)
        {
            return new SmtpClient()
            {
                Host = mailServer.Host,
                Port = mailServer.Port,
                Credentials = mailServer.UserAccount,
                EnableSsl = mailServer.SSL,
                UseDefaultCredentials = mailServer.DefaultCredentials
            };
        }

        private void AddRecipiantToMailMessage(MailMessage message, string recipiant)
            => message.To.Add(recipiant);

        private ParallelLoopResult AddRecipiants(MailMessage message, params string[] recipiants)
            => Parallel.ForEach(recipiants, recipiant
                => AddRecipiantToMailMessage(message, recipiant));

        private MailMessage GenerateMailMessage(string subject, string from, string message, bool IsHtml)
        {
            return new MailMessage()
            {
                IsBodyHtml = IsHtml,
                Subject = subject,
                From = new MailAddress(from),
                Body = message,
            };
        }

        public void SendMail(string subject, string from, string message, bool IsHtml, params string[] to)
        {
            try
            {
                var mailMessage = GenerateMailMessage(subject, from, message, IsHtml);
                var result = AddRecipiants(mailMessage, to);
                SmtpSender.Send(mailMessage);
            }
            catch (Exception error)
            {
                ErrorLogger.Instance.LogError(error);
                throw;
            }
        }

        public async void SendMailAsync(string subject, string from, string message, bool IsHtml, params string[] to)
        {
            try
            {
                await Task.Run(() => { SendMail(subject, from, message, IsHtml, to); });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}