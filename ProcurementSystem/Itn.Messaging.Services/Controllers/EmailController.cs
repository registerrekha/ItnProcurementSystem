using System.Web.Http;
using Itn.Messaging.Services.Models;
using Itn.Shared.Notification;
using StructureMap;

namespace Itn.Messaging.Services.Controllers
{
    [RoutePrefix("api/acct/email")]
    public class EmailController : ApiController
    {
        public IMessageProvider DataProvider { get; set; }

        [Route("msg", Name = "PostEmailMsg")]
     
        public IHttpActionResult PostEmailMsg([FromBody] DiEmailMessage model)
        {
            DataProvider = (ObjectFactory.GetInstance<IMessageProvider>());
            DataProvider.SendMessage(model);
            return Ok();
        }
    }
}
