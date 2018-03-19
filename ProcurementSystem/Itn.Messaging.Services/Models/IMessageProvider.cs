using Itn.Shared.Notification;

namespace Itn.Messaging.Services.Models
{
    public interface IMessageProvider
    {
       void  SendMessage(DiEmailMessage message);
    }
}