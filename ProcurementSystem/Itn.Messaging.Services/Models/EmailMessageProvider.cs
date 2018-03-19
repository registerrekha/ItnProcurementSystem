using Itn.Shared.Notification;
using System.Net.Mail;
using System.Text;
using Itn.Utilities;

namespace Itn.Messaging.Services.Models
{
    public class EmailMessageProvider : IMessageProvider
    {
        public void SendMessage(DiEmailMessage message)
        {

            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = AppConfig.GetConfigVal("Email.Ews.Uri");
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(AppConfig.GetConfigVal("Email.From"), AppConfig.GetConfigVal("Email.Password"));
            message.From = AppConfig.GetConfigVal("Email.From");
            MailMessage mm = new MailMessage(message.From, message.Recipients[0], message.Subject, message.Body);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }
    }
}