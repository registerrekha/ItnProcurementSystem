using OMSWeb.Provider;

namespace ItnOMS.Web.Registry
{
    public class BaseServicesRegistry : StructureMap.Configuration.DSL.Registry
    {
        public BaseServicesRegistry()
        {

            For(typeof(ITransactionDataProvider)).Use(typeof(TransactionRestDataProvider));

        }
    }
}