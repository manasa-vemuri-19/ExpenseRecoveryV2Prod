using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;



public class ExcelPermissionDelay
{
    static string appName = "OEI_PBS_Reports";
    public static int GetDelayTime()
    {
        string connstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectString"]; // Add your connection string key
        SqlDataAdapter sqlAdapter;
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlConnection con = new SqlConnection(connstring);
        SqlCommand cmd = new SqlCommand("select DelayInMilliSeconds from EAS_Prod.dbo.tbl_ExcelPermission_Delay", con);
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 30;
        sqlAdapter = new SqlDataAdapter(cmd);
        sqlAdapter.Fill(dt);
        int time = Convert.ToInt32(dt.Rows[0]["DelayInMilliSeconds"]);
        return time;

    }

    public static bool IsPermissionEnabled()
    {
        string connstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectString"]; // Add your connection string key
        SqlDataAdapter sqlAdapter;
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(connstring);
        SqlCommand cmd = new SqlCommand("select EnablePermission from EAS_Prod.dbo.tbl_ExcelPermission_Delay", con);
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 30;
        sqlAdapter = new SqlDataAdapter(cmd);
        sqlAdapter.Fill(dt);
        bool enable = Convert.ToBoolean(dt.Rows[0]["EnablePermission"]);
        return enable;

    }



    public static void LogExcelInstance(int processID, string csFileName, string methodName)
    {

        string connstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectString"]; // Add your connection string key        
        SqlConnection con = new SqlConnection(connstring);
        SqlCommand cmd = new SqlCommand("sp_Insert_ExcelProcess_TaskManager ", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ProcessID", processID);
        cmd.Parameters.AddWithValue("@ApplicationName", appName);
        cmd.Parameters.AddWithValue("@CSFileName", csFileName);
        cmd.Parameters.AddWithValue("@MethodName", methodName);
        cmd.CommandTimeout = int.MaxValue;
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();


    }

    public static void LogErrorDetails(string csFileName, string methodName, string message, string stackTrace)
    {

        string connstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectString"]; // Add your connection string key        
        SqlConnection con = new SqlConnection(connstring);
        SqlCommand cmd = new SqlCommand("sp_Insert_ErrorDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@AppName", appName);
        cmd.Parameters.AddWithValue("@CSFileName", csFileName);
        cmd.Parameters.AddWithValue("@MethodName", methodName);
        cmd.Parameters.AddWithValue("@ErrorMessage", message);
        cmd.Parameters.AddWithValue("@StackTrace", stackTrace);
        cmd.CommandTimeout = int.MaxValue;
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();


    }





}