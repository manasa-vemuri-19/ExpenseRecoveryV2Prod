using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DAL_New
/// </summary>
public class DAL_New
{
    static string constr = ConfigurationManager.ConnectionStrings["DBConnectString"].ConnectionString;


    public string CheckAccess(string MailId)
    {

        using (SqlConnection Conn = new SqlConnection())
        {

            Conn.ConnectionString = constr;
            SqlParameter role = new SqlParameter();
            //String DBConnecting = ConfigurationManager.AppSettings["DBConnectStringBCM-IS"];
            //SqlConnection Con = new SqlConnection(DBConnecting);



            SqlCommand cmdAccess = new SqlCommand();
            string cmdtext = "SP_ExpenseRecovery_CheckAccess_New";
            cmdAccess.CommandText = cmdtext;
            cmdAccess.Connection = Conn;
            cmdAccess.CommandType = CommandType.StoredProcedure;

            cmdAccess.Parameters.AddWithValue("@MailId", MailId);
            role.ParameterName = "@access";
            role.SqlDbType = System.Data.SqlDbType.NVarChar;
            role.Size = 100;
            role.Direction = System.Data.ParameterDirection.Output;

            cmdAccess.Parameters.Add(role);

            try
            {
                Conn.Open();
                cmdAccess.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {

            }

            //string role = parmRole.Value.ToString();
            string Role = Convert.ToString(role.Value);
            return Role;
        }
    }

    public DataSet GetData(string MailID)
    {
        string constr = ConfigurationManager.ConnectionStrings["DBConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand("SP_ExpenseRecovery_Summary_FetchData_V1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@MailID", MailID);
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        return ds;

    }
}