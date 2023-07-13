using dataRepository.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System.Data;
using System.Data.SqlClient;
using ViewModels.Models;

namespace dataRepository.Repository
{
    public class RoleRepository : IRoleRepository
    {
        public string connections = "server=192.168.2.59\\SQL2019;Database=RestaurantSystem;User Id=sa;Password=Tatva@123;Encrypt=False";

        public int addrole(RoleRegisterVm model)
        {
            int userId = 0;

            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("spInsertRole", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", model.name);
                cmd.Parameters.AddWithValue("@description", model.description);

                con.Open();
                int i = cmd.ExecuteNonQuery();

                return i;
            }

        }



        public int EditRole(RoleById model)
        {
            int userId = 0;

            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("RoleEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", model.name);
                cmd.Parameters.AddWithValue("@description", model.description);
                cmd.Parameters.AddWithValue("@id", model.roleId);

                con.Open();
                int i = cmd.ExecuteNonQuery();

                return i;
            }

        }

        //public List<SelectListItem> GetRoleList()
        //{
        //    List<SelectListItem> roles = new List<SelectListItem>();
        //    using (SqlConnection con = new SqlConnection(connections))
        //    {
        //        SqlCommand cmd = new SqlCommand("RoleData", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();
        //        while (rdr.Read())
        //        {
        //            int categoryId = Convert.ToInt32(rdr["id"]);
        //            string categoryName = rdr["categoryName"].ToString();
        //            roles.Add(new SelectListItem { Value = categoryId.ToString(), Text = categoryName });
        //        }

        //    }
        //    return roles;
        //}


        public List<RoleInfoVm> GetRoleList()
        {
            List<RoleInfoVm> model = new List<RoleInfoVm>();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("RoleData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    RoleInfoVm role = new RoleInfoVm
                    {
                        name = rdr["name"].ToString(),
                        description = rdr["description"].ToString(),
                        roleId = Convert.ToInt32(rdr["id"]),
                        //Totalprice = Convert.ToInt64(rdr["TotalPrice"]),
                        //tableno = Convert.ToInt32(rdr["TableNumber"]),
                        //ordertime = rdr["FormattedDate"].ToString(),
                    };

                    model.Add(role);
                }
                con.Close();
            }
            return model;
        }


        public RoleById RoleById(int id)
        {
            RoleById model = new RoleById();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("RoleById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    model = new RoleById
                    {
                        name = rdr["name"].ToString(),
                        description = rdr["description"].ToString(),
                        roleId = id
                    };

                }
                con.Close();

            }
            return model;
        }

        public int deleteRoleById(int id)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("DeleteRoleById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                return i;
            }
        }

        public int GetRollList()
        {
            throw new NotImplementedException();
        }
    }
}