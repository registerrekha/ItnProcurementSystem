using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Itn.MasterDataServices.Models;
using Itn.Utilities;
using Itn.MasterDataServices.DAL.Contracts;
using Itn.Utilities.Enums;
using Itn.Utilities.Exceptions;

namespace Itn.MasterDataServices.DAL.Implementations
{
    public class MasterDataSqlDataProvider : IMasterDataProvider
    {
        public List<Customer> FetchCustomers()
        {
            var result = new List<Customer>();
            var sb = new StringBuilder();
            sb.Append("Select * from Customer");
            var dr = SqlProvider.GetSqlDataReader("Itn.ProcurementDb.Connection", ConnectionStringType.DBKey,
                SqlParamStatement.Create(sb.ToString()));
            try
            {
                while (dr.Read())
                {
                    result.Add(Customer.Create(Convert.ToInt32(dr["Id"].ToString()), dr["CustomerNbr"].ToString(), dr["Name"].ToString(), dr["Email"].ToString()));
                }
                return result;
            }
            finally
            {
                dr.Close();
            }

        }

        public List<Item> FetchItems()
        {
            var result = new List<Item>();
            var sb = new StringBuilder();
            sb.Append("Select * from Item");
            var dr = SqlProvider.GetSqlDataReader("Itn.ProcurementDb.Connection", ConnectionStringType.DBKey,
                SqlParamStatement.Create(sb.ToString() ));
            try
            {
                while (dr.Read())
                {
                    result.Add(Item.Create(Convert.ToInt32(dr["Id"].ToString()), dr["ItemNbr"].ToString(), dr["Description"].ToString()));
                }
                return result;
            }
            finally
            {
                dr.Close();
            }
        }

        public Item FetchItem(string itemNbr)
        {
            var result = new Item();
            var sb = new StringBuilder();
            sb.Append("Select * from Item where ItemNbr = @itemNbr");
            var dr = SqlProvider.GetSqlDataReader("Itn.ProcurementDb.Connection", ConnectionStringType.DBKey,
                SqlParamStatement.Create(sb.ToString(), new List<SqlParameter>
                {
                    SqlParamWrapper.Create("@itemNbr", itemNbr)
                }));
            try
            {
                if (!dr.HasRows) throw new RecordNotFoundException(string.Format("{0} item is missing", itemNbr));
                while (dr.Read())
                {
                    return Item.Create(Convert.ToInt32(dr["Id"].ToString()), dr["ItemNbr"].ToString(),
                        dr["Description"].ToString());
                }

            }
            finally
            {
                dr.Close();
            }

            return result;
        }

        public Customer FetchCustomer(string customerNbr)
        {
            var result = new Customer();
            var sb = new StringBuilder();
            sb.Append("Select * from Customer Where CustomerNbr =@customerNbr");
            var dr = SqlProvider.GetSqlDataReader("Itn.ProcurementDb.Connection", ConnectionStringType.DBKey,
                SqlParamStatement.Create(sb.ToString(), new List<SqlParameter>
                {
                    SqlParamWrapper.Create("@customerNbr", customerNbr)
                }));
            try
            {
                while (dr.Read())
                {
                    return Customer.Create(Convert.ToInt32(dr["Id"].ToString()), dr["CustomerNbr"].ToString(), dr["Name"].ToString(), dr["Email"].ToString());
                }
                return result;
            }
            finally
            {
                dr.Close();
            }

        }

       
    }
}