using Itn.OMS.Services.DAL.Contracts;
using Itn.OMS.Services.DAL.Implementations;

namespace Itn.Tests.OMS.Manager.Registry
{
    public class BaseServicesRegistry : StructureMap.Configuration.DSL.Registry
    {
        public BaseServicesRegistry()
        {

            For(typeof(ITransactionProvider)).Use(typeof(TransactionSqlDataProvider));
            For(typeof(IMessageProvider)).Use(typeof(MessageDataProvider));
        }
    }
}