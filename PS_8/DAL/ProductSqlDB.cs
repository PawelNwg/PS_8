using Microsoft.Extensions.Configuration;
using PS_8.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PS_8.DAL
{
    public class ProductSqlDB : IProductDB
    {
        string connectionString;
        public List<Product> productList;
        SqlConnection con;
        public List<Product> List()
        {
            List<Product> products = new List<Product>();
            SqlCommand cmd = new SqlCommand("sp_LOAD", con);
            cmd.CommandType = CommandType.StoredProcedure;         
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    int id = Int32.Parse(reader["Id"].ToString());
                    string name = reader["name"].ToString();
                    decimal price = Decimal.Parse(reader["price"].ToString());

                    products.Add(new Product
                    {
                        id = id,
                        name = name,
                        price = price,

                    });

                }
            reader.Close();           
            con.Close();   
            return products;
        }
        public ProductSqlDB(IConfiguration _configuration)
        {
            connectionString = _configuration.GetConnectionString("myCompanyDB");
            con = new SqlConnection(connectionString);
        }
        public void Add(Product _product)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("sp_CREATE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NChar, 20));
            cmd.Parameters.Add(new SqlParameter("@price", SqlDbType.Money));
            
            cmd.Parameters["@name"].Value = _product.name;
            cmd.Parameters["@price"].Value = _product.price;
            
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Delete(int _id)
        {

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("sp_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.Int));
            cmd.Parameters["@ProductID"].Value = _id;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public Product Get(int _id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("sp_GET", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.Int));
            cmd.Parameters["@ProductID"].Value = _id;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Product p;
            reader.Read();
            if (reader.HasRows == true)
            {
                int idd = Int32.Parse(reader["Id"].ToString());
                string name = reader["name"].ToString();
                decimal price = Decimal.Parse(reader["price"].ToString());
             
                p = new Product { id = _id, name = name, price = price};
                reader.Close();

                return p;
            }
            con.Close();
            return null;
        }

       
        public void Update(Product _product)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("sp_EDIT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NChar, 20));
            cmd.Parameters.Add(new SqlParameter("@price", SqlDbType.Money));
            
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
            cmd.Parameters["@name"].Value = _product.name;
            cmd.Parameters["@price"].Value = _product.price;
           
            cmd.Parameters["@id"].Value = _product.id;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
