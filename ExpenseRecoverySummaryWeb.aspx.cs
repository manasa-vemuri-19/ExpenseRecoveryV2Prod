using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ExpenseRecoverySummaryWeb
     : System.Web.UI.Page
{

    DAL_New dal = new DAL_New();
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {

            string MailID = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);
           
            string role = dal.CheckAccess(MailID);
            if (role == "NoAccess")
            {
                Response.Redirect("AccessDenied.aspx");
            }
            else
            {

                try
                {

                    DataSet ds = new DataSet();
                    ds = GetData(MailID);
                    DataTable dt_main = new DataTable();
                    dt_main = ds.Tables[0];
                    DataTable dt_sl = new DataTable();
                    dt_sl = ds.Tables[1];
                    hdnfldData.Value = JsonConvert.SerializeObject(dt_main);
                    hdnfldData_1.Value = JsonConvert.SerializeObject(dt_sl);


                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }

    private DataSet GetData(string MailID)
    {
        DataSet ds = new DataSet();
        ds = dal.GetData(MailID);
        return ds;
    }
}