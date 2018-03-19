using Itn.OMS.Services.DAL.Contracts;
using Itn.Shared.Notification;
using Itn.Utilities;

namespace Itn.OMS.Services.DAL.Implementations
{
    public class MessageDataProvider : BaseRestProvider,IMessageProvider
    {
        public void SendEmail(DiEmailMessage message)
        {
            var route = AppConfig.GetConfigVal("Itn.Email.Post.Route");
            Post(route, message);
        }

        internal override string CoreBaseUri()
        {
            return AppConfig.GetConfigVal("Provider.Itn.Message.Services.EndPoint.Uri.Base");
        }
    }
}
