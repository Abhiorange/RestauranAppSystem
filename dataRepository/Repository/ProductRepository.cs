using dataRepository.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class ProductRepository:IProductRepository
    {
        public string connections = "server=192.168.2.59\\SQL2019;Database=RestaurantSystem;User Id=sa;Password=Tatva@123;Encrypt=False";


        public List<SelectListItem> GetProductList()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("CategoryData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int categoryId = Convert.ToInt32(rdr["id"]);
                    string categoryName = rdr["categoryName"].ToString();
                    categories.Add(new SelectListItem { Value = categoryId.ToString(), Text = categoryName });
                }

            }
            return categories;
        }
        public int AddProduct(PostProductAddVm model)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("spInsertProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", model.Productname);
                cmd.Parameters.AddWithValue("@unit", model.units);
                cmd.Parameters.AddWithValue("@unitPrice", model.unitprice);
                cmd.Parameters.AddWithValue("@categoryId", model.categoryId);
                cmd.Parameters.AddWithValue("@imageSrc", model.imageSrc);


                con.Open();

                int i = cmd.ExecuteNonQuery();

                return i;
            }
        }
        public List<ProductInfo> GetProductsList()
        {
            List<ProductInfo> model = new List<ProductInfo>();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("GetAllProductInformation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ProductInfo product = new ProductInfo
                    {
                       productID = Convert.ToInt32(rdr["ProductId"]),
                       productName= rdr["ProductName"].ToString(),
                       categoryname= rdr["CategoryName"].ToString(),
                       unit= Convert.ToInt32(rdr["Units"]),
                       unitprice = Convert.ToInt64(rdr["UnitPrice"]),
                        isactive = Convert.ToInt32(rdr["isactive"])

                    };

                    model.Add(product);
                }
                con.Close();
            }
            return model;
        }
        public ProductEditVm GetProductById(int id)
        {
            ProductEditVm model = null;
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("GetProductById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    model = new ProductEditVm
                    {
                        productname = rdr["name"].ToString(),
                        unit = Convert.ToInt32(rdr["unit"]),
                        unitprice= Convert.ToInt64(rdr["unitPrice"]),
                        categoryid= Convert.ToInt32(rdr["categoryId"]),
                        isactive = Convert.ToInt32(rdr["isActive"]),
                        productid = Convert.ToInt32(rdr["id"]),
                        imageSrc = rdr["images"].ToString(),
                    };

                }
                con.Close();

            }
            return model;
        }
        public int EditProduct(PostProductEditVm model)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("Updateproduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", model.productid);
                cmd.Parameters.AddWithValue("@name", model.productname);
                cmd.Parameters.AddWithValue("@unit", model.unit);
                cmd.Parameters.AddWithValue("@unitprice", model.unitprice);
                cmd.Parameters.AddWithValue("@categoryid", model.categoryid);
                cmd.Parameters.AddWithValue("@isactive", model.isactive);
                cmd.Parameters.AddWithValue("@imageSrc", model.imageSrc);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                return i;
            }
        }
        public int deleteProductById(int id)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("DeleteProductById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                return i;
            }
        }
    }
}
