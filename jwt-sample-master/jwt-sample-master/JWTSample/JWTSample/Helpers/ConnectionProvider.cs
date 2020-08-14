using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;




namespace VakifIlan
{
    internal sealed class ConnectionProvider
    {
        //private static string strDBServerName;
        //private static string strDBName;
        //private static string strDBUserName;
        //private static string strDBPassword;
        internal static SqlConnection CreateConnection()
        {
           
            
            
            
            SqlConnection connection = new SqlConnection();
            try
            {
                connection.ConnectionString = ConnectionProvider.ConnectionString();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


            return connection;

        }

        internal static string ConnectionString()
        {
            //strDBServerName = System.Configuration.ConfigurationManager.AppSettings["DB_Server_Name"].ToString();
            //strDBName = System.Configuration.ConfigurationManager.AppSettings["DB_Name"].ToString();
            //strDBUserName = System.Configuration.ConfigurationManager.AppSettings["DB_User_Name"].ToString();
            //strDBPassword = System.Configuration.ConfigurationManager.AppSettings["DB_Password"].ToString();

            // CustomConfigManager mng = new CustomConfigManager("212e383c-510b-4609-972e-a75c6642008d");
            ////mng.SetConnectionString("ConnectionString", "Data Source=172.20.40.78;Initial Catalog=PersonelAlim;User ID=personelalimi; pwd=PAlimUser!987;Integrated Security=false; Pooling=false;Connect Timeout=20");

            //return mng.GetConnectionString("ConnectionString");
            return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        }



    }


}
