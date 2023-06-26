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
    public class CategoryRepository:ICategoryRepository
    {
        public string connections = "Server=PCA59\\SQL2019;Database=RestaurantSystem;User Id=sa;Password=Tatva@123;Trusted_Connection=True;Encrypt=False";

        public int addcategory(CategoryVm model)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("spInsertCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@categoryname", model.name);

                con.Open();

                int i = cmd.ExecuteNonQuery();

                return i;
            }
        }
        public List<CategoryInfo> GetCategoriesList()
        {
            List<CategoryInfo> model = new List<CategoryInfo>();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("GetAllCategoryInformation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    CategoryInfo category = new CategoryInfo
                    {
                        categoryname = rdr["categoryName"].ToString(),
                        isactive = Convert.ToInt32(rdr["isActive"]),
                        stocks = rdr["stocks"] != DBNull.Value ? Convert.ToInt64(rdr["stocks"]) : 0,
                        CategoryId = Convert.ToInt32(rdr["id"]),
                    };

                    model.Add(category);
                }
                con.Close();
            }
            return model;
        }
        public CategoryEditVm GetCategoryById(int id)
        {
            CategoryEditVm model = null;
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("GetCategoryById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    model = new CategoryEditVm
                    {
                        categoryname = rdr["categoryName"].ToString(),
                        stocks = rdr["stocks"] != DBNull.Value ? Convert.ToInt64(rdr["stocks"]) : 0,
                         isActive = Convert.ToInt32(rdr["isActive"]),
                         Id= Convert.ToInt32(rdr["id"]),

                    };

                }
                con.Close();

            }
            return model;
        }
        public int EditCategory(CategoryEditVm model)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("UpdateCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", model.Id);
                cmd.Parameters.AddWithValue("@categoryname", model.categoryname);
                cmd.Parameters.AddWithValue("@isActive", model.isActive);
                cmd.Parameters.AddWithValue("@stocks", model.stocks);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                return i;
            }
        }
        public int deleteCategoryById(int id)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("DeleteCategoryById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                return i;
            }
        }
    }
}
