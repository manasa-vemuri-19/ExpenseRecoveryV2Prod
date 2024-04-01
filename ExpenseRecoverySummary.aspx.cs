using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Data;

public partial class ExpenseRecoverySummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DAL dal = new DAL();
            //Menu Menumain = (Menu)Master.FindControl("Menu_MainOptions");
            //Menumain.Items[0].Selected = true;
            string MailID = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);
            string role = dal.CheckAccess(MailID);

            //MailID = "shwetha_mh";
            
           
            //dal.CheckAccess(MailID, out role);
            //Session.Add("Role",role);
            //Session.Add("MailID", MailID);



            if (role == "NoAccess")
            {
                Response.Redirect("AccessDenied.aspx");
            }
            else
            {
                //MailID = "Aswathir_P";
                try
                {

                    ReportViewer1.Visible = true;

                    ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    Microsoft.Reporting.WebForms.ReportViewer rview = new Microsoft.Reporting.WebForms.ReportViewer();//Web Address of your report server (ex: http://rserver/reportserver (http://rserver/reportserver)) 

                    ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://nebula:1212/reportserver"); // Report Server URL



                    ReportViewer1.ServerReport.ReportPath = "/Expense Recovery Summary/ExpenseRecoverySummaryV2Prod";
                    List<ReportParameter> reportParams = new List<ReportParameter>();
                    reportParams.Add(new ReportParameter("MailID", MailID));

                    ReportViewer1.ServerReport.SetParameters(reportParams);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}