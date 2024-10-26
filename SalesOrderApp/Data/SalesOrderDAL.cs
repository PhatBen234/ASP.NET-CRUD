using SalesOrderDALApp.Models;
using System.Data.SqlClient;

namespace SalesOrderDALApp.Data
{
    public class SalesOrderDAL
    {
        private readonly string _connectionString;

        public SalesOrderDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Retrieve all
        public List<SalesOrder> GetSalesOrders()
        {
            var salesOrders = new List<SalesOrder>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM SalesOrder", con))
                {
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var order = new SalesOrder
                            {
                                SalesOrderId = rdr["SalesOrderId"].ToString(),
                                SalesOrderItem = rdr["SalesOrderItem"].ToString(),
                                WorkOrder = rdr["WorkOrder"].ToString(),
                                ProductID = rdr["ProductID"].ToString(),
                                ProductDescription = rdr["ProductDescription"].ToString(),
                                OrderQuantity = Convert.ToDecimal(rdr["OrderQuantity"]),
                                OrderStatus = rdr["OrderStatus"].ToString(),
                                Timestamp = Convert.ToDateTime(rdr["Timestamp"])
                            };

                            salesOrders.Add(order);
                        }
                    }
                }
            }

            return salesOrders;
        }

        // Add 
        public void AddSalesOrder(SalesOrder order)
        {
            
            order.Timestamp = DateTime.Now;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO SalesOrder (SalesOrderId, SalesOrderItem, WorkOrder, ProductID, ProductDescription, OrderQuantity, OrderStatus, Timestamp) " +
                    "VALUES (@SalesOrderId, @SalesOrderItem, @WorkOrder, @ProductID, @ProductDescription, @OrderQuantity, @OrderStatus, @Timestamp)", con))
                {
                    cmd.Parameters.AddWithValue("@SalesOrderId", order.SalesOrderId);
                    cmd.Parameters.AddWithValue("@SalesOrderItem", order.SalesOrderItem);
                    cmd.Parameters.AddWithValue("@WorkOrder", order.WorkOrder);
                    cmd.Parameters.AddWithValue("@ProductID", order.ProductID);
                    cmd.Parameters.AddWithValue("@ProductDescription", order.ProductDescription);
                    cmd.Parameters.AddWithValue("@OrderQuantity", order.OrderQuantity);
                    cmd.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
                    cmd.Parameters.AddWithValue("@Timestamp", order.Timestamp);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Upadte
        public void UpdateSalesOrder(SalesOrder order)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE SalesOrder SET SalesOrderItem = @SalesOrderItem, WorkOrder = @WorkOrder, ProductID = @ProductID, " +
                    "ProductDescription = @ProductDescription, OrderQuantity = @OrderQuantity, OrderStatus = @OrderStatus, Timestamp = @Timestamp " +
                    "WHERE SalesOrderId = @SalesOrderId", con))
                {
                    cmd.Parameters.AddWithValue("@SalesOrderId", order.SalesOrderId);
                    cmd.Parameters.AddWithValue("@SalesOrderItem", order.SalesOrderItem);
                    cmd.Parameters.AddWithValue("@WorkOrder", order.WorkOrder);
                    cmd.Parameters.AddWithValue("@ProductID", order.ProductID);
                    cmd.Parameters.AddWithValue("@ProductDescription", order.ProductDescription);
                    cmd.Parameters.AddWithValue("@OrderQuantity", order.OrderQuantity);
                    cmd.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
                    cmd.Parameters.AddWithValue("@Timestamp", order.Timestamp);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Delete
        public void DeleteSalesOrder(string salesOrderId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM SalesOrder WHERE SalesOrderId = @SalesOrderId", con))
                {
                    cmd.Parameters.AddWithValue("@SalesOrderId", salesOrderId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
