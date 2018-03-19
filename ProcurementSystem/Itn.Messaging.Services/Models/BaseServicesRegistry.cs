namespace Itn.Messaging.Services.Models
{
    public class BaseServicesRegistry : StructureMap.Configuration.DSL.Registry
    {
        public BaseServicesRegistry()
        {

            For(typeof(IMessageProvider)).Use(typeof(EmailMessageProvider));

        }
    }
}
