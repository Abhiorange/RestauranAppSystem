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

        public List<AllDiscount> AllDiscountList()
        {
            List<AllDiscount> model = new List<AllDiscount>();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("AllDiscountList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    AllDiscount allDiscount = new AllDiscount
                    {
                        discountType = rdr["discountType"].ToString(),
                        value = Convert.ToInt32(rdr["value"]),
                        discountid = Convert.ToInt32(rdr["discountid"]),
                    };

                    model.Add(allDiscount);
                }
                con.Close();
            }
            return model;
        }

        public int DeleteDiscountById(int id)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("DeleteDiscountById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                return i;
            }
        }

        public int deleteDiscountById(int id)
        {
            throw new NotImplementedException();
        }
    }
    }
