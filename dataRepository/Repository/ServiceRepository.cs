
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
    }
}
