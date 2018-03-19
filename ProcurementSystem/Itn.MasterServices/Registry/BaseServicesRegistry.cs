using Itn.MasterDataServices.DAL.Contracts;
using Itn.MasterDataServices.DAL.Implementations;

namespace Itn.MasterServices.Registry
{
    public class BaseServicesRegistry : StructureMap.Configuration.DSL.Registry
    {
        public BaseServicesRegistry()
        {
          
            For(typeof(IMasterDataProvider)).Use(typeof(MasterDataSqlDataProvider));
           
        }
    }
}