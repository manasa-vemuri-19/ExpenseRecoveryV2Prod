using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Data.OleDb;
using System.Collections.Generic;
using System.ComponentModel; 
using System.Linq;
using System.Diagnostics;
using VBIDE = Microsoft.Vbe.Interop;
using Excel = Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data.SqlTypes;
using Microsoft.Security.Application;
using System.Runtime.InteropServices;

public partial class _Default : System.Web.UI.Page
{


    static string subCategory_top;
    static string customerCode;
    static string ageingBucket;
    static string glLongText;
    static string PM;
    static string DM;
    static string role;
    static string MaildID;
    static string profitCentre;
    static string category_top;
    static string practiceLine;
    static int CbEditClicks = 0;
    static int EditRowIndex = 0;
    static string Commitment;
    static DataTable dtExpRec;
    static string categoryReportParam = "ALL";
    static string subcategoryReportParam = "ALL";
    static string ageingbucketReportParam = "ALL";
    static string practicelineReportParam = "ALL";
    DataTable dtaccess;
    DAL dal = new DAL();
   // SqlConnection con = new SqlConnection("Data Source=BLRKEC340745D;Initial Catalog=GMU;User ID=sa;Password=Infy123+;");
    protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data"); 
            //Menu Menumain = (Menu)Master.FindControl("Menu_MainOptions");
           // Menumain.Items[1].Selected = true;
            ClientScript.GetPostBackEventReference(this, string.Empty);

            //if (Session["MailID"] == null)
            //{
            //    Response.Redirect("SessionTimeOut.htm");
            //}

        if (!IsPostBack)
        {
            

            categoryReportParam = "ALL";
            subcategoryReportParam = "ALL";
            ageingbucketReportParam = "ALL";
            practicelineReportParam = "ALL";

                if (Request.QueryString["category"] != null)
            {
                categoryReportParam = Request.QueryString["category"].ToString();
                categoryReportParam = (String.IsNullOrEmpty(this.Request["category"]) ? "ALL" : Server.UrlDecode(this.Request["category"]));

            }

            if (Request.QueryString["subcategory"] != null)
            {
                subcategoryReportParam = Request.QueryString["subcategory"].ToString();
                subcategoryReportParam = (String.IsNullOrEmpty(this.Request["subcategory"]) ? "ALL" : Server.UrlDecode(this.Request["subcategory"]));

            }
            if (Request.QueryString["ageingbucket"] != null)
            {
                ageingbucketReportParam = Request.QueryString["ageingbucket"].ToString();
                ageingbucketReportParam = (String.IsNullOrEmpty(this.Request["ageingbucket"]) ? "ALL" : Server.UrlDecode(this.Request["ageingbucket"]));

            }
            if (Request.QueryString["practiceline"] != null)
            {
                practicelineReportParam = Request.QueryString["practiceline"].ToString();
                practicelineReportParam = (String.IsNullOrEmpty(this.Request["practiceline"]) ? "ALL" : Server.UrlDecode(this.Request["practiceline"]));
                if (practicelineReportParam == "EAS")
                    practicelineReportParam = "ALL";

            }
      
            
            
            Lblmsg.Visible = false;

           
            

           string MailID = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);

           MailID = "kshtiij.gaurav";
          Session.Add("MailID", MailID);
          role = dal.CheckAccess(MailID); 
           
            Session.Add("Role", role);
        
            SqlCommand cmd = new SqlCommand();
            string query = "exec SP_CheckAdminAccess @mailid";
            cmd.Parameters.AddWithValue("@mailid", MailID);
            cmd.CommandText = query;
            DataSet dsAdminAccess = dal.FetchDataSet(cmd);
            string validity = dsAdminAccess.Tables[0].Rows[0][0].ToString();
            
            //if ((MailID.ToLower() == "soumya_ramanathan") || (MailID == "rakhi_ap") || (MailID == "sridevi_srirangan") || (MailID == "prema_haridas") || (MailID == "glnrao"))
            if (validity=="VALID")
            {
                btnDownloadPivot.Visible = true;
                btnDownloadPivot.Enabled = true;
            }
            
                 //Button1.Visible = true;
                 //   Button1.Enabled = true;
           
      if (role == "NoAccess")
            {
                Response.Redirect("AccessDenied.aspx");
            }
            if (!IsPostBack)
            {
                try
                {
                    LoadScreen(role, MailID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
       }
        
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.Page.ClientScript.GetPostBackEventReference(this,string.Empty);
    }
    public void SaveNew()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("UpdateFromAccountTeam");
        dt.Columns.Add("InvoiceNumber");
        dt.Columns.Add("ConfirmationNo");
        dt.Columns.Add("ServeCentraleNo");
        dt.Columns.Add("Remarks");
        dt.Columns.Add("Unique");
        dt.Columns.Add("LastUpdatedBy");
        dt.Columns.Add("LastUpdatedON");
        dt.Columns.Add("ExpenseClosureDate");
        dt.Columns.Add("Category");
        dt.Columns.Add("SubCategory");
        dt.Columns.Add("Comments");
        int RowStart = gvExpenseRec.PageIndex * 15;
        int RowNo = gvExpenseRec.Rows.Count < 15 ? gvExpenseRec.Rows.Count : 15;


        for (int j = RowStart; j < RowNo + RowStart; j++)
        {
            int i = j - RowStart;
            if (((CheckBox)gvExpenseRec.Rows[i].FindControl("cbEdit")).Checked == true)
            {
                try
                {
                    DataRow dr = dt.NewRow();
                    
                        dr["UpdateFromAccountTeam"] = ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).SelectedValue; ;
                        dr["InvoiceNumber"] = ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Text; ;
                        dr["ConfirmationNo"] = ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Text; ;
                        dr["ServeCentraleNo"] = ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Text; ;
                        dr["Remarks"] = ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Text; ;
                        dr["Unique"] =  dtExpRec.Rows[j]["Unique"].ToString();
                        dr["LastUpdatedBy"] = Session["MailID"].ToString();
                        dr["LastUpdatedON"] = DateTime.Now.ToShortDateString();
                        dr["ExpenseClosureDate"] = ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Text; ;
                        dr["Category"] = ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).SelectedValue; ;
                        dr["SubCategory"] = ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).SelectedValue; ;
                        dr["Comments"] = ((TextBox)gvExpenseRec.Rows[i].FindControl("Comments")).Text; ;
                        dr["lblSubCategory_Old"] = dtExpRec.Rows[j]["lblSubCategory_Old"].ToString();
                        dt.Rows.Add(dr);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        if (dt.Rows.Count > 0)
        {
            //Saving the datatable
            String DBConnecting = ConfigurationManager.AppSettings["DBConnectString"];
            using (SqlConnection Conn = new SqlConnection())
            {

                Conn.ConnectionString = AntiXss.HtmlEncode(DBConnecting);
                try
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = Conn;
                    cmd.CommandText = "[SP_ExpenseRecovery_BulkUpdate_EAS_Save]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter paramDB = cmd.Parameters.AddWithValue("@dtExcel", dt);

                    //string username = Session["MailID"].ToString();
                    //SqlParameter paramLastUpdatedBy =

                    paramDB.Value = dt;
                    paramDB.SqlDbType = SqlDbType.Structured;
                    paramDB.TypeName = "dbo.ExpenseRecoveryBulkUpdate_Save";
                    Conn.Open();
                    cmd.ExecuteNonQuery();
                    subCategory_top = ddlSubCategory_Top.SelectedValue;
                    customerCode = ddlCustomerCode.SelectedValue;
                    ageingBucket = ddlAgeingBucket.SelectedValue;
                    glLongText = ddlLongText.SelectedValue;
                    PM = ddlPM.SelectedValue;
                    DM = ddlDM.SelectedValue;
                    profitCentre = ddlProfitCentre.SelectedValue.ToString();
                    category_top = ddlCategory_Top.SelectedValue.ToString();
                    practiceLine = ddlPracticeLine.SelectedValue.ToString();
                    Commitment = ddlCommitments.SelectedValue.ToString();
                    DataTable dtExp = FetchExpenseData(customerCode, ageingBucket, glLongText, PM, DM, profitCentre, category_top, subCategory_top, practiceLine, Session["MailID"].ToString(), Session["Role"].ToString(), Commitment);
                    BindData(dtExp);
                    Panel1.Visible = false;
                    Lblmsg.Visible = true;
                    Lblmsg.Text = "Data Saved successfully";

                }
                catch (Exception ex)
                {
                    Lblmsg.Text = "Some Error Occured,Please try after sometime.";
                }
               
            }
            }

    }

    protected DataTable FetchExpenseData(string customerCode, string ageingBucket, string glLongText, string PM, string DM, string profitCentre, string category, string status, string practiceLine, string MailID, string role, string Commitment)
    {
        SqlCommand cmd = new SqlCommand();

        string cmdExpRec = "exec SP_ExpenseRecovery_FetchData @customerCode,@ageingBucket,@glLongText,@PM,@DM,@profitCentre,@category,@status,@practiceLine,@MailID,@role,@Commitment";
        cmd.CommandText = cmdExpRec;
        cmd.Parameters.AddWithValue("@customerCode", customerCode);
        cmd.Parameters.AddWithValue("@ageingBucket", ageingBucket);
        cmd.Parameters.AddWithValue("@glLongText", glLongText);
        cmd.Parameters.AddWithValue("@PM", PM);
        cmd.Parameters.AddWithValue("@DM", DM);
        cmd.Parameters.AddWithValue("@profitCentre", profitCentre);
        cmd.Parameters.AddWithValue("@category", category);
        cmd.Parameters.AddWithValue("@status", status);
        cmd.Parameters.AddWithValue("@practiceLine", practiceLine);
        cmd.Parameters.AddWithValue("@MailID", MailID);
        cmd.Parameters.AddWithValue("@role", role);
        cmd.Parameters.AddWithValue("@Commitment", Commitment);

        DataSet dsExpRec = dal.FetchDataSet(cmd);
        dtExpRec = dsExpRec.Tables[0];
        Session["dtExpenseRecovery"] = dtExpRec;
        return dtExpRec;
    }
    private void LoadScreen(string Role,string MailID)
    {
        if (Role == "ADMIN")
        {
            //btnDownload.Enabled = true;
            btnDownload0.Enabled = true;
        }
        BtnSave.Enabled =false;
        BtnCancel.Enabled = false;
        DataTable dtExpense = new DataTable();
        DataTable dt = new DataTable();
        if (categoryReportParam == "Billable" || categoryReportParam == "Non Billable" || categoryReportParam == "Update Needed From PM/DMs")
        {

            dtExpense = FetchExpenseData("ALL", ageingbucketReportParam, "ALL", "ALL", "ALL", "ALL", "ALL", subcategoryReportParam, practicelineReportParam, Session["MailID"].ToString(), Session["Role"].ToString(), "ALL");
            string _sqlWhere = "CategoryGroup ='" + categoryReportParam + "'";
            string _sqlOrder = "category asc";

            dt = dtExpense.Select(_sqlWhere, _sqlOrder).CopyToDataTable();
        }
        else
        {
            dtExpense = FetchExpenseData("ALL", ageingbucketReportParam, "ALL", "ALL", "ALL", "ALL", categoryReportParam, subcategoryReportParam, practicelineReportParam, Session["MailID"].ToString(), Session["Role"].ToString(), "ALL");
            dt = dtExpense;
        }

     
              //, "ALL", "ALL", "ALL", Session["MailID"].ToString(), Session["Role"].ToString(), "ALL");
          LoadDropDowns(dt);


       
     
    //    protected void BindData(string customerCode, string ageingBucket, string glLongText, string PM, string DM, string profitCentre, string category, string status, string practiceLine, string MailID, string role, string Commitment)
    //{
      BindData(dt);
    
        //if (dtaccess.Rows[0][0].ToString() == "FC3")
        //{
        //    string username = System.IO.Path.GetFileName(User.Identity.Name.ToString().ToUpper());

        //    string cmdExists = "select distinct MailID from MstAccessDetails where MailID='" +username+ "'";
        //    DataSet dsExists = BusinessLogic.GetDataSet(cmdExists);
        //   DataTable dtExists = dsExists.Tables[0];
        //   if (dtExists.Rows.Count > 0)
        //   {
               
        //       BindData(subCategory_top, customerCode, ageingBucket, glLongText, PM, DM);

        //   }
        //   else
        //   {
        //       Lblmsg.ForeColor = Color.Red;
        //       Lblmsg.Visible = true;
        //       Lblmsg.Text = "No data exists!!";
        //       gvExpenseRec.Visible = false;
        //   }

        //}
   

    }


    //protected void BindData(string customerCode, string ageingBucket, string glLongText, string PM, string DM, string profitCentre, string category, string status, string practiceLine, string MailID, string role, string Commitment)
    protected void BindData(DataTable dtExpRec)
    {
        Session["key"] = null;
       
        BtnCancel.Enabled = true;
        BtnSave.Enabled = false;
        lblChkBoxErrorMsg.Visible = false;
      
        Lblmsg.Visible = false;
        
        

        if (!(dtExpRec.Rows.Count > 0))
        {
            Lblmsg.ForeColor = Color.Red;
            Lblmsg.Visible = true;
            Lblmsg.Text = "No data exists!!";
            gvExpenseRec.Visible = false;
        }
        if (dtExpRec.Rows.Count > 0)
        {

            gvExpenseRec.DataSource = dtExpRec;
            gvExpenseRec.DataBind();
            Lblmsg.Visible = false;
            gvExpenseRec.Visible = true;
          

            int RowStart = gvExpenseRec.PageIndex * 15;
            int RowNo = gvExpenseRec.Rows.Count < 15 ? gvExpenseRec.Rows.Count : 15;


            for (int j = RowStart; j < RowNo + RowStart; j++)
            {
                int i = j - RowStart;

                ((Label)gvExpenseRec.Rows[i].FindControl("lblClientIBU")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Client IBU"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblActiveProjectCode")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Active Project Code"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblCustomerCode")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["CustomerCode"].ToString());

                ((Label)gvExpenseRec.Rows[i].FindControl("lblPM")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["PM"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblDM")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["DM"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblProfitCenter")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Profit Center"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblReference")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Reference"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblDocumentNumber")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Document Number"].ToString());


                ((Label)gvExpenseRec.Rows[i].FindControl("lblLocalCurrency")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Local Currency"].ToString());

                ((Label)gvExpenseRec.Rows[i].FindControl("lblAmntLC")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Amount in LC"].ToString());
                string textBind = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Text"].ToString());
                int lenText = textBind.Length;
                if (lenText > 30)
                    textBind = textBind.Substring(0, 30) + "...";
               
                ((Label)gvExpenseRec.Rows[i].FindControl("lblText")).Text = textBind;
                ((Label)gvExpenseRec.Rows[i].FindControl("lblText")).ToolTip = dtExpRec.Rows[j]["Text"].ToString();
              
                
                ((Label)gvExpenseRec.Rows[i].FindControl("lblPostingDate")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Posting Date"].ToString());

                ((Label)gvExpenseRec.Rows[i].FindControl("lblCountry")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Country"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblUnique")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Unique"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblAmntUSD")).Text = AntiXss.HtmlEncode(Math.Round(Convert.ToDecimal(dtExpRec.Rows[j]["Amnt in USD"]), 2).ToString());
                //New Columns

                if (!(dtExpRec.Rows[j]["ExpenseClosureDate"]).Equals(System.DBNull.Value))
                {
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Text = AntiXss.HtmlEncode(Convert.ToDateTime((dtExpRec.Rows[j]["ExpenseClosureDate"])).ToShortDateString());
                }
                else
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Text = AntiXss.HtmlEncode("");
                if (!(dtExpRec.Rows[j]["InitiatedDate"]).Equals(System.DBNull.Value))
                {
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Text = AntiXss.HtmlEncode(Convert.ToDateTime((dtExpRec.Rows[j]["InitiatedDate"])).ToShortDateString());
                }
                else
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Text = AntiXss.HtmlEncode("");
                ((Label)gvExpenseRec.Rows[i].FindControl("lblCategory")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Category"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblSubCategory")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Sub-Category"].ToString());
                //((Label)gvExpenseRec.Rows[i].FindControl("lblComments")).Text = dtExpRec.Rows[j]["Comments"].ToString();

                //

                //((Label)gvExpenseRec.Rows[i].FindControl("lblUpdatefromAccountteam")).Text = dtExpRec.Rows[j]["Update from Account team"].ToString();
                ((Label)gvExpenseRec.Rows[i].FindControl("lblInvoiceNo")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Invoice No"].ToString());
                //((Label)gvExpenseRec.Rows[i].FindControl("lblConfirmationNo")).Text = dtExpRec.Rows[j]["Confirmation No"].ToString();
                string remarksBind = dtExpRec.Rows[j]["Remarks"].ToString();
                int remarkslenth = remarksBind.Length;
                if(remarkslenth>30)
                    remarksBind = remarksBind.Substring(0, 30) + "...";

                ((Label)gvExpenseRec.Rows[i].FindControl("lblRemarks")).Text = remarksBind;
                ((Label)gvExpenseRec.Rows[i].FindControl("lblRemarks")).ToolTip = dtExpRec.Rows[j]["Remarks"].ToString();
                dtExpRec.Rows[j]["Remarks"].ToString();

                ((Label)gvExpenseRec.Rows[i].FindControl("lblStageCode")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Stage Code"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblWithWhom")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["With Whom"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblServCentraleReqNo")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Serv Centrale Req No"].ToString());
                //((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).SelectedValue = dtExpRec.Rows[j]["Update from Account team"].ToString();
                ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Invoice No"].ToString());
                //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Text = dtExpRec.Rows[j]["Confirmation No"].ToString();
                ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Serv Centrale Req No"].ToString());
                ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Remarks"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblAgeingDays")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Ageing Days"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblAgeingBucket")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Ageing Bucket"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblContractType")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Contract Type"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblVendorCode")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Vendor Code"].ToString());
                



                string gllongtextBind = AntiXss.HtmlEncode(dtExpRec.Rows[j]["G/L Acct Long Text"].ToString());
                int gllongtextlen = gllongtextBind.Length;
                if (gllongtextlen > 30)
                    gllongtextBind = gllongtextBind.Substring(0, 30) + "...";



                ((Label)gvExpenseRec.Rows[i].FindControl("lblGLAcctLongText")).Text = gllongtextBind;
                ((Label)gvExpenseRec.Rows[i].FindControl("lblGLAcctLongText")).ToolTip = dtExpRec.Rows[j]["G/L Acct Long Text"].ToString();
                ((Label)gvExpenseRec.Rows[i].FindControl("lblUpdate")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Update"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblLastUpdatedBY")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["LastUpdatedBY"].ToString());
                ((Label)gvExpenseRec.Rows[i].FindControl("lblLastUpdatedOn")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["LastUpdatedON"].ToString());

                ((Label)gvExpenseRec.Rows[i].FindControl("lblResponseFromFinance")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["ResponseFromFinance"].ToString());

                ((Label)gvExpenseRec.Rows[i].FindControl("lblSubCategory_Old")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["SubCategory_Old"].ToString());
                //((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = false;

                //((Label)gvExpenseRec.Rows[i].FindControl("lblUpdatefromAccountteam")).Visible = true;
                ((Label)gvExpenseRec.Rows[i].FindControl("lblInvoiceNo")).Visible = true;
                //((Label)gvExpenseRec.Rows[i].FindControl("lblConfirmationNo")).Visible = true;
                ((Label)gvExpenseRec.Rows[i].FindControl("lblServCentraleReqNo")).Visible = true;
                ((Label)gvExpenseRec.Rows[i].FindControl("lblRemarks")).Visible = true;
                ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
                ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;
                ((Label)gvExpenseRec.Rows[i].FindControl("lblCategory")).Visible = true;
                ((Label)gvExpenseRec.Rows[i].FindControl("lblSubCategory")).Visible = true;
                //((Label)gvExpenseRec.Rows[i].FindControl("lblComments")).Visible = true;
                ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnDate")).Enabled = false;
                ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnInitatedDate")).Enabled = false;

              
                Label lblSubCategory_Old = (Label)gvExpenseRec.Rows[i].FindControl("lblSubCategory_Old");
                string SubCategory_Old = lblSubCategory_Old.Text;

                if (SubCategory_Old == "Past Commitment")
                {

                    gvExpenseRec.Rows[i].ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void LoadDropDowns(DataTable dtData)
    {

        
        //foreach (DataRow row in dtData.Rows)
        //    {
        //        foreach (DataColumn col in dtData.Columns)
        //            {
        //                if (col.DataType == typeof(System.String))
        //                {
        //                    row[col] = row[col].ToString().Trim();
        //                }
        //            }

        //    }
        DataTable dt=dtData;
        Session.Add("ExpenseRecoveryData", dtData);
       // categoryReportParam = "Billable";
       

        dt.DefaultView.Sort = "Ageing Bucket";
        ddlAgeingBucket.DataSource = dt.DefaultView.ToTable(true, "Ageing Bucket");
        ddlAgeingBucket.DataTextField = "Ageing Bucket";
        ddlAgeingBucket.DataBind();
        ddlAgeingBucket.SelectedValue = ageingbucketReportParam;

        dt.DefaultView.Sort = "CustomerCode";
        ddlCustomerCode.DataSource = dt.DefaultView.ToTable(true, "CustomerCode");
        ddlCustomerCode.DataTextField = "CustomerCode";
        ddlCustomerCode.DataBind();


        dt.DefaultView.Sort = "Profit Center";
        ddlProfitCentre.DataSource = dt.DefaultView.ToTable(true, "Profit Center");
        ddlProfitCentre.DataTextField = "Profit Center";
        ddlProfitCentre.DataBind();
     

        dt.DefaultView.Sort = "PracticeLine";
        ddlPracticeLine.DataSource = dt.DefaultView.ToTable(true, "PracticeLine");
        ddlPracticeLine.DataTextField = "PracticeLine";
        ddlPracticeLine.DataBind();
        
        ddlPracticeLine.SelectedValue = practicelineReportParam;

        dt.DefaultView.Sort = "G/L Acct Long Text";
        ddlLongText.DataSource = dt.DefaultView.ToTable(true, "G/L Acct Long Text");
        ddlLongText.DataTextField = "G/L Acct Long Text";
        ddlLongText.DataBind();
        
        dt.DefaultView.Sort = "Sub-Category";
        ddlSubCategory_Top.DataSource = dt.DefaultView.ToTable(true, "Sub-Category");
        ddlSubCategory_Top.DataTextField = "Sub-Category";
        ddlSubCategory_Top.DataBind();
        ddlSubCategory_Top.SelectedValue =subcategoryReportParam;

       

       dt.DefaultView.Sort = "category";
       ddlCategory_Top.DataSource = dt.DefaultView.ToTable(true, "category");
       ddlCategory_Top.DataTextField = "category";
       ddlCategory_Top.DataBind();
       if (categoryReportParam == "Billable" || categoryReportParam == "Non Billable" || categoryReportParam == "Update Needed From PM/DMs")
       {
           ddlCategory_Top.SelectedValue = "ALL";
       }
       else
       {
           ddlCategory_Top.SelectedValue = categoryReportParam;
       }
        

        dt.DefaultView.Sort = "PM";
        ddlPM.DataSource = dt.DefaultView.ToTable(true, "PM");
        ddlPM.DataTextField = "PM";
        ddlPM.DataBind();
       

        dt.DefaultView.Sort = "DM";
        ddlDM.DataSource = dt.DefaultView.ToTable(true, "DM");
        ddlDM.DataTextField = "DM";
        ddlDM.DataBind();

        if (categoryReportParam == "Billable" || categoryReportParam == "Non Billable" || categoryReportParam == "Update Needed From PM/DMs")
        {
            ddlCategory_Top.SelectedValue = "ALL";
        }
        else
        {
            ddlCategory_Top.SelectedValue = categoryReportParam;
        }
        ddlSubCategory_Top.SelectedValue = subcategoryReportParam;
        ddlAgeingBucket.SelectedValue = ageingbucketReportParam;
        ddlPracticeLine.SelectedValue = practicelineReportParam;
    }
    protected void gvExpenseRec_ExceptionRowDataBound(object sender, GridViewRowEventArgs e)
    {
        foreach (TableCell tc in e.Row.Cells)
        {
            tc.Attributes["style"] = "border-color: black";
           
        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label lblCategory = (Label)e.Row.FindControl("lblCategory");

        //    //DropDownList dpcategory = (DropDownList)e.Row.FindControl("ddlCategory");
        //    string value = lblCategory.Text;


        //    if (value == "Past Commitment")
        //    {
        //        e.Row.ForeColor = Color.Red;
        //    }

        //}  

    }
    private bool isEditMode = false;
    protected bool IsInEditMode
    {

        get { return this.isEditMode; }

        set { this.isEditMode = value; }

    }
    //private void DownloadExcel(DataSet ds)
    //{
    //    gvDownloadData.Visible = true;
    //    gvDownloadData.DataSource = ds.Tables[0];
    //    gvDownloadData.DataBind();
    //    try
    //    {
    //        Response.ContentType = "application/vnd.ms-excel";
    //        Response.AddHeader("Content-Disposition", "attachment; filename=Expense Recovery.xls");
    //        Response.Charset = String.Empty;
    //        StringWriter excelWriter = new StringWriter();
    //        HtmlTextWriter myHtmlTextWriter = new HtmlTextWriter(excelWriter);
    //        gvDownloadData.RenderControl(myHtmlTextWriter);
    //        Response.Write(excelWriter.ToString());
    //        Response.End();
    //    }
    //    catch
    //    {
    //        Lblmsg.Visible = true;
    //        Lblmsg.Text = "Error encountered. Please try again later.";
    //        Lblmsg.ForeColor = System.Drawing.Color.Red;
    //    }
    //    gvDownloadData.Visible = false;
    //    gvDownloadData.Columns[0].Visible = false;      

    //}

    //private void DownloadExcel2(DataTable dt)
    //{
        
    //        if (dt == null || dt.Columns.Count == 0)
    //            throw new Exception("ExportToExcel: Null or empty input table!\n");



    //        Microsoft.Office.Interop.Excel.Application WRExcel = new Microsoft.Office.Interop.Excel.Application();
    //        Microsoft.Office.Interop.Excel.Workbooks WRwbs = null;
    //        //Microsoft.Office.Interop.Excel.Workbook WRwb = new Microsoft.Office.Interop.Excel.Workbook();
    //        object objOpt = System.Reflection.Missing.Value;
    //        Microsoft.Office.Interop.Excel.Workbook WRwb = WRExcel.Workbooks.Add(objOpt);




    //        WRExcel.Visible = false;
    //        WRwbs = WRExcel.Workbooks;


    //        //WRwb = WRwbs.Open(folderadress, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt,
    //        //    objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);
    //        Microsoft.Office.Interop.Excel.Worksheet excelSheet1 = new Excel.Worksheet();

    //        //ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
    //        //Microsoft.Office.Interop.Excel.Worksheet excelSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)WRss.get_Item("ExpenseRecovery");
    //        // excelSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)WRwb.Sheets.Add("ExpenseRecovery");
    //        Microsoft.Office.Interop.Excel.Sheets WRss = null;
    //        try
    //        {
    //        WRss = WRwb.Sheets;

    //        excelSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)WRss.Add(WRss[1], Type.Missing, Type.Missing, Type.Missing);
    //        excelSheet1.Name = "ExpenseRecovery";

    //        String excelFile1 = "~\\App_Data\\ExpenseRecovery.xlsx";
    //        String destPath = Server.MapPath(excelFile1);
    //        if (File.Exists(destPath))
    //        {
    //            File.Delete(destPath);
    //        }
    //        // check fielpath







    //        DataTable dt1 = new DataTable();

    //        // column headings
    //        for (int i = 0; i < dt.Columns.Count; i++)
    //        {
    //            excelSheet1.Cells[1, (i + 1)] = dt.Columns[i].ColumnName;

    //        }


    //        int rows = dt.Rows.Count;
    //        int columns = dt.Columns.Count;
    //        int r = 0; int c = 0;
    //        object[,] DataArray = new object[rows + 1, columns + 1];
    //        for (c = 0; c <= columns - 1; c++)
    //        {
    //            DataArray[r, c] = dt.Columns[c].ColumnName;
    //            for (r = 0; r <= rows - 1; r++)
    //            {
    //                DataArray[r, c] = dt.Rows[r][c];
    //            } //end row loop
    //        } //end column loop

    //        Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)excelSheet1.Cells[2, 1];
    //        Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)excelSheet1.Cells[1 + dt.Rows.Count, dt.Columns.Count];
    //        Microsoft.Office.Interop.Excel.Range range = excelSheet1.get_Range(c1, c2);


    //        //Fill Array in Excel
    //        range.Value2 = DataArray;
    //        //      public void FormatRange(Microsoft.Office.Interop.Excel.Worksheet excel, Microsoft.Office.Interop.Excel.Range pt1, Microsoft.Office.Interop.Excel.Range pt2, Color bckColor, Color fontColor, bool fontWeight, Color gridlinesColor, Microsoft.Office.Interop.Excel.XlHAlign alignmentflag, bool isDelete, string MergedText)

    //        Microsoft.Office.Interop.Excel.Range h1 = (Microsoft.Office.Interop.Excel.Range)excelSheet1.Cells[1, 1];
    //        Microsoft.Office.Interop.Excel.Range h2 = (Microsoft.Office.Interop.Excel.Range)excelSheet1.Cells[dt.Rows.Count, dt.Columns.Count - 1];
    //        FormatRange(excelSheet1, h1, h2, Color.Beige, Color.Black, true, Color.Black, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft, false, "");

    //        if (destPath != null && destPath != "")
    //        {
    //            WRwb.SaveCopyAs(destPath);
    //            WRwb.Close(false);
    //            WRExcel.Quit();
    //            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRwb);
    //            // System.Runtime.InteropServices.Marshal.FinalReleaseComObject(oModule);
    //            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRExcel);
    //            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRss);
    //            GC.Collect();
    //            // DownloadFileProject(filename);
    //            Session["key"] = "ExpenseRecovery.xlsx";
    //            //updatepanel1.Update();

    //            iframe.Attributes.Add("src", "DownloadFile.aspx");


    //        }

    //        else
    //        {
    //            Lblmsg.Visible = true;
    //            Lblmsg.ForeColor = Color.Red;
    //            Lblmsg.Text = "Some error occurred..File cannot be downloaded";
    //        }







    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception("ExportToExcel: \n" + ex.Message);
    //    }
    //    finally
    //    {

    //        GC.Collect();
    //        GC.WaitForPendingFinalizers();
          

    //        if (WRss != null)
    //        {
    //            //wksht.Delete();
    //            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRss);
    //            WRss = null;
    //        }

    //        if (WRwb != null)
    //        {
    //            //wksht.Delete();
    //            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRwb);
    //            WRwb = null;
    //        }
    //        if (WRwbs != null)
    //        {
    //            //wksht.Delete();
    //            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRwbs);
    //            WRwbs = null;
    //        }
    //        if (WRExcel != null)
    //        {
    //            WRExcel.Quit();
    //            int hWnd1 = WRExcel.Application.Hwnd;
    //            TryKillProcessByMainWindowHwnd(hWnd1);

    //            //wksht.Delete();
    //            //WRExcel.Quit();
    //            //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRExcel);
    //            //WRExcel = null;




    //        }
    //        if (excelSheet1 != null)
    //        {
    //            //wksht.Delete();

    //            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelSheet1);
    //            excelSheet1 = null;

    //        }

    //        GC.Collect();
    //        GC.WaitForPendingFinalizers();
    //        GC.Collect();
    //        GC.WaitForPendingFinalizers();


    //    }

    //     PopUp("File Saved");
    //}


    private void DownloadExcel2(DataTable dt)
    {
        try
        {
            if (dt == null || dt.Columns.Count == 0)
                throw new Exception("ExportToExcel: Null or empty input table!\n");

            string folderadress = @"~/App_Data\ExpenseRecovery_DownloadTemplate.xlsx";
            folderadress = HttpContext.Current.Server.MapPath(folderadress);
            string storefolderadress = @"~/Template";
            storefolderadress = HttpContext.Current.Server.MapPath(storefolderadress);
            Microsoft.Office.Interop.Excel.Application WRExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks WRwbs = null;
            //Microsoft.Office.Interop.Excel.Workbook WRwb = new Microsoft.Office.Interop.Excel.Workbook();
            object objOpt = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Excel.Workbook WRwb = WRExcel.Workbooks.Add(objOpt);
            Microsoft.Office.Interop.Excel._Worksheet WRws = null;



            WRExcel.Visible = false;
            WRwbs = WRExcel.Workbooks;


            WRwb = WRwbs.Open(folderadress, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt,
                objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);
            Microsoft.Office.Interop.Excel.Sheets WRss = null;
            WRss = WRwb.Sheets;



            String excelFile1 = "~\\App_Data\\ExpenseRecovery.xlsx";
            String destPath = Server.MapPath(excelFile1);
            if (File.Exists(destPath))
            {
                File.Delete(destPath);
            }
            // check fielpath


            Microsoft.Office.Interop.Excel.Worksheet excelSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)WRss.get_Item("ExpenseRecovery");



            int ColumnIndex = 0;
            int rowIndex = 0;
            DataTable dt1 = new DataTable();

            // column headings
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                excelSheet1.Cells[1, (i + 1)] = dt.Columns[i].ColumnName;

            }


            int rows = dt.Rows.Count;
            int columns = dt.Columns.Count;
            int r = 0; int c = 0;
            object[,] DataArray = new object[rows + 1, columns + 1];
            for (c = 0; c <= columns - 1; c++)
            {
                DataArray[r, c] = dt.Columns[c].ColumnName;
                for (r = 0; r <= rows - 1; r++)
                {
                    DataArray[r, c] = dt.Rows[r][c];
                } //end row loop
            } //end column loop

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)excelSheet1.Cells[2, 1];
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)excelSheet1.Cells[1 + dt.Rows.Count, dt.Columns.Count];
            Microsoft.Office.Interop.Excel.Range range = excelSheet1.get_Range(c1, c2);


            //Fill Array in Excel
            range.Value2 = DataArray;


            if (destPath != null && destPath != "")
            {
                WRwb.SaveCopyAs(destPath);
                WRwb.Close(false);
                WRExcel.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRwb);
                // System.Runtime.InteropServices.Marshal.FinalReleaseComObject(oModule);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRExcel);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRss);
                GC.Collect();
                // DownloadFileProject(filename);
                Session["key"] = "ExpenseRecovery.xlsx";
                //updatepanel1.Update();

                iframe.Attributes.Add("src", "DownloadFile.aspx");


            }

            else
            {
                Lblmsg.Visible = true;
                Lblmsg.ForeColor = Color.Red;
                Lblmsg.Text = "Some error occurred..File cannot be downloaded";
            }







        }
        catch (Exception ex)
        {
            throw new Exception("ExportToExcel: \n" + ex.Message);
        }

        PopUp("File Saved");
    }
    private void RunMacro(object oApp, object[] oRunArgs)
    {
        oApp.GetType().InvokeMember("Run", System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.InvokeMethod, null, oApp, oRunArgs);
    }

   
    protected void ddlSubCategory_Top_SelectedIndexChanged(object sender, EventArgs e)
    {
        Lblmsg.Visible = false;
        subCategory_top = ddlSubCategory_Top.SelectedValue;
        customerCode = ddlCustomerCode.SelectedValue;
        ageingBucket = ddlAgeingBucket.SelectedValue;
        glLongText = ddlLongText.SelectedValue;
        PM = ddlPM.SelectedValue;
        DM = ddlDM.SelectedValue;

         
        
        profitCentre = ddlProfitCentre.SelectedValue.ToString();
        category_top = ddlCategory_Top.SelectedValue.ToString();
        practiceLine = ddlPracticeLine.SelectedValue.ToString();
        Commitment = ddlCommitments.SelectedValue.ToString();
        DataTable dtExp=FetchExpenseData(customerCode, ageingBucket, glLongText, PM, DM, profitCentre, category_top, subCategory_top, practiceLine, Session["MailID"].ToString(), Session["Role"].ToString(), Commitment);
        BindData(dtExp);
        
    }
    
    protected void ddlCustomerCode_SelectedIndexChanged(object sender, EventArgs e)
    {

        Lblmsg.Visible = false;
        subCategory_top = ddlSubCategory_Top.SelectedValue;
        customerCode = ddlCustomerCode.SelectedValue;
        ageingBucket = ddlAgeingBucket.SelectedValue;
        glLongText = ddlLongText.SelectedValue;
        PM = ddlPM.SelectedValue;
        DM = ddlDM.SelectedValue;
         
        
        profitCentre = ddlProfitCentre.SelectedValue.ToString();
        category_top = ddlCategory_Top.SelectedValue.ToString();
        practiceLine = ddlPracticeLine.SelectedValue.ToString();
        Commitment = ddlCommitments.SelectedValue.ToString();
        DataTable dtExp = FetchExpenseData(customerCode, ageingBucket, glLongText, PM, DM, profitCentre, category_top, subCategory_top, practiceLine, Session["MailID"].ToString(), Session["Role"].ToString(), Commitment);
        BindData(dtExp);

    }
    protected void ddlAgeingBucket_SelectedIndexChanged(object sender, EventArgs e)
    {
          Lblmsg.Visible = false;
        subCategory_top = ddlSubCategory_Top.SelectedValue;
        customerCode = ddlCustomerCode.SelectedValue;
        ageingBucket = ddlAgeingBucket.SelectedValue;
        glLongText = ddlLongText.SelectedValue;
        PM = ddlPM.SelectedValue;
        DM = ddlDM.SelectedValue;
         
         
        profitCentre = ddlProfitCentre.SelectedValue.ToString();
        category_top = ddlCategory_Top.SelectedValue.ToString();
        practiceLine = ddlPracticeLine.SelectedValue.ToString();
        Commitment = ddlCommitments.SelectedValue.ToString();
        DataTable dtExp = FetchExpenseData(customerCode, ageingBucket, glLongText, PM, DM, profitCentre, category_top, subCategory_top, practiceLine, Session["MailID"].ToString(), Session["Role"].ToString(), Commitment);
        BindData(dtExp);
    }
    protected void ddlLongText_SelectedIndexChanged(object sender, EventArgs e)
    {
        Lblmsg.Visible = false;
        subCategory_top = ddlSubCategory_Top.SelectedValue;
        customerCode = ddlCustomerCode.SelectedValue;
        ageingBucket = ddlAgeingBucket.SelectedValue;
        glLongText = ddlLongText.SelectedValue;
        PM = ddlPM.SelectedValue;
        DM = ddlDM.SelectedValue;

         
         
        profitCentre = ddlProfitCentre.SelectedValue.ToString();
        category_top = ddlCategory_Top.SelectedValue.ToString();
        practiceLine = ddlPracticeLine.SelectedValue.ToString();
        Commitment = ddlCommitments.SelectedValue.ToString();
        DataTable dtExp = FetchExpenseData(customerCode, ageingBucket, glLongText, PM, DM, profitCentre, category_top, subCategory_top, practiceLine, Session["MailID"].ToString(), Session["Role"].ToString(), Commitment);
        BindData(dtExp);


    }
    protected void ddlCommitments_SelectedIndexChanged(object sender, EventArgs e)
    {
        Lblmsg.Visible = false;
        subCategory_top = ddlSubCategory_Top.SelectedValue;
        customerCode = ddlCustomerCode.SelectedValue;
        ageingBucket = ddlAgeingBucket.SelectedValue;
        glLongText = ddlLongText.SelectedValue;
        PM = ddlPM.SelectedValue;
        DM = ddlDM.SelectedValue;



        profitCentre = ddlProfitCentre.SelectedValue.ToString();
        category_top = ddlCategory_Top.SelectedValue.ToString();
        practiceLine = ddlPracticeLine.SelectedValue.ToString();
        Commitment = ddlCommitments.SelectedValue.ToString();
        DataTable dtExp = FetchExpenseData(customerCode, ageingBucket, glLongText, PM, DM, profitCentre, category_top, subCategory_top, practiceLine, Session["MailID"].ToString(), Session["Role"].ToString(), Commitment);
        BindData(dtExp);
    }
    
      public override void VerifyRenderingInServerForm(Control control)
    {
   
    }
      protected void PopUp(string msg)
      {
          ScriptManager.RegisterStartupScript(Page, typeof(Page), "", "alert('" + msg + "');", true);
      } // EO PopUp()
      public void Message(String msg)
      {
          string script = "window.onload = function(){ alert('";
          script += msg;
          script += "');";
          script += "window.location = '";
          script += "'; }";
          ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
      }

    public void Message1()
    {
        Message("For updating the records, the excel should be downloaded using Download Bulk Update Template button");
    }

    protected void btnDownload_Click1(object sender, EventArgs e)
    {
        Session["key"] = null;
        Lblmsg.Visible = false;
        Message1();
        //Response.Write("<script language='javascript'>alert('For updating the records, the excel should be downloaded using Download Bulk Update Template button');</script>");
        subCategory_top = ddlSubCategory_Top.SelectedValue;
        customerCode = ddlCustomerCode.SelectedValue;
        ageingBucket = ddlAgeingBucket.SelectedValue;
        glLongText = ddlLongText.SelectedValue;
        PM = ddlPM.SelectedValue;
        DM = ddlDM.SelectedValue;


        profitCentre = ddlProfitCentre.SelectedValue;
        category_top = ddlCategory_Top.SelectedValue;
        practiceLine = ddlPracticeLine.SelectedValue.ToString();
        Commitment = ddlCommitments.SelectedValue.ToString();

        SqlCommand cmd = new SqlCommand();

        string cmdExpRec = "exec LoadExpenseRecoveryGrid_DownLoad @subCategory_top,@customerCode,@ageingBucket,@glLongText,@PM,@DM,@role,@MailID,@profitCentre,@category_top,@practiceLine,@Commitment";
        cmd.CommandText = cmdExpRec;
        cmd.Parameters.AddWithValue("@subCategory_top", subCategory_top);
        cmd.Parameters.AddWithValue("@customerCode", customerCode);
        cmd.Parameters.AddWithValue("@ageingBucket", ageingBucket);
        cmd.Parameters.AddWithValue("@glLongText", glLongText);
        cmd.Parameters.AddWithValue("@PM", PM);
        cmd.Parameters.AddWithValue("@DM", DM);
        cmd.Parameters.AddWithValue("@profitCentre", profitCentre);
        cmd.Parameters.AddWithValue("@category_top", category_top);
        
        cmd.Parameters.AddWithValue("@practiceLine", practiceLine);
        cmd.Parameters.AddWithValue("@MailID", Session["MailID"].ToString());
        cmd.Parameters.AddWithValue("@role", role);
        cmd.Parameters.AddWithValue("@Commitment", Commitment);

        DataSet dsExpRec = dal.FetchDataSet(cmd);


        //string cmdExpRec = "exec [LoadExpenseRecoveryGrid_DownLoad] '" + subCategory_top + "','" + customerCode + "','" + ageingBucket + "','" + glLongText + "','" + PM + "','" + DM + "','" + role + "','" + MailID + "','" + profitCentre + "','" + category_top + "','" + practiceLine + "'";
        //DataSet dsExpRec = BusinessLogic.GetDataSet(cmdExpRec);
        DownloadExcel2(dsExpRec.Tables[0]);
    }
 
  
    protected void gvExpenseRec_ExceptionPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Lblmsg.Visible = false;
        gvExpenseRec.PageIndex = e.NewPageIndex;
        if (Page.IsPostBack)
        {
            subCategory_top = ddlSubCategory_Top.SelectedValue;
            customerCode = ddlCustomerCode.SelectedValue;
            ageingBucket = ddlAgeingBucket.SelectedValue;
            glLongText = ddlLongText.SelectedValue;
            PM = ddlPM.SelectedValue;
            DM = ddlDM.SelectedValue;
            profitCentre = ddlProfitCentre.SelectedValue.ToString();
            category_top = ddlCategory_Top.SelectedValue.ToString();
            practiceLine = ddlPracticeLine.SelectedValue.ToString();
            Commitment = ddlCommitments.SelectedValue.ToString();
            DataTable dtExp = FetchExpenseData(customerCode, ageingBucket, glLongText, PM, DM, profitCentre, category_top, subCategory_top, practiceLine, Session["MailID"].ToString(), Session["Role"].ToString(), Commitment);
            BindData(dtExp);
        }
    }
       protected void chkChange_Click(object sender, EventArgs e)
         {
             Session["key"] = null;
    //protected void BtnEdit_Click(object sender, EventArgs e)
    //{
             Lblmsg.Visible = false;
        lblChkBoxErrorMsg.Visible = false;
        DataSet ds=BusinessLogic.GetDataSet("select * from Expense_category");
        DataSet dsCategory=BusinessLogic.GetDataSet("select distinct  Category from expense_Category");
        
        CbEditClicks = 0;
        int RowStart = gvExpenseRec.PageIndex * 15;
        int RowNo = gvExpenseRec.Rows.Count < 15 ? gvExpenseRec.Rows.Count : 15;
        for (int j = RowStart; j < RowNo + RowStart; j++)
        {
            int i = j - RowStart;
            if (((CheckBox)gvExpenseRec.Rows[i].FindControl("cbEdit")).Checked == true)
            { CbEditClicks++; }
        }
        if (CbEditClicks > 1)
        {
            lblChkBoxErrorMsg.ForeColor = Color.Red;
            lblChkBoxErrorMsg.Visible = true;
            //Message("Only one row can be updated at a time , for bulk update use the Download Bulk Update Template button.");
        CbEditClicks = 0;
        }
        else
        {
            btnDownload0.Enabled = false;
            //btnDownload.Enabled = false;
            bool chkstatus = false;
            BtnCancel.Enabled = true;          
            BtnSave.Enabled = true;

            int RowStart1 = gvExpenseRec.PageIndex * 15;
            int RowNo1 = gvExpenseRec.Rows.Count < 15 ? gvExpenseRec.Rows.Count : 15;
            for (int j = RowStart1; j < RowNo1 + RowStart1; j++)
            {
                int i = j - RowStart1;
               
                //DropDownList ddlUpdatefromAccountteam = ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam"));

                ////ddlUpdatefromAccountteam.DataSource = ds.Tables[0];
                ////ddlUpdatefromAccountteam.DataBind();
                //for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                //{
                //    ddlUpdatefromAccountteam.Items.Add(ds.Tables[0].Rows[count][0].ToString());
                //}
                DropDownList ddlCategory = ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory"));
                ddlCategory.Items.Clear();
                for (int count = 0; count < dsCategory.Tables[0].Rows.Count; count++)
                {
                    ddlCategory.Items.Add(dsCategory.Tables[0].Rows[count][0].ToString());
                }
                ddlCategory.Items.Insert(0, "--Select--");
               
                DropDownList ddlSubCategory = ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory"));
                ddlSubCategory.Items.Clear();
                string strcategory = ((Label)gvExpenseRec.Rows[i].FindControl("lblCategory")).Text;

                SqlCommand cmd = new SqlCommand();

                string cmdExpRec = "select distinct Subcategory from expense_Category where Category=@strcategory";
                cmd.CommandText = cmdExpRec;
                cmd.Parameters.AddWithValue("@strcategory", strcategory);


                DataSet dsSubCategory = dal.FetchDataSet(cmd);
                //DataSet dsSubCategory = BusinessLogic.GetDataSet("select distinct Subcategory from expense_Category where Category='" + strcategory + "'");
                if (strcategory == "Update Pending")
                {

                    ddlSubCategory.Items.Insert(0, "--Select--");
                }
                else
                {
                    for (int count = 0; count < dsSubCategory.Tables[0].Rows.Count; count++)
                    {
                        ddlSubCategory.Items.Add(dsSubCategory.Tables[0].Rows[count][0].ToString());
                    }
                    ddlSubCategory.Items.Insert(0, "--Select--");
                }
               
                

                if (((Label)gvExpenseRec.Rows[i].FindControl("lblCategory")).Text == "Update Pending")
                    ddlCategory.SelectedValue = "--Select--";
                else
                ddlCategory.SelectedValue = AntiXss.HtmlEncode(((Label)gvExpenseRec.Rows[i].FindControl("lblCategory")).Text);

                if (((Label)gvExpenseRec.Rows[i].FindControl("lblCategory")).Text == "Update Pending")
                    ddlSubCategory.SelectedValue = "--Select--";
                else
                    ddlSubCategory.SelectedValue = AntiXss.HtmlEncode(((Label)gvExpenseRec.Rows[i].FindControl("lblSubCategory")).Text);

                

                //if (!(dtExpRec.Rows[j]["ExpenseClosureDate"]).Equals(System.DBNull.Value))
                //   ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).SelectedDate = Convert.ToDateTime(dtExpRec.Rows[j]["ExpenseClosureDate"]).Date;
                ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Text = AntiXss.HtmlEncode(((Label)gvExpenseRec.Rows[i].FindControl("lblInvoiceNo")).Text);
                ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Text = AntiXss.HtmlEncode(((Label)gvExpenseRec.Rows[i].FindControl("lblServCentraleReqNo")).Text);
                //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Text = AntiXss.HtmlEncode(((Label)gvExpenseRec.Rows[i].FindControl("lblRemarks")).Text);

                if (((CheckBox)gvExpenseRec.Rows[i].FindControl("cbEdit")).Checked == true)
                {
                    chkstatus = true;
                    EditRowIndex = i;
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblText")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Text"].ToString());
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblGLAcctLongText")).Text = AntiXss.HtmlEncode(dtExpRec.Rows[j]["G/L Acct Long Text"].ToString());
                    

                    if ((((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).SelectedValue == "Expense Billed"))
                    {
                        ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = false;
                        ((RegularExpressionValidator)gvExpenseRec.Rows[i].FindControl("RegularExpressionValidator1")).Enabled = true;
                        ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator1")).Enabled = true;
                        //((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("reqValDate")).Enabled = true;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = true;
                        //((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
                        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
                        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
                        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = false;

                        ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;
                        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnDate")).Enabled = false;
                        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnInitatedDate")).Enabled = true;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = false;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).Visible = true;
                    }
                    else if (((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).SelectedValue == "Reversal Pending with Finance")
                    {
                        ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = false;
                        ((RegularExpressionValidator)gvExpenseRec.Rows[i].FindControl("RegularExpressionValidator1")).Enabled = true;
                        ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator1")).Enabled = true;
                        //((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("reqValDate")).Enabled = true;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = false;
                        //((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
                        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
                        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
                        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = true;

                        ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;
                        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnDate")).Enabled = false;
                        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnInitatedDate")).Enabled = true;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = false;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).Visible = true;
                    }
                    else if (((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).SelectedValue == "Expense To Be Billed")
                    {

                        //((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = true;
                        
                        ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = false;
                        ((RegularExpressionValidator)gvExpenseRec.Rows[i].FindControl("RegularExpressionValidator1")).Enabled = false;
                        //((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("reqValDate")).Enabled = true;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = false;
                        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
                        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
                        //((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
                        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = false;

                        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtComments")).Visible = true;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = true;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).Visible = false;
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;
                        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnDate")).Enabled = true;
                        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnInitatedDate")).Enabled = false;
                    }
                    else if (((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).SelectedValue == "Reversal Pending with PM/DM")
                    {

                        //((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = true;
                        
                        ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = false;
                        ((RegularExpressionValidator)gvExpenseRec.Rows[i].FindControl("RegularExpressionValidator1")).Enabled = false;
                        //((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("reqValDate")).Enabled = true;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = false;
                        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
                        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
                        //((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
                        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = true;
                        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnDate")).Enabled = true;
                        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnInitatedDate")).Enabled = false;
                        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtComments")).Visible = true;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = true;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).Visible = false;
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;
                    }
                    else if (((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).SelectedValue == "--Select--")
                    {

                        //((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = true;

                     
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = false;
                        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
                        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
                        //((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
                        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = false;

                        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtComments")).Visible = true;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = false;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).Visible = false;
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;
                    }
                   

                    //((Label)gvExpenseRec.Rows[i].FindControl("lblUpdatefromAccountteam")).Visible = false;
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblInvoiceNo")).Visible = false;
                    //((Label)gvExpenseRec.Rows[i].FindControl("lblConfirmationNo")).Visible = false;
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblServCentraleReqNo")).Visible = false;
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblRemarks")).Visible = false;
             
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblCategory")).Visible = false;
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblSubCategory")).Visible = false;
                    //((Label)gvExpenseRec.Rows[i].FindControl("lblComments")).Visible = false;
                    //((TextBox)gvExpenseRec.Rows[i].FindControl("txtCal")).Text = null;
                    Lblmsg.Visible = false;
                    //lblStatus.Visible = false;
                    Lblmsg.Visible = false;
                }

                else 
                {
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
                    
                    ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = false;

                    ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;
                    
                    ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).Visible = false;

                    if (!(dtExpRec.Rows[j]["ExpenseClosureDate"]).Equals(System.DBNull.Value))
                    {
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Text = AntiXss.HtmlEncode(Convert.ToDateTime((dtExpRec.Rows[j]["ExpenseClosureDate"])).ToShortDateString());
                    }
                    else
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Text = AntiXss.HtmlEncode("");
                    if (!(dtExpRec.Rows[j]["InitiatedDate"]).Equals(System.DBNull.Value))
                    {
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Text = AntiXss.HtmlEncode(Convert.ToDateTime((dtExpRec.Rows[j]["InitiatedDate"])).ToShortDateString());
                    }
                    else
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Text = AntiXss.HtmlEncode("");


                    ((Label)gvExpenseRec.Rows[i].FindControl("lblCategory")).Visible = true;
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblSubCategory")).Visible = true;
                    ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = false;
                    ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = false;

                    ((Label)gvExpenseRec.Rows[i].FindControl("lblInvoiceNo")).Visible = true;
                    ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = false;

                    ((Label)gvExpenseRec.Rows[i].FindControl("lblRemarks")).Visible = true;
                    ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = false;

                    ((Label)gvExpenseRec.Rows[i].FindControl("lblServCentraleReqNo")).Visible = true;
                    ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = false;
                    string remarksBind = dtExpRec.Rows[j]["Remarks"].ToString();
                    int remarkslenth = remarksBind.Length;
                    if (remarkslenth > 30)
                        remarksBind = remarksBind.Substring(0, 30) + "...";

                    ((Label)gvExpenseRec.Rows[i].FindControl("lblRemarks")).Text = remarksBind;
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblRemarks")).ToolTip = dtExpRec.Rows[j]["Remarks"].ToString();
                    string gllongtextBind = AntiXss.HtmlEncode(dtExpRec.Rows[j]["G/L Acct Long Text"].ToString());
                    int gllongtextlen = gllongtextBind.Length;
                    if (gllongtextlen > 30)
                        gllongtextBind = gllongtextBind.Substring(0, 30) + "...";

                    string textBind = AntiXss.HtmlEncode(dtExpRec.Rows[j]["Text"].ToString());
                    int lenText = textBind.Length;
                    if (lenText > 30)
                        textBind = textBind.Substring(0, 30) + "...";
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblText")).Text = textBind;
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblText")).ToolTip = dtExpRec.Rows[j]["Text"].ToString();
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblGLAcctLongText")).Text = gllongtextBind;
                    ((Label)gvExpenseRec.Rows[i].FindControl("lblGLAcctLongText")).ToolTip = dtExpRec.Rows[j]["G/L Acct Long Text"].ToString();
                    ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnDate")).Enabled = false;
                    ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnInitatedDate")).Enabled = false;
                }
            }
            //if (chkstatus == false)
            //{
            //    Lblmsg.Visible = true;
            //    Lblmsg.ForeColor = Color.Red;
            //    Lblmsg.Text = "Please select some records";
            //    BtnEdit.Enabled = true;
            //    BtnSave.Enabled = false;
            //    BtnCancel.Enabled = false;

            //}
        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Session["key"] = null;
        Response.Redirect("ExpenseRecovery.aspx");
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        Session["key"] = null;
        //SaveNew();
        Lblmsg.Visible = false;
        BtnSave.Enabled = false;
        BtnCancel.Enabled = false;
        if (role == "ADMIN")
        {
            //btnDownload.Enabled = true;
            btnDownload0.Enabled = true;
        }

        

         int RowStart = gvExpenseRec.PageIndex * 15;
        int RowNo = gvExpenseRec.Rows.Count < 15 ? gvExpenseRec.Rows.Count : 15;


       for (int j = RowStart; j < RowNo + RowStart; j++)
       {
            int i = j - RowStart;
            if (((CheckBox)gvExpenseRec.Rows[i].FindControl("cbEdit")).Checked == true)
            {
                try
                {
                    
                    //string updatefromAccountteam = ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).SelectedValue;
                    string category = ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).SelectedValue;
                    string subcategory=((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubcategory")).SelectedValue;
                    Calendar Calendar1 = ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate"));
                    Calendar Calendar2 = ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate"));
                    Label txtBlCer = (Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate");

                    string strBlCer = txtBlCer.Text;
                    Label txtBlCer1 = (Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate");

                    string strBlCer1 = txtBlCer1.Text;
                    if (category == "Expense Billed" || (category == "Not Billable-Not Client Recoverable" && subcategory == "Reversal Pending with Finance") || (category == "Not Billable-Billed as part of milestone at fixed cost" && subcategory == "Reversal Pending with Finance"))
                    {
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = false;

                        if (Calendar2.SelectedDate == DateTime.MinValue)
                        {


                            //Response.Write("<script language='javascript'>alert('Choose a expense/reversal initiation date...');</script>");
                            PopUp("Please choose a expense/reversal initiation date...");
                            btnDownload0.Enabled = false;
                            //btnDownload.Enabled = false;

                            BtnCancel.Enabled = true;
                            //BtnEdit.Enabled = false;
                            BtnSave.Enabled = true;
                            return;

                        }
                        else if (Calendar2.SelectedDate <= DateTime.Now.Date)
                        {
                            try
                            {
                                strBlCer = Calendar2.SelectedDate.ToShortDateString();
                            }
                            catch
                            {
                                //Response.Write("<script language='javascript'>alert('Enter a valid expense/reversal initiation date date...');</script>");
                                PopUp("Please enter a valid expense/reversal initiation date date...");
                                btnDownload0.Enabled = false;
                                //btnDownload.Enabled = false;

                                BtnCancel.Enabled = true;
                                //BtnEdit.Enabled = false;
                                BtnSave.Enabled = true;
                                return;
                            }
                        }
                       
                        else
                        {
                            PopUp("Please enter a past expense/reversal initated date.");
                            btnDownload0.Enabled = false;
                            //btnDownload.Enabled = false;

                            BtnCancel.Enabled = true;
                            //BtnEdit.Enabled = false;
                            BtnSave.Enabled = true;
                            return;
                        }
                    }
                    else if (category == "Expense To Be Billed")
                    {
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = false;
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).Visible = false;



                        if (Calendar1.SelectedDate >= DateTime.Now.Date)
                        {
                            try
                            {
                                strBlCer = Calendar1.SelectedDate.ToShortDateString();
                            }
                            catch
                            {
                                //Response.Write("<script language='javascript'>alert('Enter a valid date...');</script>");
                                PopUp("Please enter a valid expense closure date");
                                btnDownload0.Enabled = false;
                                //btnDownload.Enabled = false;

                                BtnCancel.Enabled = true;
                                //BtnEdit.Enabled = false;
                                BtnSave.Enabled = true;
                                return;
                            }
                        }
                        else if (Calendar1.SelectedDate == DateTime.MinValue)
                        {


                            //Response.Write("<script language='javascript'>alert('Choose a Closure date...');</script>");
                            PopUp("Please choose a expense closure date...");
                            btnDownload0.Enabled = false;
                            //btnDownload.Enabled = false;

                            BtnCancel.Enabled = true;
                            //BtnEdit.Enabled = false;
                            BtnSave.Enabled = true;
                            return;

                        }
                        else
                        {
                            PopUp("Please enter a future expense closure date.");
                            btnDownload0.Enabled = false;
                            //btnDownload.Enabled = false;

                            BtnCancel.Enabled = true;
                            //BtnEdit.Enabled = false;
                            BtnSave.Enabled = true;
                            return;
                        }
                    }
                    else if (subcategory == "Reversal Pending with PM/DM")
                    {
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = false;
                        ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).Visible = false;



                        if (Calendar1.SelectedDate >= DateTime.Now.Date)
                        {
                            try
                            {
                                strBlCer = Calendar1.SelectedDate.ToShortDateString();
                            }
                            catch
                            {
                                //Response.Write("<script language='javascript'>alert('Enter a valid date...');</script>");
                                PopUp("Please enter a valid expense closure date...");
                                btnDownload0.Enabled = false;
                                //btnDownload.Enabled = false;

                                BtnCancel.Enabled = true;
                                //BtnEdit.Enabled = false;
                                BtnSave.Enabled = true;
                                return;
                            }
                        }
                        else if (Calendar1.SelectedDate == DateTime.MinValue)
                        {


                            //Response.Write("<script language='javascript'>alert('Choose a closure date...');</script>");
                            PopUp("Please choose a expense closure date...");
                            btnDownload0.Enabled = false;
                            //btnDownload.Enabled = false;

                            BtnCancel.Enabled = true;
                            //BtnEdit.Enabled = false;
                            BtnSave.Enabled = true;
                            return;

                        }
                        else
                        {
                            PopUp("Please  enter a future expense closure date.");
                            btnDownload0.Enabled = false;
                            //btnDownload.Enabled = false;

                            BtnCancel.Enabled = true;
                            //BtnEdit.Enabled = false;
                            BtnSave.Enabled = true;
                            return;
                        }
                    } 

                    string txctcal = Calendar1.SelectedDate.ToString();
                    string txtCalInitated = Calendar2.SelectedDate.ToString();
                      //string calvalue=  Calendar1.SelectedDate = DateTime.MinValue;
                    if (subcategory.ToUpper().Trim() == "Past Commitment")
                    {
                        PopUp("Please select an appropriate status");
                     
                        BtnSave.Enabled = true;
                        BtnCancel.Enabled = true;
                    }
                 
                    else if (category.ToUpper().Trim().Contains("--SELECT--"))
                    {
                        PopUp("Please select an appropriate category");
                        //BtnEdit.Enabled = false;
                        BtnSave.Enabled = true;
                        BtnCancel.Enabled = true;
                    }
                    else if (subcategory.ToUpper().Trim().Contains("--SELECT--"))
                    {
                        PopUp("Please select an appropriate Status");
                        //BtnEdit.Enabled = false;
                        BtnSave.Enabled = true;
                        BtnCancel.Enabled = true;
                    }
                    else if (txctcal==" ")
                    {
                        PopUp("Please select a date");
                        //BtnEdit.Enabled = false;
                        BtnSave.Enabled = true;
                        BtnCancel.Enabled = true;
                    }
                    else if (txtCalInitated == " ")
                    {
                        PopUp("Please select an initated date");
                        //BtnEdit.Enabled = false;
                        BtnSave.Enabled = true;
                        BtnCancel.Enabled = true;
                    }
                    else


                    {
                        string invoiceNo = ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Text;
                        //string confirmationNo = ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Text;
                        string servCentraleReqNo = ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Text;
                        string remarks = ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Text.Replace("&amp;", " & ");
    ;
                        //string unique = dtExpRec.Rows[j]["Unique"].ToString();
                        Label txtUnique = (Label)gvExpenseRec.Rows[i].FindControl("lblUnique");
                        string unique = txtUnique.Text;
                        Label txtPM = (Label)gvExpenseRec.Rows[i].FindControl("lblPM");
                         string PM = txtPM.Text;
                         Label txtDM = (Label)gvExpenseRec.Rows[i].FindControl("lblDM");
                         string DM = txtDM.Text;
                         Label txtProfitCenter = (Label)gvExpenseRec.Rows[i].FindControl("lblProfitCenter");
                         string ProfitCenter = txtProfitCenter.Text;
                         Label txtCustomerCode = (Label)gvExpenseRec.Rows[i].FindControl("lblCustomerCode");
                         string CustomerCode = txtCustomerCode.Text.Replace("&amp;","&") ;
                         string Access= Session["Role"].ToString();

                         string invoiceNoFromDT = dtExpRec.Rows[j]["Invoice No"].ToString();

                         string PMFromDT = dtExpRec.Rows[j]["PM"].ToString();
                         string DMFromDT = dtExpRec.Rows[j]["DM"].ToString();

                         String uniqueFromDT = dtExpRec.Rows[j]["Unique"].ToString();

                        //string comments =((TextBox)gvExpenseRec.Rows[i].FindControl("txtComments")).Text;
                        DateTime expenseclosuredate;
                     
                       
                        expenseclosuredate = ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).SelectedDate;
                        string expClosuredate = expenseclosuredate.ToShortDateString();
                        DateTime initateddate;
                        initateddate = ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).SelectedDate;
                        string inidate = initateddate.ToShortDateString();

                        if (Calendar1.SelectedDate == DateTime.MinValue)
                            ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Text = System.DBNull.Value.ToString();
                        if (Calendar2.SelectedDate == DateTime.MinValue)
                            ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Text = System.DBNull.Value.ToString();
                      
                        string querystr;
                        if (category != "Expense Billed")
                            invoiceNo = "";
                        
                        if (Calendar1.SelectedDate == DateTime.MinValue)
                            expClosuredate = "";
                        if (Calendar2.SelectedDate == DateTime.MinValue)
                            inidate = "";
                        //string CommandText = "[SP_ExpenseRecovery_BulkUpdate_EAS_Screen] '" + category + "','" + subcategory + "' ,'" + remarks + "'
                        //    ,'" + expClosuredate + "' ,'" + inidate + "' ,'"+username + "', '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "' ,'" 
                        //        + unique + "' ,'" + invoiceNo + "' ,'" + servCentraleReqNo + "', '" + null + "' ,'" + "Screen Edit" + "','" 
                        //        + PM + "','" + DM + "','" + ProfitCenter + "','" + CustomerCode + "','" + Access + "','" + invoiceNoFromDT + "','" + PMFromDT + "','" + DMFromDT + "','" + uniqueFromDT + "'";
                        //DataSet dsInsertIntoTempexpense = BusinessLogic.GetDataSet(CommandText);
                        SqlCommand cmd = new SqlCommand();

                        string cmdExpRec = "exec SP_ExpenseRecovery_BulkUpdate_EAS_Screen @categoryFromScreen,@subcategoryFromScreen,@remarksFromScreen,@expenseclosuredateFromScreen,@InitatedDateFromScreen,@usernameFromScreen,@lastupdateonFromScreen,@uniqueFromScreen,@InvoiceNoFromScreen,@ServCentraleReqNoFromScreen,@DocumentNumberFromScreen,@Upload_TypeFromScreen,@PMFromScreen,@DMFromScreen,@ProfitCentreFromScreen,@CustomerCodeFromScreen,@AccessTypeFromScreen,@invoiceNoFromDT,@PMFromDT,@DMFromDT,@uniqueFromDT";
                        cmd.CommandText = cmdExpRec;
                        cmd.Parameters.AddWithValue("@categoryFromScreen", category);
                        cmd.Parameters.AddWithValue("@subcategoryFromScreen", subcategory);
                        cmd.Parameters.AddWithValue("@remarksFromScreen", remarks);
                        cmd.Parameters.AddWithValue("@expenseclosuredateFromScreen", expClosuredate);
                        cmd.Parameters.AddWithValue("@InitatedDateFromScreen", inidate);
                        cmd.Parameters.AddWithValue("@usernameFromScreen", Session["MailID"].ToString());
                        cmd.Parameters.AddWithValue("@lastupdateonFromScreen", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@uniqueFromScreen", unique);
                        cmd.Parameters.AddWithValue("@InvoiceNoFromScreen", invoiceNo);
                        cmd.Parameters.AddWithValue("@ServCentraleReqNoFromScreen", servCentraleReqNo);
                        cmd.Parameters.AddWithValue("@DocumentNumberFromScreen", "");
                        cmd.Parameters.AddWithValue("@Upload_TypeFromScreen", "Screen Edit");
                        cmd.Parameters.AddWithValue("@PMFromScreen", PM);
                        cmd.Parameters.AddWithValue("@DMFromScreen", DM);
                        cmd.Parameters.AddWithValue("@ProfitCentreFromScreen", ProfitCenter);
                        cmd.Parameters.AddWithValue("@CustomerCodeFromScreen", CustomerCode);
                        cmd.Parameters.AddWithValue("@AccessTypeFromScreen", Access);
                        cmd.Parameters.AddWithValue("@invoiceNoFromDT", invoiceNoFromDT);
                        cmd.Parameters.AddWithValue("@PMFromDT", PMFromDT);
                        cmd.Parameters.AddWithValue("@DMFromDT", DMFromDT);
                        cmd.Parameters.AddWithValue("@uniqueFromDT", uniqueFromDT);

                        DataSet dsExpRec = dal.FetchDataSet(cmd);


                        ((RegularExpressionValidator)gvExpenseRec.Rows[i].FindControl("RegularExpressionValidator1")).Enabled = false;
                        //BusinessLogic.SaveExpenseData(querystr, updatefromAccountteam, invoiceNo, confirmationNo, servCentraleReqNo, remarks, unique, username);
                        //DataSet ds = BusinessLogic.GetDataSet(querystr);

                        //((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = false;
                        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = false;
                        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = false;
                       // ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("rqExpenseClosureDate")).Enabled = false;
                        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtComments")).Visible = false;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = false;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = false;
                        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = false;
                        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = false;
                        ((CheckBox)gvExpenseRec.Rows[i].FindControl("cbEdit")).Checked = false;

                        //((Label)gvExpenseRec.Rows[i].FindControl("lblSubCategory")).Visible = false;
                        //((Label)gvExpenseRec.Rows[i].FindControl("lblComments")).Visible = false;
                        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtComments")).Visible = true;
                        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = true;
                        subCategory_top = ddlSubCategory_Top.SelectedValue;
                        customerCode = ddlCustomerCode.SelectedValue;
                        ageingBucket = ddlAgeingBucket.SelectedValue;
                        glLongText = ddlLongText.SelectedValue;
                        PM = ddlPM.SelectedValue;
                        DM = ddlDM.SelectedValue;
                        profitCentre = ddlProfitCentre.SelectedValue.ToString();
                        category_top = ddlCategory_Top.SelectedValue.ToString();
                        practiceLine = ddlPracticeLine.SelectedValue.ToString();
                        Commitment = ddlCommitments.SelectedValue.ToString();
                      
                        //SaveNew();
                      
                      

                        DataSet dscategory_top = BusinessLogic.GetDataSet("update GMU_Expense_recovery set [Update from Account team]=null where ltrim(rtrim([Update from Account team] )) =''");
                        DataTable dtExp = FetchExpenseData(customerCode, ageingBucket, glLongText, PM, DM, profitCentre, category_top, subCategory_top, practiceLine, Session["MailID"].ToString(), Session["Role"].ToString(), Commitment);
                        BindData(dtExp);
                        //lblSaveMsg.Visible = true;
                        //lblSaveMsg.ForeColor = Color.Green;

                        Lblmsg.Visible = true;
                        Lblmsg.ForeColor = Color.Green;
                        Lblmsg.Text = "Data saved successfully!!";
                        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnDate")).Enabled = false;
                        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnInitatedDate")).Enabled = false;
                        btnDownload0.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    //lblSaveError.Visible = true;
                    //lblSaveError.ForeColor = Color.Red;

                    //lblSaveError.Text = "Some Error Occurred!!";

                    Lblmsg.Visible = true;
                    Lblmsg.ForeColor = Color.Red;

                    Lblmsg.Text = ex.Message;
                    //BtnEdit.Enabled = false;
                    BtnSave.Enabled = true;
                    BtnCancel.Enabled = true;
                    return;
                   
                }
                return;
            }
          
        }
        
       
    }
    protected void btnResetFilters_Click(object sender, EventArgs e)
    {
        Lblmsg.Visible = false;
        ddlSubCategory_Top.SelectedIndex=0;
        ddlCustomerCode.SelectedIndex = 0;
        ddlAgeingBucket.SelectedIndex = 0;
        ddlLongText.SelectedIndex = 0;
        ddlPM.SelectedIndex = 0;
        ddlDM.SelectedIndex = 0;
        ddlCategory_Top.SelectedIndex = 0;
        ddlPracticeLine.SelectedIndex = 0;
        ddlProfitCentre.SelectedIndex = 0;

        subCategory_top = ddlSubCategory_Top.SelectedValue;
        customerCode = ddlCustomerCode.SelectedValue;
        ageingBucket = ddlAgeingBucket.SelectedValue;
        glLongText = ddlLongText.SelectedValue;
        PM = ddlPM.SelectedValue;
        DM = ddlDM.SelectedValue;
         
         
        profitCentre = ddlProfitCentre.SelectedValue.ToString();
        category_top = ddlCategory_Top.SelectedValue.ToString();
        practiceLine = ddlPracticeLine.SelectedValue.ToString();
        Commitment = ddlCommitments.SelectedValue.ToString();
        DataTable dtExp = FetchExpenseData(customerCode, ageingBucket, glLongText, PM, DM, profitCentre, category_top, subCategory_top, practiceLine, Session["MailID"].ToString(), Session["Role"].ToString(), Commitment);
        BindData(dtExp);
      
    }

    protected void ddlUpdatefromAccountteam_SelectedIndexChanged(object sender, EventArgs e)
    {
        Lblmsg.Visible = false;
        DropDownList ddlUpdatefromAccountteam = (DropDownList)sender;

        GridViewRow gridrow = (GridViewRow)ddlUpdatefromAccountteam.NamingContainer;
        int i = gridrow.RowIndex;
        if (((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).SelectedValue == "Already Invoiced")
        {
            ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = false;
            ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator1")).Enabled = true;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = true;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
          
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtComments")).Visible = true;
            ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = true;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = false;
        }
        else if (((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).SelectedValue == "To be reversed -  DM Approval Obtained")
        {
            ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = true;
            
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = false;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
           
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtComments")).Visible = true;
            ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = true;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = true;
        }
        else if (((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).SelectedValue == "To be reversed -  DM Approval pending")
        {
            ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = true;
            
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = false;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
          
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtComments")).Visible = true;
            ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = true;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = true;
        }
        else
        {
            ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator1")).Enabled = false;
            ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = false;
            
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = false;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
   
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtComments")).Visible = true;
            ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = true;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = true;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = true;
        }
    }

       
    protected void ddlPM_SelectedIndexChanged(object sender, EventArgs e)
    {
        Lblmsg.Visible = false;
        subCategory_top = ddlSubCategory_Top.SelectedValue;
        customerCode = ddlCustomerCode.SelectedValue;
        ageingBucket = ddlAgeingBucket.SelectedValue;
        glLongText = ddlLongText.SelectedValue;
        PM = ddlPM.SelectedValue;
        DM = ddlDM.SelectedValue;

         
         
        profitCentre = ddlProfitCentre.SelectedValue.ToString();
        category_top = ddlCategory_Top.SelectedValue.ToString();
        practiceLine = ddlPracticeLine.SelectedValue.ToString();
        Commitment = ddlCommitments.SelectedValue.ToString();
        DataTable dtExp = FetchExpenseData(customerCode, ageingBucket, glLongText, PM, DM, profitCentre, category_top, subCategory_top, practiceLine, Session["MailID"].ToString(), Session["Role"].ToString(), Commitment);
        BindData(dtExp);
    }
    protected void ddlDM_SelectedIndexChanged(object sender, EventArgs e)
    {
        Lblmsg.Visible = false;
        subCategory_top = ddlSubCategory_Top.SelectedValue;
        customerCode = ddlCustomerCode.SelectedValue;
        ageingBucket = ddlAgeingBucket.SelectedValue;
        glLongText = ddlLongText.SelectedValue;
        PM = ddlPM.SelectedValue;
        DM = ddlDM.SelectedValue;

         
         
        profitCentre = ddlProfitCentre.SelectedValue.ToString();
        category_top = ddlCategory_Top.SelectedValue.ToString();
        practiceLine = ddlPracticeLine.SelectedValue.ToString();
        Commitment = ddlCommitments.SelectedValue.ToString();
        DataTable dtExp = FetchExpenseData(customerCode, ageingBucket, glLongText, PM, DM, profitCentre, category_top, subCategory_top, practiceLine, Session["MailID"].ToString(), Session["Role"].ToString(), Commitment);
        BindData(dtExp);
    }
   

    protected void lbUpload_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Lblmsg.Visible = false;
        Session["key"]=null;
    }




    protected void lbBulkUpdate_Click(object sender, EventArgs e)
    {

        Lblmsg.Visible = false;
        DAL dal = new DAL();
        DataSet ds = new DataSet();
        DataTable dtGrid = new DataTable();
        DataTable dt = new DataTable();

        //DataSet dsStatus = new DataSet();
        //string query_status = "";
        //query_status = "select * from Expense_category";
        //dsStatus = dal.GetDataSet(query_status);
        //Session.Add("dsStatus", dsStatus);
        string role = Session["Role"].ToString();
        dtGrid = dal.FetchDetailsforTemplate(role, Session["MailID"].ToString(), ddlSubCategory_Top.SelectedValue, ddlCustomerCode.SelectedValue, ddlPM.SelectedValue, ddlLongText.SelectedValue, ddlAgeingBucket.SelectedValue, ddlDM.SelectedValue, ddlProfitCentre.SelectedValue, ddlCategory_Top.SelectedValue, ddlPracticeLine.SelectedValue, ddlCommitments.SelectedValue);
        Session.Add("dtGrid", dtGrid);
        dt = (DataTable)Session["dtGrid"];
        //dsStatus = (DataSet)Session["dsStatus"];

        string folderadress = @"~/App_Data\ExpenseRecovery_Upload_NewTemplate2.xlsx";
        folderadress = HttpContext.Current.Server.MapPath(folderadress);
        string storefolderadress = @"~/Template";
        storefolderadress = HttpContext.Current.Server.MapPath(storefolderadress);
        Microsoft.Office.Interop.Excel.Application WRExcel = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbooks WRwbs = null;
        //Microsoft.Office.Interop.Excel.Workbook WRwb = new Microsoft.Office.Interop.Excel.Workbook();
        object objOpt = System.Reflection.Missing.Value;
        Microsoft.Office.Interop.Excel.Workbook WRwb = WRExcel.Workbooks.Add(objOpt);
        Microsoft.Office.Interop.Excel._Worksheet WRws = null;

        Microsoft.Office.Interop.Excel.Worksheet excelSheet1 = null;

        WRExcel.Visible = false;
        WRwbs = WRExcel.Workbooks;


        WRwb = WRwbs.Open(folderadress, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt,
            objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);
        Microsoft.Office.Interop.Excel.Sheets WRss = null;
        WRss = WRwb.Sheets;
        try
        {
            //Microsoft.Office.Interop.Excel.ApplicationClass excelapp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            //excelapp.Visible = false;
            //string temp1 = Server.MapPath("./App_Data\\ExpenseRecovery_Upload.xls");
            //if (File.Exists(temp1))
            //    File.Delete(temp1);


            //Object oMissing = System.Reflection.Missing.Value;
            String excelFile1 = "~\\App_Data\\ExpenseRecovery_Upload_" + Session["MailID"].ToString() + ".xls";
            String destPath = Server.MapPath(excelFile1);

            //DataTable dtStatus = new DataTable();
            //dtStatus = dsStatus.Tables[0];

            ////for status
            //var listStatus = new List<string>();
            //for (int i = 0; i < dtStatus.Rows.Count; i++)
            //{
            //    listStatus.Add(dtStatus.Rows[i][0].ToString());
            //}
            //var flatSTP = string.Join(",", listStatus.ToArray());



            //Microsoft.Office.Interop.Excel.Workbook excelBook = excelapp.Workbooks.Add(oMissing);
            excelSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)WRss.get_Item("Data");



            int ColumnIndex = 0;
            int rowIndex = 0;
            DataTable dt1 = new DataTable();




            int rows = dt.Rows.Count;
            int columns = dt.Columns.Count;
            int r = 0; int c = 0;
            object[,] DataArray = new object[rows + 1, columns + 1];
            for (c = 0; c <= columns - 1; c++)
            {
                DataArray[r, c] = dt.Columns[c].ColumnName;
                for (r = 0; r <= rows - 1; r++)
                {
                    DataArray[r, c] = dt.Rows[r][c];
                } //end row loop
            } //end column loop

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)excelSheet1.Cells[2, 1];
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)excelSheet1.Cells[1 + dt.Rows.Count, dt.Columns.Count];
            Microsoft.Office.Interop.Excel.Range range = excelSheet1.get_Range(c1, c2);


            //Fill Array in Excel
            range.Value2 = DataArray;

            
            ////write header row to spreadsheet
            //int DataTableColumnCounter;
            //int ExcelColumnCounter = 1; //excel spreadsheets start at 1 when counting columns not zero!
            //for (DataTableColumnCounter = 0; DataTableColumnCounter < dt.Columns.Count; DataTableColumnCounter++)
            //{
            //    excelSheet1.Cells[1, ExcelColumnCounter] = dt.Columns[DataTableColumnCounter].ColumnName;
            //    ExcelColumnCounter = ExcelColumnCounter + 1; //moving to next column
            //}

            ////excelSheet1.get_Range("AX2", "AX" + dt.Rows.Count + 1).Validation.Add(Microsoft.Office.Interop.Excel.XlDVType.xlValidateList, Microsoft.Office.Interop.Excel.XlDVAlertStyle.xlValidAlertInformation, Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlBetween, flatSTP, Type.Missing);
            //excelSheet1.get_Range("AX2", "AX" + dt.Rows.Count + 1).Validation.IgnoreBlank = true;
            //excelSheet1.get_Range("AX2", "AX" + dt.Rows.Count + 1).Validation.ErrorMessage = "Select status from dropdown";
            //excelSheet1.get_Range("AX2", "AX" + dt.Rows.Count + 1).Validation.InputMessage = "Please select the status from the dropdown list";
            ////excelSheet1.get_Range("AU2", "AU" + dt.Rows.Count + 1).Validation.InputMessage = "Invoice Number is mandatory for status- Already Invoiced";
            ////excelSheet1.get_Range("AW2", "AW" + dt.Rows.Count + 1).Validation.InputMessage = "Servecentrale Number is mandatory for status- Need Reversal-Not Billable to Client";
            //excelSheet1.get_Range("AX2", "AX" + dt.Rows.Count + 1).Validation.InCellDropdown = true;


            //excelSheet1.get_Range("AT2", "AT" + dt.Rows.Count + 1).AutoFit();
            //excelSheet1.get_Range("I2", "I" + dt.Rows.Count + 1).NumberFormat = "@";
            //excelSheet1.get_Range("AZ2", "AZ" + dt.Rows.Count + 1).NumberFormat = "@";
            excelSheet1.get_Range("F2", "F" + dt.Rows.Count + 1).NumberFormat = "MM/DD/YYYY";
            excelSheet1.get_Range("G2", "G" + dt.Rows.Count + 1).NumberFormat = "MM/DD/YYYY";
            excelSheet1.get_Range("W2", "W" + dt.Rows.Count + 1).NumberFormat = "MM/DD/YYYY";
            excelSheet1.get_Range("AF2", "AF" + dt.Rows.Count + 1).NumberFormat = "MM/DD/YYYY";
            //excelSheet1.get_Range("I2", "I" + dt.Rows.Count + 1).NumberFormat = "@";

            //excelSheet1.get_Range("As2", "As" + dt.Rows.Count + 1).Validation.InputMessage = "Sarath";


            //excelSheet1.get_Range("A1", "AX1").AutoFit();
            excelSheet1.get_Range("A1", "AE1").AutoFilter(1, objOpt, Excel.XlAutoFilterOperator.xlAnd, objOpt, true);
           

            excelSheet1.Protect(1, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, 1, 1, objOpt);

            WRwb.SaveCopyAs(destPath);
            WRwb.Close(false);




            // DownloadFileProject(filename);
            Session["key"] = "ExpenseRecovery_Upload_" + Session["MailID"].ToString() + ".xls";
            //updatepanel1.Update();

            iframe.Attributes.Add("src", "DownloadFile.aspx");


            //if (File.Exists(destPath))
            //{
            //    FileInfo objFileInfo;
            //    objFileInfo = new FileInfo(destPath);
            //    string filefullpath = "~\\App_Data\\ExpenseRecovery_Upload.xls";
            //    Response.Clear();

            //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ExpenseRecovery_Upload.xls");

            //    Response.ContentType = "application/vnd.xlsx";

            //    Response.WriteFile(filefullpath);

            //    Response.Flush();

            //    //Response.Close();

            //    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            //    HttpContext.Current.ApplicationInstance.CompleteRequest();
            //    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRss);
            //    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRwb);

            //}

        }
        catch (Exception ex)
        {
            //lblDownload.Visible = true;
            //lblDownload.Text = ex.ToString();
        }
        finally
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (WRws != null)
            {
                //wksht.Delete();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRws);
                WRws = null;
            }

            if (WRss != null)
            {
                //wksht.Delete();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRss);
                WRss = null;
            }

            if (WRwb != null)
            {
                //wksht.Delete();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRwb);
                WRwb = null;
            }
            if (WRwbs != null)
            {
                //wksht.Delete();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRwbs);
                WRwbs = null;
            }
            if (WRExcel != null)
            {
                WRExcel.Quit();
                int hWnd1 = WRExcel.Application.Hwnd;
                TryKillProcessByMainWindowHwnd(hWnd1);

                //wksht.Delete();
                //WRExcel.Quit();
                //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRExcel);
                //WRExcel = null;




            }
            if (excelSheet1 != null)
            {
                //wksht.Delete();

                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelSheet1);
                excelSheet1 = null;

            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();


        }

    }

    public void FormatRange(Microsoft.Office.Interop.Excel.Worksheet excel, Microsoft.Office.Interop.Excel.Range pt1, Microsoft.Office.Interop.Excel.Range pt2, Color bckColor, Color fontColor, bool fontWeight, Color gridlinesColor, Microsoft.Office.Interop.Excel.XlHAlign alignmentflag, bool isDelete, string MergedText)
    {
        Microsoft.Office.Interop.Excel.Range formatRange = excel.get_Range(pt1, pt2);
        formatRange.Interior.Color = bckColor;
        formatRange.Font.Bold = fontWeight;
        formatRange.Font.Color = fontColor;
        formatRange.HorizontalAlignment = alignmentflag;
        formatRange.Borders.Color = gridlinesColor;

        if (isDelete == true)
        {
            formatRange.Value = MergedText;
            formatRange.Merge();
        }
    }

public static bool TryKillProcessByMainWindowHwnd(int hWnd)
{
    uint processID;
    GetWindowThreadProcessId((IntPtr)hWnd, out processID);
    if (processID == 0) return false;
    try
    {
        Process.GetProcessById((int)processID).Kill();
    }
    catch (ArgumentException)
    {
        return false;
    }
    catch (Exception ex)
    {
        return false;
    }
    return true;
}

[DllImport("user32.dll")]
private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

protected void btnBulkUpload_Click(object sender, EventArgs e)
{
    //System.Threading.Thread.Sleep(10000);
    Lblmsg.Visible = false;
    int Count = 0;
    //PopulateDataForDownload();
    Session["key"] = null;
    if (fuBulkUpload.PostedFile.ContentLength != 0 && fuBulkUpload.PostedFile.ContentLength < (3 * 20480 * 1024))
    {
        try
        {
            string destDir = Server.MapPath("./App_Data");

            string fileName = Path.GetFileName(fuBulkUpload.PostedFile.FileName);
            if (fileName.ToLower().CompareTo("expenserecovery_upload_" + Session["MailID"].ToString().ToLower() + ".xls") == 0 || fileName.ToLower().CompareTo("expenserecovery_upload_" + Session["MailID"].ToString().ToLower() + ".xlsx") == 0)
            {
                string destPath = Path.Combine(destDir, fileName);
                if (File.Exists(destPath))
                {
                    File.Delete(destPath);
                }
                fuBulkUpload.SaveAs(destPath);
                OleDbConnection oledbConn;
                OleDbCommand oledbCmd;
                DataTable dt = new DataTable();
                OleDbDataAdapter oledbAdap;
                try
                {
                    //string path = "D:\\VSS\\EAS_Applications\\Expense RecoveryWithSummaryPage\\App_Data";
                    //path = path + "\\" + fileName;

                    string connString = "";



                    //Connection String to Excel Workbook



                  //connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + destPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";


                    string query = "SELECT * FROM [Data$]";

                    OleDbConnection conn = new OleDbConnection(connString);

                    if (conn.State == ConnectionState.Closed)

                        conn.Open();

                    OleDbCommand cmd = new OleDbCommand(query, conn);

                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    dt = ds.Tables[0];

                    da.Dispose();

                    conn.Close();

                    conn.Dispose();


                }
                catch (System.Exception ex)
                { throw ex; }

                if (dt.Columns.Contains("SubUnit"))
                    dt.Columns.Remove("SubUnit");

                if (dt.Columns.Contains("F3%"))
                    dt.Columns.Remove("F3%");
                if (dt.Columns.Contains("ResponseFromFinance"))
                    dt.Columns.Remove("ResponseFromFinance");
                string _sqlWhere = "[unique] is not null";
                string _sqlOrder = "";

                //DataTable newDataTable = dt.Select(_sqlWhere, _sqlOrder).CopyToDataTable();
                // REMOVE ALL EMPTY ROWS
                dt.Rows.Cast<DataRow>().ToList().FindAll(Row =>
                { return String.IsNullOrEmpty(String.Join("", Row.ItemArray)); }).ForEach(Row =>
                { dt.Rows.Remove(Row); });
                //DataTable newDataTable =  = dt.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull || string.Compare((field as string).Trim(), string.Empty) == 0)).CopyToDataTable();
                Session.Add("dtNew", dt);
                Session.Add("dt", dt);


            }
            else
            {
                //Lblmsg.Visible = true;

                PopUp("Excel filename must be ExpenseRecovery_Upload_" + Session["MailID"].ToString() + ".xls");
                return;
            }
        }
        catch (Exception ex)
        {
            Lblmsg.Visible = true;
            PopUp("Some Error Occurred!");
            return;
        }

        //Validations
        DataTable dtExcel = new DataTable();
        dtExcel = (DataTable)Session["dt"];
        int newCount = ((DataTable)(Session["dtNew"])).Rows.Count;
        for (int i = 0; i < newCount; i++)
        {
            if (i == 184)
            {
            }
            Lblmsg.Text = i.ToString();
            Lblmsg.Visible = true;

            //if (dtExcel.Rows[i]["Update from Account team"].ToString() == "--Select--")
            //{
            //    lblDownload.Visible = true;
            //        lblDownload.Text ="Please select "
            //}
            if (dtExcel.Rows[i]["Category"].ToString() == "Update Pending")
            {
                Lblmsg.Visible = true;
                Lblmsg.ForeColor = Color.Red;
                Lblmsg.Font.Size = 8;
                Lblmsg.Text = "Please choose a proper Category...Please check in the " + (i + 2) + " row of the sheet!";
                return;

            }
            else if ((dtExcel.Rows[i]["Status"].ToString() == "Update Pending") || (dtExcel.Rows[i]["Status"].ToString() == "Past Commitment"))
            {
                Lblmsg.Visible = true;
                Lblmsg.ForeColor = Color.Red;
                Lblmsg.Font.Size = 8;
                Lblmsg.Text = "Please choose a proper Status...Please check in the " + (i + 2) + " row of the sheet!";
                return;

            }
            else if (dtExcel.Rows[i]["Category"].ToString() == "Expense Billed")
            {

                if (dtExcel.Rows[i]["Invoice/Confirmation No"].ToString() == "")
                {

                    Lblmsg.Visible = true;
                    Lblmsg.ForeColor = Color.Red;
                    Lblmsg.Font.Size = 8;
                    Lblmsg.Text = "Please enter Invoice/Confirmation Number...Please check in the " + (i + 2) + " row of the sheet!";
                    return;
                }
                if (dtExcel.Rows[i]["Status"].ToString() != "Expense confirmation already raised")
                {
                    Lblmsg.Visible = true;
                    Lblmsg.ForeColor = Color.Red;
                    Lblmsg.Font.Size = 8;
                    Lblmsg.Text = "Please choose a proper Status...Please check in the " + (i + 2) + " row of the sheet!";
                    return;
                }

                if (dtExcel.Rows[i]["Initiated Date (mm/dd/yyyy)"].ToString() == "")
                {
                    Lblmsg.Text = "Please choose a proper expense/reversal initiation Date (mm/dd/yyyy) when expense/reversal was initiation ...Please check in the " + (i + 2) + " row of the sheet!";
                    return;
                }

                else if (Convert.ToDateTime(dtExcel.Rows[i]["Initiated Date (mm/dd/yyyy)"]) > DateTime.Now)
                {
                    Lblmsg.Visible = true;
                    Lblmsg.ForeColor = Color.Red;
                    Lblmsg.Font.Size = 8;
                    Lblmsg.Text = "Please choose a past date (mm/dd/yyyy)  when expense/reversal was initiation...Please check in the " + (i + 2) + " row of the sheet!";
                    return;
                }

            }

            else if (dtExcel.Rows[i]["Category"].ToString() == "Expense To Be Billed")
            {

                //if (dtExcel.Rows[i]["Invoice/Confirmation No"].ToString() == "")
                //{
                //    lblDownload.Visible = true;
                //    lblDownload.Text = "Please enter Invoice/Confirmation Number...Please check in the " + (i + 2) + " row of the sheet!";
                //    return;
                //}
                if (!((dtExcel.Rows[i]["Status"].ToString() == "Pending client sign-off") || (dtExcel.Rows[i]["Status"].ToString() == "Pending with delivery - PM/DM") || (dtExcel.Rows[i]["Status"].ToString() == "Pending with Finance for activity code creation, approval, etc.") || (dtExcel.Rows[i]["Status"].ToString() == "Pending with IS - system issues")))
                {
                    Lblmsg.Visible = true;
                    Lblmsg.ForeColor = Color.Red;
                    Lblmsg.Font.Size = 8;
                    Lblmsg.Text = "Please choose a proper Status...Please check in the " + (i + 2) + " row of the sheet!";
                    return;
                }
                if (dtExcel.Rows[i]["Date (mm/dd/yyyy) by when expense will be closed in system"].ToString() == "")
                {
                    Lblmsg.Visible = true;
                    Lblmsg.ForeColor = Color.Red;
                    Lblmsg.Font.Size = 8;
                    Lblmsg.Text = "Please choose a proper date (mm/dd/yyyy) by when expense will be closed...Please check in the " + (i + 2) + " row of the sheet!";
                    return;
                }
                try
                {
                    if (Convert.ToDateTime(dtExcel.Rows[i]["Date (mm/dd/yyyy) by when expense will be closed in system"]) < DateTime.Now)
                    {
                        Lblmsg.Visible = true;
                        Lblmsg.ForeColor = Color.Red;
                        Lblmsg.Font.Size = 8;
                        Lblmsg.Text = "Please choose a Future date (mm/dd/yyyy) by when expense will be closed...Please check in the " + (i + 2) + " row of the sheet!";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Lblmsg.Visible = true;
                    Lblmsg.ForeColor = Color.Red;
                    Lblmsg.Font.Size = 8;
                    Lblmsg.Text = "Please enter the date in mm/dd/yyyy format";
                    return;
                }


            }

            else if (dtExcel.Rows[i]["Category"].ToString() == "Not Billable-Billed as part of milestone at fixed cost")
            {
                if (!((dtExcel.Rows[i]["Status"].ToString() == "Reversal Pending with Finance") || (dtExcel.Rows[i]["Status"].ToString() == "Reversal Pending with PM/DM")))
                {
                    Lblmsg.Visible = true;
                    Lblmsg.ForeColor = Color.Red;
                    Lblmsg.Font.Size = 8;
                    Lblmsg.Text = "Please choose a proper Status...Please check in the " + (i + 2) + " row of the sheet!";
                    return;
                }
                else
                {
                    if (dtExcel.Rows[i]["Status"].ToString() == "Reversal Pending with Finance")
                    {
                        
                        if (dtExcel.Rows[i]["Initiated Date (mm/dd/yyyy)"].ToString() == "")
                        {
                            Lblmsg.Visible = true;
                            Lblmsg.ForeColor = Color.Red;
                            Lblmsg.Font.Size = 8;
                            Lblmsg.Text = "Please choose a proper expense/reversal initiation Date (mm/dd/yyyy) when expense/reversal was initiation...Please check in the " + (i + 2) + " row of the sheet!";
                            return;
                        }

                        else if (Convert.ToDateTime(dtExcel.Rows[i]["Initiated Date (mm/dd/yyyy)"]) > DateTime.Now)
                        {
                            Lblmsg.Visible = true;
                            Lblmsg.ForeColor = Color.Red;
                            Lblmsg.Font.Size = 8;
                            Lblmsg.Text = "Please choose a past expense/reversal initiation Date (mm/dd/yyyy) when expense/reversal was initiation...Please check in the " + (i + 2) + " row of the sheet!";
                            return;
                        }
                    }

                    else if (dtExcel.Rows[i]["Status"].ToString() == "Reversal Pending with PM/DM")
                    {
                        
                        if (dtExcel.Rows[i]["Date (mm/dd/yyyy) by when expense will be closed in system"].ToString() == "")
                        {
                            Lblmsg.Visible = true;
                            Lblmsg.ForeColor = Color.Red;
                            Lblmsg.Font.Size = 8;
                            Lblmsg.Text = "Please choose a proper  Date (mm/dd/yyyy) when expense/reversal will be closed...Please check in the " + (i + 2) + " row of the sheet!";
                            return;
                        }

                        else if (Convert.ToDateTime(dtExcel.Rows[i]["Date (mm/dd/yyyy) by when expense will be closed in system"]) < DateTime.Now)
                        {
                            Lblmsg.Visible = true;
                            Lblmsg.ForeColor = Color.Red;
                            Lblmsg.Font.Size = 8;
                            Lblmsg.Text = "Please choose a future  Date (mm/dd/yyyy) when expense/reversal will be closed......Please check in the " + (i + 2) + " row of the sheet!";
                            return;
                        }
                    }

                }
            }
            else if (dtExcel.Rows[i]["Category"].ToString() == "Not Billable-Not Client Recoverable")
            {
                if (!((dtExcel.Rows[i]["Status"].ToString() == "Reversal Pending with Finance") || (dtExcel.Rows[i]["Status"].ToString() == "Reversal Pending with PM/DM")))
                {
                    Lblmsg.Visible = true;
                    Lblmsg.ForeColor = Color.Red;
                    Lblmsg.Font.Size = 8;
                    Lblmsg.Text = "Please choose a proper Status...Please check in the " + (i + 2) + " row of the sheet!";
                    return;
                }
                else
                {
                    if (dtExcel.Rows[i]["Status"].ToString() == "Reversal Pending with Finance")
                    {

                        if (dtExcel.Rows[i]["Initiated Date (mm/dd/yyyy)"].ToString() == "")
                        {
                            Lblmsg.Visible = true;
                            Lblmsg.ForeColor = Color.Red;
                            Lblmsg.Font.Size = 8;
                            Lblmsg.Text = "Please choose a proper expense/reversal initiation Date (mm/dd/yyyy) when expense/reversal was initiation...Please check in the " + (i + 2) + " row of the sheet!";
                            return;
                        }

                        else if (Convert.ToDateTime(dtExcel.Rows[i]["Initiated Date (mm/dd/yyyy)"]) > DateTime.Now)
                        {
                            Lblmsg.Visible = true;
                            Lblmsg.ForeColor = Color.Red;
                            Lblmsg.Font.Size = 8;
                            Lblmsg.Text = "Please choose a past expense/reversal initiation Date (mm/dd/yyyy) when expense/reversal was initiation...Please check in the " + (i + 2) + " row of the sheet!";
                            return;
                        }
                    }

                    else if (dtExcel.Rows[i]["Status"].ToString() == "Reversal Pending with PM/DM")
                    {

                        if (dtExcel.Rows[i]["Date (mm/dd/yyyy) by when expense will be closed in system"].ToString() == "")
                        {
                            Lblmsg.Visible = true;
                            Lblmsg.ForeColor = Color.Red;
                            Lblmsg.Font.Size = 8;
                            Lblmsg.Text = "Please choose a proper  Date (mm/dd/yyyy) when expense/reversal will be closed...Please check in the " + (i + 2) + " row of the sheet!";
                            return;
                        }

                        else if (Convert.ToDateTime(dtExcel.Rows[i]["Date (mm/dd/yyyy) by when expense will be closed in system"]) < DateTime.Now)
                        {
                            Lblmsg.Visible = true;
                            Lblmsg.ForeColor = Color.Red;
                            Lblmsg.Font.Size = 8;
                            Lblmsg.Text = "Please choose a future  Date (mm/dd/yyyy) when expense/reversal will be closed......Please check in the " + (i + 2) + " row of the sheet!";
                            return;
                        }
                    }

                }
            }

        }
        if (dtExcel.Rows.Count > 0)
        {
            //Saving the datatable
            String DBConnecting = ConfigurationManager.AppSettings["DBConnectString"];
            using (SqlConnection Conn = new SqlConnection())
            {

                Conn.ConnectionString = AntiXss.HtmlEncode(DBConnecting);
                try
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = Conn;
                    cmd.CommandText = "SP_ExpenseRecovery_BulkUpdate_EAS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter paramDB = cmd.Parameters.AddWithValue("@dtExcel", dtExcel);

                    //string username = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);
                    //SqlParameter paramLastUpdatedBy =
                    cmd.Parameters.AddWithValue("@username", Session["MailID"].ToString());
                    cmd.Parameters.AddWithValue("@AccessType", Session["Role"].ToString());
                    SqlParameter prmReturnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    prmReturnValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(prmReturnValue);

                    paramDB.Value = dtExcel;
                    paramDB.SqlDbType = SqlDbType.Structured;
                    paramDB.TypeName = "dbo.ExpenseRecoveryBulkUpdate_latest";
                    Conn.Open();
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();

                    int result = (Convert.ToInt32(prmReturnValue.Value));

                    subCategory_top = ddlSubCategory_Top.SelectedValue;
                    customerCode = ddlCustomerCode.SelectedValue;
                    ageingBucket = ddlAgeingBucket.SelectedValue;
                    glLongText = ddlLongText.SelectedValue;
                    PM = ddlPM.SelectedValue;
                    DM = ddlDM.SelectedValue;



                    profitCentre = ddlProfitCentre.SelectedValue.ToString();
                    category_top = ddlCategory_Top.SelectedValue.ToString();
                    practiceLine = ddlPracticeLine.SelectedValue.ToString();
                    Commitment = ddlCommitments.SelectedValue.ToString();
                    DataTable dtExp = FetchExpenseData(customerCode, ageingBucket, glLongText, PM, DM, profitCentre, category_top, subCategory_top, practiceLine, Session["MailID"].ToString(), Session["Role"].ToString(), Commitment);
                    BindData(dtExp);
                    Panel1.Visible = false;
                    Lblmsg.Visible = true;

                    if (result == -1)
                    {
                        Lblmsg.ForeColor = Color.Red;
                        Lblmsg.Text = "You are not authorized to upload the data present in the excel";
                    }

                    else if (result == 1)
                    {
                        Lblmsg.ForeColor = Color.Green;
                        Lblmsg.Text = "Data Uploaded Successfully";
                    }

                }
                catch (Exception ex)
                {
                    Lblmsg.Text = "Some Error Occurred!";
                }
            }

        }
        else
        {
            Lblmsg.Visible = true;
            Lblmsg.Text = "Please select the correct excel to upload the data";
        }

    }
}

protected void ddlProfitCentre_SelectedIndexChanged(object sender, EventArgs e)
{
    Lblmsg.Visible = false;
    subCategory_top = ddlSubCategory_Top.SelectedValue;
    customerCode = ddlCustomerCode.SelectedValue;
    ageingBucket = ddlAgeingBucket.SelectedValue;
    glLongText = ddlLongText.SelectedValue;
    PM = ddlPM.SelectedValue;
    DM = ddlDM.SelectedValue;

     
     
    profitCentre = ddlProfitCentre.SelectedValue.ToString();
    category_top = ddlCategory_Top.SelectedValue.ToString();
    practiceLine = ddlPracticeLine.SelectedValue.ToString();
    Commitment = ddlCommitments.SelectedValue.ToString();
    DataTable dtExp = FetchExpenseData(customerCode, ageingBucket, glLongText, PM, DM, profitCentre, category_top, subCategory_top, practiceLine, Session["MailID"].ToString(), Session["Role"].ToString(), Commitment);
    BindData(dtExp);
}
protected void ddlCategory_Top_SelectedIndexChanged(object sender, EventArgs e)
{
    Lblmsg.Visible = false;
    subCategory_top = ddlSubCategory_Top.SelectedValue;
    customerCode = ddlCustomerCode.SelectedValue;
    ageingBucket = ddlAgeingBucket.SelectedValue;
    glLongText = ddlLongText.SelectedValue;
    PM = ddlPM.SelectedValue;
    DM = ddlDM.SelectedValue;

     
     
    profitCentre = ddlProfitCentre.SelectedValue.ToString();
    category_top = ddlCategory_Top.SelectedValue.ToString();
    practiceLine = ddlPracticeLine.SelectedValue.ToString();
    Commitment = ddlCommitments.SelectedValue.ToString();
    DataTable dtExp = FetchExpenseData(customerCode, ageingBucket, glLongText, PM, DM, profitCentre, category_top, subCategory_top, practiceLine, Session["MailID"].ToString(), Session["Role"].ToString(), Commitment);
    BindData(dtExp);
}
protected void ddlPracticeLine_SelectedIndexChanged(object sender, EventArgs e)
{
    Lblmsg.Visible = false;
    subCategory_top = ddlSubCategory_Top.SelectedValue;
    customerCode = ddlCustomerCode.SelectedValue;
    ageingBucket = ddlAgeingBucket.SelectedValue;
    glLongText = ddlLongText.SelectedValue;
    PM = ddlPM.SelectedValue;
    DM = ddlDM.SelectedValue;

     
     
    profitCentre = ddlProfitCentre.SelectedValue.ToString();
    category_top = ddlCategory_Top.SelectedValue.ToString();
    practiceLine = ddlPracticeLine.SelectedValue.ToString();
    Commitment = ddlCommitments.SelectedValue.ToString();
    DataTable dtExp = FetchExpenseData(customerCode, ageingBucket, glLongText, PM, DM, profitCentre, category_top, subCategory_top, practiceLine, Session["MailID"].ToString(), Session["Role"].ToString(), Commitment);
    BindData(dtExp);
}
protected void btnDownloadPivot_Click(object sender, EventArgs e)
{
    Session["key"] = null;
    Lblmsg.Visible = false;
            string cmdtext = "exec SP_FetchDataForMacro";
            DataSet dsEAS = new DataSet();
            dsEAS = BusinessLogic.GetDataSet(cmdtext);
            DataTable dtEAS = new DataTable();
            dtEAS = dsEAS.Tables[0];
            DataTable dtEAS2 = new DataTable();
            dtEAS2 = dsEAS.Tables[1];
            try
            {
                var tblComparisonReport = dtEAS;
                var tblComparisonReport1 = dtEAS2;
                tblComparisonReport = dtEAS;
                if (tblComparisonReport == null || tblComparisonReport.Rows.Count == 0 || tblComparisonReport1 == null || tblComparisonReport1.Rows.Count == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script language=JavaScript>alert('No Data to download!');</script>");
                    return;
                }
                string folder = "ExcelOperations";
                var MyDir = new DirectoryInfo(Server.MapPath(folder));
                string fileName = "Expense_Recovery";
                Session["fileNameRev"] = fileName;
                FileInfo file = new FileInfo(MyDir.FullName + "\\" + fileName + ".xlsx");
                if (MyDir.GetFiles().SingleOrDefault(k => k.Name == (fileName + ".xlsx")) != null)
                    System.IO.File.Delete(MyDir.FullName + "\\" + fileName + ".xlsx");
                Session["FullfileNameRev"] = file;
                ExcelPackage pck = new ExcelPackage();
                ExcelWorksheet ws;
                ExcelWorksheet ws1;
                string sht = "Data";
                string sht1 = "Data2";

                int row = tblComparisonReport.Rows.Count;
                int col = tblComparisonReport.Columns.Count;

                int row1 = tblComparisonReport1.Rows.Count;
                int col1 = tblComparisonReport1.Columns.Count;

                {
                    ws = pck.Workbook.Worksheets.Add(sht);
                    ws.Cells["A1"].LoadFromDataTable(tblComparisonReport, true);
                    //ws.Cells[1, 1, 1, 38].Style.Font.Bold = true;
                    var fill = ws.Cells[1, 1, 1, col].Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    ws.Cells[1, 1, 1, col].Style.Font.Bold = true;
                    ws.Cells[1, 1, row, col].AutoFitColumns();


                    ws = pck.Workbook.Worksheets.Add(sht1);
                    ws.Cells["A1"].LoadFromDataTable(tblComparisonReport1, true);
                    //ws.Cells[1, 1, 1, 38].Style.Font.Bold = true;
                    var fill1 = ws.Cells[1, 1, 1, col1].Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    ws.Cells[1, 1, 1, col1].Style.Font.Bold = true;
                    ws.Cells[1, 1, row1, col1].AutoFitColumns();
                }
                if (file.Exists)
                    file.Delete();
                pck.SaveAs(file);
                pck.Dispose();
                ws = null;

                pck = null;

                DownloadFileBEReport();
                //  hdnfldFlag.Value = "1";
            }

            catch (Exception ex)
            {
            }
        }
public void RefreshPivots(Microsoft.Office.Interop.Excel.Sheets excelsheets)
{

    foreach (Microsoft.Office.Interop.Excel.Worksheet pivotSheet in excelsheets)
    {
        Microsoft.Office.Interop.Excel.PivotTables pivotTables = (Microsoft.Office.Interop.Excel.PivotTables)pivotSheet.PivotTables();
        int pivotTablesCount = pivotTables.Count;
        if (pivotTablesCount > 0)
        {
            for (int i = 1; i <= pivotTablesCount; i++)
            {


                pivotTables.Item(i).RefreshTable();

            }
        }
    }
}
private void DownloadFileBEReport()
{

    Excel.Application oExcel;
    Excel.Workbook oBook = default(Excel.Workbook);
    VBIDE.VBComponent oModule;
    try
    {
        bool forceDownload = true;
        //string path = MapPath(fname);
        string folder = "ExcelOperations";
        var MyDir = new DirectoryInfo(Server.MapPath(folder));
        String sCode;
        Object oMissing = System.Reflection.Missing.Value;
        //Create an instance of Excel.
        oExcel = new Excel.Application();


        oBook = oExcel.Workbooks.Open(Session["destPath"].ToString(), 0, false, 5, "", "", true,
            Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

        oModule = oBook.VBProject.VBComponents.Add(VBIDE.vbext_ComponentType.vbext_ct_StdModule);


        sCode = "sub ExpenseMacro()\r\n" +
            System.IO.File.ReadAllText(MyDir.FullName + "\\ExpenseMacro.txt") +
                "\nend sub";

        oModule.CodeModule.AddFromString(sCode);

        oExcel.GetType().InvokeMember("Run",
                        System.Reflection.BindingFlags.Default |
                        System.Reflection.BindingFlags.InvokeMethod,
                        null, oExcel, new string[] { "ExpenseMacro" });

        // string finalname = Session["fileNameRev"] + "_" + DateTime.Now.ToString("ddMMMyyyy_HHmm") + ".xlsx";
        string finalname = "Expense_Recovery_Report.xlsx";
        // string finalname = "RevenueMomentum_rupali03_07Aug2015_1052.xlsx";
        if (MyDir.GetFiles().SingleOrDefault(k => k.Name == finalname) != null)
            System.IO.File.Delete(MyDir.FullName + "\\" + finalname);

        oBook.SaveCopyAs(MyDir.FullName + "\\" + finalname);


        oBook.Save();


        oBook.Close(null,null,null);
        oExcel.Quit();
        oExcel = null;
        oModule = null;
        oBook = null;

        GC.Collect();

        

        string path = MyDir.FullName + "\\" + finalname;
        // string name = "RupaliExel_Test.xlsx";
        ////string name = "Revenue_Volume_BE_Dump" + "_" + DateTime.Now.ToString("ddMMMyyyy_HHmm") + ".xls" xlsx ;
        //if (ddlSU.Text == "ALL")
        //{
        //    name = "ECS" + "_" + ddlQtr.Text + year.Substring(2, 2) + "_" + "BEReport" + "_" + DateTime.Now.ToString("ddMMMyyyy_HHmm") + ".xlsx";
        //}
        //else
        //{
        //    name = ddlSU.Text + "_" + ddlQtr.Text + year.Substring(2, 2) + "_" + "BEReport" + "_" + DateTime.Now.ToString("ddMMMyyyy_HHmm") + ".xlsx";
        //}
        string ext = Path.GetExtension(path);
        string type = "";

        //string path = MyDir.FullName + "\\BEReport.xlsx";
        ////string name = "Revenue_Volume_BE_Dump" + "_" + DateTime.Now.ToString("ddMMMyyyy_HHmm") + ".xls" xlsx ;
        //string name = "BEReport" + ".xlsx";
        //string ext = Path.GetExtension(path);
        //string type = "";

     
        Response.AppendHeader("content-disposition", "attachment;  filename=" + finalname);
        Response.ContentType = "application/vnd.xls";

        Response.WriteFile(path);

        Response.Flush();
        Response.End();

        Response.Clear();



        if (ext != null)
        {
            switch (ext.ToLower())
            {
                case ".htm":
                case ".html":
                    type = "text/HTML";
                    break;

                case ".txt":
                    type = "text/plain";
                    break;



                case ".csv":
                case ".xls":
                case ".xlsx":
                    type = "Application/x-msexcel";
                    break;
            }
        }
        if (forceDownload)
        {
            Response.AppendHeader("content-disposition",
                "attachment; filename=" + finalname);
        }
        if (type != "")
            Response.ContentType = type;
        Response.WriteFile(path);
        Response.End();
        // loading.Visible = false;

    }

    catch (Exception ex)
    {
        if ((ex.Message + "").Contains("Thread was being aborted."))
        {
            oModule = null;
            oBook = null;
            oExcel = null;
            GC.Collect();
            
        }
        else
        {
            oModule = null;
            oBook = null;
            oExcel = null;
            GC.Collect();
            
            throw ex;
        }
    }
}
protected void Button1_Click(object sender, EventArgs e)
{
    Session["key"] = null;
    try
    {
        Lblmsg.Visible = false;
        DAL dal = new DAL();
        DataSet ds = new DataSet();
        DataTable dtGrid = new DataTable();
        DataTable dt = new DataTable();
     
        DataSet dsStatus = new DataSet();
        string query_status = "";
        query_status = "select * from Expense_category";
        dsStatus = dal.GetDataSet(query_status);
        Session.Add("dsStatus", dsStatus);
        DataSet dsGrid = BusinessLogic.GetDataSet("exec SP_FetchDataForMacro");
        dt = dsGrid.Tables[0];
       // DataTable dt1 = dsGrid.Tables[1];
        Session.Add("dtGrid", dt);

        string folderadress = @"~/ExcelOperations\ExpenseRecovery_template_Pivot.xlsx";
        folderadress = HttpContext.Current.Server.MapPath(folderadress);
        string storefolderadress = @"~/Template";
        storefolderadress = HttpContext.Current.Server.MapPath(storefolderadress);
        Microsoft.Office.Interop.Excel.Application WRExcel = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbooks WRwbs = null;
        //Microsoft.Office.Interop.Excel.Workbook WRwb = new Microsoft.Office.Interop.Excel.Workbook();
        object objOpt = System.Reflection.Missing.Value;
        Microsoft.Office.Interop.Excel.Workbook WRwb = WRExcel.Workbooks.Add(objOpt);
        Microsoft.Office.Interop.Excel._Worksheet WRws = null;
        WRExcel.Visible = false;
        WRwbs = WRExcel.Workbooks;
        WRwb = WRwbs.Open(folderadress, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt,
            objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);
        Microsoft.Office.Interop.Excel.Sheets WRss = null;
        WRss = WRwb.Sheets;

        Microsoft.Office.Interop.Excel.ApplicationClass excelapp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        excelapp.Visible = false;
        //string temp1 = Server.MapPath("./App_Data\\ExpenseRecovery_Upload.xls");
        //if (File.Exists(temp1))
        //    File.Delete(temp1);


        //Object oMissing = System.Reflection.Missing.Value;
        
        //DataTable dtStatus = new DataTable();
        //dtStatus = dsStatus.Tables[0];

        ////for status
        //var listStatus = new List<string>();
        //for (int i = 0; i < dtStatus.Rows.Count; i++)
        //{
        //    listStatus.Add(dtStatus.Rows[i][0].ToString());
        //}
        //var flatSTP = string.Join(",", listStatus.ToArray());



        //Microsoft.Office.Interop.Excel.Workbook excelBook = excelapp.Workbooks.Add(oMissing);
        Microsoft.Office.Interop.Excel.Worksheet excelSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)WRss.get_Item("ExpenseRec_data");
        //Microsoft.Office.Interop.Excel.Worksheet excelSheet2 = (Microsoft.Office.Interop.Excel.Worksheet)WRss.get_Item("Instructions");
       // excelSheet2.Delete();
        

        int ColumnIndex = 0;
        int rowIndex = 0;
        DataTable dt1 = new DataTable();
        int rows = dt.Rows.Count;
        int columns = dt.Columns.Count;
        int r = 0; int c = 0;
        object[,] DataArray = new object[rows + 1, columns + 1];
        for (c = 0; c <= columns - 1; c++)
        {
            DataArray[r, c] = dt.Columns[c].ColumnName;
            for (r = 0; r <= rows - 1; r++)
            {
                DataArray[r, c] = dt.Rows[r][c];
            } //end row loop
        } //end column loop
        Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)excelSheet1.Cells[2, 1];
        Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)excelSheet1.Cells[1 + dt.Rows.Count, dt.Columns.Count];
        Microsoft.Office.Interop.Excel.Range range = excelSheet1.get_Range(c1, c2);
        //Fill Array in Excel
        range.Value2 = DataArray;


   
        excelSheet1.get_Range("F2", "F" + dt.Rows.Count + 1).NumberFormat = "MM/DD/YYYY";
        excelSheet1.get_Range("V2", "V" + dt.Rows.Count + 1).NumberFormat = "MM/DD/YYYY";
        excelSheet1.get_Range("AE2", "AE" + dt.Rows.Count + 1).NumberFormat = "MM/DD/YYYY";
        //excelSheet1.get_Range("AD2", "AD" + dt.Rows.Count + 1).NumberFormat = "MM/DD/YYYY";

     
        string filename = "Expense Recovery Pivot_" + DateTime.Now.ToString("ddMMMyyyy_HHmm") + "IST.xlsx";
        String excelFile1 = "~\\ExcelOperations\\" + filename;
        String destPath = Server.MapPath(excelFile1);
        Session.Add("destPath", destPath);
        RefreshPivots(WRss);
        WRwb.SaveCopyAs(destPath);
      

        Session["key"] = filename;
        iframe.Attributes.Add("src", "DownloadFile.aspx");
        WRwb.SaveCopyAs(destPath);
        WRwb.Close(false);
        WRExcel.Quit();

        //oExcel = null;
        //oModule = null;
        //oBook = null;
        //WRss = null;
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRExcel);
        // System.Runtime.InteropServices.Marshal.FinalReleaseComObject(oModule);
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRwb);
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRss);
        GC.Collect();
        // DownloadFileProject(filename);




       
    }catch(Exception ex)
    {
    }
}
public int getGVRowIndex(Control ctl)
{
    GridViewRow GVRow = (GridViewRow)ctl.NamingContainer;
    return GVRow.RowIndex;
}

protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
{
    Lblmsg.Visible = false;
    int j = EditRowIndex;
    ((DropDownList)gvExpenseRec.Rows[j].FindControl("ddlSubCategory")).Visible = true;
    ((Label)gvExpenseRec.Rows[j].FindControl("lblSubCategory")).Visible = false;
     DropDownList ddlCategory = ((DropDownList)gvExpenseRec.Rows[j].FindControl("ddlCategory"));
     DropDownList ddlSubCategory = ((DropDownList)gvExpenseRec.Rows[j].FindControl("ddlSubCategory"));
        string strcategory = ddlCategory.SelectedValue.ToString();

        SqlCommand cmd = new SqlCommand();

        string cmdExpRec = "select distinct Subcategory from expense_Category where Category=@strcategory";
        cmd.CommandText = cmdExpRec;
        cmd.Parameters.AddWithValue("@strcategory", strcategory);

        DataSet dsSubCategory = dal.FetchDataSet(cmd);
        ddlSubCategory.Items.Clear();
        for (int count = 0; count < dsSubCategory.Tables[0].Rows.Count; count++)
        {
            ddlSubCategory.Items.Add(dsSubCategory.Tables[0].Rows[count][0].ToString());
        
    }
        ddlSubCategory.Items.Insert(0, "--Select--");
        int i = j;
        if ((((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).SelectedValue == "Expense Billed"))
        {
            ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = false;
            ((RegularExpressionValidator)gvExpenseRec.Rows[i].FindControl("RegularExpressionValidator1")).Enabled = true;
            ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator1")).Enabled = true;
          
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = true;
            //((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
            //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = false;

            ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
            ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;

            ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = false;
            ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).Visible = true;
            ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnDate")).Enabled = false;
            ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnInitatedDate")).Enabled = true;
        }
        else if (((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).SelectedValue == "Reversal Pending with Finance")
        {
            ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = false;
            ((RegularExpressionValidator)gvExpenseRec.Rows[i].FindControl("RegularExpressionValidator1")).Enabled = true;
            ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator1")).Enabled = true;
          
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;

            //((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
            //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = true;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = false;
            ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
            ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;

            ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = true;
            ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).Visible = false;
            ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnDate")).Enabled = false;
            ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnInitatedDate")).Enabled = true;
        }
        else if (((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).SelectedValue == "Expense To Be Billed")
        {

            //((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = true;
            
            ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = false;
            ((RegularExpressionValidator)gvExpenseRec.Rows[i].FindControl("RegularExpressionValidator1")).Enabled = false;
         
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = false;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
            //((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
            //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = false;

            //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtComments")).Visible = true;
            ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = true;
            ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).Visible = false;
            ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
            ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;
            ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnDate")).Enabled = true;
            ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnInitatedDate")).Enabled = false;
        }
        else if (((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).SelectedValue == "Reversal Pending with PM/DM")
        {

            //((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = true;
            
            ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = false;
            ((RegularExpressionValidator)gvExpenseRec.Rows[i].FindControl("RegularExpressionValidator1")).Enabled = false;
        
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = false;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
            ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
            //((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
            //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
            ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = true;

            //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtComments")).Visible = true;
            ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = true;
            ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).Visible = false;
            ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
            ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;
            ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnDate")).Enabled = true;
            ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnInitatedDate")).Enabled = false;
        }


         //((Label)gvExpenseRec.Rows[i].FindControl("lblUpdatefromAccountteam")).Visible = false;
         ((Label)gvExpenseRec.Rows[i].FindControl("lblInvoiceNo")).Visible = false;
         //((Label)gvExpenseRec.Rows[i].FindControl("lblConfirmationNo")).Visible = false;
         ((Label)gvExpenseRec.Rows[i].FindControl("lblServCentraleReqNo")).Visible = false;
         ((Label)gvExpenseRec.Rows[i].FindControl("lblRemarks")).Visible = false;
     
         ((Label)gvExpenseRec.Rows[i].FindControl("lblCategory")).Visible = false;
         ((Label)gvExpenseRec.Rows[i].FindControl("lblSubCategory")).Visible = false;
         //((Label)gvExpenseRec.Rows[i].FindControl("lblComments")).Visible = false;
         //((TextBox)gvExpenseRec.Rows[i].FindControl("txtCal")).Text = null;
         Lblmsg.Visible = false;
         //lblStatus.Visible = false;
         Lblmsg.Visible = false;
      
       

}
protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
{
    int i = EditRowIndex;
    if ((((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).SelectedValue == "Expense Billed") )
    {
        ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = false;
        ((RegularExpressionValidator)gvExpenseRec.Rows[i].FindControl("RegularExpressionValidator1")).Enabled = true;
        ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator1")).Enabled = true;
       
        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;
        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = true;
        //((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = false;

        ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
        ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;

        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = false;
        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).Visible = true;
        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnDate")).Enabled = false;
        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnInitatedDate")).Enabled = true;
    }
      else if (((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).SelectedValue == "Reversal Pending with Finance")
    {
          ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = false;
        ((RegularExpressionValidator)gvExpenseRec.Rows[i].FindControl("RegularExpressionValidator1")).Enabled = true;
        ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator1")).Enabled = true;

        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;
        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = false;
        //((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = true;

        ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
        ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;
     
        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = false;
        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).Visible = true;
        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnDate")).Enabled = false;
        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnInitatedDate")).Enabled = true;
    }
    else if (((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).SelectedValue == "Expense To Be Billed") 
    {

        //((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = true;
        
        ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = false;
        ((RegularExpressionValidator)gvExpenseRec.Rows[i].FindControl("RegularExpressionValidator1")).Enabled = false;

        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;
        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = false;
        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
        //((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = false;

        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtComments")).Visible = true;
        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = true;
        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).Visible = false;
        ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
        ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;
        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnDate")).Enabled = true;
        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnInitatedDate")).Enabled = false;
    }
    else if (((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).SelectedValue == "Reversal Pending with PM/DM")
    {

        //((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = true;
        
        ((RequiredFieldValidator)gvExpenseRec.Rows[i].FindControl("RequiredFieldValidator4")).Enabled = false;
        ((RegularExpressionValidator)gvExpenseRec.Rows[i].FindControl("RegularExpressionValidator1")).Enabled = false;

        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtRemarks")).Visible = true;
        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtInvoiceNo")).Visible = false;
        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlCategory")).Visible = true;
        ((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlSubCategory")).Visible = true;
        //((DropDownList)gvExpenseRec.Rows[i].FindControl("ddlUpdatefromAccountteam")).Visible = true;
        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtConfirmationNo")).Visible = false;
        ((TextBox)gvExpenseRec.Rows[i].FindControl("TxtServCentraleReqNo")).Visible = true;

        //((TextBox)gvExpenseRec.Rows[i].FindControl("TxtComments")).Visible = true;
        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalExpenseClosureDate")).Visible = true;
        ((Calendar)gvExpenseRec.Rows[i].FindControl("CalInitatedDate")).Visible = false;
        ((Label)gvExpenseRec.Rows[i].FindControl("lblExpenseClosureDate")).Visible = true;
        ((Label)gvExpenseRec.Rows[i].FindControl("lblInitatedDate")).Visible = true;
        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnDate")).Enabled = true;
        ((ImageButton)gvExpenseRec.Rows[i].FindControl("btnInitatedDate")).Enabled = false;
    }

    
    //((Label)gvExpenseRec.Rows[i].FindControl("lblUpdatefromAccountteam")).Visible = false;
    ((Label)gvExpenseRec.Rows[i].FindControl("lblInvoiceNo")).Visible = false;
    //((Label)gvExpenseRec.Rows[i].FindControl("lblConfirmationNo")).Visible = false;
    ((Label)gvExpenseRec.Rows[i].FindControl("lblServCentraleReqNo")).Visible = false;
    ((Label)gvExpenseRec.Rows[i].FindControl("lblRemarks")).Visible = false;

    ((Label)gvExpenseRec.Rows[i].FindControl("lblCategory")).Visible = false;
    ((Label)gvExpenseRec.Rows[i].FindControl("lblSubCategory")).Visible = false;
    //((Label)gvExpenseRec.Rows[i].FindControl("lblComments")).Visible = false;
    //((TextBox)gvExpenseRec.Rows[i].FindControl("txtCal")).Text = null;
    Lblmsg.Visible = false;
    //lblStatus.Visible = false;
    Lblmsg.Visible = false;
}
protected void CalExpenseClosureDate_SelectionChanged(object sender, EventArgs e)
{

    int setfocusrecord = 0;
    int RowStart = EditRowIndex;
    setfocusrecord = RowStart;
    gvExpenseRec.Rows[setfocusrecord].Focus();
    ((Label)gvExpenseRec.Rows[RowStart].FindControl("lblExpenseClosureDate")).Visible = false;
    Calendar caltent = ((Calendar)gvExpenseRec.Rows[RowStart].FindControl("CalExpenseClosureDate"));
    Label lblExpenseClosureDate = (Label)gvExpenseRec.Rows[RowStart].FindControl("lblExpenseClosureDate");
    //caltent.Visible = true;
    //TextBox txtcal = ((TextBox)gvExpenseRec.Rows[RowStart].FindControl("txtCal"));
    string strtxtcal = lblExpenseClosureDate.Text;
    lblExpenseClosureDate.Text = caltent.SelectedDate.ToShortDateString();
    lblExpenseClosureDate.Visible = true;
    caltent.Visible = false;
 }
protected void CalInitatedDate_SelectionChanged(object sender, EventArgs e)
{

    int setfocusrecord = 0;
    int RowStart = EditRowIndex;
    setfocusrecord = RowStart;
    gvExpenseRec.Rows[setfocusrecord].Focus();
    ((Label)gvExpenseRec.Rows[RowStart].FindControl("lblInitatedDate")).Visible = false;
    Calendar caltent = ((Calendar)gvExpenseRec.Rows[RowStart].FindControl("CalInitatedDate"));
    Label lblInitatedDate = (Label)gvExpenseRec.Rows[RowStart].FindControl("lblInitatedDate");
    //caltent.Visible = true;
    //TextBox txtCalInitated = ((TextBox)gvExpenseRec.Rows[RowStart].FindControl("txtCalInitated"));
    string strtxtcal = lblInitatedDate.Text;
    lblInitatedDate.Text = caltent.SelectedDate.ToShortDateString();
    lblInitatedDate.Visible = true;
    caltent.Visible = false;
}
protected void lnkDelegateAccess_Click(object sender, EventArgs e)
{
    Response.Redirect("Delegate Access.aspx");
}


//protected void DrugGridView_RowDataBound(object sender, GridViewRowEventArgs e)
//{
//    // To check condition on integer value
//    if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Dosage")) == 50)
//    {
//        e.Row.BackColor = System.Drawing.Color.Cyan;
//    }
//}

//public int getGVRowIndex(Control ctl)
//{
//    GridViewRow GVRow = (GridViewRow)ctl.NamingContainer;
//    return GVRow.RowIndex;
//}
protected void btnDate_Click(object sender, EventArgs e)
{
    int setfocusrecord = 0;
    int RowNo = getGVRowIndex((Control)sender);
    setfocusrecord = RowNo;
    gvExpenseRec.Rows[setfocusrecord].Focus();
    Calendar Calendar1 = ((Calendar)gvExpenseRec.Rows[RowNo].FindControl("CalExpenseClosureDate"));
    if (Calendar1.Visible == false)
    {
        Calendar1.Visible = true;
        Calendar1.SelectedDate = DateTime.MinValue;


    }
    else
    {
        Calendar1.Visible = false;

    }

}
protected void btnInitatedDate_Click(object sender, EventArgs e)
{
    Session["key"] = null;
    int setfocusrecord = 0;
    int RowNo = getGVRowIndex((Control)sender);
    setfocusrecord = RowNo;
    gvExpenseRec.Rows[setfocusrecord].Focus();
    Calendar Calendar1 = ((Calendar)gvExpenseRec.Rows[RowNo].FindControl("CalInitatedDate"));
    if (Calendar1.Visible == false)
    {
        Calendar1.Visible = true;
        Calendar1.SelectedDate = DateTime.MinValue;


    }
    else
    {
        Calendar1.Visible = false;

    }

}


protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
{
    Response.Redirect("FAQ.aspx");

}
  
}


