using dataRepository.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;
using ViewModels.Models;

namespace dataRepository.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        public string connections = "server=192.168.2.59\\SQL2019;Database=RestaurantSystem;User Id=sa;Password=Tatva@123;Encrypt=False";
        private string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString();
        }
        public int loginrepo(CompanyLoginVm model)
         {
            int userId = 0;
          
                using (SqlConnection con = new SqlConnection(connections))
                {
                    SqlCommand cmd = new SqlCommand("ValidateCompanyCredential", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", model.name);
                    cmd.Parameters.AddWithValue("@Password", model.password);
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["@UserID"].Value != DBNull.Value)
                    {
                        userId = (int)cmd.Parameters["@UserID"].Value;
                    }

                    con.Close();
                    return userId;
                }
            
         }

        public int forgetrepo(ForgetPasswordVm model)
        {
         

            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("UserForgetPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", model.email);
                cmd.Parameters.AddWithValue("@otp", model.otp);

                con.Open();
                int i = cmd.ExecuteNonQuery();

                return i;
            }

        }

        public int resetrepo(ResetPasswordVm model)
        {


            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("ResetPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", model.email);
                cmd.Parameters.AddWithValue("@newpassword", model.password);
                cmd.Parameters.Add("@ispasswordreset", SqlDbType.Bit).Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                int ispasswordreset = Convert.ToInt32(cmd.Parameters["@ispasswordreset"].Value);

                return ispasswordreset;
            }

        }

        public int otprepo(ForgetPasswordVm model)
        {


            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("OtpCompare", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", model.email);
                cmd.Parameters.AddWithValue("@otp", model.otp);
                cmd.Parameters.Add("@IsValid", SqlDbType.Bit).Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                int isValid = Convert.ToInt32(cmd.Parameters["@IsValid"].Value);

                return isValid;
            }

        }
        public int userloginrepo(UserLoginVm model)//user is logged in
        {
            int userId = 0;

            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("ValidateUserCredentials", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", model.email);
                cmd.Parameters.AddWithValue("@Password", model.password);
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["@UserID"].Value != DBNull.Value)
                {
                    userId = (int)cmd.Parameters["@UserID"].Value;
                }

                con.Close();
                return userId;
            }
        }
        public int registerrepo(CompanyRegisterVm model)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("spInsertComapny", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", model.companyName);
                cmd.Parameters.AddWithValue("@companyCode", model.companyCode);
                cmd.Parameters.AddWithValue("@password", model.password);
                con.Open();

                int i = cmd.ExecuteNonQuery();

                return i;
            }
        }

        public List<SelectListItem> GetRoleList()
        {
            List<SelectListItem> roles = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("RoleData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int roleId = Convert.ToInt32(rdr["id"]);
                    string role = rdr["name"].ToString();
                    roles.Add(new SelectListItem { Value = roleId.ToString(), Text = role });
                }
                
            }
            return roles;
        }
        public List<Userinfo> GetUsersList()
        {
            List<Userinfo> model = new List<Userinfo>();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("GetAllUserInformation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Userinfo user = new Userinfo
                    {  
                        UserId= Convert.ToInt32(rdr["UserId"]),
                        name = rdr["UserName"].ToString(),
                        email = rdr["UserEmail"].ToString(),
                        contact = Convert.ToInt64(rdr["UserContact"]),
                        companyname = rdr["UserCompany"].ToString(),
                        isactive= Convert.ToInt32(rdr["isactive"])

                    };

                    model.Add(user);
                }
                con.Close();
            }
            return model;
        }
        public List<CompanyInfo> GetCompaniesList()
        {
            List<CompanyInfo> model = new List<CompanyInfo>();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("GetAllCompanyInformation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    CompanyInfo company = new CompanyInfo
                    {
                       companyid= Convert.ToInt32(rdr["id"]),
                        companyname = rdr["name"].ToString(),
                        isactive= Convert.ToInt32(rdr["isActive"]),
                    };

                    model.Add(company);
                }
                con.Close();
            }
            return model;
        }
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
                        id= Convert.ToInt32(rdr["id"]),
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
        public UserEditVm GetUserById(int id)
        {
            UserEditVm model = null;
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("GetUserById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    model= new UserEditVm
                    {
                        name = rdr["name"].ToString(),
                        contact = rdr["contact"].ToString(),
                        password = rdr["password"].ToString(),
                        email = rdr["email"].ToString(),
                        companyId = Convert.ToInt32(rdr["companyId"]),
                        isactive = Convert.ToInt32(rdr["isActive"])
                        
                    };
                 
                }
                con.Close();

            }
            return model;
        }
        public int adduser(PostUserRegisterVm model)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("spInsertUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", model.name);
                cmd.Parameters.AddWithValue("@userCode", model.userCode);
                cmd.Parameters.AddWithValue("@contact", model.contact);
                cmd.Parameters.AddWithValue("@email", model.email);
                //cmd.Parameters.AddWithValue("@companyId", model.companyId);
                cmd.Parameters.AddWithValue("@roleId", model.roleId);
                cmd.Parameters.AddWithValue("@password", model.password);
                con.Open();

                int i = cmd.ExecuteNonQuery();

                return i;
            }
        }
       
        public int EditUser(PostUserEditVm model)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("Updateuser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", model.UserId);
                cmd.Parameters.AddWithValue("@name", model.name);
                cmd.Parameters.AddWithValue("@contact", model.contact);
                cmd.Parameters.AddWithValue("@email", model.email);
                cmd.Parameters.AddWithValue("@companyId", model.companyId);
                cmd.Parameters.AddWithValue("@password", model.password);
                cmd.Parameters.AddWithValue("@isactive", model.isactive);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                return i;
            }
        }     
        public int deleteUserById(int id)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("DeleteUserById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                return i;
            }
        }

        //public int forgetrepo(ForgetPasswordVm model)
        //{
        //    throw new NotImplementedException();
        //}

        public int loginrepo(ForgetPasswordVm model)
        {
            throw new NotImplementedException();
        }
    }
}
