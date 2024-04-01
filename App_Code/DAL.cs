using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
//using System.ComponentModel;
using System.Text;
using System.Drawing;
using Microsoft.Security.Application;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for DAL
/// </summary>
//[DataObject(true)]
public class DAL
{
    static string DBConnectString = ConfigurationManager.AppSettings["DBConnectString"].ToString();
    
    
	public DAL()
	{
		//
		// TODO: Add constructor logic here
		//
        //string conStr=null;
       // string connString1 = System.Configuration.ConfigurationManager.AppSettings["DBConnectString"];
       
	}
    public DataSet GetExpenseRecoveryData(string Data)
    {

        string strUserName = HttpContext.Current.User.Identity.Name.Split(new string[] { "\\" }, StringSplitOptions.None).Last().ToString();

        SqlConnection con = new SqlConnection(DBConnectString);
        SqlCommand cmd1 = new SqlCommand("SP_delayedbilling_CheckAccess", con);
        cmd1.Parameters.AddWithValue("@MailID", strUserName);
        cmd1.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sdr = new SqlDataAdapter(cmd1);
        DataSet dsRole = new DataSet();

        sdr.Fill(dsRole);
        string Role = string.Empty;
        if (dsRole.Tables[0].Rows.Count > 0)
        {
            Role = dsRole.Tables[0].Rows[0][0].ToString();
        }


        JavaScriptSerializer js = new JavaScriptSerializer();
        var obj = js.Deserialize<DB>(Data);

        SqlCommand cmd = new SqlCommand("SP_delayedbillingaccess_FetchData_New", con);
        cmd.Parameters.AddWithValue("@customerCode_Filter", obj.customerCode_Filter);
        cmd.Parameters.AddWithValue("@ageingBucket_Filter", obj.ageingBucket_Filter);
        cmd.Parameters.AddWithValue("@glLongText_Filter", obj.glLongText_Filter);
        cmd.Parameters.AddWithValue("@PM_Filter", obj.PM_Filter);
        cmd.Parameters.AddWithValue("@DM_Filter", obj.DM_Filter);
        cmd.Parameters.AddWithValue("@ProfitCentre_Filter", obj.ProfitCentre_Filter);
        cmd.Parameters.AddWithValue("@category_Filter", obj.category_Filter);
        cmd.Parameters.AddWithValue("@Status_Filter", obj.Status_Filter);
        cmd.Parameters.AddWithValue("@PracticeLine_Filter", obj.PracticeLine_Filter);
        cmd.Parameters.AddWithValue("@MailID", strUserName);
        cmd.Parameters.AddWithValue("@AccessTYpe", Role);
        cmd.Parameters.AddWithValue("@Commitment", obj.Commitment);
        cmd.Parameters.AddWithValue("@pageno", obj.pageno);
        cmd.Parameters.AddWithValue("@pagesize", obj.pagesize);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Connection = con;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        return ds;
    }


    public class DB
    {
        public string customerCode_Filter { get; set; }
        public string ageingBucket_Filter { get; set; }
        public string glLongText_Filter { get; set; }
        public string PM_Filter { get; set; }
        public string DM_Filter { get; set; }
        public string ProfitCentre_Filter { get; set; }
        public string category_Filter { get; set; }
        public string Status_Filter { get; set; }
        public string PracticeLine_Filter { get; set; }
        public string MailID { get; set; }
        public string AccessTYpe { get; set; }
        public string Commitment { get; set; }
        public string pageno { get; set; }
        public string pagesize { get; set; }
    }

    public int DelegateAccess(string EmpMailID, DataTable dtCustomerCode, DataTable dtMCC, DataTable dtPM, DataTable dtDM, DataTable dtPracticeLine, DataTable dtProfitCentre, string UserName)
    {
        using (SqlConnection Conn = new SqlConnection())
        {

            Conn.ConnectionString = AntiXss.HtmlEncode(DBConnectString);

            SqlCommand cmd = new SqlCommand("[SP_ExpenseRecovery_DelegateAccess]", Conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param_EmpMailID = new SqlParameter("@EmpMailID", SqlDbType.NVarChar, 500);
            param_EmpMailID.Value = EmpMailID;
            cmd.Parameters.Add(param_EmpMailID);

            SqlParameter param_dtCustomerCode = cmd.Parameters.AddWithValue("@dtCustomerCode", dtCustomerCode);
            param_dtCustomerCode.SqlDbType = SqlDbType.Structured;
            param_dtCustomerCode.TypeName = "dbo.ExpenseRecovery_Access_CustomerCode";



            SqlParameter param_dtMCC = cmd.Parameters.AddWithValue("@dtMCC", dtMCC);
            param_dtMCC.SqlDbType = SqlDbType.Structured;
            param_dtMCC.TypeName = "dbo.ExpenseRecovery_Access_MCC";

            SqlParameter param_dtPM = cmd.Parameters.AddWithValue("@dtPM", dtPM);
            param_dtPM.SqlDbType = SqlDbType.Structured;
            param_dtPM.TypeName = "dbo.ExpenseRecovery_Access_PM";


            SqlParameter param_dtDM = cmd.Parameters.AddWithValue("@dtDM", dtDM);
            param_dtDM.SqlDbType = SqlDbType.Structured;
            param_dtDM.TypeName = "dbo.ExpenseRecovery_Access_DM";

            SqlParameter param_dtPracticeLine = cmd.Parameters.AddWithValue("@dtPracticeLine", dtPracticeLine);
            param_dtPracticeLine.SqlDbType = SqlDbType.Structured;
            param_dtPracticeLine.TypeName = "dbo.ExpenseRecovery_Access_PracticeLine";

            SqlParameter param_dtProfitCentre = cmd.Parameters.AddWithValue("@dtProfitCentre", dtProfitCentre);
            param_dtProfitCentre.SqlDbType = SqlDbType.Structured;
            param_dtProfitCentre.TypeName = "dbo.ExpenseRecovery_Access_ProfitCentre";

            SqlParameter param_UserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 500);
            param_UserName.Value = UserName;
            cmd.Parameters.Add(param_UserName);

            SqlParameter prmReturnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
            prmReturnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(prmReturnValue);

            try
            {
                Conn.Open();
                cmd.ExecuteNonQuery();
              
                return (Convert.ToInt32(prmReturnValue.Value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }
    }

    public DataTable FetchDetails(string detail,string userName,string Unit, string IBU,string CustomerCode,string ProjectCode,string ContractType,string DM,string ActiveProjectCode)
    {

        using (SqlConnection Conn = new SqlConnection())
        {

            Conn.ConnectionString = AntiXss.HtmlEncode(DBConnectString);
            SqlCommand cmd = new SqlCommand();
            string cmdtext = "sp_ExpenseRecovery_FetchData";
            cmd.CommandText = cmdtext;
            cmd.Connection = Conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Detail", detail);
            cmd.Parameters.AddWithValue("@Username", userName);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@IBU", IBU);
            cmd.Parameters.AddWithValue("@GroupPU", CustomerCode);
            cmd.Parameters.AddWithValue("@ProfitCenter", ProjectCode);
            cmd.Parameters.AddWithValue("@ContractType", ContractType);
            cmd.Parameters.AddWithValue("@DM", DM);
            cmd.Parameters.AddWithValue("@ActiveProjectCode", ActiveProjectCode);
            
            DataTable dt = new DataTable();

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (SqlException ex)
            {

                throw ex;
            }

            return dt;
        }
    }
    public DataSet FetchDataSet(SqlCommand cmd)
    {
        try
        {

            DataSet ds = new DataSet();
            string connString = System.Configuration.ConfigurationManager.AppSettings["DBConnectString"];
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = AntiXss.HtmlEncode(connString);
                cmd.Connection = conn;
                cmd.CommandTimeout = int.MaxValue;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                

                conn.Open();
                da.Fill(ds, "Data");

                return ds;
            }
        }
        catch (SqlException ex)
        {

            throw ex;
        }
    }
    public DataTable FetchDetailsforTemplate(string access, string userName, string subCategory, string CustomerCode, string PM, string GLLongText, string AgeingBucket, string DM, string profitCentre, string category_top, string practiceline,string commitment)
    {

        using (SqlConnection Conn = new SqlConnection())
        {

            Conn.ConnectionString = AntiXss.HtmlEncode(DBConnectString);
            SqlCommand cmd = new SqlCommand();
            string cmdtext = "sp_ExpenseRecovery_FetchDataForTemplate_EAS_NewWithAccess";
            cmd.CommandText = cmdtext;
            cmd.Connection = Conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccessTYpe", access);
            cmd.Parameters.AddWithValue("@MailID", userName);
            cmd.Parameters.AddWithValue("@Status_Filter", subCategory);
            cmd.Parameters.AddWithValue("@customerCode_Filter", CustomerCode);
            cmd.Parameters.AddWithValue("@PM_Filter", PM);
            cmd.Parameters.AddWithValue("@glLongText_Filter", GLLongText);
            cmd.Parameters.AddWithValue("@ageingBucket_Filter", AgeingBucket);
            cmd.Parameters.AddWithValue("@DM_Filter", DM);
            cmd.Parameters.AddWithValue("@ProfitCentre_Filter", profitCentre);
            cmd.Parameters.AddWithValue("@category_Filter", category_top);
            cmd.Parameters.AddWithValue("@PracticeLine_Filter", practiceline);
            cmd.Parameters.AddWithValue("@Commitment_Filter", commitment);
            DataTable dt = new DataTable();

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            return dt;
        }
    }
    public int ExecQueryRtInt(string Query)
    {

        using (SqlConnection Conn = new SqlConnection())
        {

            Conn.ConnectionString = AntiXss.HtmlEncode(DBConnectString);
            int ret;
            SqlCommand cmdUser = new SqlCommand(Query, Conn);
            cmdUser.CommandType = CommandType.Text;
            Conn.Open();
            ret = cmdUser.ExecuteNonQuery();
          
            return ret;
        }

    }

    public void ExpenseRecoveryUpdate(string UpdateFromAccountTeam, string InvoiceNumber,string ConfirmationNo,string ServeCentraleNo, string Remarks, string User, string DocumentNumber, string Unique,out int Count)
    {

        using (SqlConnection Conn = new SqlConnection())
        {

            Conn.ConnectionString = AntiXss.HtmlEncode(DBConnectString);
            SqlParameter count = new SqlParameter();
            SqlCommand cmdUpdate = new SqlCommand();
            string cmdtext = "SP_ExpenseRecovery_Update";
            cmdUpdate.CommandText = cmdtext;
            cmdUpdate.Connection = Conn;
            cmdUpdate.CommandType = CommandType.StoredProcedure;
            cmdUpdate.Parameters.AddWithValue("@UpdateFromAccountTeam", UpdateFromAccountTeam);
            cmdUpdate.Parameters.AddWithValue("@InvoiceNumber", InvoiceNumber);
            cmdUpdate.Parameters.AddWithValue("@ConfirmationNo", ConfirmationNo);
            cmdUpdate.Parameters.AddWithValue("@ServeCentraleNo", ServeCentraleNo);
            cmdUpdate.Parameters.AddWithValue("@Remarks", Remarks);
            cmdUpdate.Parameters.AddWithValue("@User", User);
            cmdUpdate.Parameters.AddWithValue("@DocumnetNumber", DocumentNumber);
            cmdUpdate.Parameters.AddWithValue("@Unique", Unique);
            count.ParameterName = "@Return";
            count.SqlDbType = System.Data.SqlDbType.Int;
            count.Size = 100;
            count.Direction = System.Data.ParameterDirection.Output;
            cmdUpdate.Parameters.Add(count);
            try
            {
                Conn.Open();
                cmdUpdate.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {

            }

            Count = Convert.ToInt32(count.Value);
        }
    }
    public int getGVRowIndex(Control ctl)
    {
        GridViewRow GVRow = (GridViewRow)ctl.NamingContainer;
        return GVRow.RowIndex;

    }
    public string CheckAccess(string MailId)
    {

        using (SqlConnection Conn = new SqlConnection())
        {

            Conn.ConnectionString = AntiXss.HtmlEncode(DBConnectString);
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

    public DataSet GetDataSet(string cmdText)
    {

        using (SqlConnection con = new SqlConnection())
        {

            con.ConnectionString = AntiXss.HtmlEncode(DBConnectString);
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
