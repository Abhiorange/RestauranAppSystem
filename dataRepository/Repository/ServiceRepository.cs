
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
    public class ServiceRepository:IServiceRepository
    {
        public string connections = "Server=PCA59\\SQL2019;Database=RestaurantSystem;User Id=sa;Password=Tatva@123;Trusted_Connection=True;Encrypt=False";

        public List<CategoryDetail> GetCategoryNames()
        {
            List<CategoryDetail> model = new List<CategoryDetail>();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("categorynames", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    CategoryDetail name = new CategoryDetail
                    {
                        categoryname = rdr["categoryName"].ToString(),
                        id = Convert.ToInt32(rdr["id"]),
                    };

                    model.Add(name);
                }
                con.Close();
            }
            return model;
        }

        public List<ProductDetail> GetProductsById(int id)
        {
            List<ProductDetail> model = new List<ProductDetail>();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("productnames", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ProductDetail name = new ProductDetail
                    {
                        productname = rdr["name"].ToString(),
                        id = Convert.ToInt32(rdr["id"]),
                    };

                    model.Add(name);
                }
                con.Close();
            }
            return model;
        }

        public int AddCustomer(CustomerDetailVm model)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("spInsertCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", model.name);
                cmd.Parameters.AddWithValue("@customercode", model.customerCode);
                cmd.Parameters.AddWithValue("@address", model.address);
                cmd.Parameters.AddWithValue("@phonenumber", model.phone);
                cmd.Parameters.AddWithValue("@Email", model.email);

                cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();

                int customerId = Convert.ToInt32(cmd.Parameters["@CustomerId"].Value);

                con.Close();
             
                return customerId;
            }
        }

        public int AddOrder(ItemsDetailVm model)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("spInsertOrder", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productid", model.productid);
                cmd.Parameters.AddWithValue("@units", model.itemunit);
                cmd.Parameters.Add("@orderId", SqlDbType.Int).Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                int orderId = Convert.ToInt32(cmd.Parameters["@orderId"].Value);
                return orderId;
            }
        }
        public int AddItem(PostItemsVm model)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("InsertOrderDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@orderid", model.ordersid);
                cmd.Parameters.AddWithValue("@productid", model.productid);
                cmd.Parameters.AddWithValue("@units", model.itemunit);

                con.Open();
                int i = cmd.ExecuteNonQuery();

                return i;
            }
        }

        public List<ItemsInfo> GetItems(int id)
        {
            List<ItemsInfo> model = new List<ItemsInfo>();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("GetAllItemsInformation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@orderid", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ItemsInfo name = new ItemsInfo
                    {
                        orderdetailid= Convert.ToInt32(rdr["Orderdetailid"]),
                        itemname = rdr["productname"].ToString(),
                        units= Convert.ToInt32(rdr["Units"]),
                        productid = Convert.ToInt32(rdr["ProductId"]),
                        totalprice= Convert.ToInt64(rdr["TotalPrice"]),
                    };

                    model.Add(name);
                }
                con.Close();
            }
            return model;
        }
    }
}
