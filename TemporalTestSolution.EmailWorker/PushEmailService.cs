namespace TemporalTestSolution.EmailWorker;

using System.Net;
using System.Net.Mail;
using Temporalio.Activities;
using TemporalTestSolution.ActivityContracts;

public class PushEmailService : IPushService
{
    public PushEmailService(EmailConfiguration emailConfiguration)
    {
        this.emailConfiguration = emailConfiguration;
    }
    private static int i = 0;
    private readonly EmailConfiguration emailConfiguration;

    [Activity]
    public string NotifyEmail(string email, string content)
    {
        //retry
        // if (i < 2)
        // {
        //     i++;
        //     throw new Exception("could not send the email");
        // }

        var fromAddress = new MailAddress(emailConfiguration.From, emailConfiguration.From);
        var toAddress = new MailAddress(emailConfiguration.To, emailConfiguration.To);

        string fromPassword = emailConfiguration.Password;

        const string subject = "subject";
        const string body = "Body";

        var smtp = new SmtpClient
        {
            Host = emailConfiguration.Host,
            Port = emailConfiguration.Port,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };

        using var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        };

        smtp.Send(message);

        return "Done";
    }

    public string NotifySlack(string slackId, string content) => throw new NotImplementedException();
}