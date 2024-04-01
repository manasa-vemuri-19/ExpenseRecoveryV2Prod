using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.Security.Application;

/// <summary>
/// Summary description for BusinessLogic
/// </summary>
public class BusinessLogic
{
    static string G_connStr = ConfigurationManager.AppSettings["DBConnectString"].ToString();

    public BusinessLogic()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void SaveExpenseData(string cmdText, string updatefromAccountteam, string invoiceNo, string confirmationNo, string servCentraleReqNo, string remarks, string unique, string username)
    {
        SqlCommand cmd = new SqlCommand(cmdText);
        cmd.CommandTimeout = 60;
        using (SqlConnection G_DBConnection = new SqlConnection())
        {

            G_DBConnection.ConnectionString = AntiXss.HtmlEncode(G_connStr);
            cmd.Connection = G_DBConnection;


            cmd.Parameters.Add("@updatefromAccountteam", updatefromAccountteam);
            cmd.Parameters.Add("@invoiceNo", invoiceNo);
            cmd.Parameters.Add("@confirmationNo", confirmationNo);
            cmd.Parameters.Add("@servCentraleReqNo", servCentraleReqNo);
            cmd.Parameters.Add("@remarks", remarks);
            cmd.Parameters.Add("@unique", unique);
            cmd.Parameters.Add("@LastUpdatedBY", username);
            cmd.Parameters.Add("@LastUpdatedON", DateTime.Now.ToString());



            try
            {
                G_DBConnection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    public static DataSet GetDataSet(string cmdText)
    {

        using (SqlConnection con = new SqlConnection())
        {

            con.ConnectionString = AntiXss.HtmlEncode(G_connStr);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandTimeout = 500;
            sqlCmd.CommandText = cmdText;
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sqlCmd.Dispose();
            return ds;

        }



    }

  
}

    //This method is used to fetch the Employee mailID From EmpMAster table
