using dataRepository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Models;

namespace dataRepository.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        public string connections = "server=192.168.2.59\\SQL2019;Database=RestaurantSystem;User Id=sa;Password=Tatva@123;Encrypt=False";
        public List<OrdersVm> GetOrdersInfo()
        {
            List<OrdersVm> model = new List<OrdersVm>();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("GetAllOrdersInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    OrdersVm order = new OrdersVm
                    {
                        orderid = Convert.ToInt32(rdr["OrderId"]),
                        Totalprice = Convert.ToInt64(rdr["TotalPrice"]),
                        tableno = Convert.ToInt32(rdr["TableNumber"]),
                        ordertime = rdr["FormattedDate"].ToString(),
                    };

                    model.Add(order);
                }
                con.Close();
            }
            return model;
        }
        public List<ProductODVm> GetOrdersInfo(int id)
        {
            List<ProductODVm> model = new List<ProductODVm>();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("GetAllOrderdetailById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@orderid", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ProductODVm pro = new ProductODVm
                    {
                        image = rdr["Image"].ToString(),
                        productname = rdr["Name"].ToString(),
                        units = Convert.ToInt32(rdr["Oty"]),
                        totalprice = Convert.ToInt64(rdr["TotalPrice"]),
                        unitprice = Convert.ToInt32(rdr["UnitPrice"]),
                    };

                    model.Add(pro);
                }
                con.Close();
            }
            return model;
        }


    }
}
