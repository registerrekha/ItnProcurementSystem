using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using Itn.OMS.Services.DAL.Contracts;
using Itn.OMS.Services.Models;
using Itn.Shared.Transaction;
using Itn.Utilities;
using Itn.Utilities.Enums;

namespace Itn.OMS.Services.DAL.Implementations
{
    public class TransactionSqlDataProvider : ITransactionProvider
    {
        private const string DBKEY = "Itn.ProcurementDb.Connection";
        public List<SimplePurchaseOrder> FetchPurchaseOrdersList(PurchaseOrderFilter filter)
        {
            var result = new List<SimplePurchaseOrder>();
             
            var sb = new StringBuilder();
            sb.Append("SELECT  A.PONbr,A.CustomerNbr,C.Name as CustomerName,A.CreatedDateTime, ");
            sb.Append("A.CreatedBy,A.DeliveryDate,A.Status ");
            sb.Append(" FROM POHeader A JOIN Customer C ON A.CustomerNbr = C.CustomerNbr ");

            if (filter != null)
            {
                sb.Append(POfilter(filter));
            }
            var dr = SqlProvider.GetSqlDataReader(DBKEY, ConnectionStringType.DBKey,
                SqlParamStatement.Create(sb.ToString(),POFilterParams(filter)));
            try
            {
                while (dr.Read())
                {
                    result.Add(LoadFromDr(dr));
                }
                return result;
            }
            finally
            {
                dr.Close();
            }
        }

        private static string POfilter(PurchaseOrderFilter filter,bool startWithWhere = true)
        {
            var result = new StringBuilder();
            result.FilterAppender(filter.Email,"C.Email ", "email");
            result.FilterAppender(filter.Status, "Status", "status");
          
            var resultSql = result.ToString().TrimStart();
            if (startWithWhere)
            {
                // we need to remove the first "AND"
                resultSql = resultSql.ReplaceFirstInstance("AND", "WHERE");
            }
            return resultSql;
        }

        private List<SqlParameter> POFilterParams(PurchaseOrderFilter filter)
        {
            if (filter == null) return new List<SqlParameter>();

            return new List<SqlParameter>
            {
                SqlParamWrapper.Create("@email", filter.Email),
                SqlParamWrapper.Create("@status", filter.Status)
            };
        }

        private SimplePurchaseOrder LoadFromDr(SqlDataReader dr)
        {
            var simplePurchaseOrder = SimplePurchaseOrder.Create();
            simplePurchaseOrder.PoNbr = dr["PONbr"].ToString();
            simplePurchaseOrder.CustomerName = dr["CustomerName"].ToString();
            simplePurchaseOrder.CustomerNbr = dr["CustomerNbr"].ToString();
            simplePurchaseOrder.CreateDateTime = Convert.ToDateTime(dr["CreatedDateTime"].ToString());
            simplePurchaseOrder.CreatedBy = dr["CreatedBy"].ToString();
            simplePurchaseOrder.DeliveryDate = Convert.ToDateTime(dr["DeliveryDate"].ToString());
            simplePurchaseOrder.Status = EnumHelper.ToEnum<POStatusType>(dr["Status"].ToString());
     
            return simplePurchaseOrder;
         }

      

        public void SavePurchaseOrder(PurchaseOrder purchaseOrderModel)
        {
            InsertPOHeader(purchaseOrderModel);
            foreach (var line in purchaseOrderModel.Lines)
            {
                InsertPODetail(line, purchaseOrderModel.PONbr);
            }
        }

        public void UpdateInventory(PurchaseOrder purchaseOrderModel)
        {
            foreach (var line in purchaseOrderModel.Lines)
            {
                SqlProvider.ExecSqlNoQueryFromSp(DBKEY, ConnectionStringType.DBKey,
                    "usp_updateInventory",
                    SqlParamStatement.Create("exec usp_updateInventory @qtyFulfilled,@itemNbr",
                        new List<SqlParameter>
                        {
                            SqlParamWrapper.Create("@qtyFulfilled", line.QtyFulfilled),
                            SqlParamWrapper.Create("@itemNbr", line.ItemNbr)
                        }));
            }
        }

        public PurchaseOrder FetchPO(string poNbr)
        {
            var purchaseOrder = PurchaseOrder.Create();

            FetchHeader(purchaseOrder,poNbr);
            FetchLines(purchaseOrder,poNbr);

            return purchaseOrder;
        }

        public void UpdatePODetail(PurchaseOrder purchaseOrder)
        {
            foreach (var line in purchaseOrder.Lines)
            {
                var sb = new StringBuilder();
                sb.Append(" UPDATE PODetail Set QtyFulfilled = @qtyFulfilled WHERE ItemNbr = @itemNbr AND PONbr = @PONbr" );
                SqlProvider.ExecSqlNoQuery(DBKEY, ConnectionStringType.DBKey,
                    SqlParamStatement.Create(sb.ToString(), new List<SqlParameter>
                    {
                        SqlParamWrapper.Create("@qtyFulfilled", line.QtyFulfilled),
                        SqlParamWrapper.Create("@itemNbr", line.ItemNbr),
                        SqlParamWrapper.Create("@PONbr", purchaseOrder.PONbr)
                    }));
              
            }
        }

        public void UpdatePoStatus(string poNbr, POStatusType status)
        {
            var sb = new StringBuilder();
            sb.Append(" UPDATE POHeader Set [Status] = @status WHERE  PONbr = @PONbr");
            SqlProvider.ExecSqlNoQuery(DBKEY, ConnectionStringType.DBKey,
                SqlParamStatement.Create(sb.ToString(), new List<SqlParameter>
                {
                    SqlParamWrapper.Create("@status", status.ToString()),
                    SqlParamWrapper.Create("@PONbr", poNbr)

                }));
        }

        private void FetchLines(PurchaseOrder purchaseOrder, string poNbr)
        {
            
            var sb = new StringBuilder();
            sb.Append(" Select * from PODetail Where PONbr= @poNbr");
            var dr = SqlProvider.GetSqlDataReader(DBKEY, ConnectionStringType.DBKey,
                SqlParamStatement.Create(sb.ToString(), new List<SqlParameter>
                {
                    SqlParamWrapper.Create("@poNbr", poNbr)
                }));
            try
            {
                while (dr.Read())
                {
                    purchaseOrder.Lines.Add(PurchaseOrderLine.Create(dr["ItemNbr"].ToString(),
                        Convert.ToInt32(dr["QtyOrdered"].ToString()), Convert.ToInt32(dr["QtyFulfilled"].ToString())));
                }
            }
            finally
            {
                dr.Close();
            }

        }

     

        private void FetchHeader(PurchaseOrder purchaseOrder,string poNbr)
        {
            var sb = new StringBuilder();
            sb.Append("SELECT  A.PONbr,A.CustomerNbr,C.Name as CustomerName,A.CustomerEmail,A.CreatedDateTime, ");
            sb.Append("A.CreatedBy,A.DeliveryDate,A.Status ");
            sb.Append(" FROM POHeader A JOIN Customer C ON A.CustomerNbr = C.CustomerNbr ");
            sb.Append(" Where A.PONbr =@poNbr ");

            var dr = SqlProvider.GetSqlDataReader(DBKEY, ConnectionStringType.DBKey,
                SqlParamStatement.Create(sb.ToString(), new List<SqlParameter>
                {
                    SqlParamWrapper.Create("@poNbr", poNbr)
                }));
            try
            {
                while (dr.Read())
                {
                    LoadFromDr(dr, purchaseOrder);
                }
            }
            finally
            {
                dr.Close();
            }

        }

        private void LoadFromDr(SqlDataReader dr, PurchaseOrder purchaseOrder)
        {
           
            purchaseOrder.PONbr = dr["PONbr"].ToString();
            purchaseOrder.CustomerNbr = dr["CustomerName"].ToString();
            purchaseOrder.CustomerEmail = dr["CustomerEmail"].ToString();
            purchaseOrder.CreatedDateTime = Convert.ToDateTime(dr["CreatedDateTime"].ToString());
            purchaseOrder.CreatedBy = dr["CreatedBy"].ToString();
            purchaseOrder.DeliveryDate = Convert.ToDateTime(dr["DeliveryDate"].ToString());
            purchaseOrder.Status = EnumHelper.ToEnum<POStatusType>(dr["Status"].ToString());

        }

        private void InsertPODetail(PurchaseOrderLine line, string poNbr)
        {
            var sb = new StringBuilder();
            sb.Append(" INSERT INTO PODetail (PONbr,ItemNbr,QtyOrdered,QtyFulfilled) ");
            sb.Append("   VALUES (@PONbr, @ItemNbr, @Qty,@QtyFulfilled) ");

            var id = SqlProvider.ExecSqlNoQueryWithIdentity(DBKEY, ConnectionStringType.DBKey,
                SqlParamStatement.Create(sb.ToString(),
                    new List<SqlParameter>
                    {
                        SqlParamWrapper.Create("@PONbr", poNbr),
                        SqlParamWrapper.Create("@ItemNbr", line.ItemNbr),
                        SqlParamWrapper.Create("@Qty", line.QtyOrdered),
                        SqlParamWrapper.Create("@QtyFulfilled", line.QtyFulfilled)
                    }));
        }

        private void InsertPOHeader(PurchaseOrder purchaseOrderModel)
        {

            var sb = new StringBuilder();
            sb.Append(
                " INSERT  INTO POHeader (PONbr, CustomerNbr ,CreatedDateTime,CreatedBy,DeliveryDate,Status,CustomerEmail)");

            sb.Append(
                " VALUES   (@PONbr,@CustomerNbr,@CreatedDateTime, @CreatedBy,@DeliveryDate,@Status,@email ) ");

            var id = SqlProvider.ExecSqlNoQueryWithIdentity(DBKEY, ConnectionStringType.DBKey,
                SqlParamStatement.Create(sb.ToString(),
                    new List<SqlParameter>
                    {
                        SqlParamWrapper.Create("@PONbr", purchaseOrderModel.PONbr),
                        SqlParamWrapper.Create("@CustomerNbr", purchaseOrderModel.CustomerNbr),
                        SqlParamWrapper.Create("@CreatedDateTime", DateTime.Now),
                        SqlParamWrapper.Create("@CreatedBy",purchaseOrderModel.CreatedBy),
                        SqlParamWrapper.Create("@DeliveryDate", purchaseOrderModel.DeliveryDate),
                        SqlParamWrapper.Create("@Status",POStatusType.Open.ToString()),
                        SqlParamWrapper.Create("@email",purchaseOrderModel.CustomerEmail)
                    }));
        }
    }
}
