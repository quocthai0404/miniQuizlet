using System.Net.Mail;
using System.Net;

namespace miniQuizlet.Services;

public class MailServiceImpl : IMailService
{
    private IConfiguration configuration;
    public MailServiceImpl(IConfiguration _configuration)
    {
        configuration = _configuration;
    }
    public bool Send(string from, string to, string subject, string body)
    {
        try
        {
            var host = configuration["Gmail:Host"];
            var port = int.Parse(configuration["Gmail:Port"]);
            var username = configuration["Gmail:Username"];
            var password = configuration["Gmail:Password"];
            var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);
            var stmpClient = new SmtpClient()
            {
                Host = host,
                Port = port,
                EnableSsl = enable,
                Credentials = new NetworkCredential(username, password)
            };
            MailAddress mailAddressFrom = new MailAddress(from);
            MailAddress mailAddressTo = new MailAddress(to);
            var mailMessage = new MailMessage(mailAddressFrom, mailAddressTo);
            //var mailMessage = new MailMessage(from, to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            stmpClient.Send(mailMessage);
            return true;
        }

        catch (Exception ex)
        {

            return false;

        }
    }
}
