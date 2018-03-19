using System.Collections.Generic;
using Itn.Shared.Transaction;
using OMSWeb.Provider;
using StructureMap;

namespace ItnOMS.Web.Managers
{
    public class TransactionManager
    {
        public ITransactionDataProvider DataProvider { get; set; }

        public TransactionManager() : this(ObjectFactory.GetInstance<ITransactionDataProvider>())
        {
        }

        public TransactionManager(ITransactionDataProvider dataProvider)
        {
            DataProvider = dataProvider;
        }

        public static TransactionManager Create()
        {
            return new TransactionManager();
        }
        public static TransactionManager Create(ITransactionDataProvider dataProvider)
        {
            return new TransactionManager(dataProvider);
        }

        public List<DiPurchaseOrderListItem> FetchPOList(PurchaseOrderFilter poFilter= null)
        {
           return  DataProvider.FetchPOList(poFilter);
        }

        public void PostPOModel(DiPurchaseOrder diPurchaseOrderModel)
        {
            DataProvider.PostPOModel(diPurchaseOrderModel);
        }

        public DiPurchaseOrder FetchPO(string ponbr)
        {
            return DataProvider.FetchPurchaseOrder(ponbr);
        }

        public void FulfillPO(DiPurchaseOrder diPurchaseOrderModel)
        {
            DataProvider.PostFulfillPO(diPurchaseOrderModel);
        }
    }
}