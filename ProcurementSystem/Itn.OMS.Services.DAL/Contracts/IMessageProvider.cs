
using Itn.Shared.Notification;

namespace Itn.OMS.Services.DAL.Contracts
{
    public interface IMessageProvider
    {
       void SendEmail(DiEmailMessage message);
    }
}
