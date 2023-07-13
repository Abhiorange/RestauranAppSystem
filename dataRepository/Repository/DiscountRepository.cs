using dataRepository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dataRepository.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        public string connections = "server=192.168.2.59\\SQL2019;Database=RestaurantSystem;User Id=sa;Password=Tatva@123;Encrypt=False";
       
        
        public List<SelectListItem> GetDiscountList()
        {
            List<SelectListItem> discounts = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("DiscountData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int discounttypeId = Convert.ToInt32(rdr["id"]);
                    string discounttype = rdr["discountType"].ToString();
                    discounts.Add(new SelectListItem { Value = discounttypeId.ToString(), Text = discounttype });
                }

            }
            return discounts;
        }
        public int adddiscount(DiscountVm model)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("spInsertDiscount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", model.discounttypeid);
                cmd.Parameters.AddWithValue("@value", model.value);


                con.Open();

                int i = cmd.ExecuteNonQuery();

                return i;
            }
        }

       
    }
    }
