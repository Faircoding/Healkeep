using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Healkeep.Models;

namespace BusinessLayer
{
    public class AspNetRoleBusinessLayer
    {

        public void AddAspNetRole (AspNetRoles aspNetRole)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<AspNetRoles> aspNetRoles = new List<AspNetRoles>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddRole", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                //SqlParameter paramId = new SqlParameter();
                //paramId.ParameterName = "@Id";
                //paramId.Value = aspNetRole.Id;
                //cmd.Parameters.Add(paramId);

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = aspNetRole.Name;
                cmd.Parameters.Add(paramName);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

    }
}
